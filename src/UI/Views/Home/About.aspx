<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
    Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm>" %>

<asp:Content ContentPlaceHolderID="Menu" runat="server">
    <% Html.RenderPartial("HomeMenu"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
    <div class="section-title">
        <h2>
            <%= Model.Name %>
            <%Html.RenderPartial("EditUserGroupLink", Model); %></h2>
        <p>
            <%= Model.City %>
            <%= Model.Region %>
            <%= Model.Country%></p>
    </div>
    <div class="content">
        <%= Model.HomepageHTML %></div>
</asp:Content>
