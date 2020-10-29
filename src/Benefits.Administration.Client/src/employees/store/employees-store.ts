import { LoadingProps } from './../../app/components/loading-spinner';
import { createSlice, Dispatch, PayloadAction } from "@reduxjs/toolkit";
import Axios, { AxiosError } from "axios";
import Employee from '../types/Employee';

export type EmployeesState = {
    loading: LoadingProps;
    receivedAt: string;
    entities: Employee[];
    errorMessage: string;
}

export const initialState: EmployeesState = {
    loading: 'idle',
    receivedAt: null,
    entities: [],
    errorMessage: null,
}

const slice = createSlice({
    name: 'employees',
    initialState,
    reducers: {
        fetching(state) {
            state.loading = 'pending';
        },
        received(state, action: PayloadAction<Employee[]>) {
            state.loading = 'succeeded';
            state.receivedAt = new Date().toISOString();
            state.entities = action.payload;
        },
        error(state, action: PayloadAction<string>) {
            state.loading = 'failed';
            state.errorMessage = action.payload;
        }
    }
});

export const { 
    fetching, received, error
} = slice.actions;

export default slice.reducer;

export const fetchEmployees = (): any => async (dispatch: Dispatch) => {
    dispatch(fetching());

    try {
        var response = await Axios.get<Employee[]>("https://localhost:5001/api/Employees");

        dispatch(received(response.data))

    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
        
        dispatch(error("There was an error fetching the Employees."))
    }
}
