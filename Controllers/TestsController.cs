using System;
using Well_Up_API.Services;
using Well_Up_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
namespace Well_Up_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class TestsController : ControllerBase
    {
		private readonly TestService _testService;

		public TestsController(TestService testService) => _testService = testService;

		[HttpGet]
		public  List<TestModel> Get() =>  _testService.GetAsync();


        [HttpGet("{id}")]
        public  async Task<ActionResult<TestModel>> Get(string id)
        {
            var book =  _testService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TestModel newTest)
        {
             _testService.CreateAsync(newTest);

            return CreatedAtAction(nameof(Get), new { id = newTest.Id }, newTest);
        }
        
        [HttpPut]
          public  async Task<IActionResult> Update( TestModel newModel){
             _testService.UpdateAsync(newModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (string id){
             _testService.DeleteAsync(id);
            return NoContent();
            
        }
        

    }
}

