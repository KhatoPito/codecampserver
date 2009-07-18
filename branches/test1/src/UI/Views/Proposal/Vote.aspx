<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
Inherits="July09v31.UI.Helpers.ViewPage.BaseViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <h3>Thank You For Voting</h3>
    <h3><a href="<%= Url.Action<ProposalController>(c=>c.List(null)) %>">Vote For another session</a></h3>
		
</asp:Content>
