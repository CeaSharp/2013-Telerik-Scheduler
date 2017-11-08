using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace telerik_scheduler_sandbox.bL
{
    public class Talk :basebusiness
    {
        public static void bind_combobox(RadComboBox _obj, Int32 _id)
        {
            _obj.DataSource = dL.Talk.List(_id);
            _obj.DataTextField = "title";
            _obj.DataValueField = "talkid";
            _obj.DataBind();
        }
    }
}