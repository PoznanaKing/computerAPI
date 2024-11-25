using computerWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;

namespace computerWebAPI.Controllers
{
    [Route("osystem")]
    [ApiController]
    public class OsystemController : ControllerBase
    {
        private readonly ComputerContext computerContext;
        public OsystemController (ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<Osystem>> Post(CreateOsDTO createOsDTO)
        {
            var os = new Osystem
            {
                Id = Guid.NewGuid(),
                Name= createOsDTO.name,
            };
            if (os != null)
            {
                await computerContext.Osystems.AddAsync(os);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, os);
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<ActionResult<Osystem>> Get()
        {
            return Ok(await computerContext.Osystems.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Osystem>> GetById(Guid id)
        {
            return Ok(await computerContext.Osystems.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Osystem>> Put(Guid id, CreateOsDTO createOsDTO)
        {
            var updatingOs = await computerContext.Osystems.FirstOrDefaultAsync(x => x.Id == id);
            if (updatingOs != null)
            {
                updatingOs.Name = createOsDTO.name;
                computerContext.Osystems.Update(updatingOs);
                await computerContext.SaveChangesAsync();
                return StatusCode(200,updatingOs);
            }

            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Osystem>> Delete(Guid id)
        {
            var deletingOs = await computerContext.Osystems.FirstOrDefaultAsync(x=>x.Id == id);
            if (deletingOs != null)
            {
                computerContext.Osystems.Remove(deletingOs);
                await computerContext.SaveChangesAsync();
                return StatusCode(200,deletingOs);
            }
            return NotFound();
        }
    }
}
