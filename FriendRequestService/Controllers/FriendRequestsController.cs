using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FriendRequestService.Models;

namespace FriendRequestService.Controllers
{
    [Route("friendrequest")]
    [ApiController]
    public class FriendRequestsController : ControllerBase
    {
        private readonly FriendRequestContext _context;

        public FriendRequestsController(FriendRequestContext context)
        {
            _context = context;
        }

        // GET: api/FriendRequests
        [HttpGet]
        public IEnumerable<FriendRequest> GetFriendRequests()
        {
            return _context.FriendRequests;
        }

        // GET: api/FriendRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFriendRequest([FromRoute] FriendRequest friendRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(FriendRequestExists(friendRequest))
            {
                return Ok(friendRequest);
            }
            return NotFound();
        }

        
        // POST: api/FriendRequests
        [HttpPost]
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequest friendRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FriendRequests.Add(friendRequest);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/FriendRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var friendRequest = await _context.FriendRequests.FindAsync(id);
            if (friendRequest == null)
            {
                return NotFound();
            }

            _context.FriendRequests.Remove(friendRequest);
            await _context.SaveChangesAsync();

            return Ok(friendRequest);
        }

        private bool FriendRequestExists(FriendRequest friendRequest)
        {
            return _context.FriendRequests.Any(e => e.ToRequest == friendRequest.ToRequest);
        }
    }
}