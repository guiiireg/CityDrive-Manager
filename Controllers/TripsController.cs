using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityDriveManager.Data;
using CityDriveManager.Models;

namespace CityDriveManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TripsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            return await _context.Trips
                .Include(t => t.Vehicle)
                .Include(t => t.StartPoint)
                .Include(t => t.EndPoint)
                .ToListAsync();
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            var trip = await _context.Trips
                .Include(t => t.Vehicle)
                .Include(t => t.StartPoint)
                .Include(t => t.EndPoint)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        // POST: api/Trips
        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrip), new { id = trip.Id }, trip);
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
