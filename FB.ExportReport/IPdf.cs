
namespace FB.ExportReport
{
    public interface IPdf
    {
        PdfDocument Document { get; set; }
        PdfOutput OutputResult { get; set; }
        PdfConvertEnvironment ConvertEnvironment { get; set; }
        void GeneratePDF();
        void GeneratePDF(PdfConvertEnvironment environment);
    }
}
