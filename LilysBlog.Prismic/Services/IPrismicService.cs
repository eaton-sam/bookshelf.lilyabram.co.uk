using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LilysBlog.Prismic.Models;
using prismic;

namespace LilysBlog.Prismic.Services
{
    public interface IPrismicService
    {
        Task<Shelf> GetShelf(string uid);
        Task<IList<Shelf>> GetShelves();
        Task<Homepage> GetHomepage();
        Task<IList<Models.Experiment>> GetExperiments();
        Task<Models.Experiment> GetExperiment(string uid);
    }
}
