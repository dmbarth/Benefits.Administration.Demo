import Person from "./Person";

type Dependent = Person & {
    id: number;
    type: number;
}

export default Dependent;