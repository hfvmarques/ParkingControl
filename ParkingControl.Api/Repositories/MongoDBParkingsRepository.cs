using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ParkingControl.Api.Entities;

namespace ParkingControl.Api.Repositories
{
  public class MongoDBParkingsRepository : IParkingsRepository
  {
    private const string databaseName = "parkingControl";
    private readonly string collectionName = "parkings";
    private readonly IMongoCollection<Parking> parkingsCollection;
    private readonly FilterDefinitionBuilder<Parking> filterBuilder = Builders<Parking>.Filter;

    public MongoDBParkingsRepository(IMongoClient mongoClient)
    {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      parkingsCollection = database.GetCollection<Parking>(collectionName);
    }

    public async Task CreateParkingAsync(Parking parking)
    {
      await parkingsCollection.InsertOneAsync(parking);
    }

    public async Task<Parking> GetParkingAsync(int id)
    {
      var filter = filterBuilder.Eq(parking => parking.Id, id);
      return await parkingsCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Parking>> GetParkingsAsync()
    {
      return await parkingsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public IEnumerable<Parking> GetParkings()
    {
      return parkingsCollection.Find(new BsonDocument()).ToList();
    }

    public async Task UpdateParkingOutAsync(Parking parking)
    {
      var filter = filterBuilder.Eq(existingParking => existingParking.Id, parking.Id);
      await parkingsCollection.ReplaceOneAsync(filter, parking);
    }

    public async Task UpdateParkingPayAsync(Parking parking)
    {
      var filter = filterBuilder.Eq(existingParking => existingParking.Id, parking.Id);
      await parkingsCollection.ReplaceOneAsync(filter, parking);
    }
  }
}