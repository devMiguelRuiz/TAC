using events.tac.local.Models;
using Sitecore.Sites;
using System.Collections.Generic;

namespace events.tac.local.Repositories
{
    public interface ICountryRepository
    {
        IEnumerable<ICountry> GetCountries(SiteContext context);
    }
}