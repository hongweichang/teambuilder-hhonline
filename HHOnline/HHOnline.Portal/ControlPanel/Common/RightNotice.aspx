<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="RightNotice.aspx.cs" Inherits="ControlPanel_Common_RightNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
    <hc:Editor ID="txtAbout" runat="server" EditorMode="Enhanced" Width="500px"></hc:Editor>
    <br />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text=" 保存信息 " />
</asp:Content>

