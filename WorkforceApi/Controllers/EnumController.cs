using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkforceApi.Models;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EnumController : ControllerBase
{
	// endpoint to retrieve roles
	[HttpGet("employeeroles")]
	public IActionResult GetEmployeeRoles()
	{
		var roles = Enum.GetValues<EmployeeRole>()
		.Cast<EmployeeRole>()
		.Select(r => new {value = (int)r, label = r.ToString()})
		.ToList();

		return Ok(roles);
	}

	// enpoint to retreive statuses
	[HttpGet("employeestatuses")]
	public IActionResult GetEmployeeStatuses()
	{
		var statuses = Enum.GetValues<EmployeeStatus>()
		.Cast<EmployeeStatus>()
		.Select(s => new {value = (int)s, label = s.ToString()})
		.ToList();

		return Ok(statuses);
	}
}