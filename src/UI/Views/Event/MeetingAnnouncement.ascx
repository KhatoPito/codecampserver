<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<MeetingForm>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<%= Model.Name %>
<%= Model.Description %>
<%= Model.GetDate() %>
<%= Model.Topic %>
<%= Model.Summary %>
<%= Model.SpeakerName %>
<%= Model.SpeakerBio %>
<%= Model.SpeakerUrl %>
