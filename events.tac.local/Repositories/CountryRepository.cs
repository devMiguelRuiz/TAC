using events.tac.local.Models;
using Sitecore.Collections;
using Sitecore.Sites;
using Sitecore.Web;
using System.Collections.Generic;
using System.Linq;

namespace events.tac.local.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        public IEnumerable<ICountry> GetCountries(SiteContext context)
        {
            var countries = new List<ICountry>();
            LanguageCollection languages = context.Database.GetLanguages();

            foreach (var language in languages)
            {
                Country country = new Country
                {
                    Name = language.CultureInfo.DisplayName,
                    ImageUrl = "/~/icon/" + language.GetIcon(context.Database),
                    Url = "http://" + GetSite(language.Name).HostName
                };
                countries.Add(country);
            }
            return countries;
        }

        private SiteInfo GetSite(string language)
        {
            return SiteContextFactory.Sites.First(s => s.Language == language && s.HostName != string.Empty);
        }
    }
}