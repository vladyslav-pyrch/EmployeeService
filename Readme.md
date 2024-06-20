Employee Web Service made on the .Net Core platform.
The service can:

1. Add employees, the ID of the added employee comes in response.
2. Delete an employee by ID.
3. Output the list of employees for the specified company.
4. Output the list of employees for the specified department of the company.
5. Modify an employee by their ID. A field is changed, if it is specified in the query.

Employee model:
```json
{
  "Id": "int",
  "Name": "string",
  "Surname": "string",
  "Phone": "string",
  "CompanyId": "int",
  "Passport": {
    "Type": "string",
    "Number": "string"
  },
  "Department": {
    "Name": "string",
    "Phone": "string"
  }
}
```
All methods are implemented as HTTP requests in JSON format.
