using events.tac.local.Business;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class RelatedEventsController : Controller
    {
        private readonly RelatedEventsProvider _provider;

        public RelatedEventsController() : this (new RelatedEventsProvider())
        {
        }

        public RelatedEventsController(RelatedEventsProvider provider)
        {
            this._provider = provider;
        }

        // GET: RelatedEvents
        public ActionResult Index()
        {
            return View(_provider.GetRelatedEvents());
        }
    }
}