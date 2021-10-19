using System.Collections.Generic;
using System.Linq;
using ParkingControl.Entities;

namespace ParkingControl.Repositories
{
  public class InMemParkingsRepository : IParkingsRepository
  {
    private readonly List<Parking> parkings = new()
    {
      new Parking
      {
        Id = 1,
        Plate = "AAA-1234",
        EntryTime = System.DateTimeOffset.UtcNow
      },
      new Parking
      {
        Id = 2,
        Plate = "BBB-1234",
        EntryTime = System.DateTimeOffset.UtcNow
      },
      new Parking
      {
        Id = 3,
        Plate = "CCC-1234",
        EntryTime = System.DateTimeOffset.UtcNow
      }
    };

    public IEnumerable<Parking> GetParkings()
    {
      return parkings;
    }

    public Parking GetParking(int id)
    {
      return parkings.Where(parking => parking.Id == id).SingleOrDefault();
    }
  }
}