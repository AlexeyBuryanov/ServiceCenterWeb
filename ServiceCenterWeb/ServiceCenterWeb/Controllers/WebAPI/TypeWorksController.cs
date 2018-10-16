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
    public class TypeWorksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TypeWorksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<TypeWork>> Get() => await _db.TypeWorks.ToListAsync();

        // GET api/typeworks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var typework = await _db.TypeWorks.FindAsync(id);

            if (typework == null)
                return NotFound();

            return new ObjectResult(typework);
        }

        // POST api/typeworks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TypeWork typework)
        {
            if (typework == null) {
                return BadRequest();
            } // if

            await _db.TypeWorks.AddAsync(typework);
            await _db.SaveChangesAsync();

            return Ok(typework);
        }

        // PUT api/typeworks/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]TypeWork typework)
        {
            if (typework == null) {
                return BadRequest();
            } // if

            if (!await _db.TypeWorks.AnyAsync(x => x.Id == typework.Id)) {
                return NotFound();
            } // if

            _db.Update(typework);
            await _db.SaveChangesAsync();

            return Ok(typework);
        }

        // DELETE api/typeworks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var typework = await _db.TypeWorks.FindAsync(id);

            if (typework == null) {
                return NotFound();
            } // if

            _db.TypeWorks.Remove(typework);
            await _db.SaveChangesAsync();

            return Ok(typework);
        }
    }
}