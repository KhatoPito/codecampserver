<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="July09v31.UI.Models.Forms"%>
<%@ Import Namespace="July09v31.UI.Controllers"%>
<%
	var timeslot = (TimeSlotForm) ViewData.Model; %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
    <a title="Edit <%= timeslot.GetName() %>" href="<%= Url.Action<TimeSlotController>(t => t.Edit(null), new{timeslot = timeslot.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
<%}%>