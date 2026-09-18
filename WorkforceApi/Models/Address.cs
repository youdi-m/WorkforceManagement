namespace WorkforceApi.Models;

public class Address
{
	public int Id {get; set;}
	public required int CompanyId {get; set;}
	public Company? Company {get; set;}
	public required string StreetLine1 {get; set;}
	public string? StreetLine2 {get; set;}
	public required string Country {get; set;}
	public required string Province {get; set;}
	public required string City {get; set;}
	public required string PostalCode {get; set;}

	public bool IsPrimary {get; set;}
	public bool IsCurrent {get; set;}
	public DateOnly EffectiveFrom {get; set;}
	public DateOnly? EffectiveTo {get; set;}

}