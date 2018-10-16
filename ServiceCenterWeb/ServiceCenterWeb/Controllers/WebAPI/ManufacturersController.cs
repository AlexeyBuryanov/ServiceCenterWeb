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
    [Authorize(Roles = "Админ")]
    public class ManufacturersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ManufacturersController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Manufacturer>> Get() => await _db.Manufacturers.ToListAsync();

        // GET api/manufacturers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var manufacturer = await _db.Manufacturers.FindAsync(id);

            if (manufacturer == null)
                return NotFound();

            return new ObjectResult(manufacturer);
        }

        // POST api/manufacturers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Manufacturer manufacturer)
        {
            if (manufacturer == null) {
                return BadRequest();
            } // if

            await _db.Manufacturers.AddAsync(manufacturer);
            await _db.SaveChangesAsync();

            return Ok(manufacturer);
        }

        // PUT api/manufacturers/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Manufacturer manufacturer)
        {
            if (manufacturer == null) {
                return BadRequest();
            } // if

            if (!await _db.Manufacturers.AnyAsync(x => x.Id == manufacturer.Id)) {
                return NotFound();
            } // if

            _db.Update(manufacturer);
            await _db.SaveChangesAsync();

            return Ok(manufacturer);
        }

        // DELETE api/manufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturer = await _db.Manufacturers.FindAsync(id);

            if (manufacturer == null) {
                return NotFound();
            } // if

            _db.Manufacturers.Remove(manufacturer);
            await _db.SaveChangesAsync();

            return Ok(manufacturer);
        }
    }
}