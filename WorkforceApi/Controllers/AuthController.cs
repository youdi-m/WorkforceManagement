using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WorkforceApi.Data;
using WorkforceApi.Models;
using WorkforceApi.Dtos;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
	// holding db context
	private readonly WorkforceContext _context;

	// read appsetting values for token generation
	private readonly IConfiguration _config;

	// store given context and config
	public AuthController(WorkforceContext context, IConfiguration config)
	{
		_context = context;
		_config = config;
	}

	// function to search for email, verify password and return user id, email and role upon success
	[HttpPost("Login")]
	public async Task<IActionResult> Login(LoginRequest request)
	{
		// search for email in db, return Invalid Credentilas if not found
		var user = await _context.Employees.FirstOrDefaultAsync(e => e.Email == request.Email);
		if (user == null) {return Unauthorized("Invalid Credentials");}

		// verify that the passwords match, return Invalid Credentials if not
		bool valid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
		if (!valid) {return Unauthorized("Invalid Password");}

		// generate and return token since bot email and password are correct
		var token = GenerateToken(user);
		return Ok(new {token, user.Role});
	}

	// function to generate the token
	private string GenerateToken(Employee user)
	{
		// data used to generate token
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		};

		// load secret and sign it
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		// assembles data needed to create the token
		var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(2),
			signingCredentials: creds);

		// return token as string
		return new JwtSecurityTokenHandler().WriteToken(token);
	}

}