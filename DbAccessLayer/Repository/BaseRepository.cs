using DbAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbAccessLayer.Repository
{
    /// <summary>
    /// Base for Database interaction and manipulation
    /// </summary>
	public abstract class BaseRepository
	{
		protected readonly AppDbContext _context;

		public BaseRepository(AppDbContext context)
		{
			_context = context;
		}
	}
}
