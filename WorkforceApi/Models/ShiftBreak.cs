namespace WorkforceApi.Models;

public class ShiftBreak
{
	public required int Id {get; set;}
	public required int ShiftId {get; set;}
	public Shift? Shift {get; set;}
	public required DateTime StartTime {get; set;}
	public required DateTime EndTime {get; set;}
	public required bool IsPaid {get; set;}
	public required bool IsRequired {get; set;}
}