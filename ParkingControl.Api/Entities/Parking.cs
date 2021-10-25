using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingControl.Api.Entities
{
  public class Parking
  {
    public int Id { get; set; }
    public string Plate { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
    public DateTimeOffset EntryDate { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
    public Nullable<DateTimeOffset> ExitDate { get; set; }
    public Boolean Paid { get; set; }
    public Boolean Left { get; set; }
  }
}