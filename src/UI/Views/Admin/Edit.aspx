﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="CodeCampServer.UI.Helpers.ViewPage.AdminEditView"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>


<%@ Import Namespace="CodeCampServer.UI.Models.Forms" %>
<%@ Import Namespace="CodeCampServer.UI.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="Main" runat="server">
<form action="<%= Url.Action<AdminController>(x => x.Save(null)) %>" method="post"  >

<div>

	    <h1>Edit Admin Record</h1>
        
        <%=Errors.Display()%>

	    <table class="dataEntry">
		    <tr>
			    <td class="w50p">
						<%=InputFor(a => a.Id)%>            	
						<%=InputFor(a => a.Username)%>
						<%=InputFor(a => a.Name)%>
						<%=InputFor(a => a.EmailAddress)%>
						<%=InputFor(a => a.Password)%>
						<%=InputFor(a => a.ConfirmPassword)%>
			    </td>
		    </tr>
		  </table>
	    <br />
	    <br />
	    <div class="p10 tac">
				<%=Html.SubmitButton("save", "Save", new{@class="pr10 w100"}) %>    
				<a href="<%=Url.Action<AdminController>(x => x.Index()).ToXHTMLLink() %>"  class="pr10 mt5" rel="cancel">Cancel</a>				
	    </div>
</div>

</form>

</asp:Content>