namespace PropertyListingApi.Models;

public class Property
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }

}