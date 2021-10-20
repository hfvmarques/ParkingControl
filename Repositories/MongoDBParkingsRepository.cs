using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using ParkingControl.Entities;

namespace ParkingControl.Repositories
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

    public void CreateParking(Parking parking)
    {
      parkingsCollection.InsertOne(parking);
    }

    public Parking GetParking(int id)
    {
      var filter = filterBuilder.Eq(parking => parking.Id, id);
      return parkingsCollection.Find(filter).SingleOrDefault();
    }

    public IEnumerable<Parking> GetParkings()
    {
      return parkingsCollection.Find(new BsonDocument()).ToList();
    }

    public void UpdateParkingOut(Parking parking)
    {
      var filter = filterBuilder.Eq(existingParking => existingParking.Id, parking.Id);
      parkingsCollection.ReplaceOne(filter, parking);
    }
  }
}