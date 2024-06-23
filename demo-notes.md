# Overview

## Objective

## Stack

## Architecture

### API

#### Service Pattern

#### Repository and Unit of Work Pattern
>The repository and unit of work patterns are intended to create an abstraction layer between the data access layer and the business logic layer of an application. Implementing these patterns can help insulate your application from changes in the data store and can facilitate automated unit testing or test-driven development (TDD).

>The unit of work class serves one purpose: to make sure that when you use multiple repositories, they share a single database context.

Source: [learn.Microsoft.com | The Repository and Unit of Work Patterns](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#the-repository-and-unit-of-work-patterns)

#### Abstract Repository
The abstract repository provides virtual methods for accessing the `DbSet` and can be overriden for specific implementations in the derived class.

### Database

### UI


# Things I would change for Production

## API

### Pagination

### Filterning

### Global Exception Handler

### REST Operations
- Remove `api` prefix and use domain identifier
- Add additional response types and models to complete the OpenAPI specification

### Domain Objects

### Database

- Optimize queries by replacing EF with Dapper ORM and create efficient SQL statements with cacheable execution plans
- Or if EF is required, expand on the Repository abstract class to be more flexible; ex. expression argument for Get

## UI


# How I would deploy with AWS
