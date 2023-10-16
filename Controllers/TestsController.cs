using System;
using Well_Up_API.Services;
using Well_Up_API.Models;
using Microsoft.AspNetCore.Mvc;
namespace Well_Up_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class TestsController : ControllerBase
    {
		private readonly TestService _testService;

		public TestsController(TestService testService) => _testService = testService;

		[HttpGet]
		public async Task<List<TestModel>> Get() => await _testService.GetAsync();


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TestModel>> Get(string id)
        {
            var book = await _testService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TestModel newTest)
        {
            await _testService.CreateAsync(newTest);

            return CreatedAtAction(nameof(Get), new { id = newTest.Id }, newTest);
        }

    }
}

