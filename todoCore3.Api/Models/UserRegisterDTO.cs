using System;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class UserRegisterDTO
  {
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
  }
}
