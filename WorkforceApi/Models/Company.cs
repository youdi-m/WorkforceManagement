namespace WorkforceApi.Models;

public class Company
{
	public int Id {get; set;}
	public required string Name {get; set;}
	public string? LegalName { get; set;}
	public string? TaxId {get; set;}
	public string? Phone {get; set;}
	public string? Email {get; set;}
	public string? Website {get; set;}
	public string? LogoUrl {get; set;}

	// operational
	public string? TimeZone {get; set;}
	public string? Currency {get; set;}
	public string? PayFrequency {get; set;}
	public int? FiscalYearStartMonth {get; set;}

	// audit
	public bool IsActive {get; set;} = true;
	public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
	public DateTime? UpdatedAt {get; set;}

	public ICollection<Employee> Employees {get; set;} = new List<Employee>();
	public ICollection<Address> Addresses {get; set;} = new List<Address>();

}