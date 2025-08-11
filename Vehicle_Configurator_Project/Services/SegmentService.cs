using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Services
{
    public class SegmentService : ISegmentService
    {
        private readonly ISegmentRepository _segmentRepository;

        public SegmentService(ISegmentRepository segmentRepository)
        {
            _segmentRepository = segmentRepository;
        }

        public async Task<List<Segment>> GetAllSegmentsAsync()
        {
            return await _segmentRepository.GetAllSegmentsAsync();
        }

        public async Task<Segment> GetSegmentByIdAsync(int segId)
        {
            var segment = await _segmentRepository.GetSegmentByIdAsync(segId);
            if (segment == null)
                throw new KeyNotFoundException($"Segment not found with id {segId}");
            return segment;
        }

        public async Task<Segment> GetSegmentByNameAsync(string segName)
        {
            var segment = await _segmentRepository.GetSegmentByNameAsync(segName);
            if (segment == null)
                throw new KeyNotFoundException($"Segment not found with name {segName}");
            return segment;
        }

        public async Task<Segment> CreateSegmentAsync(Segment segment)
        {
            return await _segmentRepository.CreateSegmentAsync(segment);
        }
    }
}
