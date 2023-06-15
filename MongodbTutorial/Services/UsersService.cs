using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongodbTutorial.Models;

namespace MongodbTutorial.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UsersService(IOptions<UserDatabaseSettings> userDatabaseSettings)
    {
        var mongoClient = new MongoClient(userDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(userDatabaseSettings.Value.DatabaseName);
        _usersCollection = mongoDatabase.GetCollection<User>(userDatabaseSettings.Value.UserCollectionName);
    }
    // public UsersService(IMongoCollection<User> usersCollection)
    // {
    //     _usersCollection = usersCollection.
    // }

    //getAll
    public async Task<IEnumerable<User>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    //getById
    public async Task<User?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    //filterUser
    public async Task<IEnumerable<User>> FilterUsers(string firstName, string lastName, Gender gender, Role role)
    {
        var filterBuilder = Builders<User>.Filter;
        var filters = new List<FilterDefinition<User>>();

        if (!string.IsNullOrEmpty(firstName))
        {
            var lowerFirstName = firstName.ToLowerInvariant(); // Convert firstName to lowercase
            filters.Add(filterBuilder.Regex(u => u.FirstName, new BsonRegularExpression(lowerFirstName, "i")));
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            var lowerLastName = lastName.ToLowerInvariant(); // Convert lastName to lowercase
            filters.Add(filterBuilder.Regex(u => u.LastName, new BsonRegularExpression(lowerLastName, "i")));
        }

        if (gender != Gender.All)
        {
            filters.Add(filterBuilder.Eq(u => u.Gender, gender));
        }

        if (role != Role.All)
        {
            filters.Add(filterBuilder.Eq(u => u.Role, role));
        }

        FilterDefinition<User> filter = filterBuilder.Empty;
        if (filters.Count > 0)
        {
            filter = filterBuilder.And(filters);
        }

        return await _usersCollection.Find(filter).ToListAsync();
    }
    
    //create
    public async Task CreateAsync(User newUser) =>
        await _usersCollection.InsertOneAsync(newUser);

    //update
    public async Task UpdateAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    //delete
    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);

     // //getUserByFirstName
     // public async Task<IEnumerable<User>> GetUsersByFirstName(string firstName) =>
     //     await _usersCollection.Find(x => x.FirstName.ToLower() == firstName.ToLower()).ToListAsync();
     //
     // //getUserByLastName
     // public async Task<IEnumerable<User>> GetUsersByLastName(string lastName) =>
     //     await _usersCollection.Find(x => x.LastName.ToLower() == lastName.ToLower()).ToListAsync();
     //
     // //getUserByGender
     // public async Task<IEnumerable<User>> GetUsersByGender(Gender gender) =>
     //     await _usersCollection.Find(x => x.Gender == gender).ToListAsync();
     //
     // //getUserByRole
     // public async Task<IEnumerable<User>> GetUsersByRole(Role role) =>
     //     await _usersCollection.Find(x => x.Role == role).ToListAsync();
     
}