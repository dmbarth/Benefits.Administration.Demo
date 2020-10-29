type Person = {
    firstName: string;
    middleName: string;
    lastName: string;
}

export const fullName = (person: Person): string => {
    return person ? [person.firstName, person.middleName, person.lastName]
        .filter(val => val != null)
        .join(' ') : null;
}

export default Person;