namespace WorkforceApi.Models;

// defining Shift table
public class Shift
{
	public int Id {get; set;}
	public DateTime StartTime {get; set;}
	public DateTime EndTime {get; set;}
	public DateTime? OvertimeStart {get; set;}
	public DateTime? OvertimeEnd {get; set;}
	public int EmployeeId {get; set;}
	public Employee? Employee {get; set;}
	public bool IsCovered {get; set;}
	public ICollection<ShiftBreak> Breaks {get; set;} = new List<ShiftBreak>();
}