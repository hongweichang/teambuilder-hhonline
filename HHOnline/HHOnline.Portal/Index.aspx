<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HomePage</title>
    <script type="text/javascript">
        window.onload = function() {
            $('input[rel=datepicker]').datepick({
                year: '-40:5',
                popTo: 'input[rel=datepicker]',
                dateFormat: 'yyyy年MM月dd日'
            });
            $('#seldate').datepick({
                popTo: 'input[rel=datepicker2]',
                year: '-10:2',
                dateFormat: 'yyyy/MM/dd'
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        PickDatetime1:<asp:TextBox rel="datepicker" ID="txtDate" runat="server"></asp:TextBox><br />
        PickDatetime2:<asp:TextBox rel="datepicker2" ID="TextBox1" runat="server"></asp:TextBox><img style="cursor:pointer;" id="seldate" src="Images/Datepick/calendar.png" alt="点击选择日期" /><br />
        SiteName:<asp:TextBox ID="txtSiteName" runat="server"></asp:TextBox>
        <br />
        SMTP：<asp:TextBox ID="txtSmtpServer" runat="server"></asp:TextBox>
        <br />
        UserName:<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        <br />
        Password:<asp:TextBox ID="txtPwd" runat="server" OnTextChanged="txtPwd_TextChanged"
            Style="margin-top: 0px" Width="128px"></asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="New Pwd:"></asp:Label>
        <asp:TextBox ID="txtNewPwd" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
        <asp:Label ID="lblTip" runat="server"></asp:Label>
        <asp:Label ID="lblRes" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Button ID="Button21" runat="server" Text="Add Cache" OnClick="Button21_Click" />
        <asp:Button ID="Button22" runat="server" OnClick="Button22_Click" Text="Get Cache" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Clear Cache" />
        <asp:Label ID="lblCacheTip" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Create User" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="UserLogin" />
        <asp:Button ID="Button4" runat="server" Text="Change Pwd" OnClick="Button4_Click" />
        <br />
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Update User" />
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Delete User" />
        <asp:Button ID="Button7" runat="server" Text="Get Users" OnClick="Button7_Click" />
        <asp:Button ID="Button8" runat="server" Text="Get InactiveUser" OnClick="Button8_Click" />
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="添加资质文件" />
        <asp:Button ID="Button14" runat="server" OnClick="Button14_Click" Text="获取资质文件" />
        <asp:HyperLink ID="HyperLink1" runat="server">下载地址</asp:HyperLink>
        <br />
        <asp:Button ID="Button15" runat="server" Text="获取所有资质文件" OnClick="Button15_Click" />
        <asp:PlaceHolder ID="phLinks" runat="server"></asp:PlaceHolder>
        <br />
        <asp:Button ID="Button10" runat="server" Text="Create Company" OnClick="Button10_Click" />
        <asp:Button ID="Button11" runat="server" Text="Update Company" OnClick="Button11_Click" />
        <asp:Button ID="Button12" runat="server" Text="Delete Company" OnClick="Button12_Click" />
        <asp:Button ID="Button13" runat="server" Text="Get Company" OnClick="Button13_Click" />
        <asp:Button ID="Button16" runat="server" Text="Throw Exception" OnClick="Button16_Click" />
        <br />
        <asp:Button ID="Button17" runat="server" Text="Get All Tags" OnClick="Button17_Click" />
        <asp:Button ID="Button18" runat="server" OnClick="Button18_Click" Text="Get ArticleTag" />
        <asp:Button ID="Button19" runat="server" Text="Update Article Tag" OnClick="Button19_Click" />
        <br />
        <asp:Button ID="Button20" runat="server" Text="Test Exception" OnClick="Button20_Click" />
        <br />
        <asp:Literal ID="lblTips" runat="server" />
        <br />
        <asp:Button ID="Button23" runat="server" Text="Create Organization" OnClick="Button23_Click" />
        <asp:Button ID="Button24" runat="server" Text="Delete Organization" />
        <asp:Button ID="Button25" runat="server" Text="Get Organizations" OnClick="Button25_Click" />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button26" runat="server" Text="GetArea" OnClick="Button26_Click" />
        <asp:Button ID="Button27" runat="server" Text="GetChildArea" OnClick="Button27_Click" />
        <asp:Button ID="Button28" runat="server" Text="GetAllChildArea" OnClick="Button28_Click" />
        <asp:Button ID="Button39" runat="server" onclick="Button39_Click" 
            Text="GetParentArea" />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label">
        <br />
        </asp:Label><asp:Button ID="Button29" runat="server" Text="View Article" OnClick="Button29_Click" />
        <asp:Button ID="Button30" runat="server" OnClick="Button30_Click" Text="Build Text" />
        <asp:Button ID="Button31" runat="server" OnClick="Button31_Click" Text="Revolve" />
        <asp:Label ID="Label4" runat="server"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList>
        <br />
        <asp:FileUpload ID="FileUpload2" runat="server" />
        <asp:Button ID="Button32" runat="server" Text="Add Brand" OnClick="Button32_Click" />
        <asp:Button ID="Button33" runat="server" OnClick="Button33_Click" Text="Get Brand" />
        <asp:Image ID="Image1" runat="server" Height="80px" Width="80px" />
        <asp:HyperLink ID="HyperLink2" runat="server"></asp:HyperLink>
        <br />
        <asp:Button ID="Button34" runat="server" Text="Add Industry" OnClick="Button34_Click" />
        <asp:Button ID="Button35" runat="server" Text="Get Indstries" />
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button36" runat="server" OnClick="Button36_Click" Text="Get Dom" />
        <br />
        <br />
        <asp:Button ID="Button37" runat="server" OnClick="Button37_Click" Text="Get All Category" />
        <asp:Button ID="Button38" runat="server" OnClick="Button38_Click" Text="Get All Article" />
        <hc:Editor EditorMode="Enhanced" runat="server" ID="txtEditor" />
        <a href="javascript:void(0)" onmousedown="<%=txtEditor.GetContentInsertScript("abc") %>">
            Insert Content</a> <a href="javascript:InsertContentToHtmlEditor('<b>abc</b>')">Insert
                Content</a>
    </div>
    </form>
</body>
</html>
