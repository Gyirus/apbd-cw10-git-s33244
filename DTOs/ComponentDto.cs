namespace cw10.DTOs;

public class ComponentDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ManufacturerDto Manufacturer { get; set; } = new ManufacturerDto();
    public TypeDto Type { get; set; } = new TypeDto();
}