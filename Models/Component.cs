namespace cw10.Models;

public class Component
{
    public string Code { get; set; }     
    public string Name { get; set; }      
    public string Description { get; set; }
    
    public int ComponentManufacturersId { get; set; }  
    public int ComponentTypesId { get; set; } 
    
    public ComponentManufacturer Manufacturer { get; set; } = null!;
    public ComponentType Type { get; set; } = null!;
    
    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}