## Benefits.Administration.Demo

### Getting Started

1. Open the solution with Visual Studio
1. Specify the startup project `Benefits.Administration.API`
3. Open Package Manager Console in Visual Studio and run
```
Update-Database
```
3. Debug `Benefits.Administration.Demo`
    - Swagger will startup indicating the API is ready
4. Open a terminal and navigate to `Benefits.Administration.Client` folder
5. Run the command and let the dependencies install
```
npm i
``` 
6. Start a dev server with HMR on port `4321` for localhost
```
npm start
```
7. Open up a web browser and navigate to `http://localhost:4321`