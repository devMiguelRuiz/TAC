using events.tac.local.Repositories;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class LanguageController : Controller
    {
        readonly ICountryRepository repository;

        public LanguageController()
        {
            this.repository = new CountryRepository();
        }

        public LanguageController(ICountryRepository repository)
        {
            this.repository = repository;
        }

        // GET: Language
        public ActionResult Index()
        {
            var model = repository.GetCountries(Sitecore.Context.Site);
            return View(model);
        }
    }
}