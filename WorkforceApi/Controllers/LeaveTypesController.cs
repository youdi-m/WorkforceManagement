using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkforceApi.Data;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class LeaveTypesController : ControllerBase
{
	private readonly WorkforceContext _context;

	public LeaveTypesController(WorkforceContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetLeaveTypes()
	{
		var LeaveTypes = await _context.LeaveTypes.ToListAsync();
		return Ok(LeaveTypes);
	}
}