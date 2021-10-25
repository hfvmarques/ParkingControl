using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingControl.Api.DTOs
{
  public record ParkingDTO(
    int Id,
    string Plate,
    [RegularExpression(@"([1-9]|([012][0-9])|(3[01]))/([0]{0,1}[1-9]|1[012])/\d\d\d\d 
    (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d")]
    DateTimeOffset EntryDate,
    [RegularExpression(@"([1-9]|([012][0-9])|(3[01]))/([0]{0,1}[1-9]|1[012])/\d\d\d\d 
    (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d")]
    Nullable<DateTimeOffset> ExitDate,
    Boolean Paid,
    Boolean Left);
  public record CreateParkingDTO(
    [Required]
    [RegularExpression(@"[A-Za-z]{3}-[0-9]{4}", ErrorMessage = "Não é um formato válido de placa")]
    string Plate);
  public record UpdateParkingOutDTO();
  public record UpdateParkingPayDTO();
}