type EmployeeDeductions = {
    employeeID: number;
    costPeriods: number;
    employeeCost: number;
    employeeDiscounts: number[];
    employeeTotal: number;
    dependentsCost: number;
    dependentsDiscounts: number[];
    dependentsTotal: number;
    totalCost: number;
}

export default EmployeeDeductions;