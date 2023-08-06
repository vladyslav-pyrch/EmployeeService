using EmployeeService.Application.Employees.DeleteEmployee;
using EmployeeService.Domain.Model.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Employees.DeleteEmployee;

[ApiController]
public class DeleteEmployeeAction : ExtendedControllerBase
{
	private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;

	public DeleteEmployeeAction(DeleteEmployeeCommandHandler deleteEmployeeCommandHandler) =>
		_deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;

	[HttpDelete("api/Employee/Delete")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult Invoke([FromQuery] DeleteEmployeeRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var deleteEmployeeQuery = new DeleteEmployeeCommand(
			Guid.NewGuid(), new EmployeeId(request.EmployeeId.Value)
		);

		_deleteEmployeeCommandHandler.Handle(deleteEmployeeQuery);

		return Ok();
	}
}