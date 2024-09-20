using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextConverterApp.Models;

public class ConversionModel
{
    public Guid Id { get; set; }
   
    [Required]
    public string? Before { get; set; }
    
    public string? After { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // pgSQL enforces UTC
        
    public UserModel? UserModel { get; set; }
}