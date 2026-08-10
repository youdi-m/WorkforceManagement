namespace WorkforceApi.Data;

using Microsoft.EntityFrameworkCore;
using WorkforceApi.Models;

// defining tables for DB
public class WorkforceContext : DbContext
{
	// context constructor
	public WorkforceContext(DbContextOptions<WorkforceContext> options) : base(options) {}

	// tables in DB
	public DbSet<Employee> Employees{get; set;}
	public DbSet<LeaveType> LeaveTypes{get; set;}
	public DbSet<LeaveBalance> LeaveBalances{get; set;}
	public DbSet<LeaveRequest> LeaveRequests{get; set;}

}