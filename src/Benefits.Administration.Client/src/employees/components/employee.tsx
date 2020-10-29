import React from "react";
import { Button, Card, Col, Dropdown, Form, Row, Spinner, SplitButton } from "react-bootstrap";
import { connect, DispatchProp } from "react-redux";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { addNewEmployee, createNew, deleteEmployee, editEmployee, EmployeeState } from "../store/employee-store";
import { fullName } from "../types/Employee";
import { RootState } from "/app/store";
import * as yup from 'yup';
import EmployeeEntity from '../types/Employee';
import Dependent from "../types/Dependent";
import { Formik, FormikHelpers } from "formik";

type Props = DispatchProp & RouteComponentProps & EmployeeState;
type State = { isEditing: boolean }

class Employee extends React.Component<Props, State>
{
    schema: yup.ObjectSchema<EmployeeEntity>;

    constructor(props: Props) {
        super(props);

        this.state = { isEditing: false };

        this.schema = yup.object({
            id: yup.number().notRequired(),
            firstName: yup.string().required(),
            middleName: yup.string().notRequired(),
            lastName: yup.string().required(),
            income: yup.number().typeError().positive().required(),
            dependents: yup.array<Dependent>().notRequired()
        });
    }

    componentDidUpdate(prevProps: Props, prevSate: State): void {
        const { dispatch, deleting, history, match } = this.props;

        if (this.state.isEditing && prevSate.isEditing)
            this.setState({ isEditing: false });

        let prevId = parseInt(prevProps.match.params["id"]);
        let id = parseInt(match.params["id"]);
        
        if (id == 0 && prevId != 0)
            dispatch(createNew());

        if (deleting == 'succeeded' && prevProps.deleting == 'pending')
            history.push('/employees');
    }

    onForm_Submit(values: EmployeeEntity, actions: FormikHelpers<EmployeeEntity>): void {
        const { dispatch, match } = this.props;

        const employee = this.schema.cast(values);

        if (match.params["id"] == 0) {
            dispatch(addNewEmployee(employee));
            return;
        }

        dispatch(editEmployee(employee));
    }

    onDelete_Click(id: number){
        const { dispatch } = this.props;

        dispatch(deleteEmployee(id));
    }

    onEdit_Click(): void {
        this.setState({ isEditing: true });
    }

    render(){
        const { employee, adding, match } = this.props;
        const { isEditing } = this.state;

        if (match.params["tab"] != 'details')
            return null;

        let actionButton;

        if (this.isAddingEmployee && adding == 'pending')
        {
            actionButton = <Button variant='dark' className='float-right'>
                <Spinner as='span' size='sm' animation='border' className='mr-3' />Saving</Button>
        }
        else if (this.isAddingEmployee || isEditing)
            actionButton = <Button type='submit' variant='dark' className='float-right'>Save</Button>
        else
            actionButton = <SplitButton id={`action-${employee.id}`} title="Edit" variant='dark' className='float-right' onClick={() => this.onEdit_Click()}>
                <Dropdown.Item onClick={() => this.onDelete_Click(employee.id)}>Delete</Dropdown.Item>
            </SplitButton>

        return (
            <Card className='mt-3'>
                <Card.Body>
                    <Card.Title>{this.isAddingEmployee ? "New Employee" : fullName(employee)}</Card.Title>
                    <Formik
                        enableReinitialize
                        validationSchema={this.schema}
                        onSubmit={(values, actions) => this.onForm_Submit(values, actions)}
                        initialValues={this.initialValues}>
                        {({
                            handleSubmit,
                            handleChange,
                            values,
                            touched,
                            errors
                        }) => (
                        <Form noValidate onSubmit={handleSubmit}>
                            <Form.Group as={Row} controlId='firstName'>
                                <Form.Label column sm="3">First Name</Form.Label>
                                <Col sm="9">
                                    <Form.Control 
                                        disabled={!this.isAddingEmployee && !isEditing} 
                                        value={values.firstName} 
                                        onChange={handleChange}
                                        isValid={!errors.firstName && touched.firstName}
                                        isInvalid={errors.firstName && touched.firstName} />
                                </Col>
                            </Form.Group>                            
                            <Form.Group as={Row} controlId='middleName'>
                                <Form.Label column sm="3">Middle Name</Form.Label>
                                <Col sm="9">
                                    <Form.Control 
                                        disabled={!this.isAddingEmployee && !isEditing} 
                                        value={values.middleName} 
                                        onChange={handleChange} />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row} controlId='lastName'>
                                <Form.Label column sm="3">Last Name</Form.Label>
                                <Col sm="9">
                                    <Form.Control 
                                        disabled={!this.isAddingEmployee && !isEditing} 
                                        value={values.lastName} 
                                        onChange={handleChange}
                                        isValid={!errors.lastName && touched.lastName}
                                        isInvalid={errors.lastName && touched.lastName} />
                                </Col>
                            </Form.Group>                            
                            <Form.Group as={Row} controlId='income'>
                                <Form.Label column sm="3">Income</Form.Label>
                                <Col sm="3">
                                    <Form.Control
                                        disabled={!this.isAddingEmployee && !isEditing} 
                                        value={values.income} 
                                        onChange={handleChange}
                                        isValid={!errors.income && touched.income}
                                        isInvalid={errors.income && touched.income} />
                                </Col>
                            </Form.Group>
                            {actionButton}
                        </Form>
                        )}
                    </Formik>                    
                </Card.Body>
            </Card>
        )
    }

    get isAddingEmployee(): boolean {
        const { match } = this.props;

        return parseInt(match.params["id"]) == 0;
    }

    get initialValues(): EmployeeEntity {
        const { employee } = this.props;

        if (!employee)
            return null;

        return { ...employee, middleName: employee.middleName || '' };
    }
}

export default connect((rootState: RootState) => {
    return { ...rootState.employee }
})(withRouter(Employee));