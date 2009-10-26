<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" inherits="System.Web.Mvc.ViewPage"%>
<%@ Import Namespace="MvcContrib"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Input"%>
<asp:Content ContentPlaceHolderID="Main" runat="server">
	 	 <h2>Users
	</h2>
    
    <table class="">
        <tr>
            <th>Username</th>
            <th>Name</th>
        </tr>
        <% foreach(var user in ViewData.Get<UserInput[]>()) { %>
        <tr>
            <td><a href="/User/Edit/?userid=<%=user.Id%>"><%= user.Name%></a></td>
        </tr>
        <% } %>
    </table>
</asp:Content>