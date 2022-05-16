/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomNumber.Models;

namespace RandomNumber.Controllers
{
    [Route("api/randomnumbers")]
    [ApiController]
    public class GeneratedNumbersController : ControllerBase
    {
        private readonly RandomNumberContext _context;
        private readonly Random _random;

        public GeneratedNumbersController(RandomNumberContext context)
        {
            _context = context;
            _random = new Random();
        }

        // GET: api/GeneratedNumbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneratedNumber>>> GetGeneratedNumbers()
        {
          if (_context.GeneratedNumbers == null)
          {
              return NotFound();
          }
            return await _context.GeneratedNumbers.ToListAsync();
        }

        [HttpGet("GetMinMax")]
        public async Task<ActionResult<IEnumerable<GeneratedNumber>>> GetSmallestAndLargest()
        {
            if (_context.GeneratedNumbers == null)
            {
                return NotFound();
            }

            int max = _context.GeneratedNumbers.Max(n => n.Number);
            int min = _context.GeneratedNumbers.Min(n => n.Number);

            GeneratedNumber MaxObj = await _context.GeneratedNumbers.FirstOrDefaultAsync(n => n.Number == max);
            GeneratedNumber MinObj = await _context.GeneratedNumbers.FirstOrDefaultAsync(n => n.Number == min);

            List<GeneratedNumber> results = new List<GeneratedNumber>();
            results.Add(MaxObj);
            results.Add(MinObj);

            return results.ToList();
        }

        // GET: api/GeneratedNumbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneratedNumber>> GetGeneratedNumber(int id)
        {
          if (_context.GeneratedNumbers == null)
          {
              return NotFound();
          }
            var generatedNumber = await _context.GeneratedNumbers.FindAsync(id);

            if (generatedNumber == null)
            {
                return NotFound();
            }

            return generatedNumber;
        }

        // PUT: api/GeneratedNumbers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneratedNumber(int id, GeneratedNumber generatedNumber)
        {
            if (id != generatedNumber.Id)
            {
                return BadRequest();
            }

            _context.Entry(generatedNumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneratedNumberExists(id))
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

        // POST: api/GeneratedNumbers
        [HttpPost("GenerateRandomNumber")]
        public async Task<ActionResult<GeneratedNumber>> PostGeneratedNumber()
        {
            if (_context.GeneratedNumbers == null)
            {
                return Problem("Entity set 'RandomNumberContext.GeneratedNumbers'  is null.");
            }

            GeneratedNumber number = new GeneratedNumber();
            number.Number = _random.Next(100_000);
            number.InstanceName = "Placeholder";
            _context.GeneratedNumbers.Add(number);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGeneratedNumber", new { id = number.Id}, new { number.InstanceName ,number.Number });
        }

        // DELETE: api/GeneratedNumbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneratedNumber(int id)
        {
            if (_context.GeneratedNumbers == null)
            {
                return NotFound();
            }
            var generatedNumber = await _context.GeneratedNumbers.FindAsync(id);
            if (generatedNumber == null)
            {
                return NotFound();
            }

            _context.GeneratedNumbers.Remove(generatedNumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneratedNumberExists(int id)
        {
            return (_context.GeneratedNumbers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
