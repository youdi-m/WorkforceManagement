using Microsoft.EntityFrameworkCore;

namespace WorkforceApi.Models;

public class Employee
{
	public int Id {get; set;}
	public required string FirstName {get; set;}
	public required string LastName {get; set;}
	public string? Title {get; set;}
	public EmployeeRole Role {get; set;} = EmployeeRole.Employee;
	public required string Email {get; set;}
	public string? PhoneNumber {get; set;}
	public required string PasswordHash {get; set;}
	public DateOnly? DateOfBirth {get; set;}

	// shift
	[Precision (18, 2)]
	public required decimal Wage {get; set;}
	public ICollection<Shift> Shifts {get; set;} = new List<Shift>();
	public ICollection<LeaveRequest> LeaveRequests {get; set;} = new List<LeaveRequest>();
	public ICollection<LeaveBalance> LeaveBalances {get; set;} = new List<LeaveBalance>();

	// information
	public required int CompanyId {get; set;}
	public Company? Company {get; set;}
	public int? ManagerId {get; set;}
	public Employee? Manager {get; set;}
	public EmployeeStatus Status {get; set;} = EmployeeStatus.Active;
	public required DateTime HireDate {get; set;}
	public DateTime? OffboardDate {get; set;}

}