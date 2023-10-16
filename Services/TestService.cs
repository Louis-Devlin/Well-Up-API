using System;
using Well_Up_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace Well_Up_API.Services
{
	public class TestService
	{
		private readonly IMongoCollection<TestModel> _testsCollection;

		public TestService(IOptions<TestModelDatabaseSettings> testModelDatabaseSettings)
		{
			var mongoClient = new MongoClient(testModelDatabaseSettings.Value.ConnectionString);

			var mongoDatabase = mongoClient.GetDatabase(testModelDatabaseSettings.Value.DatabaseName);

			_testsCollection = mongoDatabase.GetCollection<TestModel>(testModelDatabaseSettings.Value.CollectionName);
		}

		public async Task<List<TestModel>> GetAsync() =>
			await _testsCollection.Find(_ => true).ToListAsync();

		public async Task<TestModel?> GetAsync(string id) => await _testsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

		public async Task CreateAsync(TestModel testModel) => await _testsCollection.InsertOneAsync(testModel);
	}


}

