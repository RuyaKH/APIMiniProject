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
	* Must include First and Last Name, every other field is optional
	* Expected input:
	 ```JSON
	{
  "lastName": "string",
  "firstName": "string",
  "title": "string",
  "titleOfCourtesy": "string",
  "birthDate": "2023-04-05T09:15:04.504Z",
  "hireDate": "2023-04-05T09:15:04.504Z",
  "address": "string",
  "city": "string",
  "region": "string",
  "postalCode": "string",
  "country": "string",
  "homePhone": "string",
  "extension": "string",
  "photo": "string",
  "notes": "string",
  "reportsTo": 0,
  "photoPath": "string",
  "reportsToNavigation": "string"
	} 
	```
- `GET Employees`: Request data regarding *all* employees
	* `http://localhost/api/Employees`
- `GET Employee By Id`: Request data associated with a *specific* employee 
	* `http://localhost/api/Employees/{employeeId}`
	* Will return a list of employees, including first and last name, job title, and their performance metrics
	* Output:
	```JSON
	[
  {
    "employeeId": 0,
    "lastName": "string",
    "firstName": "string",
    "title": "string",
    "numberOfOrders": 0,
    "totalMoneyMade": 0,
    "averageSale": 0
  }
	]
	```

	
- `GET Employee With Most Profit`: Request details of the employee who has generated the *most* income
	* `http://localhost/api/Employees/MostProfitable`
- `GET Employee With Most Sales`: Request details of the employee who has made the *most individual* sales
	* `http://localhost/api/Employees/MostItems`
- `GET Who Employee Reports To`: Request details of who *all* employees report to
	* `http://localhost/api/Employees/ReportsTo`
	* Will return a list of the name of every employee coupled with the name of the person they report to
	* Output:
	```JSON
	[
  {
    "employee": "Employee Name",
    "reportsto": "Superior Name"
  }
  	]
	```
- `GET Employees Monthly Sales`: Request details of *monthly* sales
	* `http://localhost/api/Employees/MonthlySales`
	* Will return a list of the month, year and total sales for each month
	* Output:
	```JSON
	[
  {
    "month": 7,
    "year": 1996,
    "totalSales": 30192.1
  }
  	]
	```
- `PUT Employee Territory`: Updates the territory details of a *specific* employee
	* `http://localhost/api/Employees/{employeeId}/territory`
	* Accepts a list of territory ids in the format of a list of strings
	* Will replace the territories that an employee is responsible for with the ones specified in the list provided
	* Expected input (additional territory ids can be added, separated by commas):
	```JSON
	[
	"territoryId"
	]
	```
- `PUT Employee Details`: Updates the details of a *specific* employee
	* `http://localhost/api/Employees/{employeeId}`
	* Will only change the details provided in the request body
	* Expected input (fields can be omitted):
	```JSON
	{
  "employeeId": 0,
  "lastName": "string",
  "firstName": "string",
  "title": "string",
  "address": "string",
  "city": "string",
  "postalCode": "string",
  "country": "string"
	}
	```
	
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
