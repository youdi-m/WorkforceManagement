namespace WorkforceApi.Dtos;

public class CompanyResponseDTO
{
	public int Id {get; set;}
	public required string Name {get; set;}
	public required string LegalName { get; set;}
	public required string TaxId {get; set;}
	public bool IsActive {get; set;} = true;

}