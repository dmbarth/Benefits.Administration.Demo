import React from "react";
import { Button, Jumbotron } from "react-bootstrap";
import { Link } from "react-router-dom";

export default class Home extends React.Component
{
    render(){
        return (
            <Jumbotron>
                <h1>Welcome!</h1>
                <p>
                    This is a demo that allows administrators to add, edit and delete employees and their dependents. Then, 
                    calculate their benefits deductions and display their costs.
                </p>
                <p>
                    <Button variant="dark" to="/about" as={Link}>Learn more</Button>
                </p>
            </Jumbotron>
        )
    }
}