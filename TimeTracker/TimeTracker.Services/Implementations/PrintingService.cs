using TimeTracker.Services.Interfaces;
using TimeTracker.Services.Types;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace TimeTracker.Services.Implementations
{
    public class PrintingService : IPrintingService
    {
        private readonly ITimeTrackingService _timeTrackingService;
        private readonly IEmployeeService _employeeService;

        public PrintingService(ITimeTrackingService timeTrackingService, IEmployeeService employeeService)
        {
            _timeTrackingService = timeTrackingService;
            _employeeService = employeeService;
        }

        public async Task<PdfDocumentDto> PrintAsPdf(Guid employeeId)
        {
            var trackedTimes = await _timeTrackingService.GetTimeTrackingsByEmployeeId(employeeId);
            var employee = await _employeeService.GetByIdAsync(employeeId);
            var pdfDocument = new PdfDocumentDto();
            var document = new PdfDocument();
            var page = document.AddPage();

            XPen pen = XPens.Black;
            XBrush brush = XBrushes.Black;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont textFont = new XFont("Verdana", 12, XFontStyle.Regular);
            XFont titleFont = new XFont("Verdana", 16, XFontStyle.Regular);
            XFont detailTextFont = new XFont("Verdana", 10, XFontStyle.Regular);
            XRect titleLayoutRectangle = new XRect(0, 0, page.Width, page.Height);

            gfx.DrawString($"Employee: {employee.FirstName} {employee.LastName}", titleFont, brush, titleLayoutRectangle, XStringFormats.TopCenter);

            double x = 20;
            double y = 50;

            foreach (var trackedTime in trackedTimes)
            {
                string startTime = trackedTime.StartTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                string endTime = trackedTime.EndTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");

                string title = $"Job title: {trackedTime.Job.Title}";
                string timeInterval = $"From {startTime} to {endTime}";
                string totalHours = $"Total hours: {Math.Round(trackedTime.TotalHours, 3)}h";
                string hourlyRate = $"Hourly rate: {trackedTime.Job.HourlyRate}$";
                string totalEarnings = $"Total earnings: {Math.Round(trackedTime.TotalCost, 3)}$";
                string description = $"Description: {trackedTime.Job.Description}";

                var titleSize = gfx.MeasureString(title, textFont);
                var timeIntervalSize = gfx.MeasureString(timeInterval, textFont);
                var totalHoursSize = gfx.MeasureString(totalHours, textFont);
                var hourlyRateSize = gfx.MeasureString(hourlyRate, textFont);
                var totalEarningsSize = gfx.MeasureString(totalEarnings, textFont);
                var descriptionSize = gfx.MeasureString(description, textFont);

                gfx.DrawString(title, textFont, brush, x, y);
                y += titleSize.Height;

                gfx.DrawString(timeInterval, detailTextFont, brush, x, y + titleSize.Height);
                y += timeIntervalSize.Height;

                gfx.DrawString(totalHours, detailTextFont, brush, x, y + timeIntervalSize.Height);
                y += totalHoursSize.Height;

                gfx.DrawString(hourlyRate, detailTextFont, brush, x, y + totalHoursSize.Height);
                y += hourlyRateSize.Height;

                gfx.DrawString(totalEarnings, detailTextFont, brush, x, y + hourlyRateSize.Height);
                y += totalEarningsSize.Height;

                gfx.DrawString(description, detailTextFont, brush, x, y + totalEarningsSize.Height);
                y += descriptionSize.Height;

                gfx.DrawLine(pen, new XPoint(0, y + descriptionSize.Height), new XPoint(page.Width, y + descriptionSize.Height));
                y += 50;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                pdfDocument.Data = stream.ToArray();
                pdfDocument.DocumentName = "Employee: " + employee.FirstName + " " + employee.LastName + ".pdf";
            }

            return pdfDocument;
        }
    }
}
