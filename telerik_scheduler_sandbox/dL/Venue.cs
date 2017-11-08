using ipam_generationDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace telerik_scheduler_sandbox.dL
{
    public class Venue : basedata
    {
        public static List<venue> List()
        { return _list(); }
        public static scheddtlvenue Single(Int32 _id)
        { return _single(_id); }
        private static List<venue> _list()
        {
            try
            {
                using (_ge = new generation_entities())
                { return (from a in _ge.venues orderby a.venuename select a).ToList(); }
            }
            catch (Exception ex)
            { return null; }
        }
        private static scheddtlvenue _single(Int32 _id)
        {
            try
            {
                using (_ge = new generation_entities())
                { return (from a in _ge.scheddtlvenues.Include(p => p.venue) where a.dtlvenueID == _id select a).FirstOrDefault(); }
            }
            catch (Exception ex)
            { return null; }
        }
    }
}