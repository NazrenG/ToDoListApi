using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApiFormatter.Dto;
using WebApiFormatter.Entities;
using WebApiFormatter.Services.Abstracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: api/<ToDoController>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            var list = await _service.GetAllAsync();
            var items = list.Select(p =>
            {
                return new UserDto
                {
                    Id = p.Id,
                    Fullname = p.Fullname,
                    SeriaNo = p.SeriaNo,
                    Age = p.Age,
                    Score = p.Score,

                };
            });
            return items;
        }

        // GET api/<ToDoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var item = await _service.GetAsync(p => p.Id == id);
            if (item != null)
            {
                var items = new UserDto
                {
                    Id = item.Id,
                    Fullname = item.Fullname,
                    SeriaNo = item.SeriaNo,
                    Age = item.Age,
                    Score = item.Score,

                };

                return Ok(items);
            }
            return BadRequest();
        }

        // POST api/<ToDoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserAddDto value)
        {
            if (value.Fullname != null && value.SeriaNo != null)
            {
                var item = new User
                { 
                    Fullname = value.Fullname,
                    SeriaNo = value.SeriaNo,
                    Age = value.Age,
                    Score = value.Score,
                };
                await _service.AddAsync(item);
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/<ToDoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto value)
        {
            var item = await _service.GetAsync(p => p.Id == id);
            if (item != null)
            {
                item.Id = id;
                item.Fullname = value.Fullname;
                item.SeriaNo = value.SeriaNo;
                item.Age = value.Age;
                item.Score = value.Score;
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
            if (item != null)
            {
                await _service.DeleteAsync(item);
                return Ok(item);
            }
            return NotFound();
        }
    }
}
