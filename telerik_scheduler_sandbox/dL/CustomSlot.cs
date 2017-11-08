using ipam_generationDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace telerik_scheduler_sandbox.dL
{
    public class CustomSlot : basedata
    {
        public static scheddtlcustom Single(Int32 _id)
        { return _single(_id); }
        public static scheddtlcustom _single(Int32 _id)
        {
            try
            {
                using (_ge = new generation_entities())
                {
                    return (from a in _ge.scheddtlcustoms where a.customID == _id select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            { return null; }
        }
    }
}