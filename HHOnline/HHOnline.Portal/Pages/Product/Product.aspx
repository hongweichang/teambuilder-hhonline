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
        <div class="pdLeft">
            <div class="content-r1c1" id="divPics"></div>
            <div class="content-r1c2" >
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <th style="width:60px">编码：</th>
                        <td colspan="3"><asp:Literal ID="ltProductCode" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <th>品牌：</th>
                        <td colspan="3"><asp:Literal ID="ltBrand2" runat="server"></asp:Literal></td>
                    </tr>
                   <tr>
                        <th>名称：</th>
                        <td colspan="3"><asp:Literal ID="ltProductName2" runat="server"></asp:Literal></td>
                    </tr>
                    <tr runat="server" visible="false">
                        <th valign="top">摘要：</th>
                        <td colspan="3" valign="top"><asp:Literal ID="ltProductAbstract" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <th valign="top">属性：</th>
                        <td colspan="3" valign="top">
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
                        </td>
                    </tr>
                    <tr>
                        <th valign="top"><asp:Label ID="lblPrice" runat="server" Text="市场价"></asp:Label>：</th>
                        <td valign="top" class="price"><asp:Literal ID="ltPrice1" runat="server"></asp:Literal></td>
                        <th valign="top">会员价：</th>
                        <td valign="top" class="price"><asp:Literal ID="ltPrice2" runat="server" Text="登录后显示"></asp:Literal></td>
                    </tr>
                    <tr runat="server" visible="false">
                        <th>型号：</th>
                        <td colspan="3" valign="top" id="modelTracer">
                            <asp:Literal ID="ltModel" runat="server"></asp:Literal>
                            <asp:RadioButtonList ID="rbModel" RepeatLayout="Flow" runat="server" ></asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="productPriceOpts">
                <div class="ppo-r1">购买：<input type="text" id="txtAmount" value="1" maxlength="4" />&nbsp;件。&nbsp;<span id="spnMsg">**请填写正确的数目！</span></div>
                <div class="ppo-r2">
                    <a href="javascript:{}" onfocus="this.blur()" class="favcar addprice"></a>
                    <a id="anchorAddCar" href="javascript:{}" onfocus="this.blur()" class="favcar addcar"></a>
                    
    
                </div>
                <a href="javascript:{}" id="anchorAddFav" title="加为收藏" class="addProductFav">&nbsp;</a>
            </div>
            <div class="productDetailsView">
                <div class="pdtitle">产品简介</div>
                <div class="descDetails"><asp:Literal ID="ltPAbstract" runat="server"></asp:Literal></div>
                <br />
                 <div class="pdtitle">详细介绍</div>
                <div class="descDetails"><asp:Literal ID="ltProductDetails" runat="server"></asp:Literal></div>
                <br />
                 <div class="pdtitle">相关产品</div>
                <div class="descDetails">
                <hc:productLikeList ID="pllProduct" CssClass="productPromotionList" runat="server"></hc:productLikeList>
                </div>
            </div>
        </div>
        <div class="main-r4c2 pdRight">
            <div class="list-title">
                <div class="title-left">
                </div>
                <div class="title-content">
                    品牌介绍</div>
                <div class="title-right">
                </div>
            </div>
            <div class="list-content">
                <div class="industryAbstract"><asp:Literal ID="ltBrand" runat="server"></asp:Literal></div>
            </div>
            <br />
             <div class="list-title">
                <div class="title-left">
                </div>
                <div class="title-content">
                    所属分类</div>
                <div class="title-right">
                </div>
            </div>
            <div class="list-content">
                <asp:Literal ID="ltCategory" runat="server"></asp:Literal>
            </div>
            <br />
            
             <div class="list-title">
                <div class="title-left">
                </div>
                <div class="title-content">
                    相关行业</div>
                <div class="title-right">
                </div>
            </div>
            <div class="list-content">
                <asp:Literal ID="ltIndustry" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</div>
</asp:Content>

