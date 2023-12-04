using Microsoft.AspNetCore.Mvc;
using WebApp.Common.Interfaces;
using WebApp.Common.Models;

namespace WebApp.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AddressController : ControllerBase
  {
    private readonly IDataAccessService _dataAccessService;

    public AddressController(IDataAccessService dataAccessService)
    {
      _dataAccessService = dataAccessService;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllAsync()
    {
      List<Address>? addresses = await _dataAccessService.GetObjectsAsync<Address>();
      return Ok(addresses);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
      Address? address = await _dataAccessService.GetObjectAsync<Address>(id);
      return Ok(address);
    }

    [HttpPost]
    [Route("{firstName}/{lastName}")]
    public async Task<IActionResult> CreateAddressAsync(string firstName, string lastName)
    {
      Address? createdAddress = await _dataAccessService.CreateObjectAsync(new Address
      {
        FirstName = firstName,
        LastName = lastName
      });

      return Ok(createdAddress);
    }

    [HttpPut]
    [Route("{id}/{firstName}/{lastName}")]
    public async Task<IActionResult> UpdateAddressAsync(Guid id, string firstName, string lastName)
    {
      Address? updatedAddress = await _dataAccessService.UpdateObjectAsync(new Address
      {
        Id = id,
        FirstName = firstName,
        LastName = lastName
      });

      return Ok(updatedAddress);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAddressAsync(Guid id)
    {
      bool result = await _dataAccessService.DeleteObjectAsync<Address>(id);
      return Ok(result);
    }
  }
}
