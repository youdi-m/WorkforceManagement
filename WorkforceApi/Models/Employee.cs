using Microsoft.Identity.Client;

namespace WorkforceApi.Models;

// defining Employee table
public class Employee
{
	public int Id {get; set;}
	public required string FirstName {get; set;}
	public required string LastName {get; set;}
	public required string Title {get; set;}
	public required string Email {get; set;}
	public required string PasswordHash {get; set;}
	public required int CompanyId {get; set;}
	public Company? Company {get; set;}
	public int? ManagerId {get; set;}
	public Employee? Manager {get; set;}
	public EmployeeStatus Status {get; set;} = EmployeeStatus.Active;
	public EmployeeRole Role {get; set;} = EmployeeRole.Employee;
	public required DateOnly DateOfBirth {get; set;}
	public required DateTime HireDate {get; set;}
	public DateTime? OffboardDate {get; set;}
	public required int Wage {get; set;}
	public ICollection<Shift> Shifts {get; set;} = new List<Shift>();
	public ICollection<LeaveRequest> LeaveRequests {get; set;} = new List<LeaveRequest>();

}