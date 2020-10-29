## Benefits.Administration.Demo

### Getting Started

1. Open the solution with Visual Studio
2. Debug `Benefits.Administration.Demo` targeting `Benefits.Administration.API`
    - EF Core will seed some data and scaffold out the database on (localdb)MSSQLLocalDB
    - Swagger will startup indicating the API is ready
3. Open a terminal and navigate to `Benefits.Administration.Client` folder
4. Run the command `npm i` and let the dependencies install
5. Run `npm start` and a dev server with HMR will startup on port `4321` for localhost
6. Open up a web browser and navigate to `http://localhost:4321`