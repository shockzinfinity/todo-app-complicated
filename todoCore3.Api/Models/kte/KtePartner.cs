using System;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models.kte
{
  public class KtePartner
  {
    public long Id { get; set; }

    public int WPUserId { get; set; }
    public string UserLogin { get; set; }
    public string UserNickName { get; set; }
    public string UserEmail { get; set; }
    public string PartnerName { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
