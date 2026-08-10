namespace WorkforceApi.Models;

// defining Employee table
public class Employee
{
	public int Id {get; set;}
	public required string FirstName {get; set;}
	public required string LastName {get; set;}
	public required string Title {get; set;}
	public int? ManagerId {get; set;}
	public Employee? Manager {get; set;}
	
}