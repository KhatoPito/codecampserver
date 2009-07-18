<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<SponsorForm>" %>
<%@ Import Namespace="July09v31.UI.Models.Forms"%>
<%@ Import Namespace="July09v31.UI.Controllers"%>
<a href="<%= Url.Action<SponsorController>(t => t.Edit(null,Guid.Empty), new{sponsorID = Model.ID}).ToXHTMLLink() %>"><img src="/images/icons/application_edit.png" /></a>
