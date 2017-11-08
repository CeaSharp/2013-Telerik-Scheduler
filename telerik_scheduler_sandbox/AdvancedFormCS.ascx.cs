using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using telerik_scheduler_sandbox.dL;

namespace telerik_scheduler_sandbox
{
    public enum AdvancedFormMode { Insert, Edit }

    public partial class AdvancedFormCS : System.Web.UI.UserControl
    {
        private AdvancedFormMode mode = AdvancedFormMode.Insert;
        private bool FormInitialized
        {
            get { return (bool)(ViewState["FormInitialized"] ?? false); }
            set { ViewState["FormInitialized"] = value; }
        }

        protected RadScheduler Owner { get { return Appointment.Owner; } }
        protected Appointment Appointment { get { SchedulerFormContainer container = (SchedulerFormContainer)BindingContainer; return container.Appointment; } }
        public Int32? _program
        {
            get
            {
                return HttpContext.Current.Session["CurrentProgram"] != null ? Convert.ToInt32(HttpContext.Current.Session["CurrentProgram"].ToString()) : Convert.ToInt32(null);
            }
        }
        public AdvancedFormMode Mode
        { get; set; }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public string DetailID { get; set; }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public string TalkID
        {
            get { return cbtalk.SelectedItem.Value; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    cbtalk.SelectedItem.Value = value.ToString();
                    cbtalk.SelectedValue = value.ToString();
                }
                else
                    return; 
            }
        }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public string StandardSlotID
        {
            get { return cbstandard.SelectedItem.Value; }
            set { cbstandard.SelectedItem.Value = value.ToString(); cbstandard.SelectedValue = value.ToString(); }
        }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public string VenueID
        {
            get { return cbvenue.SelectedItem.Value; }
            set { cbvenue.SelectedItem.Value = value.ToString(); cbvenue.SelectedValue = value.ToString(); }
        }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public String CustomSlotTitle
        {
            get { return txcustomslot.Text; }
            set { txcustomslot.Text = value; }
        }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public String Subject
        {
            get{return String.Empty;}
            set{value = value;}
        }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public String RecurrenceRuleText
        {
            get
            {
                if (Owner.RecurrenceSupport)
                {
                    bool dateSpecified = StartDate.SelectedDate.HasValue && EndDate.SelectedDate.HasValue;
                    bool timeSpecified = StartTime.SelectedDate.HasValue && EndTime.SelectedDate.HasValue;

                    if ((AllDayEvent.Checked && !dateSpecified) ||
                        (!AllDayEvent.Checked && !(dateSpecified && timeSpecified)))
                    {
                        return string.Empty;
                    }

                    AppointmentRecurrenceEditor.StartDate = Start;
                    AppointmentRecurrenceEditor.EndDate = End;

                    RecurrenceRule rrule = AppointmentRecurrenceEditor.RecurrenceRule;

                    if (rrule == null)
                    {
                        return string.Empty;
                    }

                    RecurrenceRule originalRule;
                    if (RecurrenceRule.TryParse(OriginalRecurrenceRule.Value, out originalRule))
                    {
                        rrule.Exceptions = originalRule.Exceptions;
                    }

                    if (rrule.Range.RecursUntil != DateTime.MaxValue)
                    {
                        rrule.Range.RecursUntil = Owner.DisplayToUtc(rrule.Range.RecursUntil);

                        if (!AllDayEvent.Checked)
                        {
                            rrule.Range.RecursUntil = rrule.Range.RecursUntil.AddDays(1);
                        }
                    }

                    return rrule.ToString();
                }

                return string.Empty;
            }

            set
            {
                RecurrenceRule rrule;
                RecurrenceRule.TryParse(value, out rrule);

                if (rrule != null)
                {
                    if (rrule.Range.RecursUntil != DateTime.MaxValue)
                    {
                        DateTime recursUntil = Owner.UtcToDisplay(rrule.Range.RecursUntil);

                        if (!IsAllDayAppointment(Appointment))
                        {
                            recursUntil = recursUntil.AddDays(-1);
                        }

                        rrule.Range.RecursUntil = recursUntil;
                    }

                    AppointmentRecurrenceEditor.RecurrenceRule = rrule;

                    OriginalRecurrenceRule.Value = value;
                }
            }
        }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public DateTime Start
        {
            get
            {
                DateTime result = StartDate.SelectedDate.Value.Date;

                if (AllDayEvent.Checked)
                {
                    result = result.Date;
                }
                else
                {
                    TimeSpan time = StartTime.SelectedDate.Value.TimeOfDay;
                    result = result.Add(time);
                }

                return Owner.DisplayToUtc(result);
            }

            set
            {
                StartDate.SelectedDate = Owner.UtcToDisplay(value);
                StartTime.SelectedDate = Owner.UtcToDisplay(value);
            }
        }
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public DateTime End
        {
            get
            {
                DateTime result = EndDate.SelectedDate.Value.Date;

                if (AllDayEvent.Checked)
                {
                    result = result.Date.AddDays(1);
                }
                else
                {
                    TimeSpan time = EndTime.SelectedDate.Value.TimeOfDay;
                    result = result.Add(time);
                }

                return Owner.DisplayToUtc(result);
            }

            set
            {
                EndDate.SelectedDate = Owner.UtcToDisplay(value);
                EndTime.SelectedDate = Owner.UtcToDisplay(value);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateButton.ValidationGroup = Owner.ValidationGroup;
            UpdateButton.CommandName = Mode == AdvancedFormMode.Edit ? "Update" : "Insert";

            binddata_venues();
            binddata_talk();
            binddata_standardslots();
            InitializeString();
            if (!FormInitialized)
                UpdateResetExceptionsVisibility();
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!FormInitialized)
            {
                if (IsAllDayAppointment(Appointment))
                    EndDate.SelectedDate = EndDate.SelectedDate.Value.AddDays(-1);
                FormInitialized = true;
            }
            if (String.IsNullOrEmpty(Appointment.Attributes["VenueID"]))
                cbvenue.SelectedIndex = -1;
            else
                cbvenue.SelectedValue = Appointment.Attributes["VenueID"].ToString();
            if (String.IsNullOrEmpty(Appointment.Attributes["TalkID"]))
                cbtalk.SelectedIndex = -1;
            else
                cbtalk.SelectedValue = Appointment.Attributes["TalkID"].ToString();
            if (String.IsNullOrEmpty(Appointment.Attributes["StandardSlotID"]))
                cbstandard.SelectedIndex = -1;
            else
                cbstandard.SelectedValue = Appointment.Attributes["StandardSlotID"].ToString();
        }
        protected void BasicControlsPanel_DataBinding(object sender, EventArgs e)
        { AllDayEvent.Checked = IsAllDayAppointment(Appointment); }
        protected void DurationValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        { args.IsValid = (End - Start) > TimeSpan.Zero; }
        protected void ResetExceptions_OnClick(object sender, EventArgs e)
        {
            Owner.RemoveRecurrenceExceptions(Appointment);
            Owner.Rebind();
            Owner.ShowAdvancedEditForm(Appointment, true);
            OriginalRecurrenceRule.Value = Appointment.RecurrenceRule;
            ResetExceptions.Text = Owner.Localization.AdvancedDone;
        }

