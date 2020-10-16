using System;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models.Auth
{
  public class ValidateResetTokenRequest
  {
    [Required]
    public string Token { get; set; }
  }
}
