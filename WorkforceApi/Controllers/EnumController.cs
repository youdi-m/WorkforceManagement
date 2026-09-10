using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkforceApi.Models;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EnumController : ControllerBase
{
	[HttpGet("employeeroles")]
	public IActionResult GetEmployeeRoles()
	{
		var roles = Enum.GetValues<EmployeeRole>()
		.Cast<EmployeeRole>()
		.Select(r => new {value = (int)r, label = r.ToString()})
		.ToList();

		return Ok(roles);
	}

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