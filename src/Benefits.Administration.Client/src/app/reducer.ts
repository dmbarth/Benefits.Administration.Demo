import { combineReducers } from "@reduxjs/toolkit";
import employees from '../employees/store/employees-store'
import employee from '../employees/store/employee-store'
import deductions from '../employees/store/deductions-store'

export default combineReducers({
    employees,
    employee,
    deductions
});