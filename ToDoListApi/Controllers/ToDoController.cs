using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ToDoListApi.Dto;
using ToDoListApi.Entities;
using ToDoListApi.Services.Abstracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;

        public ToDoController(IToDoService service)
        {
            _service = service;
        }

        // GET: api/<ToDoController>
        [HttpGet]
        public async Task<IEnumerable<ToDoDto>> Get()
        {
            var list = await _service.GetAllAsync();
            var items = list.Select(p =>
            {
                return new ToDoDto
                {
                    Title = p.Title,
                    Description = p.Description,
                    IsCompleted = p.IsCompleted

                };
            });
            return items;
        }

        // GET api/<ToDoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var item = await _service.GetAsync(p=>p.Id==id);
            if (item != null)
            {
                var items = new ToDoDto
                {
                    Title = item.Title,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted

                };

                return Ok(items);
            }
            return BadRequest();
        }

        // POST api/<ToDoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToDoDto value)
        {
            if (value.Title != null && value.Description != null)
            {
                var item = new ToDo
                {
                    Title = value.Title,
                    Description = value.Description,
                    IsCompleted = value.IsCompleted
                };
                await _service.AddAsync(item);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT api/<ToDoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ToDoDto value)
        {
         var item= await  _service.GetAsync(p => p.Id == id);
            if (item != null)
            {
                item.IsCompleted = value.IsCompleted;
                item.Title = value.Title;
                item.Description = value.Description;
                await _service.UpdateAsync(item); 
                return Ok(item);
            }
            return BadRequest();

        }

        // DELETE api/<ToDoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetAsync(p => p.Id == id);
            if(item != null)
            {
                await _service.DeleteAsync(item);
                return Ok(item);
            }
            return NotFound();  
        }
    }
}
