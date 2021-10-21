using System;

namespace ParkingControl.Api.Entities
{
  public class Parking
  {
    public int Id { get; set; }
    public string Plate { get; set; }
    public DateTimeOffset EntryDate { get; set; }
    public Nullable<DateTimeOffset> ExitDate { get; set; }
    public Boolean Paid { get; set; }
    public Boolean Left { get; set; }
  }
}