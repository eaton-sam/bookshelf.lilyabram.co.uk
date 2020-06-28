using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilysBlog.Prismic.Models
{
    public class Shelf
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IList<string> ImageUrls { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string DownloadUrl { get; set; }
    }

    public static class ShelfExtensions
    {
        public static Shelf ToShelf(this prismic.Document doc)
        {
            return new Shelf()
            {
                Uid = doc.Uid,
                Name = doc.GetText($"{doc.Type}.name"),
                Description = doc.GetHtml($"{doc.Type}.description", new Helpers.PrismicLinkResolver()),
                Order = Convert.ToInt32(doc.GetNumber($"{doc.Type}.order")?.Value),
                ImageUrls = doc.GetGroup($"{doc.Type}.images").GroupDocs.Select(x => x.GetImageView("image", "main").Url).ToList(),
                MetaTitle = doc.GetText($"{doc.Type}.meta_title"),
                MetaDescription = doc.GetText($"{doc.Type}.meta_description"),
                DownloadUrl = doc.GetLink($"{doc.Type}.attachment")?.GetUrl(new Helpers.PrismicLinkResolver())
            };
        }
    }
}
