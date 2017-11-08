using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace telerik_scheduler_sandbox
{
    public partial class scheduler : System.Web.UI.UserControl
    {
        private const string AppointmentsKey = "AppointmentCollection";
        private const Int32 _programid = 773;
        protected override void OnInit(EventArgs e)
        {
            ScriptManager manager = RadScriptManager.GetCurrent(Page);
            manager.Scripts.Add(new ScriptReference(ResolveUrl("js/AdvancedForm.js")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove(AppointmentsKey);
                Session["CurrentProgram"] = _programid;
                InitializeResources();
                InitializeAppointments();
            }
            this.radcalendar.DataSource = bL.Schedule.Appointments;
        }

        private void InitializeResources()
        {
            bL.Venue.bind_resources(ref this.radcalendar);
        }
        private void InitializeAppointments()
        {
            bL.Schedule.bind_appointments();
            //bL.Schedule.bind_appointments(_programid);
        }
        protected void radcalendar_AppointmentInsert(object sender, AppointmentInsertEventArgs e)
        {
            int _eid = 0;
            _eid = e.Appointment.ID != null ? Convert.ToInt32(e.Appointment.ID.ToString()) : 0;
            if (_eid == 0)
            {
                //ach.UpdateActivity(ref _eid, e.Appointment.Subject, Convert.ToInt32(e.Appointment.Attributes["VenueID"].ToString()), e.Appointment.Description, e.Appointment.Start, e.Appointment.End, Convert.ToInt32(e.Appointment.Attributes["EventTypeID"].ToString()), "ADD", e.Appointment.RecurrenceRule);
            }
            bL.Schedule.Appointments.Add(new telerik_scheduler_sandbox.dL.Schedule.AppointmentInfo(e.Appointment));
            this.radcalendar.Rebind();
        }

        protected void radcalendar_AppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {
            int _eid = 0;
            _eid = e.Appointment.ID.ToString() != "" ? Convert.ToInt32(e.Appointment.ID.ToString()) : 0;
            /*switch (e.ModifiedAppointment.Attributes["EventTypeID"].ToString())
            {
                case "999":
                    ach.UpdateActivity(ref _eid, e.ModifiedAppointment.Subject, Convert.ToInt32(e.ModifiedAppointment.Attributes["VenueID"].ToString()), e.ModifiedAppointment.Description, e.ModifiedAppointment.Start, e.ModifiedAppointment.End, Convert.ToInt32(e.ModifiedAppointment.Attributes["EventTypeID"].ToString()), "UPDATE", e.ModifiedAppointment.RecurrenceRule);
                    break;
                case "7":
                case "8":
                    Application_Database_Helper.InsertEventRecurrence(_eid, null, e.ModifiedAppointment.RecurrenceRule);
                    break;
            }*/
            telerik_scheduler_sandbox.dL.Schedule.AppointmentInfo ai = FindById(e.ModifiedAppointment.ID.ToString());
            ai.CopyInfo(e.ModifiedAppointment);
            this.radcalendar.Rebind();
        }

        protected void radcalendar_AppointmentDelete(object sender, AppointmentDeleteEventArgs e)
        {
            int _eid = 0;
            _eid = e.Appointment.ID.ToString() != "" ? Convert.ToInt32(e.Appointment.ID.ToString()) : 0;

            /*switch (e.Appointment.Attributes["EventTypeID"].ToString())
            {
                case "999":
                    ach.DeleteActivity(_eid, "MANUAL");
                    Appointments.Remove(FindById(e.Appointment.ID.ToString()));
                    break;
                case "7":
                case "8":
                    Application_Database_Helper.InsertEventRecurrence(_eid, null, "");
                    Appointments.Remove(FindById(e.Appointment.RecurrenceParentID.ToString()));
                    break;
            }*/
            this.radcalendar.Rebind();
        }
        protected void radcalendar_FormCreating(object sender, SchedulerFormCreatingEventArgs e)
        {
            if (e.Appointment.ID != null)
            {
                /*switch (e.Appointment.Attributes["EventTypeID"].ToString())
                {
                    case "999":
                    case "7":
                    case "8":
                        if (e.Appointment.ID != null)
                        {
                            if (e.Appointment.Attributes["EventTypeID"].ToString() == "7" || e.Appointment.Attributes["EventTypeID"].ToString() == "8")
                            {
                                if ((e.Appointment.RecurrenceParentID == null) && e.Appointment.Attributes["EventTypeID"].ToString() != "999")
                                {
                                    e.Cancel = true;
                                    Response.Redirect("IPAM_Workshops/IPAMWorkshops_Workshop_EventInformation.aspx?eid=" + e.Appointment.ID.ToString());
                                    break;
                                }
                            }
                            e.Appointment.Attributes["EventTypeID"] = e.Appointment.Attributes["EventTypeID"].ToString();
                        }
                        break;
                    default:
                        e.Cancel = true;
                        Response.Redirect("IPAM_Workshops/IPAMWorkshops_Workshop_EventInformation.aspx?eid=" + e.Appointment.ID.ToString());
                        break;
                }*/
            }
            else
            {
                /*e.Appointment.Attributes["EventTypeID"] = "999";*/
            }
        }

        protected void radcalendar_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            switch (e.Appointment.Attributes["VenueID"].ToString())
            {
                case "1000":
                    e.Appointment.CssClass = "rsResourceEvent" + e.Appointment.Attributes["VenueID"].ToString();
                    if (e.Appointment.Start.Date.AddDays(1).ToShortDateString() != e.Appointment.End.Date.ToShortDateString())
                    {
                        if (e.Appointment.Start.ToShortTimeString() == "12:00 AM")
                        {
                            e.Appointment.End = e.Appointment.End.AddHours(23).AddMinutes(59);
                        }
                    }
                    break;
                case "1001":
                    e.Appointment.CssClass = "rsResourceEvent" + e.Appointment.Attributes["VenueID"].ToString();
                    if (e.Appointment.Start.Date.AddDays(1).ToShortDateString() != e.Appointment.End.Date.ToShortDateString())
                    {
                        if (e.Appointment.Start.ToShortTimeString() == "12:00 AM")
                        {
                            e.Appointment.End = e.Appointment.End.AddHours(23).AddMinutes(59);
                        }
                    }
                    break;
                case "1002":
                    e.Appointment.CssClass = "rsResourceEvent" + e.Appointment.Attributes["VenueID"].ToString();
                    if (e.Appointment.Start.Date.AddDays(1).ToShortDateString() != e.Appointment.End.Date.ToShortDateString())
                    {
                        if (e.Appointment.Start.ToShortTimeString() == "12:00 AM")
                        {
                            e.Appointment.End = e.Appointment.End.AddHours(23).AddMinutes(59);
                        }
                    }
                    break;
                case "1026":
                    e.Appointment.CssClass = "rsResourceEvent" + e.Appointment.Attributes["VenueID"].ToString();
                    if (e.Appointment.Start.Date.AddDays(1).ToShortDateString() != e.Appointment.End.Date.ToShortDateString())
                    {
                        if (e.Appointment.Start.ToShortTimeString() == "12:00 AM")
                        {
                            e.Appointment.End = e.Appointment.End.AddHours(23).AddMinutes(59);
                        }
                    }
                    break;
                default:
                    e.Appointment.CssClass = "rsResourceEvent9999";
                    if (e.Appointment.Start.Date.AddDays(1).ToShortDateString() != e.Appointment.End.Date.ToShortDateString())
                    {
                        if (e.Appointment.Start.ToShortTimeString() == "12:00 AM")
                        {
                            e.Appointment.End = e.Appointment.End.AddHours(23).AddMinutes(59);
                        }
                    }
                    break;
            }
        }

        protected void radcalendar_RecurrenceExceptionCreated(object sender, RecurrenceExceptionCreatedEventArgs e)
        {}

        protected void radcalendar_OccurrenceDelete(object sender, OccurrenceDeleteEventArgs e)
        { }

        public telerik_scheduler_sandbox.dL.Schedule.AppointmentInfo FindById(object ID)
        {
            foreach (telerik_scheduler_sandbox.dL.Schedule.AppointmentInfo ai in bL.Schedule.Appointments)
            {
                if (ai.DetailID == ID.ToString())
                {
                    return ai;
                }
            }
            return null;
        }
    }
}