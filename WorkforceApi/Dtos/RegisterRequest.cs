namespace WorkforceApi.Dtos;

public class RegisterRequest {
	// employee info
	public required string FirstName {get; set;}
	public required string LastName {get; set;}
	public required string Email {get; set;}
	public required string Password {get; set;}
	public required decimal Wage {get; set;}

	// company info
	public required string Name {get; set;}
	public required string LegalName { get; set;}
	public required string TaxId { get; set;}
	public string? TimeZone {get; set;}
	public string? Currency {get; set;}

	// company address info
	public required string StreetLine1 {get; set;}
	public string? StreetLine2 {get; set;}
	public required string Country {get; set;}
	public required string Province {get; set;}
	public required string City {get; set;}
	public required string PostalCode {get; set;}
}