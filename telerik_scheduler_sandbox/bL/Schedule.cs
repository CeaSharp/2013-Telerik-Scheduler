using ipam_generationDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using telerik_scheduler_sandbox;
using telerik_scheduler_sandbox.dL;

namespace telerik_scheduler_sandbox.bL
{
    public class Schedule : basebusiness
    {
        public static List<dL.Schedule.AppointmentInfo> Appointments
        {
            get
            {
                List<dL.Schedule.AppointmentInfo> sessApts = HttpContext.Current.Session[dL.Schedule.AppointmentsKey] as List<dL.Schedule.AppointmentInfo>;
                if (sessApts == null)
                {
                    sessApts = new List<dL.Schedule.AppointmentInfo>();
                    HttpContext.Current.Session[dL.Schedule.AppointmentsKey] = sessApts;
                }
                return sessApts;
            }
        }
        public static void bind_appointments()
        {
            Appointments.Add(new dL.Schedule.AppointmentInfo("10489", "", "", "1", "Check-In/Breakfast (Hosted by IPAM)", "", "", "", Convert.ToDateTime("12/4/2017 8:30:00 AM"), Convert.ToDateTime("12/4/2017 8:45:00 AM"), "", ""));
            Appointments.Add(new dL.Schedule.AppointmentInfo("2", "", "", "2", "Breakfast", "", "1", "IPAM Building", Convert.ToDateTime("12/1/2017 09:00:00 AM"), Convert.ToDateTime("12/1/2017 09:30:00 AM"), "", ""));
            Appointments.Add(new dL.Schedule.AppointmentInfo("3", "", "", "", "", "Side Panel with Daniel Burke", "2", "IPAM Building - Room 1200", Convert.ToDateTime("12/1/2017 09:30:00 AM"), Convert.ToDateTime("12/1/2017 10:30:00 AM"), "", ""));
        }
        public static void bind_appointments(Int32 _programID)
        {
            List<scheduleheader> _list = dL.Schedule.List(_programID);
            foreach (scheduleheader _hdritem in _list)
            {
                foreach (scheduledetail _item in _hdritem.scheduledetails)
                {
                    scheddtlcustom _custom = _item.scheddtlcustoms.Count > 0 ? CustomSlot.Single(_item.scheddtlcustoms.ElementAt(0).customID) : new scheddtlcustom();
                    scheddtlstandard _standard = _item.scheddtlstandards.Count > 0 ? dL.StandardSlot.Single(_item.scheddtlstandards.ElementAt(0).standardID) : new scheddtlstandard();
                    scheddtltalk _talk = _item.scheddtltalks.Count > 0 ? dL.Talk.Single(_item.scheddtltalks.ElementAt(0).scheddtltalkID) : new scheddtltalk();
                    talkspeaker _talkspeaker = _talk.talk != null ? dL.Talk.Speaker.Single(_talk.talk.talkID) : null;
                    scheddtlvenue _venue = _item.scheddtlvenues.Count > 0 ? dL.Venue.Single(_item.scheddtlvenues.ElementAt(0).dtlvenueID) : new scheddtlvenue();
                    scheduledtlrecurrence _recurrence = _item.scheduledtlrecurrences.FirstOrDefault();
                    string _talkid = _talk.talk != null ? _talk.talkID.ToString() : String.Empty;
                    string _talktitle = _talk.talk != null ? _talk.talk.title : String.Empty;
                    string _speaker = _talkspeaker != null ? _talkspeaker.participant.participantprofile.lastname : String.Empty;
                    string _standardid = _standard.slotinfo != null ? _standard.standardslotID.ToString() : String.Empty;
                    string _standardtitle = _standard.slotinfo != null ? _standard.slotinfo.slotinfoname : String.Empty;
                    string _customtitle = _custom.customslot != null ? _custom.customslot : String.Empty;
                    string _venueid = _venue.venue != null ? _venue.venueID.ToString() : String.Empty;
                    string _venuename = _venue.venue != null ? _venue.venue.venuename : String.Empty;
                    string _recrule = _recurrence != null ? _recurrence.recurrence : String.Empty;
                    string _recparentid = _recurrence != null ? _recurrence.dtlID.ToString() : String.Empty;

                    Appointments.Add(new dL.Schedule.AppointmentInfo(_item.dtlID.ToString(), _talkid, _talktitle, _standardid, _standardtitle, _customtitle, _venueid, _venuename, _item.start, _item.end, _recrule, _recparentid));
                    //Appointments.Add(new dL.Schedule.AppointmentInfo(_item.dtlID.ToString(), _talk, _standard, _custom, _venue, _item.start, _item.end, _recurrence != null ? _recurrence.recurrence : String.Empty, _recurrence != null ? _recurrence.dtlID.ToString() : String.Empty));
                }
            }
        }

        public telerik_scheduler_sandbox.dL.Schedule.AppointmentInfo FindById(object _id)
        {
            foreach (telerik_scheduler_sandbox.dL.Schedule.AppointmentInfo _ai in Appointments)
            {
                if (_ai.DetailID == _id.ToString())
                    return _ai;
            }
            return null;
        }
    }
}