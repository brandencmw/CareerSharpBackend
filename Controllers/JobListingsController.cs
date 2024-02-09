using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerSharp.Models;
using CareerSharp.Services;

namespace CareerSharp.Controllers
{
    [Route("api/JobListings")]
    [ApiController]
    public class JobListingsController : ControllerBase
    {
        private readonly JobListingContext _context;
        private readonly IJobListingsService _service;

        public JobListingsController(JobListingContext context, IJobListingsService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/JobListings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobListing>>> GetJobListings()
        {
            return await _context.JobListings.ToListAsync();
        }

        // GET: api/JobListings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobListing>> GetJobListing(long id)
        {
            var jobListing = await _context.JobListings.FindAsync(id);

            if (jobListing == null)
            {
                return NotFound();
            }

            return jobListing;
        }

        // GET: api/JobListings/search/search%20terms
        [HttpGet("search/{query}")]
        public async Task<ActionResult<IEnumerable<JobListing>>> SearchListings(string query)
        {
            Console.WriteLine(query);
            var listings = await _service.SearchJobs(query);
            return listings.Any() ? Ok(listings) : NotFound();
        }

        // PUT: api/JobListings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobListing(long id, JobListing jobListing)
        {
            if (id != jobListing.ID)
            {
                return BadRequest();
            }

            _context.Entry(jobListing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobListingExists(id))
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

        // POST: api/JobListings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobListing>> PostJobListing(JobListing jobListing)
        {
            _context.JobListings.Add(jobListing);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobListing), new { id = jobListing.ID }, jobListing);
        }

        // DELETE: api/JobListings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobListing(long id)
        {
            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing == null)
            {
                return NotFound();
            }

            _context.JobListings.Remove(jobListing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobListingExists(long id)
        {
            return _context.JobListings.Any(e => e.ID == id);
        }
    }
}
