<%@ Page Title="" Language="C#" 
Inherits="ViewPage<PropertyViewModel<DateTime>>" %>
<%@ Import Namespace="CodeCampServer.UI.Helpers.Extensions"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
<%=Html.Hidden(Model.Name,Model.Value) %>
<%=Html.TextBox(Model.Name+"_date",Model.Value.ToString("MM/dd/yyyy"),new {@id=Model.Name+"_date",@style="width:110px"}) %>
<%=Html.DropDownList(Model.Name + "_hour", Model.Value.GetHourSelectItems(), new { @id = Model.Name + "_hour" })%>
<%=Html.DropDownList(Model.Name + "_minute", Model.Value.GetMinuteSelectItems(), new { @id = Model.Name + "_minute" })%>
<%=Html.DropDownList(Model.Name + "_noon", Model.Value.GetNoonSelectItems(), new { @id = Model.Name + "_noon" })%>

	<script language="javascript">
	$(function() {
		$("#<%=Model.Name%>_date").datePicker();
});
$(function() {
    $('#<%=Model.Name %>_hour').blur(function() {
        updateDateTime<%=Model.Name%>();
    })
});
$(function() {
    $('#<%=Model.Name %>_minute').blur(function() {
        updateDateTime<%=Model.Name%>();
    })
});
$(function() {
    $('#<%=Model.Name %>_noon').blur(function() {
        updateDateTime<%=Model.Name%>();
    })
});
$(function() {
    $('#<%=Model.Name %>_date').change(function() {
        updateDateTime<%=Model.Name%>();
    })
});
function updateDateTime<%=Model.Name%>()
{
    $('#<%=Model.Name%>').val($('#<%=Model.Name %>_date').val()+' '+(parseInt($('#<%=Model.Name %>_hour').val(),10)+parseInt($('#<%=Model.Name %>_noon').val(),10))+ ':' + $('#<%=Model.Name %>_minute').val());
}
	</script>
</asp:Content>
