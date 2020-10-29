import { createSlice, Dispatch, PayloadAction } from "@reduxjs/toolkit"
import Axios, { AxiosError } from "axios"
import Dependent from "../types/Dependent"
import Employee from "../types/Employee"
import { fetchEmployeeDeductions } from "./deductions-store"
import { fetchEmployees } from "./employees-store"
import { LoadingProps } from "/app/components/loading-spinner"

export type EmployeeState = {
    loading: LoadingProps;
    adding: LoadingProps;
    deleting: LoadingProps;
    editing: LoadingProps;
    receivedAt: string;
    employee: Employee;
    errorMessage: string;
}

export const initialState: EmployeeState = {
    loading: 'idle',
    adding: 'idle',
    deleting: 'idle',
    editing: 'idle',
    receivedAt: null,
    employee: {
        id: 0,
        firstName: '',
        middleName: '',
        lastName: '',
        income: 0,
        dependents: []
    },
    errorMessage: null
}

const slice = createSlice({
    name: 'employee',
    initialState,
    reducers: {
        fetching(state) {
            state.loading = 'pending';
        },
        adding(state) {
            state.adding = 'pending';
        },
        added(state){
            state.adding = 'succeeded';
            state.employee = initialState.employee;
            state.receivedAt = initialState.receivedAt;
        },
        deleting(state) {
            state.deleting = 'pending';
        },
        deleted(state){
            state.deleting = 'succeeded';
            state.employee = initialState.employee;
            state.receivedAt = initialState.receivedAt;
        },
        editing(state) {
            state.editing = 'pending';
        },
        edited(state){
            state.editing = 'succeeded';
        },
        received(state, action: PayloadAction<Employee>){
            state.loading = 'succeeded';
            state.receivedAt = new Date().toISOString();
            state.employee = action.payload;
        },
        error(state, action: PayloadAction<string>) {
            state.loading = 'failed';
            state.errorMessage = action.payload;
        },
        createNew(state) {
            state.loading = 'succeeded';
            state.receivedAt = initialState.receivedAt;
            state.employee = initialState.employee;
        },
        updateEmployee(state, action: PayloadAction<Employee>) {
            state.employee = action.payload;
        }
    }
});

export const { 
    fetching, received, error, createNew, added, adding, deleted, deleting, edited, editing, updateEmployee
} = slice.actions;

export default slice.reducer;

export const fetchEmployee = (id: number, costPeriods?: number): any => async (dispatch: Dispatch) => {
    dispatch(fetching());
    
    try {
        var route = `https://localhost:5001/api/Employees/${id}`;
        var employeePromise = Axios.get<Employee>(`${route}`);

        var results = await Promise.all([employeePromise, dispatch(fetchEmployeeDeductions(id, costPeriods))]);
        var employee = results[0].data;

        dispatch(received(employee));
    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
    }
}

export const addNewEmployee = (employee: Employee): any => async (dispatch: Dispatch) => {
    dispatch(adding());
    
    try {
        var route = `https://localhost:5001/api/Employees`;

        await Axios.post<void>(`${route}`, employee);

        dispatch(added());
        dispatch(fetchEmployees());
    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
    }
}

export const addNewDependent = (employeeId: number, dependent: Dependent): any => async (dispatch: Dispatch) => {
    dispatch(adding());
    
    try {
        var route = `https://localhost:5001/api/Employees/${employeeId}/Dependents`;

        await Axios.post<void>(`${route}`, dependent);

        dispatch(added());
        dispatch(fetchEmployees());
    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
    }
}

export const deleteEmployee = (id: number): any => async (dispatch: Dispatch) => {
    dispatch(deleting());
    try {
        var route = `https://localhost:5001/api/Employees/${id}`;

        await Axios.delete<void>(`${route}`);

        dispatch(deleted())
        dispatch(fetchEmployees());
    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
    }
}

export const deleteDependent = (id: number, dependentId: number): any => async (dispatch: Dispatch) => {
    dispatch(deleting());
    try {
        var route = `https://localhost:5001/api/Employees/${id}/Dependents/${dependentId}`;

        await Axios.delete<void>(`${route}`);

        dispatch(deleted())
        dispatch(fetchEmployees());
    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
    }
}

export const editEmployee = (employee: Employee): any => async (dispatch: Dispatch) => {
    dispatch(editing());
    try {
        var route = `https://localhost:5001/api/Employees/${employee.id}`;

        await Axios.put<void>(`${route}`, employee);

        dispatch(edited())
        dispatch(fetchEmployees());
    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
    }
}

export const editDependent = (id: number, dependent: Dependent): any => async (dispatch: Dispatch) => {
    dispatch(editing());
    try {
        var route = `https://localhost:5001/api/Employees/${id}/Dependents/${dependent.id}`;

        await Axios.put<void>(`${route}`, dependent);

        dispatch(edited())
        dispatch(fetchEmployees());
    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
    }
}