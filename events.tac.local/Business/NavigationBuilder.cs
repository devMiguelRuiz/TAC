using events.tac.local.Models;
using System.Collections.Generic;
using System.Linq;
using TAC.Sitecore.Abstractions.Interfaces;
using TAC.Sitecore.Abstractions.SitecoreImplementation;

namespace events.tac.local.Business
{
    public class NavigationBuilder
    {
        private readonly IRenderingContext _context;

        public NavigationBuilder() : this(SitecoreRenderingContext.Create())
        {
        }

        public NavigationBuilder(IRenderingContext context)
        {
            this._context = context;
        }

        public NavigationMenuItem Build()
        {
            IItem root = _context?.DatasourceOrContextItem;
            IEnumerable<NavigationMenuItem> children = root != null && _context?.ContextItem != null ? Build(root, _context.ContextItem) : null;

            return new NavigationMenuItem(root?.DisplayName, root?.Url, children);
        }

        private IEnumerable<NavigationMenuItem> Build(IItem node, IItem current)
        {
            return node.GetChildren()
                .Select(i => new NavigationMenuItem(i.DisplayName, i.Url, i.IsAncestorOrSelf(current) ? Build(i, current) : null));
        }
    }
}