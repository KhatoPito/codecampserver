<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="July09v31.UI.Models.Forms"%>

<%@ Import Namespace="July09v31.UI.Controllers"%>

<%
	var session = (SessionForm) ViewData.Model; %>
		<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
				<a href="<%= Url.Action<SessionController>(t => t.Delete(null), new{session = session.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_delete.png" /></a>
		<%}%>		
