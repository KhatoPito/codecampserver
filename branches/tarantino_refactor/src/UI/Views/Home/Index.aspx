<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
<script type="text/javascript" src="/scripts/rsswidget.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
        <div class="w95p tac">
        
        <div class="cleaner"></div>
</asp:Content>