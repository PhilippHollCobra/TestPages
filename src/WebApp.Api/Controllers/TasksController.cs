using Microsoft.AspNetCore.Mvc;
using WebApp.Common.Interfaces;
using WebApp.Common.Models;

namespace WebApp.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TasksController : ControllerBase
  {
    private readonly IDataAccessService _dataAccessService;

    public TasksController(IDataAccessService dataAccessService)
    {
      _dataAccessService = dataAccessService;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllAsync()
    {
      List<MyTask>? tasks = await _dataAccessService.GetObjectsAsync<MyTask>();
      return Ok(tasks);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
      MyTask? task = await _dataAccessService.GetObjectAsync<MyTask>(id);
      return Ok(task);
    }

    [HttpPost]
    [Route("{name}")]
    public async Task<IActionResult> CreateTaskAsync(string name)
    {
      MyTask? createdTask = await _dataAccessService.CreateObjectAsync(new MyTask
      {
        Name = name,
      });

      return Ok(createdTask);
    }

    [HttpPut]
    [Route("{id}/{name}")]
    public async Task<IActionResult> UpdateTaskAsync(Guid id, string name)
    {
      MyTask? updatedTask = await _dataAccessService.UpdateObjectAsync(new MyTask
      {
        Id = id,
        Name = name
      });

      return Ok(updatedTask);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteTaskAsync(Guid id)
    {
      bool result = await _dataAccessService.DeleteObjectAsync<MyTask>(id);
      return Ok(result);
    }
  }
}
