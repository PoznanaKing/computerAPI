using computerWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace computerWebAPI.Controllers
{
    [Route("comp")]
    [ApiController]
    public class CompController : ControllerBase
    {
        private readonly ComputerContext computerContext;
        public CompController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }
        [HttpPost]
        public async Task<ActionResult<Comp>> Post(CreateCompDTO createCompDTO)
        {
            var Computer = new Comp
            {
                Id = Guid.NewGuid(),
                Brand = createCompDTO.Brand,
                Type = createCompDTO.Type,
                Display = createCompDTO.Display,
                Memory = createCompDTO.Memory,
                CreatedTime = DateTime.Now,
                OsId = createCompDTO.OsId,
            };
            if (Computer != null)
            {
                await computerContext.Comps.AddAsync(Computer);
                await computerContext.SaveChangesAsync();
                return Ok(Computer);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<ActionResult<Comp>> Get()
        {
            return Ok(await computerContext.Comps.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Comp>> GetById(Guid id)
        {
            return Ok(await computerContext.Comps.FirstOrDefaultAsync(c => c.Id == id));
        }
    }
}
