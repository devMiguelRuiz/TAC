using events.tac.local.Business;
using Sitecore.Data;
using Sitecore.Mvc.Presentation;
using System.Linq;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class EventListController : Controller
    {
        private const string durationParam = "duration[]";
        private const string difficultyParam = "difficultylevel[]";

        private readonly EventsProvider _provider;

        public EventListController() : this(new EventsProvider()) { }

        public EventListController(EventsProvider provider)
        {
            _provider = provider;
        }

        // GET: EventList
        public ActionResult Index(int page = 1)
        {
            string language = RenderingContext.Current.ContextItem.Language.Name;
            string database = RenderingContext.Current.ContextItem.Database.Name.ToLower();
            ID contextId = RenderingContext.Current.ContextItem.ID;

            ViewBag.Page = page;
            ViewBag.Language = language;
            ViewBag.Database = database;
            ViewBag.ContextId = contextId.ToString();

            var result = _provider.GetEventList(page, contextId, language, database);
            return View(result);
        }

        [HttpGet]
        public ActionResult GetEvents(int page, int[] duration, int[] difficultylevel, string contextId, string language, string database)
        {
            ViewBag.Page = page;

            if (duration == null)
            {

                duration = Request.QueryString[durationParam] != null
                    ? Request.QueryString[durationParam].ToString().Split(',').Select(x => int.Parse(x)).ToArray()
                    : new int[0];
            }

            if (difficultylevel == null)
            {
                difficultylevel = Request.QueryString[difficultyParam] != null
                    ? Request.QueryString[difficultyParam].ToString().Split(',').Select(x => int.Parse(x)).ToArray()
                    : new int[0];
            }

            Database db = Sitecore.Configuration.Factory.GetDatabase(database);
            var item = db.GetItem(contextId);

            var result = _provider.GetEventList(page, duration, difficultylevel, item.ID,language,database);
            return View("List", result);
        }
    }
}