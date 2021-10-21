using ParkingControl.Api.DTOs;
using ParkingControl.Api.Entities;

namespace ParkingControl.Api
{
  public static class Extensions
  {
    public static ParkingDTO AsDTO(this Parking parking)
    {
      return new ParkingDTO(
        parking.Id,
        parking.Plate,
        parking.EntryDate,
        parking.ExitDate,
        parking.Paid,
        parking.Left);
    }
  }
}