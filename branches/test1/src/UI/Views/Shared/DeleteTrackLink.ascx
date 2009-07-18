<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<TrackForm>" %>
<%@ Import Namespace="July09v31.UI.Models.Forms"%>
<%@ Import Namespace="July09v31.UI.Controllers"%>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
<a title="Delete <%=Model.Name%>" href="<%= Url.Action<TrackController>(t => t.Delete(null), new{track = Model.Id}).ToXHTMLLink() %>"><img src="<%= Url.Content("~/images/icons/delete.png").ToXHTMLLink() %>" /></a>
<%}%>		
