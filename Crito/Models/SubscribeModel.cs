using System.ComponentModel.DataAnnotations;

namespace Crito.Models;

public class SubscribeModel
{
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Display(Name = "Email Address")]
    public string Email { get; set; }
}
