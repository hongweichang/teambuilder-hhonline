<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="SupplyDetails.aspx.cs" Inherits="ControlPanel_product_SupplyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" Runat="Server">
<table class="detail" cellspacing="0" cellspacing="0">
    <tr>
        <th style="width:120px;">公司</th>
        <td colspan="3"><asp:Literal ID="ltCompanyName" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th>供货区域</th>
        <td><asp:Literal ID="ltRegion" runat="server"></asp:Literal></td>
        <th>最短供货时间</th>
        <td><hc:DateSpan ID="dsDeliverySpan" Enabled="false" runat="server" /></td>
    </tr>
    <tr>
        <th>最小订货量</th>
        <td><asp:Literal ID="ltAmount" runat="server"></asp:Literal></td>
        <th>单价(元)</th>
        <td><asp:Literal ID="ltPrice" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th>报价起始日期</th>
        <td><asp:Literal ID="ltStartTime" runat="server"></asp:Literal></td>
        <th>报价截止日期</th>
        <td><asp:Literal ID="ltEndTime" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th>报价自动延续周期</th>
        <td><asp:Literal ID="ltQuoteRenewal" runat="server"></asp:Literal></td>
        <th>产品保修期</th>
        <td><hc:DateSpan ID="dsWS" Enabled="false" runat="server" /></td>
    </tr>
    <tr>
        <th>是否包含运费</th>
        <td><asp:Literal ID="ltHasfeight" runat="server"></asp:Literal></td>
        <th>是否正常供应</th>
        <td><asp:Literal ID="ltSupply" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th>是否含税</th>
        <td><asp:Literal ID="ltIncludeTax" runat="server"></asp:Literal></td>
        <th>供货税率</th>
        <td><asp:Literal ID="ltFaxRate" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th>创建时间</th>
        <td><asp:Literal ID="ltCreateTime" runat="server"></asp:Literal></td>
        <th>创建者</th>
        <td><asp:Literal ID="ltCreateUser" runat="server"></asp:Literal></td>
    </tr>
</table>
</asp:Content>

