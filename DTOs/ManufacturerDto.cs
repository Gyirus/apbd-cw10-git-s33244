namespace cw10.DTOs;

public class ManufacturerDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; }
}