using events.tac.local.Business;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class EventListController : Controller
    {
        private readonly EventsProvider _provider;

        public EventListController() : this(new EventsProvider()) { }

        public EventListController(EventsProvider provider)
        {
            _provider = provider;
        }
        // GET: EventList
        public ActionResult Index(int page = 1)
        {
            var result = _provider.GetEventList(page);
            return View(result);
        }
    }
}