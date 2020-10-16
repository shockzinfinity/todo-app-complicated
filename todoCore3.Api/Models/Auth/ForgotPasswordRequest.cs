using System;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models.Auth
{
  public class ForgotPasswordRequest
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}
