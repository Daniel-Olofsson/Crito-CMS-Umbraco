using System.ComponentModel.DataAnnotations;

namespace Crito.Models;

public class FormModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Message { get; set; }


    public string? RedirectUrl { get; set; } 
    public FormModel() 
    {
        Name = null!;
        Email = null!;
        Message = null!;
        RedirectUrl =  "/contact";
    }
}
