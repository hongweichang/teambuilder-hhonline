<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/MsgMasterPage.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Pages_Messages_Error" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMsg" Runat="Server">
<div class="error-content">
    <h3><asp:Literal ID="ltTitle" runat="server"></asp:Literal></h3>
    <p><asp:Literal ID="ltBody" runat="server"></asp:Literal></p>
    <br />
    <h3>详细信息</h3>
    <p><asp:Literal ID="ltDetails" runat="server"></asp:Literal></p>
    <br />
    <p id="buttonholder"><asp:Button ID="btnBack" runat="server" Text="<<返回首页" />&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="btnPostBack" runat="server" Text="重新尝试>>"  /></p>
</div>
</asp:Content>

