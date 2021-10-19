using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ParkingControl.DTOs;
using ParkingControl.Entities;
using ParkingControl.Repositories;

namespace ParkingControl.Controllers
{
  [ApiController]
  [Route("parking")]
  public class ParkingsController : ControllerBase
  {
    private readonly IParkingsRepository repository;

    public ParkingsController(IParkingsRepository repository)
    {
      this.repository = repository;
    }

    // GET /parking
    [HttpGet]
    public IEnumerable<ParkingDTO> GetParkings()
    {
      var parkings = repository.GetParkings().Select(parking => parking.AsDTO());
      return parkings;
    }

    // GET /parking/{id}
    [HttpGet("{id}")]
    public ActionResult<ParkingDTO> GetParking(int id)
    {
      var parking = repository.GetParking(id);

      if (parking is null)
      {
        return NotFound();
      }

      return parking.AsDTO();
    }
  }
}