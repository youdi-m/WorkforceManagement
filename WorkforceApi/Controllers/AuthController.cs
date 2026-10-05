using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WorkforceApi.Data;
using WorkforceApi.Models;
using WorkforceApi.Dtos;
using WorkforceApi.Constants;

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

	// endpoint to register a new user
	[HttpPost("Register")]
	public async Task<IActionResult> Register(RegisterRequest request)
	{
		// verify if user and company exist

		var email = await _context.Employees.FirstOrDefaultAsync(e => e.Email == request.Email);
		if (email != null) {return Conflict("Email already registered");}

		var companyExists = await _context.Companies.FirstOrDefaultAsync(c => c.TaxId == request.TaxId &&
																																	c.LegalName == request.LegalName);
		if (companyExists != null) {return Conflict("Company already registered");}

		// create company
		var company = new Company
		{
			Name = request.Name,
			LegalName = request.LegalName,
			TaxId = request.TaxId,
			TimeZone = request.TimeZone,
			Currency = request.Currency,
		};

		// save company to db
		_context.Companies.Add(company);
		await _context.SaveChangesAsync();

		// create employee
		var employee = new Employee
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Email = request.Email,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
			Wage = request.Wage,
			CompanyId = company.Id,
			HireDate = DateTime.UtcNow,
		};

		// create address
		var address = new Address
		{
			StreetLine1 = request.StreetLine1,
			StreetLine2 = request.StreetLine2,
			Country = request.Country,
			Province = request.Province,
			City = request.City,
			PostalCode = request.PostalCode,
			CompanyId = company.Id
		};

		// save employee and address to db
		_context.Employees.Add(employee);
		_context.Addresses.Add(address);
		await _context.SaveChangesAsync();

		// return OK
		return Ok("Registration successful");
	}

	// endpoint to search for email, verify password and return user id, email and role upon success
	[HttpPost("Login")]
	public async Task<IActionResult> Login(LoginRequest request)
	{
		// search for email in db, return Invalid Credentilas if not found
		var user = await _context.Employees.FirstOrDefaultAsync(e => e.Email == request.Email);
		if (user == null) {return Unauthorized("Invalid Credentials");}

		// verify that the passwords match, return Invalid Credentials if not
		bool valid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
		if (!valid) {return Unauthorized("Invalid Password");}

		// generate and return token since both email and password are correct
		var token = GenerateToken(user);
		return Ok(new {token, user.Role, user.CompanyId});
	}

	// function to generate the token
	private string GenerateToken(Employee user)
	{
		// data used to generate token
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Role, user.Role.ToString()),
			new Claim(CustomClaimTypes.CompanyId, user.CompanyId.ToString())
		};

		// load secret and sign it
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		// assembles data needed to create the token
		var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(8),
			signingCredentials: creds);

		// return token as string
		return new JwtSecurityTokenHandler().WriteToken(token);
	}

}