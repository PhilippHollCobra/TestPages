using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Common.Models
{
  public class Entity
  {
    [Column("ID"), Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("DATE_CREATE")]
    public DateTime? DateCreate { get; set; }

    [Column("CREATED_BY")]
    public string? CreatedBy { get; set; }

    public Entity() 
    {
      DateCreate = DateTime.UtcNow;
      CreatedBy = "Api";
    }
  }
}
