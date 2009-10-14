<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<SponsorInput>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%if (ViewContext.HttpContext.User.Identity.IsAuthenticated){%>
    <%= Html.ImageLink<SponsorController>(
            t=>t.Edit(null,Guid.Empty), new{sponsorID = Model.ID},
            "~/images/icons/application_edit.png", "Edit the conference") %>
<%}%>