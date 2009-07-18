<%@ Page Language="C#" AutoEventWireup="true"
	Inherits="July09v31.UI.Helpers.ViewPage.BaseViewPage<SessionForm[]>" %>

	<% foreach (SessionForm session in Model) { %>
	<div class="session">
		<%Html.RenderPartial("EditSessionLink",session,ViewData); %>
		<span><a href="/<%=ViewData.Get<Conference>().Key + "/sessions/" +session.Key%>"><%=session.Title %></a></span><span>(Room: <%=session.RoomNumber %>)</span>
	</div>			
	<% } %>
