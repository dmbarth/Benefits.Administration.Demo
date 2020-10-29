import { createSlice, Dispatch, PayloadAction } from "@reduxjs/toolkit"
import Axios, { AxiosError } from "axios"
import qs from "qs"
import EmployeeDeductions from "../types/EmployeeDeductions"
import { LoadingProps } from "/app/components/loading-spinner"

export type DeductionsState = {
    loading: LoadingProps;
    receivedAt: string;
    deductions: EmployeeDeductions;
    costPeriods: number;
    errorMessage: string;
}

export const initialState: DeductionsState = {
    loading: 'idle',
    receivedAt: null,
    deductions: null,
    costPeriods: null,
    errorMessage: null
}

const slice = createSlice({
    name: 'deductions',
    initialState,
    reducers: {
        fetching(state, action: PayloadAction<number>) {
            state.loading = 'pending';
            state.costPeriods = action.payload;
        },
        received(state, action: PayloadAction<EmployeeDeductions>){
            state.loading = 'succeeded';
            state.receivedAt = new Date().toISOString();
            state.deductions = action.payload;
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

export const fetchEmployeeDeductions = (id: number, costPeriods: number): any => async (dispatch: Dispatch) => {
    dispatch(fetching(costPeriods));

    try {
        var route = `https://localhost:5001/api/Employees/${id}/Deductions${qs.stringify({costPeriods}, { addQueryPrefix: true })}`;
            
        var result = await Axios.get<EmployeeDeductions>(route);

        dispatch(received(result.data));
    } catch (err) {
        const axiosError: AxiosError = err;

        console.error(axiosError.message);
    }
}