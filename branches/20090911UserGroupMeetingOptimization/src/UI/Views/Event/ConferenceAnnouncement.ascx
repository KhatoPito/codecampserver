<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ConferenceForm>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<%= Model.Name %>
<%= Model.Description %>
<%= Model.GetDate() %>
