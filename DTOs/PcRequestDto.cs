namespace cw10.DTOs;

public class PcRequestDto
{
    public string Name { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}