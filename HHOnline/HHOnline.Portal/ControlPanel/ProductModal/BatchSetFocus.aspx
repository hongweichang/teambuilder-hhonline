<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="BatchSetFocus.aspx.cs" Inherits="ControlPanel_ProductModal_BatchDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" >
    <tr>
        <th>栏目类型(30天有效期)</th>
        <td style="height:150px">
            <hc:MsgBox ID="mbMsg" runat="server" SkinID="msgBox"></hc:MsgBox>
            <hc:FocusTypeList ID="ftl1" runat="server" RepeatDirection="Horizontal" SelectedValue="None" CssClass="focusType"></hc:FocusTypeList>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:Button ID="btnPost" runat="server" Text=" 保存 " OnClick="btnPost_Click" PostBackUrl="#">
            </asp:Button>
            <asp:Button ID="btnClose" runat="server" Text=" 取消 " CausesValidation="false" OnClientClick="return cancel();">
            </asp:Button>
        </td>
    </tr>
</table>
</asp:Content>

