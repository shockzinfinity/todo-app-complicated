using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class TodoItem
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsCompleted { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
