# APIMiniProject
---
# API-Mini-Project

## What is it?
An API that serves the purpose of providing users the ability to request and receive data over HTTP regarding **Employees** of the **Northwind** Database.

## Technologies Used
- ASP.NET Core
- MS SQL Server (Northwind Database)
- Entity Framework
- MoQ Mocking Framework (Testing)

## Implemented Functionality
Following a CRUD approach, a set of operations were implemented:
- `POST Employee`: Creation of a *new* employee
	* `http://localhost/api/Employees/{employeeId}`
- `GET Employees`: Request data regarding *all* employees
	* `http://localhost/api/Employees`
- `GET Employee By Id`: Request data associated with a *specific* employee 
	* `http://localhost/api/Employees/{employeeId}`
- `GET Employee With Most Profit`: Request details of the employee who has generated the *most* income
	* `http://localhost/api/Employees/MostProfitable`
- `GET Employee With Most Sales`: Request details of the employee who has made the *most individual* sales
	* `http://localhost/api/Employees/MostItems`
- `GET Who Employee Reports To`: Request details of who *all* employees report to
	* `http://localhost/api/Employees/ReportsTo`
- `GET Employees Monthly Sales`: Request details of *monthly* sales
	* `http://localhost/api/Employees/MonthlySales`
- `PUT Employee Territory`: Updates the territory details of a *specific* employee
	* `http://localhost/api/Employees/{employeeId}/territory`
- `PUT Employee Details`: Updates the details of a *specific* employee
	* `http://localhost/api/Employees/{employeeId}`
- `DELETE Employee`: Deletion of an *existing* employee
	* `http://localhost/api/Employees/{employeeId}`
- `DELETE Employee Territory`: Deletion of a territory that an *existing* employee is no longer responsible for
	* `http://localhost/api/Employees/{employeeId}/Terrirtories/{territoryId}`

## Contributors
- Ruya
- Alin
- Alex
- Ali
- Max
- Henry
- Luke
