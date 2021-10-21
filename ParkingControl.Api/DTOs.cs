using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingControl.Api.DTOs
{
  public record ParkingDTO(
    int Id,
    string Plate,
    DateTimeOffset EntryDate,
    Nullable<DateTimeOffset> ExitDate,
    Boolean Paid,
    Boolean Left);
  public record CreateParkingDTO([Required] string Plate);
  public record UpdateParkingOutDTO();
  public record UpdateParkingPayDTO();

}