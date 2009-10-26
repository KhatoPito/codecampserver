<%@ Language="C#" Inherits="System.Web.Mvc.ViewPage<PropertyViewModel<object>>" %>
<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
    <%=Html.TextBox(Model.Name,Model.Value) %>    
</asp:Content>
