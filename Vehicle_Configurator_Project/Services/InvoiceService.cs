using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.Models;
using System.Text.Json;
using Vehicle_Configurator_Project.Repositories;

namespace Vehicle_Configurator_Project.Services
{
    public class InvoiceService
    {
        private readonly IUserRepository _userRepo;
        private readonly IModelRepository _modelRepo;
        private readonly IVehicleDetailRepository _vehicleDetailRepo;
        private readonly IAlternateComponentRepository _alternateCompRepo;
        private readonly IInvoiceHeaderRepository _invoiceHeaderRepo;
        private readonly IInvoiceDetailRepository _invoiceDetailRepo;

        public InvoiceService(
            IUserRepository userRepo,
            IModelRepository modelRepo,
            IVehicleDetailRepository vehicleDetailRepo,
            IAlternateComponentRepository alternateCompRepo,
            IInvoiceHeaderRepository invoiceHeaderRepo,
            IInvoiceDetailRepository invoiceDetailRepo)
        {
            _userRepo = userRepo;
            _modelRepo = modelRepo;
            _vehicleDetailRepo = vehicleDetailRepo;
            _alternateCompRepo = alternateCompRepo;
            _invoiceHeaderRepo = invoiceHeaderRepo;
            _invoiceDetailRepo = invoiceDetailRepo;
        }

        public async Task<InvoiceHeader> CreateInvoiceAsync(InvoiceWrapper wrapper)
        {
            var user = await _userRepo.GetByIdAsync(wrapper.UserId)
                ?? throw new Exception("User not found");

            var model = await _modelRepo.GetByIdAsync(wrapper.ModelId)
                ?? throw new Exception("Model not found");

            var header = new InvoiceHeader
            {
                User = user,
                UserId = user.UserId,
                Model = model,
                ModelId = model.ModelId,
                Quantity = wrapper.Quantity,
                InvDate = DateTime.Now,
                CustDetails = BuildCustomerDetails(user),
            };

            decimal baseAmount = model.Price * wrapper.Quantity;
            decimal altAmount = 0;

            // Create lookup map from wrapper.Details: compId -> IsAlternate
            var selectedAlternates = wrapper.Details.ToDictionary(d => d.CompId, d => d.IsAlternate);

            // Fetch all default components for the model (configurable and non-configurable)
            var defaultComponents = await _vehicleDetailRepo.GetByModelIdAsync(model.ModelId);

            var invoiceDetails = new List<InvoiceDetail>();

            foreach (var vd in defaultComponents)
            {
                var baseCompId = vd.Component.CompId;

                var detail = new InvoiceDetail
                {
                    InvoiceHeader = header,
                    InvoiceHeaderId = header.InvId
                };

                if (selectedAlternates.TryGetValue(baseCompId, out var isAlternate) && isAlternate == "Y")
                {
                    var altComps = await _alternateCompRepo.GetByModelIdAndCompIdAsync(model.ModelId, baseCompId);

                    var altComp = altComps.FirstOrDefault()
                        ?? throw new Exception($"Alternate component not found for baseCompId {baseCompId} and modelId {model.ModelId}");

                    altAmount += altComp.DeltaPrice ?? 0m;

                    detail.Component = altComp.AlternateComponentEntity;
                    detail.ComponentId = altComp.AlternateComponentEntity.CompId;
                    detail.IsAlternate = "Y";
                }

                else
                {
                    detail.Component = vd.Component;
                    detail.ComponentId = vd.Component.CompId;
                    detail.IsAlternate = "N";
                }

                invoiceDetails.Add(detail);
            }


            var unitPrice = model.Price + altAmount; 

            var finalAmount = unitPrice * wrapper.Quantity;

            var tax = Math.Round(finalAmount * 0.18m, 2);
            var total = finalAmount + tax;


            header.FinalAmount = finalAmount;
            header.Tax = tax;
            header.TotalAmount = total;

            await _invoiceHeaderRepo.AddAsync(header);
            await _invoiceDetailRepo.AddRangeAsync(invoiceDetails);
            await _invoiceHeaderRepo.SaveChangesAsync();

            return header;
        }

        private string BuildCustomerDetails(User user)
        {
            return
                $"Name: {user.AuthName}, Tel: {user.AuthTel}, Cell: {user.Cell}, Email: {user.Email}, Fax: {user.Fax}, " +
                $"Designation: {user.Designation}, Company: {user.CompanyName}, Company ST No: {user.CompanyStNo}, " +
                $"Company VAT No: {user.CompanyVatNo}, Holding Type: {user.Holding_Type}, Tax PAN: {user.TaxPan}, " +
                $"Address: {user.Add1}, {user.Add2}, City: {user.City}, State: {user.State}, PIN: {user.Pin}";
        }
        public async Task<InvoiceHeader?> GetInvoiceByIdAsync(int id)
        {
            return await _invoiceHeaderRepo.GetByIdAsync(id);
        }
    }

}
