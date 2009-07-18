<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="July09v31.UI.Helpers.ViewPage.BaseViewPage<UserForm>"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>


<%@ Import Namespace="July09v31.UI.Models.Forms" %>
<%@ Import Namespace="July09v31.UI.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<form action="<%= Url.Action<UserController>(x => x.Save(null)) %>" method="post"  >

<div>

	    <h1>User</h1>
        
        <%=Errors.Display()%>

	    <table class="dataEntry">
		    <tr>
			    <td class="w50p">
						<%=Html.Input(a => a.Id)%>            	
						<%=Html.Input(a => a.Username)%>
						<%=Html.Input(a => a.Name)%>
						<%=Html.Input(a => a.EmailAddress)%>
						<%=Html.Input(a => a.Password)%>
						<%=Html.Input(a => a.ConfirmPassword)%>
			    </td>
		    </tr>
		  </table>
	    <br />
	    <br />
	    <div class="p10 tac">
				<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
				<a href="<%=Url.Action<AdminController>(x => x.Index(null)).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
	    </div>
</div>

</form>

</asp:Content>