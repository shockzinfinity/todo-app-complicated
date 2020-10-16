using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todoCore3.Api.Models.Auth;
using todoCore3.Api.Models.Auth.Entities;
using todoCore3.Api.Services;

namespace todoCore3.Api.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [ApiController]
  public class AccountsController : BaseController
  {
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountsController(IAccountService accountService, IMapper mapper)
    {
      _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost("authenticate")]
    public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
      var response = _accountService.Authenticate(request, ipAddress());
      setTokenCookie(response.RefreshToken);

      return Ok(response);
    }

    [HttpPost("refresh-token")]
    public ActionResult<AuthenticateResponse> RefreshToken()
    {
      var refreshToken = Request.Cookies["refreshToken"];
      var response = _accountService.RefreshToken(refreshToken, ipAddress());
      setTokenCookie(response.RefreshToken);

      return Ok(refreshToken);
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
      _accountService.Register(request, Request.Headers["origin"]);

      return Ok(new { message = "Registration successful, check your email verification." });
    }

    [HttpPost("verify-email")]
    public IActionResult VerifyEmail(VerifyEmailRequest request)
    {
      _accountService.VerifyEmail(request.Token);

      return Ok(new { message = "Verification successful, you can now login." });
    }

    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword(ForgotPasswordRequest request)
    {
      _accountService.ForgotPassword(request, Request.Headers["origin"]);

      return Ok(new { message = "Please check your email for password reset instructions." });
    }

    [HttpPost("validate-reset-token")]
    public IActionResult ValidateResetToken(ValidateResetTokenRequest request)
    {
      _accountService.ValidateResetToken(request);

      return Ok(new { message = "Token is valid." });
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword(ResetPasswordRequest request)
    {
      _accountService.ResetPassword(request);

      return Ok(new { message = "Password reset successful, you can now login." });
    }

    [Authorize(Role.Admin)]
    [HttpGet]
    public ActionResult<IEnumerable<AccountResponse>> GetAll()
    {
      var accounts = _accountService.GetAll();

      return Ok(accounts);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public ActionResult<AccountResponse> GetBy(int id)
    {
      if (id != Account.Id && Account.Role != Role.Admin)
        return Unauthorized(new { message = "Unauthorized" });

      var account = _accountService.GetBy(id);

      return Ok(account);
    }

    [Authorize(Role.Admin)]
    [HttpPost]
    public ActionResult<AccountResponse> Create(CreateAccountRequest request)
    {
      var account = _accountService.Create(request);

      return Ok(account);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public ActionResult<AccountResponse> Update(int id, UpdateAccountRequest request)
    {
      if (id != Account.Id && Account.Role != Role.Admin)
        return Unauthorized(new { message = "Unauthorized" });

      if (Account.Role != Role.Admin)
        request.Role = null;

      var account = _accountService.Update(id, request);

      return Ok(account);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
      if (id != Account.Id && Account.Role != Role.Admin)
        return Unauthorized(new { message = "Unauthorized" });

      _accountService.Delete(id);

      return Ok(new { message = "Account is deleted." });
    }

    [Authorize]
    [HttpPost("revoke-token")]
    public IActionResult RevokeToken(RevokeTokenRequest request)
    {
      // accept toke from request body or cookie
      var token = request.Token ?? Request.Cookies["refreshToken"];

      if (string.IsNullOrEmpty(token))
        return BadRequest(new { message = "Token is required." });

      // users can revoke their own tokens and admins can revoke any tokens
      if (!Account.OwnsToken(token) && Account.Role != Role.Admin)
        return Unauthorized(new { message = "Unauthorized." });

      _accountService.RevokeToken(token, ipAddress());

      return Ok(new { message = "Token revoked." });
    }

    private void setTokenCookie(string token)
    {
      var cookieOptions = new CookieOptions
      {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddDays(7)
      };
      Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    private string ipAddress()
    {
      if (Request.Headers.ContainsKey("X-Forwarded-For"))
        return Request.Headers["X-Forwarded-For"];
      else
        return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
  }
}
