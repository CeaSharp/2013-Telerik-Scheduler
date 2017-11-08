using ipam_generationDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace telerik_scheduler_sandbox.dL
{
    public class StandardSlot : basedata
    {
        public static List<slotinfo> List()
        { return _list(); }
        public static scheddtlstandard Single(Int32 _id)
        { return _single(_id); }
        private static List<slotinfo> _list()
        {
            try
            {
                using (_ge = new generation_entities())
                { return (from a in _ge.slotinfoes where a.isactive == true select a).ToList(); }
            }
            catch (Exception ex)
            { return null; }
        }
        private static scheddtlstandard _single(Int32 _id)
        {
            try
            {
                using (_ge = new generation_entities())
                { return (from a in _ge.scheddtlstandards.Include(p => p.slotinfo) where a.standardID == _id select a).FirstOrDefault(); }
            }
            catch (Exception ex)
            { return null; }
        }
    }
}