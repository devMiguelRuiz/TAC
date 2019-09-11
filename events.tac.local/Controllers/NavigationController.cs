using events.tac.local.Business;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class NavigationController : Controller
    {
        private readonly NavigationBuilder _builder;

        public NavigationController() : this (new NavigationBuilder())
        {

        }

        public NavigationController(NavigationBuilder builder)
        {
            this._builder = builder;
        }

        // GET: Navigation
        public ActionResult Index()
        {
            return View(_builder.Build());
        }
    }
}