﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true" Inherits="ViewPage<UserInput>"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Input"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">

<fieldset>
<h3>Edit User</h3>
<%=Html.ValidationSummary() %>
<% using(Html.BeginForm()) { %>
	<%=Html.Input(m=>m.Username) %>
	<%=Html.Input(m=>m.Name) %>
	<%=Html.Input(m=>m.EmailAddress) %>
	<%=Html.InputButtons( ) %>
<%	}%>
</fieldset>			

</asp:Content>