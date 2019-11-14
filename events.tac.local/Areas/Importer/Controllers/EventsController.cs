using events.tac.local.Areas.Importer.Models;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Areas.Importer.Controllers
{
    public class EventsController : Controller
    {
        private const string masterDB = "master";

        // GET: Importer/Events
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string parentPath)
        {
            IEnumerable<Event> events = null;

            using (var reader = new System.IO.StreamReader(file.InputStream))
            {
                var content = reader.ReadToEnd();

                try
                {
                    events = JsonConvert.DeserializeObject<IEnumerable<Event>>(content);

                    if (events.Any())
                    {
                        var eventDetailsTemplateId = new TemplateID(new ID("{1EE92533-8833-4210-818C-7D27E6245462}"));

                        Database database = Sitecore.Configuration.Factory.GetDatabase(masterDB);
                        Item parentItem = database.GetItem(parentPath);

                        using (new SecurityDisabler())
                        {
                            foreach (var @event in events)
                            {
                                string name = ItemUtil.ProposeValidItemName(@event.ContentHeading);
                                Item item = parentItem.Add(name, eventDetailsTemplateId);

                                /* Transaction starts */
                                item.Editing.BeginEdit();
                                item["ContentHeading"] = @event.ContentHeading;
                                item["ContentIntro"] = @event.ContentIntro;
                                item["Highlights"] = @event.Highlights;
                                item["StartDate"] = Sitecore.DateUtil.ToIsoDate(@event.StartDate);
                                item["Duration"] = @event.Duration.ToString();
                                item["DifficultyLevel"] = @event.Difficulty.ToString();
                                item.Editing.EndEdit();
                                /* Transaction ends */
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // to be added later
                }
            }
            return View();
        }
    }
}