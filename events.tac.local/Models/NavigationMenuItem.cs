using System.Collections.Generic;

namespace events.tac.local.Models
{
    public class NavigationMenuItem : NavigationItem
    {
        public IEnumerable<NavigationMenuItem> Children { get; private set; }

        public NavigationMenuItem(string title, string url, IEnumerable<NavigationMenuItem> children) : base (title, url, false)
        {
            this.Children = children != null ? children : new List<NavigationMenuItem>();
        }
    }
}