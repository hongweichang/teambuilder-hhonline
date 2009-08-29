<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="UserLicence.aspx.cs" Inherits="ControlPanel_Users_UserLicence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
<asp:TextBox ID="txtLicence" runat="server" Width="600px" Height="400px" TextMode="MultiLine"></asp:TextBox>
<br />
<br />
<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存协议" />
</asp:Content>

