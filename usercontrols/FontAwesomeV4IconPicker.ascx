<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FontAwesomeV4IconPicker.ascx.cs" Inherits="monosnow.umbraco.uCssClassNameDropdown.usercontrols.FontAwesomeV4IconPicker" %>
<asp:ListView ID="lvIcons" runat="server" ItemPlaceholderID="itemPlaceHolder" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder">
<LayoutTemplate>
<div style="height:400px; width:650px;overflow:scroll">
<asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
</div>
</LayoutTemplate>
<GroupTemplate>
<div style="clear:both"></div>
<div>
<asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
</div>
</GroupTemplate>
<ItemTemplate>
<a style="text-decoration:none; font-size:1.4em;color:#000000" href="#" onclick="setDropdownValue('<%#Container.DataItem %>');"><div style="width:150px;height:50px;float:left;"><i class="icon-<%#Container.DataItem %>"></i> <%#Container.DataItem %></div></a>
</ItemTemplate>
</asp:ListView>
<asp:DropDownList ID="ddlIcons" runat="server"></asp:DropDownList>

<script type="text/javascript">
    function setDropdownValue(selectedValue) {
        document.getElementById("<%=ddlIcons.ClientID %>").value = selectedValue;       
    }


</script>