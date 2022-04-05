using TimeTracker.Services.Types;

namespace TimeTracker.Services.Interfaces
{
    public interface IPrintingService
    {
        public Task<PdfDocumentDto> PrintAsPdf(Guid employeeId);
    }
}
