using Microsoft.AspNetCore.Mvc.Formatters;

namespace WorkforceApi.Dtos;

public class EnumType
{
	public int Value {get; set;}
	public string Label {get; set;} = string.Empty;

}