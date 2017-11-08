using ipam_generationDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace telerik_scheduler_sandbox.dL
{
    public class Talk : basedata
    {
        public static List<talk> List(Int32 _id)
        { return _list(_id); }
        public static scheddtltalk Single(Int32 _id)
        { return _single(_id); }
        private static List<talk> _list(Int32 _id)
        {
            try
            {
                using (_ge = new generation_entities())
                { return (from a in _ge.talks where a.talkprograms.Any(b => b.programID == _id) select a).ToList(); }
            }
            catch (Exception ex)
            { return null; }
        }
        private static scheddtltalk _single(Int32 _id)
        {
            try
            {
                using (_ge = new generation_entities())
                { return (from a in _ge.scheddtltalks.Include(p => p.talk) where a.scheddtltalkID == _id select a).FirstOrDefault(); }
            }
            catch (Exception ex)
            { return null; }
        }
        public class Speaker
        {
            public static talkspeaker Single(Int32 _id)
            { return _single(_id); }
            private static talkspeaker _single(Int32 _id)
            {
                try
                { 
                    using (_ge = new generation_entities())
                    { return (from a in _ge.talkspeakers.Include(p => p.participant.participantprofile) where a.talkID == _id select a).FirstOrDefault(); }
                }
                catch (Exception ex)
                { return null; }
            }
        }
    }
}