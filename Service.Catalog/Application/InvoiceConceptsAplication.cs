using Service.Catalog.Application.IApplication;

namespace Service.Catalog.Application
{
    public class InvoiceConceptsAplication : IInvoiceConceptsAplication
    {
        private readonly IInvoiceConceptsAplication _repository;

        public InvoiceConceptsAplication(IInvoiceConceptsAplication repository)
        {
            _repository = repository;
        }

    }
}
