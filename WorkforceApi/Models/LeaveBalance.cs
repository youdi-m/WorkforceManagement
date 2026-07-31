namespace WorkforceApi.Models;

public class LeaveBalance
{
	public int Id {get; set;}
	public int EmployeeId {get; set;}
	public Employee? Employee {get; set;}
	public int LeaveTypeId {get; set;}
	public LeaveType? LeaveType {get; set;}
	public int Year {get; set;}
	public int DaysAllotted {get; set;}
	public int DaysUsed {get; set;}
	
}