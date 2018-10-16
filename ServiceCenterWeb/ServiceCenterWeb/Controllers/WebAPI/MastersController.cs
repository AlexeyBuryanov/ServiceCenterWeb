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
    public class MastersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MastersController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Master>> Get() => await _db.Masters.ToListAsync();

        // GET api/masters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var master = await _db.Masters.FindAsync(id);

            if (master == null)
                return NotFound();

            return new ObjectResult(master);
        }

        // POST api/masters
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Master master)
        {
            if (master == null) {
                return BadRequest();
            } // if

            await _db.Masters.AddAsync(master);
            await _db.SaveChangesAsync();

            return Ok(master);
        }

        // PUT api/masters/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Master master)
        {
            if (master == null) {
                return BadRequest();
            } // if

            if (!await _db.Masters.AnyAsync(x => x.Id == master.Id)) {
                return NotFound();
            } // if

            _db.Update(master);
            await _db.SaveChangesAsync();

            return Ok(master);
        }

        // DELETE api/masters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var master = await _db.Masters.FindAsync(id);

            if (master == null) {
                return NotFound();
            } // if

            _db.Masters.Remove(master);
            await _db.SaveChangesAsync();

            return Ok(master);
        }
    }
}
