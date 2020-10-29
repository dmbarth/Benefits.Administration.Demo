import React from 'react';
import { Card, Nav } from 'react-bootstrap';
import { connect, DispatchProp } from 'react-redux';
import { Link, Route, RouteComponentProps, Switch, withRouter } from 'react-router-dom';
import { EmployeesState } from '../store/employees-store';
import { RootState } from '/app/store';
import Deductions from './deductions';
import Dependents from './dependents';
import Employee from './employee';
import { fetchEmployee } from '../store/employee-store';

type Props = DispatchProp & RouteComponentProps & EmployeesState;

class EmployeesTabs extends React.Component<Props>
{
    componentDidMount(){
        const { dispatch, match } = this.props;

        if (match.params["id"] == 0)
            return;

        dispatch(fetchEmployee(match.params["id"]));
    }

    componentDidUpdate(prevProps: Props){
        const { dispatch, match } = this.props;
        
        let prevId = prevProps.match.params["id"];
        let id = match.params["id"];

        if (id == prevId || id == 0)
            return;

        dispatch(fetchEmployee(id));
    }

    render(){
        const { match, location } = this.props;

        let id = match.params["id"];

        if (!id)
            return null;

        return (
            <Card>
                <Card.Header>
                    <Nav variant="tabs">
                        <Nav.Item>
                            <Nav.Link as={Link} 
                                active={`${match.url}/details` == location.pathname}
                                to={`${match.url}/details`}>
                                Employee
                            </Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link as={Link} disabled={id == 0}
                                active={`${match.url}/dependents` == location.pathname}
                                to={`${match.url}/dependents`}>
                                Dependents
                            </Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link as={Link} disabled={id == 0}
                                active={`${match.url}/deductions` == location.pathname}
                                to={`${match.url}/deductions`}>
                                Deductions
                            </Nav.Link>
                        </Nav.Item>
                    </Nav>
                </Card.Header>
                <Card.Body>
                    <Switch>
                        <Route path={`${match.path}/:tab`}>
                            <Employee />
                            <Dependents />
                            <Deductions />
                        </Route>
                    </Switch>
                </Card.Body>
            </Card>
        )
    }
}

export default connect((rootState: RootState) => {
    return { ...rootState.employees }
})(withRouter(EmployeesTabs));