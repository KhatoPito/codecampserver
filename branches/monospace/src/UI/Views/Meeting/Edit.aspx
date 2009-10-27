<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true"
  ValidateRequest="false" Inherits="ViewPage<MeetingInput>" %>

<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Input"%>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <fieldset id="meeting">
    <h3>Edit Meeting</h3>
    <%=Html.ValidationSummary() %>
<% using(Html.BeginForm()) { %>
    <%=Html.Input(m=>m.Id) %>
    <%=Html.Input(m=>m.Name) %>
    <%=Html.Input(m=>m.Topic) %>
    <%=Html.Input(m=>m.Summary) %>
    <%=Html.Input(m=>m.Key) %>
    <%=Html.Input(m=>m.LocationName) %>
    <%=Html.Input(m=>m.LocationUrl) %>
    <%=Html.Input(m=>m.SpeakerName) %>
    <%=Html.Input(m=>m.SpeakerUrl) %>
    <%=Html.Input(m=>m.SpeakerBio) %>
    <%=Html.Input(m=>m.StartDate) %>
    <%=Html.Input(m=>m.EndDate) %>
    <%=Html.Input(m=>m.TimeZone) %>
    <%=Html.Input(m=>m.Description) %>
		<%=Html.InputButtons( ) %>
<%	}%>
  </fieldset>
</asp:Content>
