using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPKTOnline.Management.Menu
{
    public interface IMenu<T>
    {
        T Parent { get; set; }
        List<T> Children { get; set; }
    }

    public class Menu : IMenu<Menu>
    {
        public Menu()
        { }

        public Menu(string Title, string Link, bool IsActive)
        {
            this.Title = Title;
            this.Link = Link;
            this.IsActive = IsActive;
        }

        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public Menu Parent { get; set; }
        public List<Menu> Children { get; set; }
    }
}