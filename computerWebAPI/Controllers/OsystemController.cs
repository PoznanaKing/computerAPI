using computerWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<Osystem> Post(CreateOsDTO createOsDTO)
        {
            var os = new Osystem
            {
                Id = Guid.NewGuid(),
                Name= createOsDTO.name,
            };
            if (os != null)
            {
                computerContext.Osystems.Add(os);
                computerContext.SaveChanges();
                return StatusCode(201, os);
            }

            return BadRequest();
        }
    }
}
