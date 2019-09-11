namespace events.tac.local.Models
{
    public class NavigationItem
    {
        public string Title { get; private set; }
        public string URL { get; private set; }
        public bool Active { get; private set; }

        public NavigationItem(string title, string url, bool active = false)
        {
            this.Title = title;
            this.URL = url;
            this.Active = active;
        }
    }
}