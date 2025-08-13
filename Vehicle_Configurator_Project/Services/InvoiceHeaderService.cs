using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project.Services
{
    public class InvoiceHeaderService : IInvoiceHeaderService
    {
        private readonly IInvoiceHeaderRepository _invoiceRepo;
        private readonly IUserRepository _userRepo;
        private readonly IComponentRepository _componentRepo;
        private readonly IModelRepository _modelRepo;
        private readonly IInvoiceHeaderRepository _headerRepo;
        public InvoiceHeaderService(IInvoiceHeaderRepository invoiceRepo, IUserRepository userRepo, 
            IComponentRepository componentRepo, IModelRepository modelRepo, IInvoiceHeaderRepository headerRepo)
        {
            _invoiceRepo = invoiceRepo;
            _userRepo = userRepo;
            _componentRepo = componentRepo;
            _modelRepo = modelRepo;
            _headerRepo = headerRepo;
        }

        public async Task<InvoiceHeader> CreateInvoiceAsync(int userId, InvoiceCreateRequest request)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            string custDetails = $"CompanyName: {user.CompanyName}, Authorized Person: {user.AuthName}, Email: {user.Email}, Tel: {user.Tel ?? "N/A"}";

            // Fetch model base price - you need to inject IModelRepository and use it here
            var model = await _modelRepo.GetByIdAsync(request.ModelId);
            if (model == null)
                throw new Exception("Model not found");

            decimal basePrice = model.Price;

            // Await the async method and pass basePrice
            decimal finalAmount = await CalculateFinalAmountAsync(request, basePrice);

            var invoice = new InvoiceHeader
            {
                UserId = userId,
                ModelId = request.ModelId,
                Quantity = request.Quantity,
                CustDetails = custDetails,
                InvDate = DateTime.Now,
                FinalAmount = finalAmount,
            };

            invoice.Tax = Math.Round(invoice.FinalAmount * 0.18m, 2);
            invoice.TotalAmount = Math.Round(invoice.FinalAmount + invoice.Tax, 2);

            return await _invoiceRepo.AddInvoiceAsync(invoice);
        }



        public async Task<decimal> CalculateFinalAmountAsync(InvoiceCreateRequest request, decimal basePrice)
        {
            decimal alternateComponentsTotal = 0m;

            foreach (var comp in request.Details)
            {
                if (comp.IsAlternate == "Y")
                {
                    // Assume _componentRepo.GetAlternateComponentPriceAsync fetches price for alternate component by compId
                    decimal price = await _componentRepo.GetAlternateComponentPriceAsync(comp.CompId);
                    alternateComponentsTotal += price;
                }
            }

            decimal finalAmount = (basePrice + alternateComponentsTotal) * request.Quantity;
            return finalAmount;
        }
        public async Task<InvoiceHeader?> GetInvoiceWithDetailsAsync(int invoiceId)
        {
            // Load InvoiceHeader including details and components
            return await _headerRepo.GetByIdAsync(invoiceId);
        }

    }
}
