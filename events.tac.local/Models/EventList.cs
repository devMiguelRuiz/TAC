using Sitecore.ContentSearch.Linq;
using System.Collections.Generic;

namespace events.tac.local.Models
{
    public class EventList
    {
        public IEnumerable<EventDetails> Events { get; set; }
        public int TotalResultCount { get; set; }
        public int PageSize { get; set; }
        public FacetResults Facets { get; set; }
    }
}