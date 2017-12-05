using ipam_generationDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Telerik.Web.UI;

namespace telerik_scheduler_sandbox.dL
{
    public class Schedule : basedata
    {
        private const string _venueID = "76";
        private const string _venuename = "IPAM Building";
        public const string AppointmentsKey = "ipam.test.schedule.appointments";

        public static List<scheduleheader> List(Int32? _id)
        { return _list(_id); }
        private static List<scheduleheader> _list(Int32? _id)
        {
            using (_ge = new generation_entities())
            {
                program _p = (from a in _ge.programs where a.programID == 773 select a).FirstOrDefault();
                if (_id != null)
                {
                    List<scheduleheader> _tlist = new List<scheduleheader>();
                    _tlist = (from a in _ge.scheduleheaders
                                .Include(p => p.scheduledetails)
                                .Include(p => p.scheduledetails.Select(x => x.scheddtlcustoms))
                                .Include(p => p.scheduledetails.Select(x => x.scheddtlstandards))
                                .Include(p => p.scheduledetails.Select(x => x.scheddtltalks))
                                .Include(p => p.scheduledetails.Select(x => x.scheddtlvenues))
                                .Include(p => p.scheduledetails.Select(x => x.scheduledtlrecurrences))
                            where a.programID == _id select a).ToList();
                    return _tlist;
                }
                else
                    return (from a in _ge.scheduleheaders where a.scheduledetails.Any(b => b.start > DateTime.Today.AddDays(-30)) && a.programID == null select a).ToList();
            }
        }
        
        public class AppointmentInfo
        {
            public string ID { get; set; }
            public string CustomSlotTitle { get; set; }
            public string Description { get; set; }
            public string HeaderID { get; set; }
            public string DetailID { get; set; }
            public DateTime End { get; set; }
            public string RecurrenceParentID { get; set; }
            public string RecurrenceRule { get; set; }
            public string Reminder { get; set; }
            public string StandardSlotID { get; set; }
            public string StandardSlotTitle { get; set; }
            public DateTime Start { get; set; }
            public string Subject { get; set; }
            public string TalkID { get; set; }
            public string TalkTitle { get; set; }
            public string VenueID { get; set; }
            public string VenueName { get; set; }

            private AppointmentInfo()
            { ID = Guid.NewGuid().ToString(); }

            public AppointmentInfo(string _hdrid, string _dtlid, string _speaker, string _talkid, string _talktitle, string _standardID, string _standardname, string _custom, string _venueid, string _venuename, DateTime _start, DateTime _end, string _recrule, string _recparentid) : this()
            {
                CustomSlotTitle = !String.IsNullOrEmpty(_custom) ? _custom : "";
                Description = "";
                HeaderID = _hdrid;
                DetailID = _dtlid;
                End = _end;
                RecurrenceParentID = _recparentid;
                RecurrenceRule = _recrule;
                Reminder = "";
                StandardSlotID = !String.IsNullOrEmpty(_standardID) ? _standardID : "";
                StandardSlotTitle = !String.IsNullOrEmpty(_standardname) ? _standardname : "";
                Start = _start;
                Subject = !String.IsNullOrEmpty(_talktitle) ? String.Format("{0} - {1}", _speaker, _talktitle ) : !String.IsNullOrEmpty(_standardname) ? _standardname : !String.IsNullOrEmpty(_custom) ? _custom : "";
                TalkID = !String.IsNullOrEmpty(_talkid) ? _talkid : "";
                TalkTitle = !String.IsNullOrEmpty(_talktitle) ? _talktitle : "";
                VenueID = !String.IsNullOrEmpty(_venueid) ? _venueid : "";
                VenueName = !String.IsNullOrEmpty(_venuename) ? _venuename : "";
            }

            /*public AppointmentInfo(string _dtlid, scheddtltalk _talk, scheddtlstandard _standardslot, scheddtlcustom _customslot, scheddtlvenue _venue, DateTime _start, DateTime _end, string _recrule, string _recparentid)
                : this()
            {
                CustomSlotTitle = _customslot.customID != 0 ? _customslot.customslot : String.Empty;
                Description = String.Empty;
                DetailID = _dtlid;
                End = _end;
                RecurrenceParentID = _recparentid;
                RecurrenceRule = _recrule;
                Reminder = String.Empty;
                StandardSlotID = _standardslot.slotinfo != null ? _standardslot.slotinfo.slotinfoID.ToString() : String.Empty;
                StandardSlotTitle = _standardslot.slotinfo != null ? _standardslot.slotinfo.slotinfoname : String.Empty;
                Start = _start;
                Subject = _talk.talk != null ? _talk.talk.title : _standardslot.slotinfo != null ? _standardslot.slotinfo.slotinfoname : _customslot != null ? _customslot.customslot : String.Empty;
                TalkID = _talk.talk != null ? _talk.talkID.ToString() : String.Empty;
                TalkTitle = _talk.talk != null ? _talk.talk.title : String.Empty;
                VenueID = _venue.venue != null ? _venue.venueID.ToString() : _venueID;
                VenueName = _venue.venue != null ? _venue.venue.venuename : _venuename;
            }*/

            public AppointmentInfo(Appointment source)
                : this()
            { CopyInfo(source); }

            public void CopyInfo(Appointment source)
            {
                Subject = source.Subject;
                Start = source.Start;
                End = source.End;
                Description = source.Description;
                RecurrenceRule = source.RecurrenceRule;
                RecurrenceParentID = source.RecurrenceParentID != null ? source.RecurrenceParentID.ToString() : string.Empty;
                CustomSlotTitle = source.Attributes["CustomSlotTitle"].ToString();
                HeaderID = source.Attributes["HeaderID"].ToString();
                DetailID = source.Attributes["DetailID"].ToString();
                StandardSlotID = source.Attributes["StandardSlotID"].ToString();
                TalkID = source.Attributes["TalkID"].ToString();
                VenueID = source.Attributes["VenueID"].ToString();
            }
        }

        public static void process(Int32? _hdrid, Int32? _dtlid, Int32 _pid, Int32? _talkid, Int32 _standardid, string _custom, Int32? _venueid, DateTime _start, DateTime _end, string _recrule)
        {
            
        }
    }
}