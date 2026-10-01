namespace WorkforceApi.Dtos;

public class CompanyResponse
{
	public int Id {get; set;}
	public required string Name {get; set;}
	public string? LegalName { get; set;}
	public string? TaxId {get; set;}
	public bool IsActive {get; set;} = true;

}