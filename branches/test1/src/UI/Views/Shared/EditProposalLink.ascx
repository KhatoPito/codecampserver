<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="July09v31.UI.Models.Forms"%>
<%@ Import Namespace="July09v31.UI.Controllers"%>
<%
	var proposal = (ProposalForm) ViewData.Model; %>
		<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
				<a href="<%= Url.Action<ProposalController>(t => t.Edit(null), new{proposalId = proposal.Id}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
		<%}%>		
