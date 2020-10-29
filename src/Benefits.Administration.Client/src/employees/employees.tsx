import React from "react";
import { Button, Card, Col, ListGroup, Row } from "react-bootstrap";
import { connect, DispatchProp } from "react-redux";
import { EmployeesState, fetchEmployees } from "./store/employees-store";
import { RootState } from "/app/store";
import LoadingSpinner from "/app/components/loading-spinner";
import { Link, Route, RouteComponentProps, Switch, withRouter } from "react-router-dom";
import EmployeeTabs from './components/employees-tabs';
import { fullName } from "./types/Employee";

type Props = DispatchProp & RouteComponentProps & EmployeesState;

class Employees extends React.Component<Props>
{
    componentDidMount() {
        const { dispatch } = this.props;

        dispatch(fetchEmployees());
    }
    
    render() {
        const { entities, loading, match } = this.props;

        const tabs = match.params && match.params["id"] ? <EmployeeTabs /> : null;
        
        return (
            <div className="mt-5">
                <LoadingSpinner loading={loading} error={this.errorMessage}>
                    <Row>
                        <Col sm={4}>
                            <Card className='mb-3'>
                                <Card.Header as="h5" className='d-flex flex-row justify-content-between align-items-center'>
                                    Employees
                                    <Button as={Link} size='sm' to={`/employees/0/details`} variant='dark' >Add</Button>
                                </Card.Header>
                                <Card.Body>
                                    <ListGroup>
                                        {entities.map((entity, idx) => (
                                            <ListGroup.Item key={idx}
                                                as={Link} action variant="dark"
                                                to={`/employees/${entity.id}/details`}
                                                active={match.params["id"] == entity.id}>
                                                {fullName(entity)}
                                            </ListGroup.Item>
                                        ))}
                                    </ListGroup>
                                </Card.Body>
                            </Card>
                        </Col>
                        <Col sm={8}>{tabs}</Col>
                    </Row>
                </LoadingSpinner>
            </div>
        )
    }
    
    get errorMessage(): React.ReactNode {
        const { errorMessage } = this.props;

        return (
            <div>{errorMessage}</div>
        )
    }
}

export default connect((rootState: RootState) => {
    return { ...rootState.employees }
})(withRouter(Employees));