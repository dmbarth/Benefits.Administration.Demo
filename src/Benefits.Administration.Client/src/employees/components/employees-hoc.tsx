import qs from 'qs';
import React from 'react';
import { RouteComponentProps } from 'react-router-dom';

type Props = RouteComponentProps;

export type EmployeeComponentProps = {
    employeeId: number;
    tab: string;
    costPeriods: number;
    isAddingEmployee: boolean;
    employeeAction: 'add' | 'edit';
}

const withEmployees = (
    WrappedComponent
) => {
    return class extends React.Component<Props> {
        render() {
            return <WrappedComponent
                {...this.props}
                employeeId={this.employeeId}
                tab={this.tab}
                costPeriods={this.costPeriods}
                isAddingEmployee={this.isAddingEmployee}
                employeeAction={this.employeeAction} />
        }
        
        get employeeId(): number {
            const { location } = this.props;
            const { employeeId } = qs.parse(location.search, { ignoreQueryPrefix: true });

            if (!employeeId)
                return undefined;

            return parseInt(employeeId as string);
        }

        get tab(): string {
            const { location } = this.props;
            const { tab } = qs.parse(location.search, { ignoreQueryPrefix: true });

            if (!tab)
                return undefined;

            return tab as string;
        }

        get costPeriods(): number {
            const { location } = this.props;
            const { costPeriods } = qs.parse(location.search, { ignoreQueryPrefix: true });

            if (!costPeriods)
                return undefined;

            return parseInt(costPeriods as string);
        }

        get isAddingEmployee(): boolean {
            const { location } = this.props;
            const { add } = qs.parse(location.search, { ignoreQueryPrefix: true });

            if (!add)
                return undefined;

            return !!add;
        }
        
        get employeeAction(): string {
            const { location } = this.props;
            const { action } = qs.parse(location.search, { ignoreQueryPrefix: true });

            if (!action)
                return undefined;

            return action as string;
        }
    }
}

export default withEmployees;