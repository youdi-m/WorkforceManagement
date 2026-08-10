using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkforceApi.Data;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LeaveRequestController : ControllerBase
{
	// const to store db context
	private readonly WorkforceContext _context;

	// storing given context
	public LeaveRequestController(WorkforceContext context)
	{
		_context = context;
	}

	// function to get all rows from the LeaveRequests table
	[HttpGet]
	public async Task<IActionResult> GetLeaveRequests()
	{
		var leaveRequests = await _context.LeaveRequests.ToListAsync();
		return Ok(leaveRequests);
	}
}