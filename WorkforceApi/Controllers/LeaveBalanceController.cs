using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkforceApi.Data;

[ApiController]
[Route("api/[controller]")]
[Authorize]

//TODO: remove this, balances are accessed through employees

public class LeaveBalanceController : ControllerBase
{
	// const to hold db context
	private readonly WorkforceContext _context;

	// grabbing db context
	public LeaveBalanceController(WorkforceContext context)
	{
		_context = context;
	}

	// function to get all rows from the leaveBalance table
	[HttpGet]
	public async Task<IActionResult> GetLeaveBalances()
	{
		var LeaveBalances = await _context.LeaveBalances.ToListAsync();
		return Ok(LeaveBalances);
	}
}