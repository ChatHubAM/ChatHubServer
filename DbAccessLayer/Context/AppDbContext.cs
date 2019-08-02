using DbAccessLayer.Entities;
using DbAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbAccessLayer.Context
{
    /// <summary>
    /// Class for accesing the Db via EF Core
    /// </summary>
    public class AppDbContext: DbContext
    {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<UserEntity> Users { get; set; }
        public DbSet<User_Role> User_Role { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<VideoToFrom> VideoRequests { get; set; }

	}
}
