using System;

namespace ParkingControl.Entities
{
  public record Parking
  {
    public int Id { get; init; }
    public string Plate { get; init; }
    public DateTimeOffset EntryTime { get; init; }
  }
}