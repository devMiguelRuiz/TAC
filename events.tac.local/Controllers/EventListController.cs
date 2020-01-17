using events.tac.local.Business;
using Sitecore.Data;
using Sitecore.Mvc.Presentation;
using System.Linq;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class EventListController : Controller
    {
        private const string DurationParam = "duration[]";
        private const string DifficultyParam = "difficultyLevel[]";

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
        public ActionResult GetEvents(int page, string search, string contextId, string language, string database)
        {
            ViewBag.Page = page;
            int[] duration = QueryStringToIntArray(DurationParam);
            int[] difficultyLevel = QueryStringToIntArray(DifficultyParam);

            Database db = Sitecore.Configuration.Factory.GetDatabase(database);
            var item = db.GetItem(contextId);

            var result = _provider.GetEventList(page, search, duration, difficultyLevel, item.ID, language, database);
            return View("List", result);
        }

        private int[] QueryStringToIntArray(string stringName)
        {
            return Request.QueryString[stringName] != null
                ? Request.QueryString[stringName].Split(',').Select(int.Parse).ToArray()
                : new int[0];
        }
    }
}