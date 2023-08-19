using EmployeeService.Api.Contracts;
using EmployeeService.Application.Companies.GetDepartmentById;
using EmployeeService.Domain.Model.Companies.Departments;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.GetDepartmentById;

[ApiController]
public class GetDepartmentByIdAction : ExtendedControllerBase
{
	private readonly GetDepartmentByIdQueryHandler _getDepartmentByIdQueryHandler;

	public GetDepartmentByIdAction(GetDepartmentByIdQueryHandler getDepartmentByIdQueryHandler)
	{
		_getDepartmentByIdQueryHandler = getDepartmentByIdQueryHandler;
	}

	[HttpGet("api/Company/GetDepartmentById")]
	[ProducesResponseType(typeof(GetDepartmentByIdResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromQuery] GetDepartmentByIdRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var getDepartmentByIdQuery = new GetDepartmentByIdQuery(
			new DepartmentId(request.DepartmentId.Value)
		);

		Department department = _getDepartmentByIdQueryHandler.Handle(
			getDepartmentByIdQuery
		);

		var departmentDto = new DepartmentDto(
			department.Identity.Deconvert(), department.Name, department.PhoneNumber.Number
		);

		return Ok(new GetDepartmentByIdResponse(departmentDto));
	}
}