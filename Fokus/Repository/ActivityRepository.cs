using Fokus.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fokus.Repository
{
    public class ActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> FindAll()
        {
            return await _context.Set<Activity>().ToListAsync();
        }
    }
}