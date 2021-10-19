using System.ComponentModel.DataAnnotations;

namespace ParkingControl.DTOs
{
  public record CreateParkingDTO
  {
    [Required]
    // [DisplayFormat(DataFormatString = "{0:LLL-####}")]
    public string Plate { get; init; }
  }
}