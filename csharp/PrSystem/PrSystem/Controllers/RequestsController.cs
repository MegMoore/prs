using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PrSystem.Data;
using PrSystem.Models;

namespace PrSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrSystemContext _context;

        public RequestsController(PrSystemContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            return await _context.Requests.Include(x => x.User).Include(x => x.RequestLines).ToListAsync(); ;
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            var request = await _context.Requests.Include(x => x.User)
                                    .Include(x => x.RequestLines)
                                    .SingleOrDefaultAsync(x => x.Id == id); ;

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        //Added Methods//
        //***************************************************************************************//

        // GET: api/requests/reviews/{id}
        [HttpGet("review")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsIfReview()
        {
            if (_context.Requests == null)
            {
                return NotFound();
            }
            return await _context.Requests
                          .Where(x => x.Status == "REVIEW")
                          .Include(x => x.User)
                          .ToListAsync();
        }


        //Put: api/request/review/{id}
        //Updating Request Status to Review

        [HttpPut("review/{id}")]
        public async Task<IActionResult> SetRequestStatusToReview(Request request, int id)
        {
            request.Status = "REVIEW";
            return await PutRequest(id, request);
        }

        //PUT: api/Orders/approve/{id}

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> SetRequestStatusToApprove(Request request, int id)
        {
            request.Status = "APPROVE";
            return await PutRequest(id, request);
        }


        //PUT: api/Orders/reject/{id}

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> SetRequestStatusToReject(Request request, int id)
        {
            request.Status = "REJECT";
            return await PutRequest(id, request);
        }



        //******************************************************************************************//

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
          if (_context.Requests == null)
          {
              return Problem("Entity set 'PrSystemContext.Request'  is null.");
          }
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            if (_context.Requests == null)
            {
                return NotFound();
            }
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
