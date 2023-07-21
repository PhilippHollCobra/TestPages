using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Common.Models
{
  public class Address : Entity
  {
    [Column("FIRST_NAME")]
    public string? FirstName { get; set; }

    [Column("LAST_NAME")]
    public string? LastName { get; set; }
  }
}
