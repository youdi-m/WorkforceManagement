using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WorkforceApi.Data;
using WorkforceApi.Dtos;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class CompanyController : ControllerBase
{
	private readonly WorkforceContext _context;

	public CompanyController(WorkforceContext context)
	{
		_context = context;
	}

	// endpoint to fetch companies from Companies Table
	[HttpGet]
	public async Task<IActionResult> GetCompanies()
	{
		var companies = await _context.Companies
		.Select(c => new CompanyResponseDTO
		{
			Id = c.Id,
			Name = c.Name,
			LegalName = c.LegalName,
			TaxId = c.TaxId,
			IsActive = (bool)c.IsActive,
		})
		.ToListAsync();
		return Ok(companies);
	}

}