using System;

namespace ParkingControl.DTOs
{
  public record ParkingDTO
  {
    public int Id { get; init; }
    public string Plate { get; init; }
    public DateTimeOffset EntryTime { get; init; }
  }
}