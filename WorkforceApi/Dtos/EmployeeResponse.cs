namespace WorkforceApi.Dtos;

public class EmployeeResponse
{
	public int Id {get; set;}
	public string FirstName {get; set;} = string.Empty;
	public string LastName {get; set;} = string.Empty;
	public string Email {get; set;} = string.Empty;
	public int Role {get; set;} 
	public string Title {get; set;} = string.Empty;
	public int? ManagerId {get; set;} 
	public int Status {get; set;} 
	public TimeOnly ShiftStartTime {get; set;}
	public TimeOnly ShiftEndTime {get; set;}
	public DateOnly DateOfBirth {get; set;}

}