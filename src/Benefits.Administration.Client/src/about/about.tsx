import React from "react";
import { Container } from "react-bootstrap";

export default class About extends React.Component
{
    render(){
        return (
            <Container>
                <h4 className="mt-5">Business Need:</h4>
                <p>
                    One of the critical functions that we provide for our clients is the ability to pay for their employees’ benefits
                    packages. A portion of these costs are deducted from their paycheck, and we handle that deduction. Please
                    demonstrate how you would code the following scenario:
                </p>
                <ul>
                    <li>The cost of benefits is $1000/year for each employee</li>
                    <li>Each dependent (children and possibly spouses) incurs a cost of $500/year</li>
                    <li>Anyone whose name starts with ‘A’ gets a 10% discount, employee or dependent</li>
                </ul>
                <p>
                    We’d like to see this calculation used in a web application where employers input employees and their
                    dependents, and get a preview of the costs. This is of course a contrived example. We want to know how you
                    would implement the application structure and calculations and get a brief preview of how you work.
                    Please implement a web application based on these assumptions:
                </p>
                <ul>
                    <li>All employees are paid $2000 per paycheck before deductions</li>
                    <li>There are 26 paychecks in a year</li>
                </ul>
            </Container>
        )
    }
}