using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenterWeb.Models.Database;

namespace ServiceCenterWeb.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Админ, Клиент, Мастер")]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ClientsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get() => await _db.Clients.ToListAsync();

        // GET api/clients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _db.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            return new ObjectResult(client);
        }

        // POST api/clients
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Client client)
        {
            if (client == null) {
                return BadRequest();
            } // if

            await _db.Clients.AddAsync(client);
            await _db.SaveChangesAsync();

            return Ok(client);
        }

        // PUT api/clients/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Client client)
        {
            if (client == null) {
                return BadRequest();
            } // if

            if (!await _db.Clients.AnyAsync(x => x.Id == client.Id)) {
                return NotFound();
            } // if

            _db.Update(client);
            await _db.SaveChangesAsync();

            return Ok(client);
        }

        // DELETE api/clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _db.Clients.FindAsync(id);

            if (client == null) {
                return NotFound();
            } // if

            _db.Clients.Remove(client);
            await _db.SaveChangesAsync();

            return Ok(client);
        }
    }
}