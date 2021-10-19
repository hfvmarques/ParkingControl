using System;

namespace ParkingControl.Entities
{
  public record Parking
  {
    public int Id { get; init; }
    public string Plate { get; init; }
    public DateTimeOffset EntryDate { get; init; }
    public Nullable<DateTimeOffset> ExitDate { get; init; }
    public Boolean Paid { get; init; }
    public Boolean Left { get; init; }
  }
}