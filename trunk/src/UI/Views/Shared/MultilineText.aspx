<%@ Page Title="" Language="C#" MasterPageFile="Field.Master" Inherits="System.Web.Mvc.ViewPage<PropertyViewModel<object>>" %>
<%@ Import Namespace="CodeCampServer.UI.Services.Common"%>
<%@ Import Namespace="MvcContrib.UI.ParamBuilder"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server"><%=Html.TextArea(Model.Name,Model.Value.ToString(),Params.With.Rows(10)) %></asp:Content>
