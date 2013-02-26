<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="monosnow.umbraco.uCssClassNameDropdown.test" %>
<%@ Register Src="/usercontrols/FontAwesomeIconPicker.ascx" TagName="FontAwesomeIconPicker" TagPrefix="monosnow" %>
<%@ Register Src="/usercontrols/BootstrapIconPicker.ascx" TagName="BootstrapIconPicker" TagPrefix="monosnow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

 <hr />
    <monosnow:BootstrapIconPicker ID="bip2" runat="server" />
    </div>
    </form>
   
</body>
</html>
