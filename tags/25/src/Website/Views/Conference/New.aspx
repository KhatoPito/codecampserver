﻿<%@ Page Language="C#" MasterPageFile="~/Views/Layouts/Default.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="CodeCampServer.Website.Views.Conference.New" Title="CodeCampServer - New Conference" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="../content/css/forms.css" type="text/css" rel="Stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Left" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Center" runat="server">
    
    
    <form method="post" action="conference/create">    
    
        <fieldset>
            <legend>New Conference</legend>
            
            <label for="conf_name">Name</label>
            <input type="text" id="conf_name" />
            <span class="info">The name of the conference.</span>
            
            <label for="conf_key">Unique Key</label>
            <input type="text" id="conf_key" />
            <span class="info">A unique name to identify the conference.  Will be used in a url, so it must not contain illegal characters such as spaces or symbols.</span>
            
            <label for="conf_start">Starts</label>        
            <input type="text" id="conf_start" />
            
            <label for="conf_end">Ends</label>        
            <input type="text" id="conf_end" />
            
            <label for="conf_descr">Description</label>
            <textarea rows="6" cols="50" id="conf_descr"></textarea>
            <span class="info">Max 1000 characters.  No formatting.</span>
            
            <span class="button-row">
                <input type="submit" id="conf_create" value="Create" />
            </span>
        </fieldset>
    
    </form>
    

</asp:Content>
