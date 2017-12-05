using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace telerik_scheduler_sandbox
{
    public partial class master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bL.menu.BindToIEnumerable(basemenu);
            }
        }
        protected void basemenu_ItemDataBound(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
            //idashboard.menunavigation _o = (idashboard.menunavigation)e.Item.DataItem;
            //e.Item.ToolTip = _o._description;            
            e.Item.ToolTip = e.Item.Text;
        }
    }
}