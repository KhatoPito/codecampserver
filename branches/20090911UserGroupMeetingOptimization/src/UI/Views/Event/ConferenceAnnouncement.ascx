<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ConferenceForm>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<h1 class="title"><%=Html.ActionLink<ConferenceController>(c=>c.Index(null), Model.Name, new{conferenceKey = Model.Key}) %></h1> 
<div class="entry"> 
    <p><b>When:</b> <%=Model.GetDate() %> </p> 
    <p><b>Location:</b> <%=Model.LocationName %> - <a href="<%=Model.LocationUrl %>" class="more">map</a></p> 
    <p><%=Model.Description %></p> 
</div> 
