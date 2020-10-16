using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models.Auth
{
  public class AuthenticateRequest
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
