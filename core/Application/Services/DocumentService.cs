using Application.Documents;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Models;
using Domain.Services;
using Domain.Stores;

namespace Application.Services
{
    public class DocumentService
    {
        private readonly Intervention _interventionStore;

        public DocumentService(Intervention interventionStore)
        {
            _interventionStore = interventionStore;
            
        }

        public async Task CreateDocument()
        {
            var output = Path.Combine(Environment.CurrentDirectory, "FieldOps_RapportIntervention_Template.docx");
            ReportDocumentBuilder.Create(output, _interventionStore);
            Console.WriteLine($"Generated: {output}");
        }

    }
}
