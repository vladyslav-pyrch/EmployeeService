using EmployeeService.Application.Companies.UpdateDepartment;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.UpdateDepartment;

[ApiController]
public class UpdateDepartmentAction : ExtendedControllerBase
{
	private readonly UpdateDepartmentCommandHandler _updateDepartmentCommandHandler;

	public UpdateDepartmentAction(UpdateDepartmentCommandHandler updateDepartmentCommandHandler)
	{
		_updateDepartmentCommandHandler = updateDepartmentCommandHandler;
	}

	[HttpPatch("api/Company/UpdateDepartment")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromBody] UpdateDepartmentRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var updateDepartmentCommand = new UpdateDepartmentCommand(
			Guid.NewGuid(), new DepartmentId(request.DepartmentId.Value)
		)
		{
			Name = request.Name,
			PhoneNumber = request.PhoneNumber == null ? null : new PhoneNumber(request.PhoneNumber)
		};
		
		_updateDepartmentCommandHandler.Handle(updateDepartmentCommand);

		return Ok();
	}
}