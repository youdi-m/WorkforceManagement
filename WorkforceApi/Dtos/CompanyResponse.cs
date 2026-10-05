namespace WorkforceApi.Dtos;

public class CompanyResponse
{
	public int Id {get; set;}
	public required string Name {get; set;}
	public required string LegalName { get; set;}
	public required string TaxId {get; set;}
	public bool IsActive {get; set;} = true;

}