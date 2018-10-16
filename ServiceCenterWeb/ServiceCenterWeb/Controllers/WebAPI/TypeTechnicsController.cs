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
    public class TypeTechnicsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TypeTechnicsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<TypeTechnic>> Get() => await _db.TypeTechnics.ToListAsync();

        // GET api/typetechnics/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var typetechnic = await _db.TypeTechnics.FindAsync(id);

            if (typetechnic == null)
                return NotFound();

            return new ObjectResult(typetechnic);
        }

        // POST api/typetechnics
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TypeTechnic typetechnic)
        {
            if (typetechnic == null) {
                return BadRequest();
            } // if

            await _db.TypeTechnics.AddAsync(typetechnic);
            await _db.SaveChangesAsync();

            return Ok(typetechnic);
        }

        // PUT api/typetechnics/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]TypeTechnic typetechnic)
        {
            if (typetechnic == null) {
                return BadRequest();
            } // if

            if (!await _db.TypeTechnics.AnyAsync(x => x.Id == typetechnic.Id)) {
                return NotFound();
            } // if

            _db.Update(typetechnic);
            await _db.SaveChangesAsync();

            return Ok(typetechnic);
        }

        // DELETE api/typetechnics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var typetechnic = await _db.TypeTechnics.FindAsync(id);

            if (typetechnic == null) {
                return NotFound();
            } // if

            _db.TypeTechnics.Remove(typetechnic);
            await _db.SaveChangesAsync();

            return Ok(typetechnic);
        }
    }
}