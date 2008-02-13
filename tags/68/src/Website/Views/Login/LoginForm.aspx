﻿<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true"  Inherits="CodeCampServer.Website.Views.ViewBase" Title="CodeCampServer - Login" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div style="text-align:center">
		<h3>Login</h3>
		<% using (Html.Form("process", "login")) { %>
		<table style="text-align:left">
			<tr>
				<td>Email:</td>
				<td><%=Html.TextBox("email", 50) %></td>
			</tr>
			<tr>
				<td>Password:</td>
				<td><%=Html.Password("password", 50)%></td>
			</tr>
			<tr>
			    <td></td>
			    <td><%=Html.SubmitButton("Login") %></td>
			</tr>
        </table>
        <% } %>
    </div>
</asp:Content>
