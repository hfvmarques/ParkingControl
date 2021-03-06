using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ParkingControl.Api.DTOs;
using ParkingControl.Api.Entities;
using ParkingControl.Api.Repositories;

namespace ParkingControl.Api.Controllers
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
    public async Task<IEnumerable<ParkingDTO>> GetParkingsAsync(string plate = null)
    {
      var parkings = (await repository.GetParkingsAsync()).Select(parking => parking.AsDTO());

      if (!string.IsNullOrWhiteSpace(plate))
      {
        parkings = parkings.Where(parking => parking.Plate.Equals(plate, StringComparison.OrdinalIgnoreCase));
      }

      return parkings;
    }

    public IEnumerable<ParkingDTO> GetParkings()
    {
      var parkings = repository.GetParkings().Select(parking => parking.AsDTO());
      return parkings;
    }

    // GET /parking/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ParkingDTO>> GetParkingAsync(ObjectId id)
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
      Parking parking = new()
      {
        Id = new ObjectId(),
        Plate = parkingDTO.Plate.ToUpper(),
        EntryDate = DateTimeOffset.UtcNow,
        ExitDate = null,
        Paid = false,
        Left = false
      };

      await repository.CreateParkingAsync(parking);

      return CreatedAtAction(nameof(GetParkingAsync), new { id = parking.Id }, parking.AsDTO());
    }

    // PUT /parking/{id}
    [HttpPut("{id}/pay")]
    public async Task<ActionResult> UpdateParkingPayAsync(ObjectId id, UpdateParkingPayDTO parkingDTO)
    {
      var existingParking = await repository.GetParkingAsync(id);

      if (existingParking is null)
      {
        return NotFound();
      }

      existingParking.ExitDate = DateTimeOffset.UtcNow;
      existingParking.Paid = true;

      await repository.UpdateParkingOutAsync(existingParking);

      return NoContent();
    }

    // PUT /parking/{id}
    [HttpPut("{id}/out")]
    public async Task<ActionResult> UpdateParkingOutAsync(ObjectId id, UpdateParkingOutDTO parkingDTO)
    {
      var existingParking = await repository.GetParkingAsync(id);

      if (existingParking is null)
      {
        return NotFound();
      }

      if (existingParking.Paid == false)
      {
        return NoContent();
      }

      existingParking.Left = true;

      await repository.UpdateParkingOutAsync(existingParking);

      return NoContent();
    }

    // DELETE /parking/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteParkingAsync(ObjectId id)
    {
      var existingParking = await repository.GetParkingAsync(id);

      if (existingParking is null)
      {
        return NotFound();
      }

      await repository.DeleteParkingAsync(id);

      return NoContent();
    }
  }
}