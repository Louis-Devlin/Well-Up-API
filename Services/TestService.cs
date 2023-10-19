using System;
using Well_Up_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Well_Up_API.Services
{
	public class TestService
	{
		private readonly PostgresDbContext _context;

		public TestService(PostgresDbContext context)
		{
			_context = context;
		}

		public List<TestModel> GetAsync() =>
			  _context.Test.ToList();

		public  TestModel GetAsync(string id) =>  _context.Test.Find(id);

		public  void CreateAsync(TestModel testModel) { 
			 _context.AddAsync(testModel); 
			 _context.SaveChangesAsync();
		}

		public  void UpdateAsync (TestModel testModel) {
			var entity = _context.Test.FirstOrDefault(x => x.Id == testModel.Id);
			if(entity != null){
				entity.Name = testModel.Name;
				
			}
			_context.SaveChanges();
		}
		public  void DeleteAsync(string id){
			var rem = new TestModel{Id = id};
			 _context.Remove(rem);
			_context.SaveChanges();
		}
	}


}

