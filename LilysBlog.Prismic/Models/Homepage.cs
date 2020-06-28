using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilysBlog.Prismic.Models
{
    public class Homepage
    {
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
    }

    public static class HomepageExtensions
    {
        public static Homepage ToHomepage(this prismic.Document doc)
        {
            return new Homepage()
            {
                Description = doc.GetHtml($"{doc.Type}.description", new Helpers.PrismicLinkResolver()),
                MetaTitle = doc.GetText($"{doc.Type}.meta_title"),
                MetaDescription = doc.GetText($"{doc.Type}.meta_description")
            };
        }
    }
}
