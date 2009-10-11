<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="AddCartSuccess.aspx.cs" Inherits="Pages_Profiles_AddCartSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellpadding="0" cellspacing="0">
    <tr>
        <td style="height:100px;line-height:100px; text-align:center;">你所选择购买的产品已经被放入购物车！</td>
    </tr>
    <tr>
        <th style="padding:10px; text-align:center;">
            <asp:Button ID="btnSeeShopCart" Text="查看购物车" runat="server" />
            <asp:Button ID="btnContinue" Text="继续购物>>" runat="server" OnClientClick="return cancel();" />
        </th>
    </tr>
</table>
</asp:Content>

