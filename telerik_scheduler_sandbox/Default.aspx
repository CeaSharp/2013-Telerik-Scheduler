<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/scheduler.ascx" TagName="ipamscheduler" TagPrefix="test" %>

<asp:Content ID="headercontent" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="bodycontent" ContentPlaceHolderID="content" runat="server">
    <test:ipamscheduler id="ucshceduler" runat="server" />
</asp:Content>