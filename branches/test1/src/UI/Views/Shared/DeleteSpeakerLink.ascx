<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="July09v31.UI.Models.Forms"%>

<%@ Import Namespace="July09v31.UI.Controllers"%>

<%
var speaker = (SpeakerForm) ViewData.Model; %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
		<a title="Delete <%=speaker.FirstName%> <%=speaker.LastName %>" href="<%= Url.Action<SpeakerController>(t => t.Delete(null,null), new{speaker = speaker.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_delete.png" /></a>
<%}%>		
