<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="scheduler.ascx.cs" Inherits="telerik_scheduler_sandbox.scheduler" %>
<%@ Register TagPrefix="scheduler" TagName="AdvancedForm" Src="~/AdvancedFormCS.ascx" %>

    <script type="text/javascript">
        //<![CDATA[

        // Dictionary containing the advanced template client object
        // for a given RadScheduler instance (the control ID is used as key).
        var schedulerTemplates = {};

        function schedulerFormCreated(scheduler, eventArgs) {
            // Create a client-side object only for the advanced templates
            var mode = eventArgs.get_mode();
            if (mode == Telerik.Web.UI.SchedulerFormMode.AdvancedInsert ||
                mode == Telerik.Web.UI.SchedulerFormMode.AdvancedEdit) {
                // Initialize the client-side object for the advanced form
                var formElement = eventArgs.get_formElement();
                var templateKey = scheduler.get_id() + "_" + mode;
                var advancedTemplate = schedulerTemplates[templateKey];
                if (!advancedTemplate) {
                    // Initialize the template for this RadScheduler instance
                    // and cache it in the schedulerTemplates dictionary
                    var schedulerElement = scheduler.get_element();
                    var isModal = scheduler.get_advancedFormSettings().modal;
                    advancedTemplate = new window.SchedulerAdvancedTemplate(schedulerElement, formElement, isModal);
                    advancedTemplate.initialize();

                    schedulerTemplates[templateKey] = advancedTemplate;

                    // Remove the template object from the dictionary on dispose.
                    scheduler.add_disposing(function () {
                        schedulerTemplates[templateKey] = null;
                    });
                }

                // Are we using Web Service data binding?
                if (!scheduler.get_webServiceSettings().get_isEmpty()) {
                    // Populate the form with the appointment data
                    var apt = eventArgs.get_appointment();
                    var isInsert = mode == Telerik.Web.UI.SchedulerFormMode.AdvancedInsert;
                    advancedTemplate.populate(apt, isInsert);
                }
            }
        }
        function OnClientRecurrenceActionDialogShowing(sender, args) {
            var action = args.get_recurrenceAction();
            var appointment = args.get_appointment();
            if (action == Telerik.Web.UI.RecurrenceAction.Edit) {
                args.set_cancel(true);
                sender.showAdvancedEditForm(appointment);
            } else if (action == Telerik.Web.UI.RecurrenceAction.Delete) {
                args.set_cancel(true);
                sender.deleteAppointment(appointment, false)
            } else //If the action is resize the event is just canceled.
            {
                args.set_cancel(false);
            }
        }
        //]]></script>

<telerik:RadAjaxManagerProxy ID="ajaxproxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="radcalendar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="radcalendar" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<div style="padding-top: 21px; padding-bottom: 21px;">
    <telerik:RadRadioButtonList ID="rdoProgram" runat="server" RenderMode="Auto" AutoPostBack="true" Direction="Horizontal">
        <Items>
            <telerik:ButtonListItem Text="RIPS2016PD" Value="418" />
            <telerik:ButtonListItem Text="CMWS2" Value="529" />
            <telerik:ButtonListItem Text="ELWS4" Value="773" />
        </Items>
    </telerik:RadRadioButtonList>
</div>
<div style="padding-top: 21px; padding-bottom: 21px;">
    <telerik:RadScheduler ID="radcalendar" runat="server" DayEndTime="20:00:00"
        DayStartTime="06:00:00"
        EnableDescriptionField="True" OverflowBehavior="Expand" Skin="Black"
        StartInsertingInAdvancedForm="True"
        CustomAttributeNames="DetailID,VenueID,TalkID,StandardSlotID,CustomSlotTitle" DataEndField="End" DataKeyField="DetailID"
        DataRecurrenceField="RecurrenceRule"
        DataRecurrenceParentKeyField="RecurrenceParentID" DataReminderField="Reminder"
        DataStartField="Start" DataSubjectField="Subject"
        OnAppointmentDelete="radcalendar_AppointmentDelete"
        OnAppointmentInsert="radcalendar_AppointmentInsert"
        OnAppointmentUpdate="radcalendar_AppointmentUpdate"
        OnClientFormCreated="schedulerFormCreated" OnClientRecurrenceActionDialogShowing="OnClientRecurrenceActionDialogShowing"
        EnableExactTimeRendering="True" Height="100%"
        RowHeight="60px" OnFormCreating="radcalendar_FormCreating"
        OnAppointmentDataBound="radcalendar_AppointmentDataBound"
        EnableCustomAttributeEditing="True" SelectedView="MonthView"
        OnRecurrenceExceptionCreated="radcalendar_RecurrenceExceptionCreated" ShowAllDayRow="False" RenderMode="Auto">
        <AdvancedForm Modal="True" EnableCustomAttributeEditing="True" />
        <MonthView AdaptiveRowHeight="True" VisibleAppointmentsPerDay="5" />
        <Reminders Enabled="false" />
        <AppointmentTemplate>
            <div class="rsAptSubject">
                <%# (Eval("Subject").ToString().Length > 75) ? Eval("Subject").ToString().Substring(0, 71) + "..." : Eval("Subject") %>
            </div>
            <div>
                <%# (Convert.ToDateTime(Eval("Start")).Date < Convert.ToDateTime(Eval("End")).Date) ? Eval("Start", "{0:MM/dd/yyyy}") + " - " + Eval("End", "{0:MM/dd/yyyy}") : Eval("Start", "{0:MM/dd/yyyy}") + "<br /> " + Eval("Start", "{0:hh:mm}") + " - " + Eval("End", "{0:hh:mm}") %>
            </div>
        </AppointmentTemplate>
        <AdvancedEditTemplate>
            <scheduler:AdvancedForm runat="server" ID="AdvancedEditForm1" Mode="Edit"
                Subject='<%# Eval("Subject") %>'
                Start='<%# Eval("Start") %>'
                End='<%# Eval("End") %>'
                RecurrenceRuleText='<%# Eval("RecurrenceRule") %>'
                VenueID='<%# Eval("VenueID") %>'
                DetailID='<%# Eval("DetailID") %>'
                CustomSlotTitle='<%# Eval("CustomSlotTitle") %>'
                StandardSlotID='<%# Eval("StandardSlotID") %>'
                TalkID='<%# Eval("TalkID") %>' />
        </AdvancedEditTemplate>
        <AdvancedInsertTemplate>
            <scheduler:AdvancedForm runat="server" ID="AdvancedInsertForm1" Mode="Insert"
                Subject='<%# Eval("Subject") %>'
                Start='<%# Eval("Start") %>'
                End='<%# Eval("End") %>'
                RecurrenceRuleText='<%# Eval("RecurrenceRule") %>'
                VenueID='<%# Eval("VenueID") %>'
                DetailID='<%# Eval("DetailID") %>'
                CustomSlotTitle='<%# Eval("CustomSlotTitle") %>'
                StandardSlotID='<%# Eval("StandardSlotID") %>'
                TalkID='<%# Eval("TalkID") %>' />
        </AdvancedInsertTemplate>
        <ResourceTypes>
            <telerik:ResourceType KeyField="VenueID" Name="Venue" TextField="DataTextField" ForeignKeyField="VenueID" />
        </ResourceTypes>

        <TimeSlotContextMenuSettings EnableDefault="true" />
        <AppointmentContextMenuSettings EnableDefault="true" />
    </telerik:RadScheduler>

</div>
