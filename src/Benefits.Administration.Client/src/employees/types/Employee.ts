import Dependent from "./Dependent";
import Person from "./Person";

type Employee = Person & {
    id: number;
    income: number;
    dependents: Dependent[];
}

export const fullName = (entity: Employee | Dependent): string => {
    return entity ? [entity.firstName, entity.middleName, entity.lastName]
        .filter(val => val != null)
        .join(' ') : null;
}

export default Employee;