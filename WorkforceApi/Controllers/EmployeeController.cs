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
		var employees = await _context.Employees
		.Select(e => new EmployeeResponse
		{
			Id = e.Id,
			FirstName = e.FirstName,
			LastName = e.LastName,
			Email = e.Email,
			Role = (int)e.Role,
			Title = e.Title,
			ManagerId = e.ManagerId,
			Status = (int)e.Status,
			ShiftStartTime = e.ShiftStartTime,
			ShiftEndTime = e.ShiftEndTime,
			DateOfBirth = e.DateOfBirth,
		})
		.ToListAsync();
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
		Console.WriteLine("**************HERE**************" + System.Text.Json.JsonSerializer.Serialize(updatedEmployee));
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
			if (updatedEmployee.Role != null) employee.Role = (EmployeeRole)updatedEmployee.Role;
			if (updatedEmployee.Status != null) employee.Status = (EmployeeStatus)updatedEmployee.Status;

			if(updatedEmployee.ShiftStartTime != null) employee.ShiftStartTime = TimeOnly.Parse(updatedEmployee.ShiftStartTime);
			if(updatedEmployee.ShiftEndTime != null)employee.ShiftEndTime = TimeOnly.Parse(updatedEmployee.ShiftEndTime);
			if(updatedEmployee.DateOfBirth != null)employee.DateOfBirth = DateOnly.Parse(updatedEmployee.DateOfBirth);

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