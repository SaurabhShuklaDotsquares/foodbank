using System.Collections.Generic;

namespace FB.ExportReport
{
    public class Preview<T> : IPreview<T> where T : class
    {
        public TemplateType TemplateType { get; set; }
        public Template Template { get; set; }
        public List<T> Data { get; set; }

        public string GeneratePreview()
        {
           return string.Empty;
        }
    }
}
