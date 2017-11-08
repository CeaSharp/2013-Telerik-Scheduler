<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedFormCS.ascx.cs" Inherits="telerik_scheduler_sandbox.AdvancedFormCS" %>

<div class="rsDialog rsAdvancedEdit rsAdvancedModal" style="position: relative">
    <%-- Title bar. --%>
    <div class="rsAdvTitle rsTitle">
        <%-- The rsAdvInnerTitle element is used as a drag handle when the form is modal. --%>
        <p class="rsAdvInnerTitle">
            <%= (this.Mode.ToString() == "Edit") ? Owner.Localization.AdvancedEditAppointment : Owner.Localization.AdvancedNewAppointment %>
        </p>
        <asp:LinkButton runat="server" ID="AdvancedEditCloseButton" CssClass="rsAdvEditClose"
            CommandName="Cancel" CausesValidation="false" ToolTip='<%# Owner.Localization.AdvancedClose %>'>       
            <span class="p-icon p-i-close"></span>
        </asp:LinkButton>
    </div>
    <div class="rsAdvContentWrapper rsBody">
        <%-- Scroll container - when the form height exceeds MaximumHeight scrollbars will appear on this element--%>
        <asp:Panel runat="server" ID="AdvOptionsScroll" CssClass="rsAdvOptionsScroll" OnDataBinding="BasicControlsPanel_DataBinding">
            <ul class="rfbGroup">
                <li class="rfbRow">
                    <asp:ValidationSummary ID="valSummary" runat="server"
                        ValidationGroup="<%# Owner.ValidationGroup %>" CssClass="ValidationSummary" />
                </li>
                <li class="rfbRow">
                    <telerik:RadComboBox runat="server" Label="Talk" ID="cbtalk" RenderMode='<%# Owner.RenderMode %>' Width="100%"></telerik:RadComboBox>
                </li>
                <li class="rfbRow">
                    <asp:Label runat="server" ID="lblstandard" Text="Standard Slot"></asp:Label>
                    <telerik:RadComboBox runat="server" ID="cbstandard" RenderMode='<%# Owner.RenderMode %>' Width="100%"></telerik:RadComboBox>
                </li>
                <li class="rfbRow">
                    <asp:Label runat="server" ID="lbcustom" Text="Custom"></asp:Label>
                    <telerik:RadTextBox runat="server" ID="txcustomslot" RenderMode='<%# Owner.RenderMode %>' Width="100%" Skin='<%# Owner.Skin %>' />
                </li>
                <li class="rfbRow">
                    <asp:Label runat="server" ID="lbvenue" Text="Venue"></asp:Label>
                    <telerik:RadComboBox runat="server" ID="cbvenue" RenderingMode='<%# Owner.RenderMode %>' Width="100%"></telerik:RadComboBox>
                </li>
                <li class="rfbRow rsTimePick">
                    <label for='<%= StartDate.ClientID %>_dateInput_text'>
                        <%= Owner.Localization.AdvancedFrom %></label><%--
                            Leaving a newline here will affect the layout, so we use a comment instead.
                        --%><telerik:RadDatePicker runat="server" ID="StartDate" RenderMode="Lightweight"
                            CssClass="rsAdvDatePicker"
                            Width="110px" SharedCalendarID="SharedCalendar"
                            Skin='<%# Owner.Skin %>' Culture='<%# Owner.Culture %>'
                            MinDate="1900-01-01">
                            <DatePopupButton Visible="False" />
                            <DateInput ID="DateInput2" runat="server"
                                RenderMode='<%# Owner.RenderMode %>'
                                DateFormat='<%# Owner.AdvancedForm.DateFormat %>'
                                EmptyMessageStyle-CssClass="riError" EmptyMessage=" "
                                EnableSingleInputRendering="false" />
                        </telerik:RadDatePicker>
                    <%--
							
                    --%><telerik:RadTimePicker runat="server" ID="StartTime" RenderMode="Lightweight" CssClass="rsAdvTimePicker"
                        Width="110px" Skin='<%# Owner.Skin %>' Culture='<%# Owner.Culture %>'>
                        <DateInput ID="DateInput3" runat="server" RenderMode="Lightweight" EmptyMessageStyle-CssClass="riError" EmptyMessage=" " />
                        <TimePopupButton Visible="false" />
                        <TimeView ID="TimeView1" runat="server" Columns="2" ShowHeader="false" StartTime="08:00"
                            EndTime="18:00" Interval="00:30" />
                    </telerik:RadTimePicker>
                </li>
                <li class="rfbRow rsTimePick rsEndTimePick">
                    <label for='<%= EndDate.ClientID %>_dateInput_text'>
                        <%= Owner.Localization.AdvancedTo%></label><%--
							
                        --%><telerik:RadDatePicker runat="server" ID="EndDate" RenderMode="Lightweight" CssClass="rsAdvDatePicker"
                            Width="83px" SharedCalendarID="SharedCalendar" Skin='<%# Owner.Skin %>' Culture='<%# Owner.Culture %>'
                            MinDate="1900-01-01">
                            <DatePopupButton Visible="False" />
                            <DateInput ID="DateInput4" runat="server" RenderMode="Lightweight" DateFormat='<%# Owner.AdvancedForm.DateFormat %>'
                                EmptyMessageStyle-CssClass="riError" EmptyMessage=" " EnableSingleInputRendering="false" />
                        </telerik:RadDatePicker>
                    <%--
							
                    --%><telerik:RadTimePicker runat="server" ID="EndTime" RenderMode="Lightweight" CssClass="rsAdvTimePicker"
                        Width="65px" Culture='<%# Owner.Culture %>'>
                        <DateInput ID="DateInput5" runat="server" EmptyMessageStyle-CssClass="riError" EmptyMessage=" " />
                        <TimePopupButton Visible="false" />
                        <TimeView ID="TimeView2" runat="server" Columns="2" ShowHeader="false" StartTime="08:00"
                            EndTime="18:00" Interval="00:30" />
                    </telerik:RadTimePicker>
                </li>
                <li class="rfbRow rsAllDayWrapper">
                    <label class="rfbLabel" for='<%= AllDayEvent.ClientID %>'>
                        <%= Owner.Localization.AdvancedAllDayEvent%></label>
                    <asp:CheckBox runat="server" ID="AllDayEvent" CssClass="rsAdvChkWrap" Checked="false" />
                </li>
            </ul>
            <div class="rfbRow rsReminderWrapper">
                <label for='<%# ReminderDropDown.ClientID %>'>
                    <%# Owner.Localization.Reminder %>
                </label>
                <telerik:RadComboBox runat="server" ID="ReminderDropDown" RenderMode='<%# Owner.RenderMode %>' Width="120px" Skin='<%# Owner.Skin %>'>
                    <Items>
                        <telerik:RadComboBoxItem Text='<%# Owner.Localization.ReminderNone %>' Value="" />
                        <telerik:RadComboBoxItem Text='<%# "0 " + Owner.Localization.ReminderMinutes %>'
                            Value="0" />
                        <telerik:RadComboBoxItem Text='<%# "5 " + Owner.Localization.ReminderMinutes %>'
                            Value="5" />
                        <telerik:RadComboBoxItem Text='<%# "10 " + Owner.Localization.ReminderMinutes %>'
                            Value="10" />
                        <telerik:RadComboBoxItem Text='<%# "15 " + Owner.Localization.ReminderMinutes %>'
                            Value="15" />
                        <telerik:RadComboBoxItem Text='<%# "30 " + Owner.Localization.ReminderMinutes %>'
                            Value="30" />
                        <telerik:RadComboBoxItem Text='<%# "1 " + Owner.Localization.ReminderHour %>' Value="60" />
                        <telerik:RadComboBoxItem Text='<%# "2 " + Owner.Localization.ReminderHours %>' Value="120" />
                        <telerik:RadComboBoxItem Text='<%# "3 " + Owner.Localization.ReminderHours %>' Value="180" />
                        <telerik:RadComboBoxItem Text='<%# "4 " + Owner.Localization.ReminderHours %>' Value="240" />
                        <telerik:RadComboBoxItem Text='<%# "5 " + Owner.Localization.ReminderHours %>' Value="300" />
                        <telerik:RadComboBoxItem Text='<%# "6 " + Owner.Localization.ReminderHours %>' Value="360" />
                        <telerik:RadComboBoxItem Text='<%# "7 " + Owner.Localization.ReminderHours %>' Value="420" />
                        <telerik:RadComboBoxItem Text='<%# "8 " + Owner.Localization.ReminderHours %>' Value="480" />
                        <telerik:RadComboBoxItem Text='<%# "9 " + Owner.Localization.ReminderHours %>' Value="540" />
                        <telerik:RadComboBoxItem Text='<%# "10 " + Owner.Localization.ReminderHours %>' Value="600" />
                        <telerik:RadComboBoxItem Text='<%# "11 " + Owner.Localization.ReminderHours %>' Value="660" />
                        <telerik:RadComboBoxItem Text='<%# "12 " + Owner.Localization.ReminderHours %>' Value="720" />
                        <telerik:RadComboBoxItem Text='<%# "18 " + Owner.Localization.ReminderHours %>' Value="1080" />
                        <telerik:RadComboBoxItem Text='<%# "1 " + Owner.Localization.ReminderDays %>' Value="1440" />
                        <telerik:RadComboBoxItem Text='<%# "2 " + Owner.Localization.ReminderDays %>' Value="2880" />
                        <telerik:RadComboBoxItem Text='<%# "3 " + Owner.Localization.ReminderDays %>' Value="4320" />
                        <telerik:RadComboBoxItem Text='<%# "4 " + Owner.Localization.ReminderDays %>' Value="5760" />
                        <telerik:RadComboBoxItem Text='<%# "1 " + Owner.Localization.ReminderWeek %>' Value="10080" />
                        <telerik:RadComboBoxItem Text='<%# "2 " + Owner.Localization.ReminderWeeks %>' Value="20160" />
                    </Items>
                </telerik:RadComboBox>
            </div>
            <asp:RequiredFieldValidator runat="server" ID="StartDateValidator" ControlToValidate="StartDate"
                EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />
            <asp:RequiredFieldValidator runat="server" ID="StartTimeValidator" ControlToValidate="StartTime"
                EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />
            <asp:RequiredFieldValidator runat="server" ID="EndDateValidator" ControlToValidate="EndDate"
                EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />
            <asp:RequiredFieldValidator runat="server" ID="EndTimeValidator" ControlToValidate="EndTime"
                EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />
            <asp:CustomValidator runat="server" ID="DurationValidator" ControlToValidate="StartDate"
                EnableClientScript="false" Display="Dynamic" CssClass="rsValidatorMsg rsInvalid"
                OnServerValidate="DurationValidator_OnServerValidate" />
            <%-- </asp:Panel>
        <asp:Panel runat="server" ID="AdvancedControlsPanel" CssClass="rsAdvMoreControls">--%>
            <asp:HiddenField ID="etypeid" runat="server" />
            <label>
                Location:
            </label>
            <telerik:RadComboBox runat="server" RenderMode='<%# Owner.RenderMode %>' ID="eloc" Width="320px"
                Skin='WebBlue' EnableAriaSupport="True" EnableItemCaching="True"
                EnableVirtualScrolling="True" ZIndex="9000"
                Filter="Contains" MaxHeight="400px"
                ShowMoreResultsBox="True">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="eloc"
                EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />

            <span class="rsAdvResetExceptions">
                <asp:LinkButton runat="server" ID="ResetExceptions" OnClick="ResetExceptions_OnClick" />
            </span>
            <telerik:RadSchedulerRecurrenceEditor runat="server" ID="AppointmentRecurrenceEditor" RenderMode='<%# Owner.RenderMode %>' />
            <asp:HiddenField runat="server" ID="OriginalRecurrenceRule" />
            <telerik:RadCalendar runat="server" ID="SharedCalendar" RenderMode='<%# Owner.RenderMode %>' Skin='<%# Owner.Skin %>'
                CultureInfo='<%# Owner.Culture %>' ShowRowHeaders="false" RangeMinDate="1900-01-01" />

        </asp:Panel>
        <asp:Panel runat="server" ID="ButtonsPanel" CssClass="rsAdvancedSubmitArea rsAdvButtonWrapper rsButtons">
            <div class="rsAdvButtonWrapper">
                <asp:LinkButton runat="server" ID="UpdateButton" CssClass="rsAdvEditSave rsButton">
                    <span><%= Owner.Localization.Save %></span>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="CancelButton" CssClass="rsAdvEditCancel rsButton" CommandName="Cancel"
                    CausesValidation="false">
                    <span><%= Owner.Localization.Cancel %></span>
                </asp:LinkButton>
            </div>
        </asp:Panel>
    </div>
</div>
