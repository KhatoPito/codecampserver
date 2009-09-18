<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<MeetingForm>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<h1 class="title"> <%=Model.Name %></h1> 
<div class="entry"> 
    <p><b>When:</b> <%=Model.GetDate() %> </p> 
    <p><b>Speaker:</b> <a href="<%=Model.SpeakerUrl %>"><%=Model.SpeakerName %></a></p> 
    <p><b>Speaker Bio:</b> <%=Model.SpeakerBio %></p> 
    <p><b>Topic:</b> <%=Model.Topic %></p> 
    <p><b>Summary:</b> <%=Model.Summary %></p> 
    <p><b>Location:</b> <%=Model.LocationName %> - <a href="<%=Model.LocationUrl %>" class="more">map</a></p> 
    <p><b>Meeting info:</b> <%=Model.Description %></p> 
</div> 