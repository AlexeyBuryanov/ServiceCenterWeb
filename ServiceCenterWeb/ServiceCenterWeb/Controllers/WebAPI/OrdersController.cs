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
    [Authorize(Roles = "Админ, Мастер")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> Get() =>
            await _db.Orders
               .Include(c => c.Client)
               .Include(t => t.Technic)
               .Include(m => m.Master)
               .Include(w => w.TypeWork)
               .ToListAsync();

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _db.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            var client = await _db.Clients
                                  .FirstAsync(c => c.Id == order.ClientId);
            var technic = await _db.Technics
                                   .FirstAsync(t => t.Id == order.TechnicId);
            var master = await _db.Masters
                                  .FirstAsync(m => m.Id == order.MasterId);
            var typeWork = await _db.TypeWorks
                                    .FirstAsync(w => w.Id == order.TypeWorkId);

            if (client != null && technic != null
                               && master != null
                               && typeWork != null) {
                order.Id = id;
                order.Client = client;
                order.Technic = technic;
                order.Master = master;
                order.TypeWork = typeWork;
            } else {
                return BadRequest();
            } // if

            return new ObjectResult(order);
        }

        // POST api/orders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Order order)
        {
            if (order == null) {
                return BadRequest();
            } // if

            var client = await _db.Clients
                                  .FirstAsync(c => c.LastName == order.Client.LastName);
            var technic = await _db.Technics
                                   .FirstAsync(t => t.Name == order.Technic.Name);
            var master = await _db.Masters
                                  .FirstAsync(m => m.LastName == order.Master.LastName);
            var typeWork = await _db.TypeWorks
                                    .FirstAsync(w => w.Name == order.TypeWork.Name);

            if (client != null && technic != null
                               && master != null
                               && typeWork != null) {
                order.Client = client;
                order.Technic = technic;
                order.Master = master;
                order.TypeWork = typeWork;
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();
            } else {
                return BadRequest();
            } // if

            return Ok(order);
        }

        // PUT api/orders/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Order order)
        {
            if (order == null) {
                return BadRequest();
            } // if

            if (!await _db.Orders.AnyAsync(x => x.Id == order.Id)) {
                return NotFound();
            } // if

            var client = await _db.Clients
                                  .FirstAsync(c => c.LastName == order.Client.LastName);
            var technic = await _db.Technics
                                   .FirstAsync(t => t.Name == order.Technic.Name);
            var master = await _db.Masters
                                  .FirstAsync(m => m.LastName == order.Master.LastName);
            var typeWork = await _db.TypeWorks
                                    .FirstAsync(w => w.Name == order.TypeWork.Name);

            if (client != null && technic != null
                               && master != null
                               && typeWork != null) {
                order.Client = client;
                order.Technic = technic;
                order.Master = master;
                order.TypeWork = typeWork;
                _db.Update(order);
                await _db.SaveChangesAsync();
            } else {
                return BadRequest();
            } // if

            return Ok(order);
        }

        // DELETE api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _db.Orders.FindAsync(id);

            if (order == null) {
                return NotFound();
            } // if

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return Ok(order);
        }
    }
}