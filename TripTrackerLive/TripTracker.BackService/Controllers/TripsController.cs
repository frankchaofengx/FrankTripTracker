using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripTracker.BackService.Models;
using TripTracker.BackService.Data;
using Microsoft.EntityFrameworkCore;

namespace TripTracker.BackService.Controllers
{
    [Route("api/[controller]")]
 //   [ApiController]
    public class TripsController : ControllerBase
    {

        
        public TripsController(TripContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        private TripContext _context;
        // GET api/Tripss
        [HttpGet]
        //public async Task <ActionResult<IEnumerable<Trip>>> GetAsync()
        public async Task<IActionResult> GetAsync()
        {
             // return await _context.Trips.ToListAsync(); 
             var trips = await _context.Trips
                .AsNoTracking()
                .ToListAsync();
            return Ok(trips);
        }

        // GET api/Tripss/5
        [HttpGet("{id}")]
        public ActionResult<Trip> Get(int id)
        {
            return _context.Trips.Find(id);
        }

        // POST api/Tripss
        [HttpPost]
        public IActionResult Post([FromBody] Trip value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Trips.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/Tripss/5
        [HttpPut("{id}")]
        public async Task <IActionResult> PutAsync(int id, [FromBody] Trip value)
        {
            // what about nulls?
           // if(_context.Trips.Find(id)== null)
           if(!_context.Trips.Any(t=> t.Id == id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Trips.Update(value);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/Tripss/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // what about nulls?
            var myTrip = _context.Trips.Find(id);
            if (myTrip == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Trips.Remove(myTrip);
            _context.SaveChanges();
            return Ok();
        }
    }
}
