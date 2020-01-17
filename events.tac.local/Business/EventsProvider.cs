using events.tac.local.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using System.Linq;
using Sitecore.StringExtensions;

namespace events.tac.local.Business
{
    public class EventsProvider
    {
        private const int PageSize = 2;

        public EventList GetEventList(int pageNumber, ID contextId, string language, string database)
        {
            return GetEventList(pageNumber, string.Empty, new int[0], new int[0], contextId, language, database);
        }

        public EventList GetEventList(int pageNumber, string search, int[] durations, int[] difficultyLevels, ID contextId, string language, string database)
        {
            ISearchIndex index = ContentSearchManager.GetIndex($"events_{database}_index");

            using (IProviderSearchContext context = index.CreateSearchContext())
            {
                var predicate = PredicateBuilder.True<EventDetails>().And(i => i.Paths.Contains(contextId)).And(i => i.Language == language);

                if (!search.IsNullOrEmpty())
                {
                    predicate = predicate.And(i => i.ContentHeading.Contains(search) || i.ContentIntro.Contains(search));
                }

                // filter for Duration property
                if (durations.Any())
                {
                    predicate = predicate.And(i => durations.Contains(i.Duration));
                }

                // filter for Difficulty property
                if (difficultyLevels.Any())
                {
                    predicate = predicate.And(i => difficultyLevels.Contains(i.DifficultyLevel));
                }

                var query = context.GetQueryable<EventDetails>().Where(predicate).FacetOn(e => e.Duration).FacetOn(e=> e.DifficultyLevel);

                SearchResults<EventDetails> results = query.Page(pageNumber - 1, PageSize).GetResults();

                var result = new EventList
                {
                    Events = results.Hits.Select(h => h.Document).ToArray(),
                    PageSize = PageSize,
                    TotalResultCount = query.Count(),
                    Facets = results.Facets
                };

                return result;
            }
        }
    }
}