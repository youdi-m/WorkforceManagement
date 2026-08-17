using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkforceApi.Data;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class LeaveTypesController : ControllerBase
{
	// const to hold db context
	private readonly WorkforceContext _context;

	// store given context
	public LeaveTypesController(WorkforceContext context)
	{
		_context = context;
	}

	// function to get all rows from the LeaveTypes table
	[HttpGet]
	public async Task<IActionResult> GetLeaveTypes()
	{
		var leaveTypes = await _context.LeaveTypes.ToListAsync();
		return Ok(leaveTypes);
	}
}