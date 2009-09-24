<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/AdminMasterPage.master" AutoEventWireup="true" CodeFile="CompanyUsers.aspx.cs" Inherits="Pages_Profiles_CompanyUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphOpts" Runat="Server">
<asp:LinkButton ID="lbNewRole" runat="server" SkinID="lnkopts" OnClientClick="return addUser();">
    <span>新 增</span>
</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
<hc:MsgBox ID="mbNC" runat="server" SkinID="msgBox"></hc:MsgBox>
<asp:Repeater ID="rpList" runat="server" OnItemCommand="rpList_ItemCommand">
    <HeaderTemplate>
        <table class="postform" cellpadding="10" cellspacing="10">
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <th style="width:200px;text-align:left;"><%# Eval("DisplayName") %></th>
            <td>
                <a class="opts edit" href="javascript:{}" onclick='editUser(<%# Eval("UserID") %>)'></a>
                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("UserID") %>' SkinID="lnkdelete" CommandName="Delete" OnClientClick="return confirm('删除后，此账户将被永久停止使用，确定继续？')" PostBackUrl="#"></asp:LinkButton>
                <a class="opts view" href="javascript:{}" onclick='showDetails(<%# Eval("UserID") %>)'></a>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
</asp:Content>

