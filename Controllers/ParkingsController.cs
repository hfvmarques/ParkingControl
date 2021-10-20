using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public async Task<IEnumerable<ParkingDTO>> GetParkingsAsync()
    {
      var parkings = (await repository.GetParkingsAsync()).Select(parking => parking.AsDTO());
      return parkings;
    }

    public IEnumerable<ParkingDTO> GetParkings()
    {
      var parkings = repository.GetParkings().Select(parking => parking.AsDTO());
      return parkings;
    }

    // GET /parking/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ParkingDTO>> GetParkingAsync(int id)
    {
      var parking = await repository.GetParkingAsync(id);

      if (parking is null)
      {
        return NotFound();
      }

      return parking.AsDTO();
    }

    // POST /parkings
    [HttpPost]
    public async Task<ActionResult<ParkingDTO>> CreateParkingAsync(CreateParkingDTO parkingDTO)
    {
      int lastId;
      if (GetParkings().LastOrDefault() is null)
      {
        lastId = 0;
      }
      else
      {
        lastId = GetParkings().Last().Id;
      }

      Parking parking = new()
      {
        Id = lastId + 1,
        Plate = parkingDTO.Plate,
        EntryDate = DateTimeOffset.UtcNow,
        ExitDate = null,
        Paid = false,
        Left = false
      };

      await repository.CreateParkingAsync(parking);

      return CreatedAtAction(nameof(GetParkingAsync), new { id = parking.Id }, parking.AsDTO());
    }

    // PUT /parking/{id}
    [HttpPut("{id}/out")]
    public async Task<ActionResult> UpdateParkingOutAsync(int id, UpdateParkingOutDTO parkingDTO)
    {
      var existingParking = await repository.GetParkingAsync(id);

      if (existingParking is null)
      {
        return NotFound();
      }

      Parking updatedParking = existingParking with
      {
        Left = true
      };

      await repository.UpdateParkingOutAsync(updatedParking);

      return NoContent();
    }

    // PUT /parking/{id}
    [HttpPut("{id}/pay")]
    public async Task<ActionResult> UpdateParkingPayAsync(int id, UpdateParkingOutDTO parkingDTO)
    {
      var existingParking = await repository.GetParkingAsync(id);

      if (existingParking is null)
      {
        return NotFound();
      }

      Parking updatedParking = existingParking with
      {
        ExitDate = DateTimeOffset.UtcNow,
        Paid = true
      };

      await repository.UpdateParkingOutAsync(updatedParking);

      return NoContent();
    }
  }
}