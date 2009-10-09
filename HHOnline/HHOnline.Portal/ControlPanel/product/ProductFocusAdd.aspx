<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ProductFocusAdd.aspx.cs" Inherits="ControlPanel_product_ProductFocusAdd"
    Title="产品关注" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th style="width: 150px;">
                产品名称
            </th>
            <td>
                <asp:HyperLink ID="hyProductName" runat="server"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                关注类别(<span style="color: #ff0000">必填</span>)
            </th>
            <td>
                <hc:FocusTypeList ID="ddlFocusType" runat="server" RepeatDirection="Horizontal" SelectedValue="New" CssClass="focusType">
                </hc:FocusTypeList>
            </td>
        </tr>
        <tr>
            <th>
                关注起始日期(<span style="color: #ff0000">必填</span>)
            </th>
            <td>
                <asp:TextBox ID="txtFocusFrom" rel="datepicker1" runat="server" Width="200px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revQuoteFrom" runat="server" ControlToValidate="txtFocusFrom"
                    ValidationExpression="\d{4}年\d{1,2}月\d{1,2}日" ErrorMessage="必须为日期格式 - yyyy年MM月dd日！">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="关注起始日期不能为空。" Display="Dynamic"
                    ControlToValidate="txtFocusFrom"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                关注截止日期(<span style="color: #ff0000">必填</span>)
            </th>
            <td>
                <asp:TextBox ID="txtFocusEnd" rel="datepicker2" runat="server" Width="200px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revQuoteEnd" runat="server" ControlToValidate="txtFocusEnd"
                    ValidationExpression="\d{4}年\d{1,2}月\d{1,2}日" ErrorMessage="必须为日期格式 - yyyy年MM月dd日！">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="关注截止日期不能为空。" Display="Dynamic"
                    ControlToValidate="txtFocusEnd"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                备注
            </th>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine"  Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                排序序号
            </th>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" MaxLength="8" Text="0" Width="200px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtDisplayOrder"
                    ValidationExpression="(\d){1,3}" ErrorMessage="必须为0-999的数字"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfv3" Display="Dynamic" runat="server" ControlToValidate="txtDisplayOrder"
                    ErrorMessage="必须填写！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                是否启用
            </th>
            <td>
                <hc:ComponentStatusList ID="csFocus" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <hc:MsgBox ID="mbMessage" runat="server" SkinID="msgBox"></hc:MsgBox>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnPost" runat="server" Text="提 交" PostBackUrl="#" Visible="false"
                    OnClick="btnPost_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnPostBack" runat="server" Text="返 回" Visible="false" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
