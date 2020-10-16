using System;

namespace todoCore3.Api.Services
{
  public interface IEmailService
  {
    void Send(string to, string subject, string html, string from = null);
  }

  public class EmailService : IEmailService
  {
    public EmailService()
    {
    }

    public void Send(string to, string subject, string html, string from = null)
    {
      throw new NotImplementedException();
    }
  }
}
