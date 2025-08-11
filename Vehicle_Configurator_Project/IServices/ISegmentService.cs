using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.IServices
{
    public interface ISegmentService
    {
        Task<List<Segment>> GetAllSegmentsAsync();
        Task<Segment> GetSegmentByIdAsync(int segId);
        Task<Segment> GetSegmentByNameAsync(string segName);
        Task<Segment> CreateSegmentAsync(Segment segment);
    }
}
