namespace cw10.DTOs;

public class ComponentInPcDto
{
    public int Amount { get; set; }
    public ComponentDto Component { get; set; } = new ComponentDto();
}