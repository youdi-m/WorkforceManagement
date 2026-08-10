using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkforceApi.Data;
using WorkforceApi.Models;

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
	public async Task<IActionResult> GetLeaveRequest()
	{
		var leaveRequests = await _context.LeaveRequests.ToListAsync();
		return Ok(leaveRequests);
	}

	// function to create a new request
	[HttpPost]
	public async Task<IActionResult> CreateRequest(LeaveRequest request)
	{
		// setting status as requested
		request.Status = LeaveStatus.Requested;

		_context.LeaveRequests.Add(request);
		await _context.SaveChangesAsync();
		return CreatedAtAction(nameof(GetLeaveRequest), new { id = request.Id }, request);
	}
	
}