using System;
using System.Text.Json.Serialization;

namespace todoCore3.Api.Models.Auth
{
  public class AuthenticateResponse
  {
    public int Id { get; set; }
    public string Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsVerified { get; set; }
    public string JwtToken { get; set; }

    [JsonIgnore] // refresh token 은 http only cookie 로만 전달
    public string RefreshToken { get; set; }
  }
}
