using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WorkforceApi.Data;
using WorkforceApi.Dtos;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class AddressController : ControllerBase
{
	private readonly WorkforceContext _context;

	public AddressController(WorkforceContext context)
	{
		_context = context;
	}

	// endpoint to fetch companies from Companies Table
	[HttpGet]
	public async Task<IActionResult> GetAddresses()
	{
		var addresses = await _context.Addresses
		.Select(a => new AddressResponseDTO
		{
			Id = a.Id,
			StreetLine1 = a.StreetLine1,
			Country = a.Country,
			Province = a.Province,
			City = a.City,
			PostalCode = a.PostalCode,
		})
		.ToListAsync();
		return Ok(addresses);
	}


}