using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenterWeb.Models.Database;

namespace ServiceCenterWeb.Controllers.WebAPI
{
    [Route("api/personal/orders")]
    [ApiController]
    [Authorize(Roles = "Пользователь, Клиент, Мастер")]
    public class PersonalOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PersonalOrdersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET api/personal/orders/00000000-0000-0000-0000-000000000000
        [HttpGet("{guid}")]
        public async Task<IEnumerable<Order>> Get(string guid) =>
            await Task.Run(() => {
                return _db.Orders
                          .Include(c => c.Client)
                          .Include(t => t.Technic)
                          .Include(m => m.Master)
                          .Include(w => w.TypeWork)
                          .AsParallel()
                          .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                          .Where(order => order.Client.Guid == guid)
                          .OrderBy(order => order.Id);
            });

        // GET api/personal/orders/master/00000000-0000-0000-0000-000000000000
        [HttpGet("master/{guid}")]
        public async Task<IEnumerable<Order>> GetOrdersForMaster(string guid) =>
            await Task.Run(() => {
                return _db.Orders
                          .Include(c => c.Client)
                          .Include(t => t.Technic)
                          .Include(m => m.Master)
                          .Include(w => w.TypeWork)
                          .AsParallel()
                          .WithDegreeOfParallelism(System.Environment.ProcessorCount)
                          .Where(order => order.Master.Guid == guid)
                          .OrderBy(order => order.Id);
            });

        // GET api/personal/orders/forid/5
        [HttpGet("forid/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _db.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            return new ObjectResult(order);
        }

        // POST api/personal/orders/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Order order)
        {
            if (order == null) {
                return BadRequest();
            } // if

            var client = await _db.Clients
                            .FirstAsync(c => c.Guid == order.Client.Guid);
            var technic = await _db.Technics
                             .FirstAsync(t => t.Name == order.Technic.Name);
            var master = await _db.Masters
                             .FirstAsync(m => m.LastName == order.Master.LastName);
            var typeWork = await _db.TypeWorks
                            .FirstAsync(w => w.Name == order.TypeWork.Name);

            if (client != null && technic != null 
                               && master != null 
                               && typeWork != null) {
                order.ClientId = client.Id;
                order.Client = client;
                order.Technic = technic;
                order.Master = master;
                order.TypeWork = typeWork;
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();
            } else
                return BadRequest();

            return Ok(order);
        }

        // PUT api/personal/orders/
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
                            .FirstAsync(c => c.Guid == order.Client.Guid);
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
            } else
                return BadRequest();

            return Ok(order);
        }

        // DELETE api/personal/orders/
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