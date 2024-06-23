import React from 'react';
import { Button, Card, Dropdown, Spinner, SplitButton } from 'react-bootstrap';
import { connect, DispatchProp } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router-dom';
import { updateEmployee, EmployeeState, deleteDependent, fetchEmployee } from '../store/employee-store';
import { RootState } from '/app/store';
import DependentCard from './dependent-card';

type Props = DispatchProp & RouteComponentProps & EmployeeState;
type State = {
  isAdding: boolean;
  isEditingId: number;
}

class Dependents extends React.Component<Props, State> {
  constructor(props: Props) {
    super(props);

    this.state = {
      isAdding: false,
      isEditingId: -1
    };
  }

  componentDidUpdate(prevProps: Props, prevState: State) {
    const { dispatch, employee } = this.props;
    const { isAdding } = this.state;

    if (prevState.isAdding && isAdding) {
      this.setState({ isAdding: false });

      let emp = {
        id: employee.id,
        firstName: employee.firstName,
        middleName: employee.middleName,
        lastName: employee.lastName,
        income: employee.income,
        dependents: [...employee.dependents]
      };

      emp.dependents.pop();

      dispatch(updateEmployee(emp));
    }
  }

  onAdd_Click() {
    const { dispatch, employee } = this.props;

    let emp = {
      id: employee.id,
      firstName: employee.firstName,
      middleName: employee.middleName,
      lastName: employee.lastName,
      income: employee.income,
      dependents: [...employee.dependents]
    };

    emp.dependents.push({
      id: 0,
      firstName: '',
      middleName: '',
      lastName: '',
      type: 0
    });

    this.setState({ isAdding: true });

    dispatch(updateEmployee(emp));
  }

  onEdit_Click(id: number) {
    this.setState({ isEditingId: id });
  }

  onDelete_Click(id: number, dependentId: number) {
    const { dispatch } = this.props;

    dispatch(deleteDependent(id, dependentId));
  }

  onCancel_Click() {
    this.setState({ isAdding: false, isEditingId: -1 });
  }

  render() {
    const { match, employee } = this.props;
    const { isAdding, isEditingId } = this.state;

    if (match.params["tab"] != "dependents" || !employee)
      return null;

    if (employee.dependents.length == 0)
      return (
        <Card>
          <Card.Body className='d-flex flex-row justify-content-between align-items-center'>
            <div>The Employee has no dependents.</div>
            <Button variant='dark' onClick={() => this.onAdd_Click()}>Add</Button>
          </Card.Body>
        </Card>
      )

    const lastIndex = employee.dependents.length - 1;

    return <>
      {employee.dependents.map((dependent, idx) => (
        <DependentCard key={dependent.id}
          title={isAdding && idx == lastIndex ? "New Dependent" : null}
          dependent={dependent}
          onEdit={this.onEdit_Click.bind(this)}
          onDelete={this.onDelete_Click.bind(this)}
          onCancel={this.onCancel_Click.bind(this)}
          isAdding={isAdding && idx == lastIndex}
          isEditing={isEditingId == dependent.id}
          isDisabled={isAdding && idx != lastIndex}/>
      ))}
      {isAdding || isEditingId !== -1 ? null : <Button variant='dark' className='mt-3 float-right' onClick={() => this.onAdd_Click()}>Add</Button>}
    </>
  }
}

export default connect((rootState: RootState) => {
  return { ...rootState.employee }
})(withRouter(Dependents));