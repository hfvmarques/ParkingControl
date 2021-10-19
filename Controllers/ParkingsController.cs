using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ParkingControl.Entities;
using ParkingControl.Repositories;

namespace ParkingControl.Controllers
{
  [ApiController]
  [Route("parking")]
  public class ParkingsController : ControllerBase
  {
    private readonly InMemParkingsRepository repository;

    public ParkingsController()
    {
      repository = new InMemParkingsRepository();
    }

    // GET /parking
    [HttpGet]
    public IEnumerable<Parking> GetParkings()
    {
      var parkings = repository.GetParkings();
      return parkings;
    }

    // GET /parking/{id}
    [HttpGet("{id}")]
    public ActionResult<Parking> GetParking(int id)
    {
      var parking = repository.GetParking(id);

      if (parking is null)
      {
        return NotFound();
      }

      return parking;
    }
  }
}