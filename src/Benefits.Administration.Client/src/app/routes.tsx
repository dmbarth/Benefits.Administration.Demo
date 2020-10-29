import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import NotFound from "./components/not-found";
import Employees from "../employees/employees";
import AppLayout from "./layouts/app-layout";
import Home from "../home/home";
import About from "/about/about";

export default class AppRoutes extends React.Component
{
    render() {
        return (
            <BrowserRouter>
                <AppLayout>
                    <Switch>
                        <Route path="/" exact>
                            <Home />
                        </Route>
                        <Route path="/employees/:id?">
                            <Employees />
                        </Route>
                        <Route path="/about">
                            <About />
                        </Route>
                        <Route>
                            <NotFound />
                        </Route>
                    </Switch>
                </AppLayout>
            </BrowserRouter>
        )
    }
}