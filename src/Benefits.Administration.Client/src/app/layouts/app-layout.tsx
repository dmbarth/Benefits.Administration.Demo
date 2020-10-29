import React from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import { Link } from "react-router-dom";

import './app-layout.scss';

export default class AppLayout extends React.Component
{
    render(){
        return (
            <div id="app-layout">
                <Navbar bg="dark" variant="dark" sticky="top">
                    <Navbar.Brand>Benefits</Navbar.Brand>
                    <Nav className="mr-auto">
                        <Nav.Link as={Link} to="/">Home</Nav.Link >
                        <Nav.Link as={Link} to="/employees">Employees</Nav.Link>
                        <Nav.Link as={Link} to="/about">About</Nav.Link>
                    </Nav>
                </Navbar>
                <Container>
                    {this.props.children}
                </Container>
            </div>
        )
    }
}