using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApp.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TasksController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      List<MyTask> list = new List<MyTask>
      {
        new MyTask
        {
          Id = 1,
          Name = "Test",
          Description = "Test"
        },
        new MyTask
        {
          Id = 2,
          Name = "Test1",
          Description = "Test1",
          TaskType = TaskType.Demo
        }
      };

      return Ok(list);
    }

    [HttpPost]
    public IActionResult Post([FromBody] List<MyTask> tasks)
    {
      return Ok(tasks);
    }
  }

    public class MyTask
    {
      public int? Id { get; set; }
      public string? Name { get; set; }
      public string? Description { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public TaskType? TaskType { get; set; }
    }

    public enum TaskType
    {
      Default,
      Demo,
      Impostant
    }
  }
