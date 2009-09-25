<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/AdminMasterPage.master" AutoEventWireup="true" CodeFile="CompanyTypeView.aspx.cs" Inherits="Pages_Profiles_CompanyTypeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphOpts" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
<table cellpadding="10" cellspacing="10" class="postform">
    <tr>
        <th style="width:140px;">当前类型</th>
        <td><asp:Literal ID="ltComType" runat="server"></asp:Literal></td>
    </tr>
</table>
</asp:Content>

