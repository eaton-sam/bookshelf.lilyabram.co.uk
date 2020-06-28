using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LilysBlog.Prismic.Models
{
    public class Experiment
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IList<string> ImageUrls { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
    }

    public static class ExperimentExtensions
    {
        public static Experiment ToExperiment(this prismic.Document doc)
        {
            return new Experiment()
            {
                Uid = doc.Uid,
                Name = doc.GetText($"{doc.Type}.name"),
                Description = doc.GetHtml($"{doc.Type}.description", new Helpers.PrismicLinkResolver()),
                Order = Convert.ToInt32(doc.GetNumber($"{doc.Type}.order")?.Value),
                ImageUrls = doc.GetGroup($"{doc.Type}.images").GroupDocs.Select(x => x.GetImageView("image", "main").Url).ToList(),
                MetaTitle = doc.GetText($"{doc.Type}.meta_title"),
                MetaDescription = doc.GetText($"{doc.Type}.meta_description")
            };
        }
    }
}
