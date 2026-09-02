namespace WorkforceApi.Dtos;

public class UpdateEmployee
{
	public required string FirstName {get; set;} 
	public required string LastName {get; set;} 
	public required string Email {get; set;} 
	//public int Role {get; set;} 
	public required string  Title {get; set;} 
	//public int ManagerId {get; set;} 
}