using Microsoft.AspNetCore.Mvc;
using WorkforceApi.Data;
using Microsoft.EntityFrameworkCore;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
	// const to hold db context
	private readonly WorkforceContext _context;

	// store given context
	public EmployeeController(WorkforceContext context)
	{
		_context = context;
	}

	// function to get all rows from the Employees table
	[HttpGet]
	public async Task<IActionResult> GetEmployees()
	{
		var employees = await _context.Employees.ToListAsync();
		return Ok(employees);
	}
}