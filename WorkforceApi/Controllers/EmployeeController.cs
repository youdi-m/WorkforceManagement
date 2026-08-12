using Microsoft.AspNetCore.Mvc;
using WorkforceApi.Data;
using Microsoft.EntityFrameworkCore;
using WorkforceApi.Models;
using BCrypt.Net;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
	// const to hold db context
	private readonly WorkforceContext _context;

	// store given context
	public EmployeeController(WorkforceContext context)
	{
		_context = context;
	}

	// function to get all rows from the Employees table
	[HttpGet]
	public async Task<IActionResult> GetEmployees()
	{
		var employees = await _context.Employees.ToListAsync();
		return Ok(employees);
	}

	// function to create a new employee
	[HttpPost]
	public async Task<IActionResult> CreateEmployee(Employee employee)
	{
		// hashing password before storing in db
		employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(employee.PasswordHash);
		
		_context.Employees.Add(employee);
		await _context.SaveChangesAsync();
		return CreatedAtAction(nameof(GetEmployees), new {id = employee.Id}, employee);
	}

	// function to offboard an employee
	[HttpPut("{Id}")]
	public async Task<IActionResult> UpdateEmployee(int id, Employee updatedEmployee)
	{
		// look for employee, return 404 if not found
		var employee = await _context.Employees.FindAsync(id);
		if (employee == null) return NotFound();

		// update employee
		employee.FirstName = updatedEmployee.FirstName;
		employee.LastName = updatedEmployee.LastName;
		employee.Title = updatedEmployee.Title;
		employee.ManagerId = updatedEmployee.ManagerId;

		// save and return NoContent
		await _context.SaveChangesAsync();
		return NoContent();
	}

	// function to offboard employees
	[HttpPut("{Id}/offboard")]
	public async Task<IActionResult> OffboardEmployee(int id)
	{
		// find employee, return 404 if not found
		var employee = await _context.Employees.FindAsync(id);
		if (employee == null) return NotFound();

		// set status to offboarded
		employee.Status = EmployeeStatus.Offboarded;

		// save and return no content
		await _context.SaveChangesAsync();
		return NoContent();
	}
}