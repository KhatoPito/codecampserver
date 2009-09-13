<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<EventForm[]>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<% foreach (EventForm eventForm in Model)
   {
       Html.RenderAction("announcement", "event", new { @event = eventForm.Key });
   } %>
