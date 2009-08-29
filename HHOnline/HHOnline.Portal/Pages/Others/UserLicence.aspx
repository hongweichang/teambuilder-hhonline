<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="UserLicence.aspx.cs" Inherits="Pages_Others_UserLicence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div class="homemastercontent userLicenceContent">
    <h4>用户注册协议</h4>
    <asp:TextBox ID="txtLicence" runat="server" Width="600px" Height="500px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
</div>
</asp:Content>

