<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true"
    Inherits="ViewPage<HomeDisplay>" %>
<%@ Import Namespace="CodeCampServer.UI.Models.Input"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>


<asp:Content ContentPlaceHolderID="Main" runat="server">
    <%=Html.ValidationSummary()%>
<% foreach (var session in Model.Meetings){%>
		<%
	Html.RenderPartial("MeetingAnnouncement", session);%>
<%	} %>
    
</asp:Content>
