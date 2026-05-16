namespace cw10.Models;

public class ComponentManufacturer
{
    public int Id { get; set; }            
    public string Abbreviation { get; set; } 
    public string FullName { get; set; }     
    public DateTime FoundationDate { get; set; }
    
    
    public ICollection<Component> Components { get; set; } = new List<Component>();
}