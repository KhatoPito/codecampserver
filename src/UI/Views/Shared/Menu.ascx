﻿<%@ Control Language="C#" AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="menu">
			<ul>
				<asp:LoginView ID="LoginView1" runat="server">
					<LoggedInTemplate>
						<li><a href="<%=Url.Action<AdminController>(c=>c.Index()) %>">Admin</a></li>
					</LoggedInTemplate>
				</asp:LoginView>
				
				<li><a href="<%=Url.Action<ScheduleController>(c=>c.Index(null)) %>">Schedule</a></li>
				<li><a href="<%=Url.Action<SpeakerController>(c=>c.List()) %>">Speakers</a></li>
				<li><a href="<%=Url.Action<SessionController>(c=>c.List(null)) %>">Sessions</a></li>
				<li><a href="<%=Url.Action<AttendeeController>(c=>c.Index(null)) %>">Attendees</a></li>
				<li><a href="<%=Url.Action<AttendeeController>(c=>c.New(null)) %>">Register</a></li>
				<li><a href="<%=Url.Action<ProposalController>(c=>c.List(null)) %>">Session&nbsp;Proposals</a></li>
			</ul>
		</div>