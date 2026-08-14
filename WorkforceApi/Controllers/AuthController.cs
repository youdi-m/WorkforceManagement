using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkforceApi.Data;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
	// holding db context
	private readonly WorkforceContext _context;
	// store given context
	public AuthController(WorkforceContext context)
	{
		_context = context;
	}

	// function to search for email, verify password and return user id, email and role upon success
	[HttpPost("Login")]
	public async Task<IActionResult> LoginUser(LoginRequest request)
	{
		var user = await _context.Employees.FirstOrDefaultAsync(e => e.Email == request.Email);
		if (user == null) {return Unauthorized("Invalid Credentials");}

		bool valid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
		if (!valid) {return Unauthorized("Invalid Credentials");}

		return Ok(new {user.Id, user.Email, user.Role});
	}

}