using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace telerik_scheduler_sandbox.bL
{
    public class StandardSlot :basebusiness
    {
        public static void bind_combobox(RadComboBox _obj)
        {
            HardList(ref _obj);
            /*_obj.DataSource = dL.StandardSlot.List();
            _obj.DataTextField = "slotinfoname";
            _obj.DataValueField = "slotinfoid";
            _obj.DataBind();*/
        }

        private static void HardList(ref RadComboBox _obj)
        {
            _obj.Items.Add(new RadComboBoxItem("Check-In/Breakfast (Hosted by IPAM)", "1"));
            _obj.Items.Add(new RadComboBoxItem("Breakfast", "2"));
        }
    }
}