<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="menu"> 
    <ul> 
    <li><a href="/">Sessions</a></li>
    <li><a href="/User">Users</a></li>
        <asp:LoginView ID="LoginView1" runat="server">
	        <LoggedInTemplate>
	        </LoggedInTemplate>
        </asp:LoginView>
    </ul> 
</div> 


