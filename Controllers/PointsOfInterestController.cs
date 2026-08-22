using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityDriveManager.Data;
using CityDriveManager.Models;

namespace CityDriveManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PointsOfInterestController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PointsOfInterest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterest>>> GetPointsOfInterest()
        {
            return await _context.PointsOfInterest.ToListAsync();
        }

        // GET: api/PointsOfInterest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PointOfInterest>> GetPointOfInterest(int id)
        {
            var poi = await _context.PointsOfInterest.FindAsync(id);

            if (poi == null)
            {
                return NotFound();
            }

            return poi;
        }

        // POST: api/PointsOfInterest/campus
        [HttpPost("campus")]
        public async Task<ActionResult<Campus>> PostCampus(Campus campus)
        {
            _context.Campuses.Add(campus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPointOfInterest), new { id = campus.Id }, campus);
        }

        // POST: api/PointsOfInterest/monument
        [HttpPost("monument")]
        public async Task<ActionResult<HistoricalMonument>> PostMonument(HistoricalMonument monument)
        {
            _context.HistoricalMonuments.Add(monument);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPointOfInterest), new { id = monument.Id }, monument);
        }

        // PUT: api/PointsOfInterest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPointOfInterest(int id, PointOfInterest poi)
        {
            if (id != poi.Id)
            {
                return BadRequest();
            }

            _context.Entry(poi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/PointsOfInterest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePointOfInterest(int id)
        {
            var poi = await _context.PointsOfInterest.FindAsync(id);
            if (poi == null)
            {
                return NotFound();
            }

            _context.PointsOfInterest.Remove(poi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PoiExists(int id)
        {
            return _context.PointsOfInterest.Any(e => e.Id == id);
        }
    }
}
