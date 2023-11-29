using System.Collections.Generic;

namespace FB.ExportReport
{
    public interface IPreview<T> where T : class
    {
        TemplateType TemplateType { get; set; }
        Template Template { get; set; }
        List<T> Data { get; set; }
        string GeneratePreview();
    }
}
