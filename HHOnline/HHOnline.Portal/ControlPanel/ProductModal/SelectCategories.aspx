<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="SelectCategories.aspx.cs" Inherits="ControlPanel_ProductModal_SelectCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table cellpadding="0" cellspacing="0" class="detail" >
    <tr>
        <td style="height:300px;" valign="top">
            <div class="sc_nav" id="sc_select">
                
            </div>
            <div id="sc_list">
                加载中。。。
            </div>
        </td>
    </tr>
    <tr>
        <th style="text-align:right">
            <asp:Button ID="btnPost" runat="server" Text=" 确 定 " CausesValidation="false" OnClientClick="insertCats();return cancel();"></asp:Button>
            <asp:Button ID="btnClose" runat="server" Text=" 关 闭 " CausesValidation="false" OnClientClick="return cancel();"></asp:Button>
        </th>
    </tr>
</table>
</asp:Content>

