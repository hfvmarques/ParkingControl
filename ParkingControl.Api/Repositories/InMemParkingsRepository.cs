// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using ParkingControl.Api.Entities;

// namespace ParkingControl.Api.Repositories
// {
//   public class InMemParkingsRepository : IParkingsRepository
//   {
//     private readonly List<Parking> parkings = new()
//     {
//       new Parking
//       {
//         Id = 1,
//         Plate = "AAA-1234",
//         EntryDate = System.DateTimeOffset.UtcNow
//       },
//       new Parking
//       {
//         Id = 2,
//         Plate = "BBB-1234",
//         EntryDate = System.DateTimeOffset.UtcNow
//       },
//       new Parking
//       {
//         Id = 3,
//         Plate = "CCC-1234",
//         EntryDate = System.DateTimeOffset.UtcNow
//       }
//     };

//     public async Task<IEnumerable<Parking>> GetParkingsAsync()
//     {
//       return await Task.FromResult(parkings);
//     }

//     public IEnumerable<Parking> GetParkings()
//     {
//       return await Task.FromResult(parkings);
//     }

//     public async Task<Parking> GetParkingAsync(int id)
//     {
//       var parking = parkings.Where(parking => parking.Id == id).SingleOrDefault();
//       return await Task.FromResult(parking);
//     }

//     public async Task CreateParkingAsync(Parking parking)
//     {
//       parkings.Add(parking);
//       await Task.CompletedTask;
//     }

//     public async Task UpdateParkingOutAsync(Parking parking)
//     {
//       var index = parkings.FindIndex(existingParking => existingParking.Id == parking.Id);
//       parkings[index] = parking;
//       await Task.CompletedTask;
//     }

//     public async Task UpdateParkingPayAsync(Parking parking)
//     {
//       var index = parkings.FindIndex(existingParking => existingParking.Id == parking.Id);
//       parkings[index] = parking;
//       await Task.CompletedTask;
//     }
//   }
// }