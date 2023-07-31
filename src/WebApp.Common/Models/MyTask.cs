using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Common.Models
{
  public class MyTask : Entity
  {
    [Column("NAME")]
    public string? Name { get; set; }
    
    [Column("DESCRIPTION")]
    public string? Description { get; set; }

    [Column("TASK_TYPE", TypeName = "nvarchar(24)")]
    public TaskType? TaskType { get; set; }
  }

  public enum TaskType
  {
    Default,
    Demo,
    Impostant
  }
}
