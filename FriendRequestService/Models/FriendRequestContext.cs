using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendRequestService.Models
{
    public class FriendRequestContext : DbContext
    {
        public FriendRequestContext(DbContextOptions<FriendRequestContext> options) : base(options)
        {
        }

        public DbSet<FriendRequest> FriendRequests {get; set;}
    }
}
