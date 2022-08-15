namespace Service.Report.PdfModel
{
    public class InvoiceData
    {
        public decimal? Subtotal { get; set; }
        public decimal? IVA { get; set; }
        public decimal Total { get; set; }
    }
}
