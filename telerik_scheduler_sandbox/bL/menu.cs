using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using telerik_scheduler_sandbox.dL;

namespace telerik_scheduler_sandbox.bL
{
    public class menu
    {
        public static void BindToIEnumerable(RadMenu _menu)
        {
            List<menuitem> menuItem = new List<menuitem>();
            menuItem.Add(new menuitem(2, null, "Search Engines", ""));
            menuItem.Add(new menuitem(3, null, "News Sites", ""));
            menuItem.Add(new menuitem(4, 2, "Yahoo", "http://www.yahoo.com"));
            menuItem.Add(new menuitem(5, 2, "MSN", "http://www.msn.com"));
            menuItem.Add(new menuitem(6, 2, "Google", "http://www.google.com"));
            menuItem.Add(new menuitem(7, 3, "CNN", "http://www.cnn.com"));
            menuItem.Add(new menuitem(8, 3, "BBC", "http://www.bbc.co.uk"));
            menuItem.Add(new menuitem(9, 3, "FOX", "http://www.foxnews.com"));

            _menu.DataTextField = "Text";
            _menu.DataNavigateUrlField = "Url";
            _menu.DataFieldID = "ID";
            _menu.DataFieldParentID = "ParentID";
            _menu.DataSource = menuItem;
            _menu.DataBind();
        }
    }
}