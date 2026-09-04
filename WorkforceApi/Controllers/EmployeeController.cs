using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WorkforceApi.Data;
using WorkforceApi.Models;
using WorkforceApi.Dtos;

namespace WorkforceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

	// function to update an employee
	[HttpPut("update/{id}")]
	
	public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployee updatedEmployee)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);
		// look for employee, return 404 if not found
		var employee = await _context.Employees.FindAsync(id);
		var managerId = await _context.Employees.FindAsync(updatedEmployee.ManagerId);

		// return not found if the employee id doesnt exist
		if (employee == null)
		{
			return NotFound();
		}
		else
		{
			// update employee
			if (!string.IsNullOrWhiteSpace(updatedEmployee.FirstName)) employee.FirstName = updatedEmployee.FirstName;
			if (!string.IsNullOrWhiteSpace(updatedEmployee.LastName)) employee.LastName = updatedEmployee.LastName;
			if (!string.IsNullOrWhiteSpace(updatedEmployee.Email)) employee.Email = updatedEmployee.Email;
			if (!string.IsNullOrWhiteSpace(updatedEmployee.Title)) employee.Title = updatedEmployee.Title;

			if (managerId != null) employee.ManagerId = updatedEmployee.ManagerId;

			employee.Role = (EmployeeRole)updatedEmployee.Role;
			employee.Status = (EmployeeStatus)updatedEmployee.Status;

			// save and return NoContent
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}

	// function to offboard employees
	[HttpPut("offboard/{id}")]
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