﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="ViewPage<UserGroupInput>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">


</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
    <%Html.RenderAction("List", "Event", ViewContext.RouteData.DataTokens);%>    
</asp:Content>
