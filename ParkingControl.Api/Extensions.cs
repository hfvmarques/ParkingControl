using ParkingControl.Api.DTOs;
using ParkingControl.Api.Entities;

namespace ParkingControl.Api
{
  public static class Extensions
  {
    public static ParkingDTO AsDTO(this Parking parking)
    {
      return new ParkingDTO
      {
        Id = parking.Id,
        Plate = parking.Plate,
        EntryDate = parking.EntryDate,
        ExitDate = parking.ExitDate,
        Paid = parking.Paid,
        Left = parking.Left
      };
    }
  }
}