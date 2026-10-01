namespace WorkforceApi.Dtos;

public class AddressResponse
{
	public int Id {get; set;}
	public required string StreetLine1 {get; set;}
	public required string Country {get; set;}
	public required string Province {get; set;}
	public required string City {get; set;}
	public required string PostalCode {get; set;}
}