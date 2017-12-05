<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedFormCS.ascx.cs" Inherits="telerik_scheduler_sandbox.AdvancedFormCS" %>

<div class="rsDialog rsAdvancedEdit rsAdvancedModal" style="position: relative">
    <style type="text/css">
        .timelabel {
            padding-left: 7px !important;
            padding-right: 3px !important;
        }
    </style>
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
            </ul>
            
            <div class="rfbRow">
                <asp:ValidationSummary ID="valSummary" runat="server"
                    ValidationGroup="<%# Owner.ValidationGroup %>" CssClass="ValidationSummary" />
            </div>
            <div class="rfbRow" style="padding-bottom:7px !important;">
                <asp:Label runat="server" ID="lbltalk" Text="Talk" Width="100px"></asp:Label>
                <telerik:RadComboBox runat="server" ID="cbtalk" RenderMode='<%# Owner.RenderMode %>' Width="500px"></telerik:RadComboBox>
            </div>
            <div class="rfbRow" style="margin-bottom: 7px !important;">
                <asp:Label runat="server" ID="lblstandard" Text="Standard Slot" Width="100px"></asp:Label>
                <telerik:RadComboBox runat="server" ID="cbstandard" RenderMode='<%# Owner.RenderMode %>' Width="500px"></telerik:RadComboBox>
            </div>
            <div class="rfbRow" style="margin-bottom: 7px !important;">
                <asp:Label runat="server" ID="lbcustom" Text="Custom" Width="100px"></asp:Label>
                <telerik:RadTextBox runat="server" ID="txcustomslot" RenderMode='<%# Owner.RenderMode %>' Width="500px" Skin='<%# Owner.Skin %>' />
            </div>
            <div class="rfbRow" style="margin-bottom: 7px !important;">
                <asp:Label runat="server" ID="lbvenue" Text="Venue" Width="100px"></asp:Label>
                <telerik:RadComboBox runat="server" ID="cbvenue" RenderMode='<%# Owner.RenderMode %>' Width="300px"></telerik:RadComboBox>
            </div>
            <div class="rfbRow rsTimePick" style="margin-bottom: 7px !important;">
                <asp:Label ID="lbstartdate" runat="server" Text="Date" Width="103px"></asp:Label><%--
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
							
                --%><asp:Label ID="lbstarttime" runat="server" Text="Begin" CssClass="timelabel"></asp:Label><telerik:RadTimePicker runat="server" ID="StartTime" RenderMode="Lightweight" CssClass="rsAdvTimePicker"
                    Width="125px" Skin='<%# Owner.Skin %>' Culture='<%# Owner.Culture %>'>
                    <DateInput ID="DateInput3" runat="server" RenderMode="Lightweight" EmptyMessageStyle-CssClass="riError" EmptyMessage=" " />
                    <TimePopupButton Visible="false" />
                    <TimeView ID="TimeView1" runat="server" Columns="2" ShowHeader="false" StartTime="08:00"
                        EndTime="18:00" Interval="00:30" />
                </telerik:RadTimePicker>
                <%--
							
                --%><asp:Label ID="lbendtime" runat="server" Text="End" CssClass="timelabel"></asp:Label><telerik:RadTimePicker runat="server" ID="EndTime" RenderMode="Lightweight" CssClass="rsAdvTimePicker"
                    Width="125px" Skin='<%# Owner.Skin %>' Culture='<%# Owner.Culture %>'>
                    <DateInput ID="DateInput5" runat="server" RenderMode="Lightweight" EmptyMessageStyle-CssClass="riError" EmptyMessage=" " />
                    <TimePopupButton Visible="false" />
                    <TimeView ID="TimeView2" runat="server" Columns="2" ShowHeader="false" StartTime="08:00"
                        EndTime="18:00" Interval="00:30" />
                </telerik:RadTimePicker>
            </div>
            <div class="rfbRow rsTimePick rsEndTimePick" style="display:none;">
                <asp:Label ID="lbenddate" runat="server" Text="End" Width="100px"></asp:Label><%--
							
                    --%><telerik:RadDatePicker runat="server" ID="EndDate" RenderMode="Lightweight" CssClass="rsAdvDatePicker"
                        Width="83px" SharedCalendarID="SharedCalendar" Skin='<%# Owner.Skin %>' Culture='<%# Owner.Culture %>'
                        MinDate="1900-01-01">
                        <DatePopupButton Visible="False" />
                        <DateInput ID="DateInput4" runat="server" RenderMode="Lightweight" DateFormat='<%# Owner.AdvancedForm.DateFormat %>'
                            EmptyMessageStyle-CssClass="riError" EmptyMessage=" " EnableSingleInputRendering="false" />
                    </telerik:RadDatePicker>
                <%--
							
                --%>
            </div>
            <div class="rfbRow rsAllDayWrapper">
                <asp:Label ID="lballday" runat="server" Text="All Day?" Width="100px"></asp:Label>
                <label class="rfbLabel" for='<%= AllDayEvent.ClientID %>' style="width: 0px; display: none;">
                    <%= Owner.Localization.AdvancedAllDayEvent%></label>
                <asp:CheckBox runat="server" ID="AllDayEvent" CssClass="rsAdvChkWrap" Checked="false" />
            </div>
            <asp:RequiredFieldValidator runat="server" ID="StartDateValidator" ControlToValidate="StartDate"
                EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />
            <asp:RequiredFieldValidator runat="server" ID="StartTimeValidator" ControlToValidate="StartTime"
                EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />
            <asp:RequiredFieldValidator runat="server" ID="EndTimeValidator" ControlToValidate="EndTime"
                EnableClientScript="true" Display="None" CssClass="rsValidatorMsg" />
            <asp:CustomValidator runat="server" ID="DurationValidator" ControlToValidate="StartDate"
                EnableClientScript="false" Display="Dynamic" CssClass="rsValidatorMsg rsInvalid"
                OnServerValidate="DurationValidator_OnServerValidate" />
            <%-- </asp:Panel>
        <asp:Panel runat="server" ID="AdvancedControlsPanel" CssClass="rsAdvMoreControls">--%>
            <asp:HiddenField ID="etypeid" runat="server" />
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
