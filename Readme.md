Employee Web Service made on the .Net Core platform.
The service is able to:

1. Add employees, the Id of the added employee should come in response.
2. Delete employees by Id.
3. Output the list of employees for the specified company. All available fields.
4. Output the list of employees for the specified department of the company. All available fields.
5. Modify an employee by his Id. Only those fields should be changed,
that are specified in the query.

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
All methods should be implemented as HTTP requests in JSON format.
