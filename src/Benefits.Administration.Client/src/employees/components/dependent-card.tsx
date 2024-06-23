import { Formik, FormikHelpers } from 'formik';
import React from 'react';
import * as yup from 'yup';
import { Button, Card, Col, Dropdown, Form, Row, Spinner, SplitButton } from 'react-bootstrap';
import Dependent from '../types/Dependent';
import { fullName } from '../types/Person';
import { RouteComponentProps, withRouter } from 'react-router-dom';
import { connect, DispatchProp } from 'react-redux';
import { addNewDependent, editDependent, EmployeeState } from '../store/employee-store';
import { RootState } from '/app/store';

type Props = DispatchProp & RouteComponentProps & EmployeeState & {
  title?: string;
  dependent: Dependent;
  isAdding: boolean;
  isEditing: boolean;
  isDisabled: boolean;
  onEdit: Function;
  onDelete: Function;
  onCancel: Function;
}

class DependentCard extends React.Component<Props> {

  schema: yup.ObjectSchema<Dependent>;

  constructor(props: Props) {
    super(props);

    this.schema = yup.object({
      id: yup.number().notRequired(),
      firstName: yup.string().required(),
      middleName: yup.string().notRequired(),
      lastName: yup.string().required(),
      type: yup.number().typeError().required()
    });
  }

  onForm_Submit(values: Dependent, actions: FormikHelpers<Dependent>): void {
    const { dispatch, employee, isAdding } = this.props;

    const dependent = this.schema.cast(values);

    if (isAdding) {
      dispatch(addNewDependent(employee.id, dependent));
      return;
    }

    dispatch(editDependent(employee.id, dependent));
  }

  createActionButton(dependent: Dependent, reset: Function): React.ReactNode {
    const { employee, isAdding, adding, isEditing, onCancel, onDelete, onEdit } = this.props;

    if (isAdding && adding == 'pending')
      return (
        <Button variant='dark' className='float-right'>
          <Spinner as='span' size='sm' animation='border' className='mr-3' />Saving</Button>)

    if (isAdding || isEditing)
      return (
        <div>
          <Button type='submit' variant='dark' className='float-right'>Save</Button>
          <Button type='reset' variant='outline-secondary' className='float-right mr-2' onClick={() => {
            reset();
            onCancel();
          }}>Cancel</Button>
        </div>
      )

    return (
      <SplitButton id='edit-split' title="Edit" variant='dark' className='float-right' onClick={() => onEdit(dependent.id)}>
        <Dropdown.Item onClick={() => onDelete(employee.id, dependent.id)}>Delete</Dropdown.Item>
      </SplitButton>
    )
  }

  render() {
    const { dependent, title, isAdding, isEditing } = this.props;

    if (!dependent)
      return null;

    return (
      <Card className='mt-3'>
        <Card.Body>
          <Card.Title>{title ?? fullName(dependent)}</Card.Title>
          <Formik
            enableReinitialize
            validationSchema={this.schema}
            onSubmit={(values, actions) => this.onForm_Submit(values, actions)}
            initialValues={this.initialValues}>
            {({
              handleSubmit,
              handleChange,
              handleReset,
              values,
              touched,
              errors
            }) => (
              <Form noValidate onSubmit={handleSubmit}>
                <Form.Group as={Row} controlId='firstName'>
                  <Form.Label column sm="3">First Name</Form.Label>
                  <Col sm="9">
                    <Form.Control
                      disabled={!isAdding && !isEditing}
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
                      disabled={!isAdding && !isEditing}
                      value={values.middleName}
                      onChange={handleChange} />
                  </Col>
                </Form.Group>
                <Form.Group as={Row} controlId='lastName'>
                  <Form.Label column sm="3">Last Name</Form.Label>
                  <Col sm="9">
                    <Form.Control
                      disabled={!isAdding && !isEditing}
                      value={values.lastName}
                      onChange={handleChange}
                      isValid={!errors.lastName && touched.lastName}
                      isInvalid={errors.lastName && touched.lastName} />
                  </Col>
                </Form.Group>
                <Form.Group as={Row} controlId='type'>
                  <Form.Label column sm="3">Type</Form.Label>
                  <Col sm="3">
                    <Form.Control as="select"
                      disabled={!isAdding && !isEditing}
                      value={values.type}
                      onChange={handleChange}
                      isValid={!errors.type && touched.type}
                      isInvalid={errors.type && touched.type}>
                      <option value="0">Other</option>
                      <option value="1">Spouse</option>
                      <option value="2">Child</option>
                    </Form.Control>
                  </Col>
                </Form.Group>
                {this.createActionButton(dependent, handleReset)}
              </Form>
            )}
          </Formik>
        </Card.Body>
      </Card>
    )
  }

  get initialValues(): Dependent {
    const { dependent } = this.props;

    if (!dependent)
      return null;

    return { ...dependent, middleName: dependent.middleName || '' };
  }
}

export default connect((rootState: RootState) => {
  return { ...rootState.employee }
})(withRouter(DependentCard));