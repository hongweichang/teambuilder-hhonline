<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Pages_Product_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
<script type="text/javascript">
    var activeTab = 'product';
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" Runat="Server">
    <huc:Search ID="schMain" runat="server" /></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div class="homemastercontent productDetails">
    <h3 class="title"><asp:Literal ID="ltProductName" runat="server"></asp:Literal></h3>
    <div class="description">
        <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
    </div>
    <div class="content">
        <div class="content-r1c1" id="divPics"></div>
        <div class="content-r1c2" >
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <th style="width:70px">编码：</th>
                    <td><asp:Literal ID="ltProductCode" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th style="width:70px">分类：</th>
                    <td class="nav"><asp:Literal ID="ltCategory" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th style="width:70px">行业：</th>
                    <td class="nav"><asp:Literal ID="ltIndustry" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th style="width:70px">品牌：</th>
                    <td class="nav"><asp:Literal ID="ltBrand" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th valign="top">摘要：</th>
                    <td valign="top"><asp:Literal ID="ltProductAbstract" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th valign="top">价格：</th>
                    <td valign="top" class="price"><asp:Literal ID="ltPrice" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th>&nbsp;</th>
                    <td>
                        <a id="anchorAddFav" href="javascript:{}" onfocus="this.blur()" class="favcar addfav"></a>
                        <a id="anchorAddCar" href="javascript:{}" onfocus="this.blur()" class="favcar addcar"></a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="content-r2">
        <fieldset>
            <legend>产品属性</legend>
            <div class="descDetails">
                <hc:MsgBox ID="msgBox"  runat="server"></hc:MsgBox>
                <asp:Repeater ID="rpProperties" runat="server">
                    <HeaderTemplate>
                        <table class="propertyForm" cellpadding="0" cellspacing="0">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th><%#Eval("PropertyName") %></th>
                            <td><%#Eval("PropertyValue") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </fieldset>
        <fieldset>
            <legend>相关描述</legend>
            <div class="descDetails"><asp:Literal ID="ltDescription1" runat="server"></asp:Literal></div>
        </fieldset>
    </div>
</div>
</asp:Content>

