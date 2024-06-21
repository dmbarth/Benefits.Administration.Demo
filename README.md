## Benefits.Administration.Demo

### Getting Started

1. Update `BenefitsDbContext` connection string in the `appsettings.Development.json`
2. Run the EF database migration
```
dotnet ef database update --project src/Benefits.Administration.API
```
3. Run the backend
```
dotnet run --project  src/Benefits.Administration.API
```
4. Install client packages
```
cd src/Benefits.Administration.Client && npm i
``` 
5. Start the client dev server with HMR on port `4321` for localhost
```
npm start
```
6. Open up a web browser and navigate to `http://localhost:4321`