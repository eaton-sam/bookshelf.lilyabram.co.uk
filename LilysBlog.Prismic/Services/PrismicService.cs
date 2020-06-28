using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prismic;
using Microsoft.Extensions.Logging;
using LilysBlog.Prismic.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using LilysBlog.Prismic.Helpers;

namespace LilysBlog.Prismic.Services
{
    public class PrismicService : IPrismicService
    {
        protected ILogger<PrismicService> Logger { get; }
        public IPrismicApiAccessor ApiAccessor { get; }
        protected string ApiUrl { get; set; }
        protected HttpClient HttpClient { get; set; }

        private Lazy<Task<List<Shelf>>> shelfs;

        private Lazy<Task<List<Models.Experiment>>> experiments;

        private Lazy<Task<Homepage>> homepage;

        public PrismicService(ILogger<PrismicService> logger, IConfiguration configuration, IPrismicApiAccessor apiAccessor)
        {
            Logger = logger;
            ApiAccessor = apiAccessor;
            ApiUrl = configuration.GetSection("Prismic:Url").Value;

            shelfs = new Lazy<Task<List<Shelf>>>(async () =>
            {
                var api = await GetApi();
                var response = await api.Query(Predicates.At("document.type", "shelf")).PageSize(100).Submit();
                return response.Results.Select(x => x.ToShelf()).OrderBy(x => x.Order).ToList();
            });

            experiments = new Lazy<Task<List<Models.Experiment>>>(async () =>
            {
                var api = await GetApi();
                var response = await api.Query(Predicates.At("document.type", "experiment")).PageSize(100).Submit();
                return response.Results.Select(x => x.ToExperiment()).OrderBy(x => x.Order).ToList();
            });

            homepage = new Lazy<Task<Homepage>>(async () =>
            {
                var api = await GetApi();
                var response = await api.QueryFirst(Predicates.At("document.type", "homepage"));
                return response.ToHomepage();
            });
        }

        private Api _api;
        private async ValueTask<Api> GetApi()
        {
            if (_api == null)
            {
                _api = await ApiAccessor.GetApi(ApiUrl);
            }

            return _api;
        }

        public async Task<Shelf> GetShelf(string uid)
        {
            return (await GetShelves()).FirstOrDefault(x => x.Uid == uid);
        }

        public async Task<IList<Shelf>> GetShelves()
        {
            return await shelfs.Value;
        }

        public async Task<Homepage> GetHomepage()
        {
            return await homepage.Value;
        }

        public async Task<IList<Models.Experiment>> GetExperiments()
        {
            return await experiments.Value;
        }

        public async Task<Models.Experiment> GetExperiment(string uid)
        {
            return (await GetExperiments()).FirstOrDefault(x => x.Uid == uid);
        }
    }
}
