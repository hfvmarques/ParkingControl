using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingControl.Api.Entities;

namespace ParkingControl.Api.Repositories
{
  public interface IParkingsRepository
  {
    Task<Parking> GetParkingAsync(int id);
    Task<IEnumerable<Parking>> GetParkingsAsync();
    IEnumerable<Parking> GetParkings();
    Task CreateParkingAsync(Parking parking);
    Task UpdateParkingOutAsync(Parking parking);
    Task UpdateParkingPayAsync(Parking parking);
    Task DeleteParkingAsync(int id);
  }
}
