using ipam_generationDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace telerik_scheduler_sandbox.bL
{
    public class Venue : basebusiness
    {
        public static void bind_combobox(RadComboBox _obj)
        {
            HardList(ref _obj);
            /*_obj.DataSource = dL.Venue.List();
            _obj.DataTextField = "venuename";
            _obj.DataValueField = "venueid";
            _obj.DataBind();*/
        }

        private static void HardList(ref RadComboBox _obj)
        {
            _obj.Items.Add(new RadComboBoxItem("IPAM Building", "1"));
            _obj.Items.Add(new RadComboBoxItem("IPAM Building - Room 1200", "2"));
        }
        public static List<venue> List()
        {return dL.Venue.List();}

        public static void bind_resources(ref RadScheduler _obj)
        {
            foreach (venue _item in List())
            {
                _obj.Resources.Add(new Resource("Venue", _item.venueID, _item.venuename));
            }
        }
    }
}