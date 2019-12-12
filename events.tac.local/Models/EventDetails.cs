using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Web.UI.WebControls;
using System;
using System.Web;

namespace events.tac.local.Models
{
    public class EventDetails : SearchResultItem
    {
        public string ContentHeading { get; set; }
        public string ContentIntro { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public int DifficultyLevel { get; set; }
        public HtmlString EventImage
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(GetItem(), "EventImage", "DisableWebEditing=true&w=150"));
            }
        }
    }
}