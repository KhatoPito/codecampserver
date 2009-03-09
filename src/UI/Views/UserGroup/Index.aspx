<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" 
Inherits="System.Web.Mvc.ViewPage<UserGroupForm>"%>
<%@ Import Namespace="CodeCampServer.UI.Models.Forms"%>
<%@ Import Namespace="MvcContrib"%>


<asp:Content ContentPlaceHolderID="Main" runat="server">
	 <div>
		    <h1><%=Model.Name%> <%Html.RenderPartial("EditUserGroupLink", Model); %></h1>		    
		    <div>  
	   			        						<div>Key: <%=Model.Key%></div>
					    						<div>Id: <%=Model.Id%></div>
					    						<div>Name: <%=Model.Name%></div>
					    			</div>
        </div>
</asp:Content>

