using System;
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

    // POST /parkings
    [HttpPost]
    public ActionResult<ParkingDTO> CreateParking(CreateParkingDTO parkingDTO)
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

      repository.CreateParking(parking);

      return CreatedAtAction(nameof(GetParking), new { id = parking.Id }, parking.AsDTO());
    }

    // PUT /parking/{id}
    [HttpPut("{id}/out")]
    public ActionResult UpdateParkingOut(int id, UpdateParkingOutDTO parkingDTO)
    {
      var existingParking = repository.GetParking(id);

      if (existingParking is null)
      {
        return NotFound();
      }

      Parking updatedParking = existingParking with
      {
        Left = true
      };

      repository.UpdateParkingOut(updatedParking);

      return NoContent();
    }

    // PUT /parking/{id}
    [HttpPut("{id}/pay")]
    public ActionResult UpdateParkingPay(int id, UpdateParkingOutDTO parkingDTO)
    {
      var existingParking = repository.GetParking(id);

      if (existingParking is null)
      {
        return NotFound();
      }

      Parking updatedParking = existingParking with
      {
        ExitDate = DateTimeOffset.UtcNow,
        Paid = true
      };

      repository.UpdateParkingOut(updatedParking);

      return NoContent();
    }
  }
}