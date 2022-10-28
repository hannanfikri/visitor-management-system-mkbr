using System.Text.RegularExpressions;

namespace Visitor.Web.Xss
{
    
    public class DefaultHtmlSanitizer : IHtmlSanitizer
    {
        public string Sanitize(string html)
        {
            return Regex.Replace(html, "<.*?>|&.*?;", string.Empty);
        }
    }
}