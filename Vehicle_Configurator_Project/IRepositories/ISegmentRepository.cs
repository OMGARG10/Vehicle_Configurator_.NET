using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IRepositories
{
    public interface ISegmentRepository
    {
        Task<List<Segment>> GetAllSegmentsAsync();
        Task<Segment> GetSegmentByIdAsync(int segId);
        Task<Segment> GetSegmentByNameAsync(string segName);
        Task<Segment> CreateSegmentAsync(Segment segment);
    }
}
