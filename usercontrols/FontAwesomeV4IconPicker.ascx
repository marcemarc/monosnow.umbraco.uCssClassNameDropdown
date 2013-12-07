<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FontAwesomeV4IconPicker.ascx.cs" Inherits="monosnow.umbraco.uCssClassNameDropdown.usercontrols.FontAwesomeV4IconPicker" %>
<div id="fontAwesomeIconPickerHolder" runat="server">
<asp:ListView ID="lvIcons" runat="server" ItemPlaceholderID="itemPlaceHolder" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder">
<LayoutTemplate>
<div class="uCssClassNamePickerHolder" style="height:400px; width:650px;overflow:scroll">
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
<a style="text-decoration:none; font-size:1.4em;color:#000000" href="#" onclick="setDropdownValue('<%#Container.DataItem %>');"><div class="iconHolder iconHolder-<%#Container.DataItem %>" style="width: 150px; height: 50px; float: left; border: 0px; font-weight: normal;"><i class="fa fa-<%#Container.DataItem %>"></i> <%#Container.DataItem %></div></a>
</ItemTemplate>
</asp:ListView>
<asp:DropDownList ID="ddlIcons" runat="server"></asp:DropDownList>
    </div>


<script type="text/javascript">
    $(document).ready(function () {
        // first load, set highlight on selected icon
      
        setIconHighlight('<%=this.value.ToString()%>');
        $('#<%=ddlIcons.ClientID %>').change(function (e) {
             setIconHighlight(this.value);

        });

    });

    function setIconHighlight(selectedValue) {
     
        //first remove existing highlights
  
        $('#<%=fontAwesomeIconPickerHolder.ClientID %> div.iconHolder').css("background-color", "").css("border", "0px").css("font-weight", "normal");

        // add highlight to selected icon
        if (typeof selectedValue != 'undefined' && selectedValue != '') {

            $('#<%=fontAwesomeIconPickerHolder.ClientID %> div.iconHolder-' + selectedValue).css("background-color", "#FFFFCC").css("border", "3px dashed orange").css("font-weight", "bold");
            //scroll to highlighted
            var container = $('div.uCssClassNamePickerHolder');
            var scrollTo = $('div.iconHolder-' + selectedValue);

            container.scrollTop(
             scrollTo.offset().top - container.offset().top - 175 + container.scrollTop()
         );



        }
    }


    function setDropdownValue(selectedValue) {
        $('#<%=ddlIcons.ClientID %>').val(selectedValue);
        // document.getElementById("body_ctl25_ddlIcons").value = selectedValue;  
        setIconHighlight(selectedValue);
    }


</script>