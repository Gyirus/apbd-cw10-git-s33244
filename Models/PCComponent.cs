namespace cw10.Models;

public class PCComponent
{
    public int PCId { get; set; }         
    public string ComponentCode { get; set; } 
    public int Amount { get; set; }          
    
    public PC PC { get; set; } = null!;
    public Component Component { get; set; } = null!;
}