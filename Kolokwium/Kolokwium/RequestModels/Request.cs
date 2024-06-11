using System.ComponentModel.DataAnnotations;

namespace Kolokwium.RequestModels;

public class Request
{
    [Required]
    [MinLength(1)]
    public List<int> items { get; set; }
    
}