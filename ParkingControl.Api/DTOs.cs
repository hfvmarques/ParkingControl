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
  public record CreateParkingDTO(
    [Required]
    [RegularExpression(@"[A-Za-z]{3}-[0-9]{4}", ErrorMessage = "Não é um formato válido de placa")]
    string Plate);
  public record UpdateParkingOutDTO();
  public record UpdateParkingPayDTO();
}