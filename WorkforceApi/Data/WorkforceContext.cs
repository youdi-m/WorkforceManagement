namespace WorkforceApi.Data;

using Microsoft.EntityFrameworkCore;
using WorkforceApi.Models;

public class WorkforceContext : DbContext
{
	public WorkforceContext(DbContextOptions<WorkforceContext> options) : base(options) {}

	public DbSet<Employee> Employees{get; set;}
	public DbSet<LeaveType> LeaveTypes{get; set;}
	public DbSet<LeaveBalance> LeaveBalances{get; set;}
	public DbSet<LeaveRequest> LeaveRequests{get; set;}

}