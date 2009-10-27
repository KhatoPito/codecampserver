<%@ Page Language="C#" AutoEventWireup="true" 
  Inherits="ViewPage<MeetingAnnouncementDisplay>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Input"%>

<%@ Import Namespace="CodeCampServer.UI.Helpers" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>
<h1 class="title">
  <%=Html.Display(m=>m.Heading).Partial(DisplayPartial.Inline).Label("") %>
  </h1><a href="/Meeting/Edit?meeting=<%=Model.Id %>">Edit</a>
<div class="entry">
  <p>
    <%=Html.Display(m=>m.When).Partial(DisplayPartial.Inline) %>
    (<a href="<%=Model.LocationUrl %>"><%=Model.LocationName %></a>)
  </p>
  <%=Html.Display(m=>m.Topic) %>
  <%=Html.Display(m=>m.Summary).Label("") %>
  <p>
    <%=Html.Label(m=>m.SpeakerName)%>
    <a href="<%=Model.SpeakerUrl %>"><%=Model.SpeakerName %></a>
  </p>
  <%=Html.Display(m=>m.SpeakerBio).Label("") %>
  <%=Html.Display(m=>m.MeetingInfo) %>
</div>

