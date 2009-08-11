<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="DenyUser.aspx.cs" Inherits="ControlPanel_Site_DenyUser" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <div style="padding:10px 0 5px 40px">
        使用下面的列表来指定不能在用户名中使用的词语( <b>每行一个名字</b> ).
    </div>
    <div  style="padding:10px 0 5px 40px">
        <asp:TextBox ID="Usernames" TextMode="Multiline" Width="400px" Height="200px" runat="server" />
    </div>
    <div  style="padding:10px 0 5px 40px">
        <hc:MsgBox ID="mbMessage" runat="server" SkinID="msgBox"></hc:MsgBox>
        <asp:Button ID="btnPost" runat="server" Text="更 新" PostBackUrl="#" Visible="true"
            OnClick="btnPost_Click" />&nbsp;&nbsp;
    </div>
</asp:Content>
