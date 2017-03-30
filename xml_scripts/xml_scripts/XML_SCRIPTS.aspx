<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XML_SCRIPTS.aspx.cs" Inherits="RunFeed.XML_SCRIPTS" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
        .style2
        {
            width: 95px;
        }
        .style3
        {
            height: 22px;
            width: 95px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td class="style2">
                Section:</td>
            <td>
                <asp:TextBox ID="txtSection" runat="server" Width="332px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                Key:</td>
            <td class="style1">
                <asp:TextBox ID="txtKey" runat="server" Width="333px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                Value:</td>
            <td class="style1">
                <asp:TextBox ID="txtSetting" runat="server" Width="333px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
            </td>
            <td class="style1">
                
                <asp:Button ID="btnRead" runat="server" Text="Read setting" 
                    onclick="btnRead_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnWrite" runat="server" Text="Write setting" 
                    onclick="btnWrite_Click" />
                
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
