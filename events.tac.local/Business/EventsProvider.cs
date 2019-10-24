using events.tac.local.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Mvc.Presentation;
using System.Linq;

namespace events.tac.local.Business
{
    public class EventsProvider
    {
        private const int pageSize = 2;
        public EventList GetEventList(int pageNumber)
        {
            string indexName = $"events_{RenderingContext.Current.ContextItem.Database.Name.ToLower()}_index";
            ISearchIndex index = ContentSearchManager.GetIndex(indexName);

            using (IProviderSearchContext context = index.CreateSearchContext())
            {
                var results = context.GetQueryable<EventDetails>()
                    .Where(i => i.Paths.Contains(RenderingContext.Current.ContextItem.ID))
                    .Where(i => i.Language == RenderingContext.Current.ContextItem.Language.Name)
                    .Page(pageNumber - 1, pageSize)
                    .GetResults();

                var result = new EventList()
                {
                    Events = results.Hits.Select(h => h.Document).ToArray(),
                    PageSize = pageSize,
                    TotalResultCount = results.TotalSearchResults
                };

                return result;
            }
        }
    }
}