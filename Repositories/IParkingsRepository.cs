using System.Collections.Generic;
using ParkingControl.Entities;

namespace ParkingControl.Repositories
{
  public interface IParkingsRepository
  {
    Parking GetParking(int id);
    IEnumerable<Parking> GetParkings();

    void CreateParking(Parking parking);
    void UpdateParkingOut(Parking parking);
  }
}
