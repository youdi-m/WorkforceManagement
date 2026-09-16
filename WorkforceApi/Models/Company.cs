namespace WorkforceApi.Models;

public class Company
{
	public int Id {get; set;}
	public required string Name {get; set;}
	public required string Address {get; set;}
	public ICollection<Employee> Employees {get; set;} = new List<Employee>();
}