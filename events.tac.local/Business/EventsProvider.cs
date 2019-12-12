using events.tac.local.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using System.Linq;

namespace events.tac.local.Business
{
    public class EventsProvider
    {
        private const int pageSize = 2;

        public EventList GetEventList(int pageNumber, ID contextId, string language, string database)
        {
            return GetEventList(pageNumber, new int[0], new int[0], contextId, language, database);
        }

        public EventList GetEventList(int pageNumber, int[] durations, int[] difficultyLevels, ID contextId, string language, string database)
        {
            string indexName = $"events_{database}_index";

            ISearchIndex index = ContentSearchManager.GetIndex(indexName);

            using (IProviderSearchContext context = index.CreateSearchContext())
            {
                var predicate = PredicateBuilder.True<EventDetails>();
                //predicate = predicate.And(i => i.Paths.Contains(RenderingContext.Current.ContextItem.ID)).And(i => i.Language == RenderingContext.Current.ContextItem.Language.Name);
                predicate = predicate.And(i => i.Paths.Contains(contextId)).And(i => i.Language == language);

                // filter for Duration property
                if (durations.Count() > 0)
                {
                    predicate = predicate.And(i => durations.Contains(i.Duration));
                }

                // filter for Difficulty property
                if (difficultyLevels.Count() > 0)
                {
                    predicate = predicate.And(i => difficultyLevels.Contains(i.DifficultyLevel));
                }

                var query = context.GetQueryable<EventDetails>().Where(predicate).FacetOn(e => e.Duration).FacetOn(e=> e.DifficultyLevel);

                SearchResults<EventDetails> results = query.Page(pageNumber - 1, pageSize).GetResults();

                var result = new EventList()
                {
                    Events = results.Hits.Select(h => h.Document).ToArray(),
                    PageSize = pageSize,
                    TotalResultCount = results.Count(),
                    Facets = results.Facets
                };

                return result;
            }
        }
    }
}