<%@ Page Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master"
    AutoEventWireup="true" CodeFile="ProductPriceAdd.aspx.cs" Inherits="ControlPanel_product_ProductPriceAdd"
    Title="产品报价" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="ContentOpts" ContentPlaceHolderID="cphOpts" runat="Server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="postform" cellpadding="10" cellspacing="10">
        <tr>
            <th style="width: 100px;">
                产品名称
            </th>
            <td>
                <asp:HyperLink ID="hyProductName" runat="server"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                最短供货时间
            </th>
            <td>
                <hc:DateSpan runat="server" ID="piDeliverySpan" />
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                产品保修期
            </th>
            <td>
                <hc:DateSpan runat="server" ID="piWarrantySpan" />
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                包含运费
            </th>
            <td>
                <hc:IncludeTypeList runat="server" ID="piFreight">
                </hc:IncludeTypeList>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                包含税
            </th>
            <td>
                <hc:IncludeTypeList runat="server" ID="piTax">
                </hc:IncludeTypeList>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                供货税率
            </th>
            <td>
                <asp:TextBox ID="txtApplyTaxRate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                最小订货量
            </th>
            <td>
                <asp:TextBox ID="txtQuoteMOQ" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                市场价格
            </th>
            <td>
                <asp:TextBox ID="txtPriceMarket" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                促销价格
            </th>
            <td>
                <asp:TextBox ID="txtPricePromotion" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                保底价格
            </th>
            <td>
                <asp:TextBox ID="txtPriceFloor" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                采购价格
            </th>
            <td>
                <asp:TextBox ID="txtPricePurchase" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                五级会员价格
            </th>
            <td>
                <asp:TextBox ID="txtPriceGradeA" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                四级会员价格
            </th>
            <td>
                <asp:TextBox ID="txtPriceGradeB" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                三级会员价格
            </th>
            <td>
                <asp:TextBox ID="txtPriceGradeC" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                二级会员价格
            </th>
            <td>
                <asp:TextBox ID="txtPriceGradeD" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                一级会员价格
            </th>
            <td>
                <asp:TextBox ID="txtPriceGradeE" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                供货区域
            </th>
            <td>
                <asp:DropDownList ID="ddlSupplyRegion" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                报价起始日期
            </th>
            <td>
                <asp:TextBox ID="txtQuoteFrom" rel="datepicker" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revQuoteFrom" runat="server" ControlToValidate="txtQuoteFrom"
                    ValidationExpression="\d{4}年\d{1,2}月\d{1,2}日" ErrorMessage="必须为日期格式 - yyyy年MM月dd日！">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                报价截止日期
            </th>
            <td>
                <asp:TextBox ID="txtQuoteEnd" rel="datepicker" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revQuoteEnd" runat="server" ControlToValidate="txtQuoteEnd"
                    ValidationExpression="\d{4}年\d{1,2}月\d{1,2}日" ErrorMessage="必须为日期格式 - yyyy年MM月dd日！">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                报价自动续期周期
            </th>
            <td>
                <asp:TextBox ID="txtQuoteRenewal" runat="server"></asp:TextBox>月
            </td>
        </tr>
        <tr>
            <th>
                是否启用
            </th>
            <td>
                <hc:ComponentStatusList ID="csPrice" runat="server" />
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
