using EmployeeService.Application.Companies.DeleteDepartment;
using EmployeeService.Domain.Model.Companies.Departments;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.DeleteDepartment;

public class DeleteDepartmentAction : ExtendedControllerBase
{
	private readonly DeleteDepartmentCommandHandler _deleteDepartmentCommandHandler;

	public DeleteDepartmentAction(DeleteDepartmentCommandHandler deleteDepartmentCommandHandler) =>
		_deleteDepartmentCommandHandler = deleteDepartmentCommandHandler;

	[HttpDelete("api/Department/DeleteDepartment")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromQuery] DeleteDepartmentRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var deleteCompanyCommand = new DeleteDepartmentCommand(
			Guid.NewGuid(), new DepartmentId(request.DepartmentId.Value)
		);

		_deleteDepartmentCommandHandler.Handle(deleteCompanyCommand);

		return Ok();
	}
}