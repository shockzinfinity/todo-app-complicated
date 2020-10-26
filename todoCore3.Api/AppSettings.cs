namespace todoCore3.Api
{
  public class AppSettings
  {
    public string Secret { get; set; }
    public string EmailFrom { get; set; }
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; }
    public string SmtpPassword { get; set; }
    public string wcBaseApiUrl { get; set; }
    public string wooApiId { get; set; }
    public string wooApiPw { get; set; }
  }
}
