using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Repositories
{
    public class SegmentRepository : ISegmentRepository
    {
        private readonly ApplicationDbContext context;

        public SegmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Segment>> GetAllSegmentsAsync()
        {
            return await context.Segments.ToListAsync();
        }

        public async Task<Segment> GetSegmentByIdAsync(int segId)
        {
            return await context.Segments.FindAsync(segId);
        }

        public async Task<Segment> GetSegmentByNameAsync(string segName)
        {
            return await context.Segments.FirstOrDefaultAsync(s => s.SegName == segName);
        }

        public async Task<Segment> CreateSegmentAsync(Segment segment)
        {
            context.Segments.Add(segment);
            await context.SaveChangesAsync();
            return segment;
        }
    }
}