        private void InitializeString()
        {
            AllDayEvent.Text = Owner.Localization.AdvancedAllDayEvent;

            StartDateValidator.ErrorMessage = Owner.Localization.AdvancedStartDateRequired;
            StartDateValidator.ValidationGroup = Owner.ValidationGroup;

            StartTimeValidator.ErrorMessage = Owner.Localization.AdvancedStartTimeRequired;
            StartTimeValidator.ValidationGroup = Owner.ValidationGroup;

            EndDateValidator.ErrorMessage = Owner.Localization.AdvancedEndDateRequired;
            EndDateValidator.ValidationGroup = Owner.ValidationGroup;

            EndTimeValidator.ErrorMessage = Owner.Localization.AdvancedEndTimeRequired;
            EndTimeValidator.ValidationGroup = Owner.ValidationGroup;

            DurationValidator.ErrorMessage = Owner.Localization.AdvancedStartTimeBeforeEndTime;
            DurationValidator.ValidationGroup = Owner.ValidationGroup;

            ResetExceptions.Text = Owner.Localization.AdvancedReset;

            SharedCalendar.FastNavigationSettings.OkButtonCaption = Owner.Localization.AdvancedCalendarOK;
            SharedCalendar.FastNavigationSettings.CancelButtonCaption = Owner.Localization.AdvancedCalendarCancel;
            SharedCalendar.FastNavigationSettings.TodayButtonCaption = Owner.Localization.AdvancedCalendarToday;
        }

        private void InitializeRecurrenceEditor()
        {
            AppointmentRecurrenceEditor.SharedCalendar = SharedCalendar;
            AppointmentRecurrenceEditor.Culture = Owner.Culture;
            AppointmentRecurrenceEditor.StartDate = Appointment.Start;
            AppointmentRecurrenceEditor.EndDate = Appointment.End;
        }

        private void UpdateResetExceptionsVisibility()
        {
            // Render the reset exceptions checkbox when using web service binding
            if (String.IsNullOrEmpty(Owner.WebServiceSettings.Path))
            {
                ResetExceptions.Visible = false;
                RecurrenceRule rrule;
                try
                {
                    if (RecurrenceRule.TryParse(Appointment.RecurrenceRule, out rrule))
                    {
                        ResetExceptions.Visible = rrule.Exceptions.Count > 0;
                    }
                }
                catch (Exception ex)
                { String _s = "In here"; }
            }
        }

        private bool IsAllDayAppointment(Appointment appointment)
        {
            DateTime displayStart = Owner.UtcToDisplay(appointment.Start);
            DateTime displayEnd = Owner.UtcToDisplay(appointment.End);
            return displayStart.CompareTo(displayStart.Date) == 0 && displayEnd.CompareTo(displayEnd.Date) == 0;
        }

        private void binddata_venues()
        { bL.Venue.bind_combobox(this.cbvenue); }

        private void binddata_standardslots()
        { bL.StandardSlot.bind_combobox(this.cbstandard); }

        private void binddata_talk()
        { bL.Talk.bind_combobox(this.cbtalk, Convert.ToInt32(_program)); }
    }
}