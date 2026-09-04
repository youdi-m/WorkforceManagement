namespace WorkforceApi.Dtos;

public class UpdateEmployee
{
	public string? FirstName {get; set;}
	public string? LastName {get; set;} 
	public string? Email {get; set;} 
	public int Role {get; set;} 
	public string? Title {get; set;} 
	public int ManagerId {get; set;} 
	public int Status {get; set;} 
}