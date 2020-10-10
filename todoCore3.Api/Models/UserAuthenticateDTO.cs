using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class UserAuthenticateDTO
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
