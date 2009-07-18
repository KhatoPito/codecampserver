<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="July09v31.UI.Models.Forms"%>

<%@ Import Namespace="July09v31.UI.Controllers"%>
<%
   
var speaker = (SpeakerForm) ViewData.Model; %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
		<a href="<%= Url.Action<SpeakerController>(t => t.Edit(null,null), new{speaker = speaker.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
<%}%>		
