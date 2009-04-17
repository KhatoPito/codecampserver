﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" 
AutoEventWireup="true" Inherits="CodeCampServer.UI.Helpers.ViewPage.BaseViewPage<UserGroupForm[]>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Menu" runat="server">
<% Html.RenderPartial("HomeMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <div class="dataContainerQuadWide mt10">
<h2>User Groups
		<%if (User.Identity.IsAuthenticated){%>
				<a class="" href="<%=Url.Action<UserGroupController>(c=>c.New())%>" title="Add a new User Group"><img src="/images/icons/application_add.png" /></a>
		<%}%>
</h2>
		<table class="datatable">
		    <thead>
		        <tr>
		            <th>User Group</th>
		            <th>Location</th>
		            <%if (User.Identity.IsAuthenticated){%>
		            <th>Action</th>
		            <%}%>
		        </tr>
		    </thead>		
		    <tbody>
	            <% foreach (var userGroup in Model) { %>
		        <tr>
		            <td>
		            <a href="http://<%=userGroup.DomainName+":"+this.ViewContext.HttpContext.Request.Url.Port%>" title="Goto UserGroup site" title="<%= userGroup.Name%> <%= userGroup.Key %>"><%=Html.Encode( userGroup.Name)%></a>
		            </td>
		            <td><%=userGroup.City%> <%=userGroup.Region%> <%=userGroup.Country%></td>		            
            		<%if (User.Identity.IsAuthenticated){%>
		            <td>
		            <a href="<%=Url.Action<UserGroupController>(c=>c.Index(null),new {UserGroup = userGroup.Key})%>" title="View User Group " title="<%= userGroup.Name%> <%= userGroup.Key %>">View <%=Html.Encode( userGroup.Name)%></a>
		                <a href="http://<%=userGroup.DomainName+":"+this.ViewContext.HttpContext.Request.Url.Port%>" title="Goto UserGroup site" title="<%= userGroup.Name%> <%= userGroup.Key %>">Goto the website: <%=Html.Encode( userGroup.Name)%></a>
			            <div class="fr"><%Html.RenderPartial("EditUserGroupLink",userGroup); %></div>			            
                    </td>
                    <%}%>
		        </tr>
	<% } %>
		    </tbody>
		</table>
</div>
</asp:Content>