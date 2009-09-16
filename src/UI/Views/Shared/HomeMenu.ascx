<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="navigationWrapper">
	<div id="leftNavigationEndCap"></div>
	<ul id="navigationMenu">
		<asp:LoginView ID="LoginView1" runat="server">
			<LoggedInTemplate>
				<li><a href="<%=Url.Action<AdminController>(c=>c.Index(null)) %>">admin</a></li>
			</LoggedInTemplate>
		</asp:LoginView>
		
		<li><a href="<%=Url.Action<EventController>(c=>c.List(null)) %>">all events</a></li>
		<li><a href="<%=Url.Action<HomeController>(c=>c.About(null)) %>">about us</a></li>
		<li><a href="<%=Url.Action<UserGroupController>(c=>c.List()) %>">user groups</a></li>
	</ul>
	<div id="rightNavigationEndCap"></div>
</div>
<div class="cleaner"></div>