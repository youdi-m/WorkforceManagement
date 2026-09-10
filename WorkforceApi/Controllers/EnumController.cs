using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkforceApi.Data;
using WorkforceApi.Models;
using WorkforceApi.Dtos;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EnumController : ControllerBase
{
	// const to hold db context
	private readonly WorkforceContext _context;

	// store given context
	public EnumController(WorkforceContext context)
	{
		_context = context;
	}
	
}