using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;
using Vehicle_Configurator_Project.Repositories;

namespace Vehicle_Configurator_Project.Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IVehicleDetailService _vehicleDetailService;
        private readonly IAlternateComponentRepository _alternateComponentRepository;
        private readonly IVehicleDetailRepository _vehicleDetailRepository;  // declared here

        // Constructor must receive it and assign
        public ModelService(IModelRepository modelRepository,
                            IVehicleDetailService vehicleDetailService,
                            IAlternateComponentRepository alternateComponentRepository,
                            IVehicleDetailRepository vehicleDetailRepository)   // added here
        {
            _modelRepository = modelRepository;
            _vehicleDetailService = vehicleDetailService;
            _alternateComponentRepository = alternateComponentRepository;
            _vehicleDetailRepository = vehicleDetailRepository;            // assign here
        }

        public async Task<List<Model>> GetAllModelsAsync()
        {
            return await _modelRepository.GetAllAsync();
        }

        public async Task<Model> CreateModelAsync(Model model)
        {
            return await _modelRepository.AddAsync(model);
        }

        public async Task<Model> GetModelByIdAsync(int modelId)
        {
            return await _modelRepository.GetByIdAsync(modelId);
        }

        public async Task<List<Model>> GetModelsBySegmentAsync(Segment segment)
        {
            return await _modelRepository.GetBySegmentAsync(segment);
        }

        public async Task<List<Model>> GetModelsByManufacturerAndSegmentAsync(Manufacturer manufacturer, Segment segment)
        {
            return await _modelRepository.GetByManufacturerAndSegmentAsync(manufacturer, segment);
        }

        public async Task<Model> GetModelWithDefaultComponentsAsync(int modelId)
        {
            return await _modelRepository.GetModelWithDefaultComponentsAsync(modelId);
        }

        public async Task<List<VehicleDetail>> GetComponentsByModelIdAsync(int modelId)
        {
            return await _vehicleDetailService.GetByModelIdAsync(modelId);
        }

        public async Task<decimal?> GetBasePriceByModelIdAsync(int modelId)
        {
            return await _modelRepository.GetBasePriceByModelIdAsync(modelId);
        }

        public async Task<Dictionary<char, Dictionary<int, List<AlternateComponent>>>> GetGroupedAlternatesByModelAsync(int modelId)
        {
            // Get configurable components for the model
            var vehicleDetails = await _vehicleDetailRepository.GetByModelIdAndIsConfigurableAsync(modelId, 'Y');

            var grouped = new Dictionary<char, Dictionary<int, List<AlternateComponent>>>();

            foreach (var vd in vehicleDetails)
            {
                // Check if ComponentId has value
                if (!vd.ComponentId.HasValue)
                    continue; // or handle this case as needed

                char compType = vd.CompType;   // e.g. 'S', 'I', 'E'
                int compId = vd.ComponentId.Value;

                var alternates = await _alternateComponentRepository.GetByModelIdAndCompIdAsync(modelId, compId);

                if (!grouped.ContainsKey(compType))
                    grouped[compType] = new Dictionary<int, List<AlternateComponent>>();

                grouped[compType][compId] = alternates;
            }

            return grouped;
        }
    }
}
