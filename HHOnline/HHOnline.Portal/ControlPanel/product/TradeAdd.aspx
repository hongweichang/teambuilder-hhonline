<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="TradeAdd.aspx.cs" Inherits="ControlPanel_product_TradeAdd"
    Title="无标题页" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr runat="server" id="parentRow" visible="false">
            <th>
                父级行业名称
            </th>
            <td>
                <asp:Label runat="server" ID="lblParentName" />
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                行业名称
            </th>
            <td>
                <asp:TextBox ID="txtIndustryName" runat="server" Width="200" MaxLength="50" />
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="行业名称不能为空。" Display="Dynamic"
                    ControlToValidate="txtIndustryName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                标题说明
            </th>
            <td>
                <asp:TextBox ID="txtIndustryTitle" runat="server" Width="500" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <th>
                介绍摘要
            </th>
            <td>
                <asp:TextBox ID="txtIndustryAbstract" runat="server" Width="500" />
            </td>
        </tr>
        <tr>
            <th>
                介绍内容
            </th>
            <td>
                <hc:Editor ID="txtIndustryContent" runat="server" Width="500" />
            </td>
        </tr>
        <tr>
            <th>
                行业图标
            </th>
            <td>
                <asp:Image runat="server" ID="imgLogo" Width="80" Height="80" />
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:FileUpload ID="fuLogo" runat="server" Width="200" />
            </td>
        </tr>
        <tr>
            <th>
                排序序号
            </th>
            <td>
                <asp:TextBox ID="txtDisplayOrder" runat="server" MaxLength="8" Text="0"></asp:TextBox>
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
                <hc:ComponentStatusList ID="csIndustry" runat="server" />
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
