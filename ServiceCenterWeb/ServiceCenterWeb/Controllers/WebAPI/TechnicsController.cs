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
    [Authorize(Roles = "Админ, Клиент")]
    public class TechnicsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TechnicsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Technic>> Get() => 
            await _db.Technics
               .Include(t => t.TypeTechnic)
               .Include(m => m.Manufacturer)
               .ToListAsync();

        // GET api/technics/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var technic = await _db.Technics.FindAsync(id);

            if (technic == null)
                return NotFound();

            return new ObjectResult(technic);
        }

        // POST api/technics
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Technic technic)
        {
            if (technic == null) {
                return BadRequest();
            } // if

            var typeTechnic = await _db.TypeTechnics
                                       .FirstAsync(c => c.Id == technic.TypeTechnicId);
            var manufacturer = await _db.Manufacturers
                                       .FirstAsync(c => c.Id == technic.ManufacturerId);

            if (typeTechnic != null && manufacturer != null) {
                technic.TypeTechnic = typeTechnic;
                technic.Manufacturer = manufacturer;

                await _db.Technics.AddAsync(technic);
                await _db.SaveChangesAsync();
            } else
                return BadRequest();

            return Ok(technic);
        }

        // PUT api/technics/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Technic technic)
        {
            if (technic == null) {
                return BadRequest();
            } // if

            if (!await _db.Technics.AnyAsync(x => x.Id == technic.Id)) {
                return NotFound();
            } // if

            var typeTechnic = await _db.TypeTechnics
                                       .FirstAsync(c => c.Id == technic.TypeTechnicId);
            var manufacturer = await _db.Manufacturers
                                        .FirstAsync(c => c.Id == technic.ManufacturerId);

            if (typeTechnic != null && manufacturer != null) {
                technic.TypeTechnic = typeTechnic;
                technic.Manufacturer = manufacturer;

                _db.Update(technic);
                await _db.SaveChangesAsync();
            } else
                return BadRequest();

            return Ok(technic);
        }

        // DELETE api/technics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var technic = await _db.Technics.FindAsync(id);

            if (technic == null) {
                return NotFound();
            } // if

            _db.Technics.Remove(technic);
            await _db.SaveChangesAsync();

            return Ok(technic);
        }
    }
}