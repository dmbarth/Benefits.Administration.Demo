import React, { ChangeEvent } from "react";
import { Card, Col, Form, Row } from "react-bootstrap";
import { connect, DispatchProp } from "react-redux";
import { RootState } from "/app/store";
import _ from 'lodash';
import DeductionCard from "./deduction-card";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { DeductionsState, fetchEmployeeDeductions } from "../store/deductions-store";
import { EmployeeState } from "../store/employee-store";

type Props = DispatchProp & RouteComponentProps & EmployeeState & DeductionsState;

export type DeductionsPreview = { preview: 'annually' | 'monthly' | 'period' }

class Deductions extends React.Component<Props>
{
    componentDidUpdate(prevProps: Props) {
        const { dispatch, employee, match, costPeriods } = this.props;

        const prevTab = match.params["tab"];
        const tab = match.params["tab"]

        if (tab == "deductions" && prevTab != "deductions")
            dispatch(fetchEmployeeDeductions(employee.id, costPeriods));
    }

    previewBy_Changed(value: string): void {
        const { dispatch, employee } = this.props;

        let costPeriods = value == "0" ? null : parseInt(value);

        dispatch(fetchEmployeeDeductions(employee.id, costPeriods));
    }

    render(){
        const { match, employee, deductions } = this.props;

        if (match.params["tab"] != "deductions")
            return null;
        // if (isAddingEmployee)
        //     return null;

        if (!deductions)
            return null;

        const { 
            employeeCost, employeeDiscounts, employeeTotal,
            dependentsCost, dependentsDiscounts, dependentsTotal,
            totalCost, costPeriods
        } = deductions;

        return (
            <>
                <Card className='mt-3'>
                    <Card.Body>
                        <Card.Title>Summary</Card.Title>
                        <Row>
                            <Col lg={{ order: 2, span: 4, offset: 2}} className='text-right'>     
                                <Form.Group controlId={`preview-by-${employee.id}`} >
                                    <Form.Label>Preview By:</Form.Label>
                                        <Form.Control as="select"
                                            value={costPeriods}
                                            onChange={e => this.previewBy_Changed(e.target.value)}>
                                            <option value="0">Pay Period</option>
                                            <option value="1">Annually</option>
                                            <option value="12">Monthly</option>
                                        </Form.Control>
                                </Form.Group>
                            </Col>
                            <Col lg="6">
                                <Row>                           
                                    <Col sm="6">Employee</Col>
                                    <Col className='text-right'>{employeeTotal.format()}</Col>
                                </Row>
                                <Row>                                
                                    <Col sm="6">Dependent(s)</Col>
                                    <Col className='text-right'>{dependentsTotal.format()}</Col>
                                </Row>
                                <Row>
                                    <Col sm="6">Total</Col>
                                    <Col className='text-right'>{totalCost.toCurrency()}</Col>
                                </Row>
                            </Col>
                        </Row>
                    </Card.Body>
                </Card>
                <Row>
                    <Col lg="6">
                        <DeductionCard 
                            title="Employee"
                            cost={employeeCost} 
                            discounts={employeeDiscounts} 
                            total={employeeTotal} />
                    </Col>
                    <Col lg="6">
                        <DeductionCard 
                            title="Dependent(s)"
                            cost={dependentsCost} 
                            discounts={dependentsDiscounts} 
                            total={dependentsTotal} 
                            costMultiplier={employee.dependents.length} />
                    </Col>
                </Row>
            </>
        )
    }
}

export default connect((rootState: RootState) => {
    return { 
        ...rootState.employee,
        ...rootState.deductions
    }
})(withRouter(Deductions));