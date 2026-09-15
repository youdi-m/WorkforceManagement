namespace WorkforceApi.Models;

// defining LeaveType table
public class LeaveType
{
	public int Id {get; set;}
	public required string Name {get; set;}
	public int? MaxDaysPerYear { get; set; }
	public bool RequiresApproval { get; set; }
	public bool IsPaid { get; set; }

}