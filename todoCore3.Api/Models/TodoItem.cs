using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class TodoItem
  {
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [DefaultValue(false)]
    public bool IsCompleted { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
