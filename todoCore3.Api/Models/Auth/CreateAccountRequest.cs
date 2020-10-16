using System;
using System.ComponentModel.DataAnnotations;
using todoCore3.Api.Models.Auth.Entities;

namespace todoCore3.Api.Models.Auth
{
  public class CreateAccountRequest
  {
    [Required]
    public string Gender { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EnumDataType(typeof(Role))]
    public string Role { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
  }
}
