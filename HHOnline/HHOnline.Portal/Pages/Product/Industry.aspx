<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Industry.aspx.cs" Inherits="Pages_Product_Industry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
<script type="text/javascript">
    var activeTab = 'product';
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" Runat="Server">
    <huc:Search ID="sMain" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div class="homemastercontent">
    <div class="cdCatNavigate">
        <hc:IndustryNavigate ID="inProduct" runat="server"></hc:IndustryNavigate>
    </div>
    <div style="padding-left:20px;">
        <hc:HPIndustryList ID="hpilProduct" runat="server" CssClass="hpCategoryList pdCategoryList" Columns="1" />
    </div>
    <div class="navigateData">相关行业</div>
    <div>
        <hc:IndustryLikeList ID="illProduct" runat="server" CssClass="cllProduct"></hc:IndustryLikeList>
    </div>
     <div class="navigateData">子行业信息</div>
    <div>
        <hc:IndustrySubList ID="islProduct" runat="server" CssClass="cllProduct"></hc:IndustrySubList>
    </div>
    <asp:Panel ID="pnlSort" runat="server">
        <div class="sortList">
            <div class="sortViewTitle">显示方式：</div>
            <asp:LinkButton ID="lnkGrid" OnClick="linkButton_Click"  CssClass="showByGrid showBy" runat="server" Text=" " ToolTip="按列表显示"></asp:LinkButton>
            <asp:LinkButton ID="lnkList" OnClick="linkButton_Click" CssClass="showByList showBy" runat="server" ToolTip="按大图显示"></asp:LinkButton>
            <div class="sortByTitle">排序：</div>
            <div class="sortBy">
                <asp:DropDownList  ID="ddlSortBy" runat="server" Width="130px" Height="20px" onchange="beginCallFunction(this)">
                    <asp:ListItem Text="默认排序" Value="None" ></asp:ListItem>
                    <asp:ListItem Text="按时间排序" Value="Date" ></asp:ListItem>
                    <asp:ListItem Text="按名称排序" Value="PruductName"></asp:ListItem>
                    <asp:ListItem Text="按品牌排序" Value="Variety"></asp:ListItem>
                    <asp:ListItem Text="按价格由高到低" Value="PriceDesc"></asp:ListItem>
                    <asp:ListItem Text="按价格由低到高" Value="PriceAsc"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </asp:Panel>
    <div style="padding:5px;">
     <hc:MsgBox ID="msgBox" SkinID="msgBox" runat="server"></hc:MsgBox>
            <asp:DataList ID="dlProduct" runat="server" OnItemDataBound="dlProduct_ItemDataBound" RepeatColumns="1" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" >
                <ItemTemplate>
                    <div class="productGridShow">
                        <asp:Image BorderWidth="4" BorderColor="#dedede" ID="imgProduct" runat="server" />
                        <div class="productTitle">
                            <div class="productTitle-r1">
                                <a href='product-product&ID=<%# HHOnline.Framework.GlobalSettings.Encrypt(Eval("ProductID").ToString()) %>' target="_blank"><%# Eval("ProductName") %></a>
                            </div>
                            <div class="productTitle-r2">
                                <%# Eval("ProductAbstract")%>
                            </div>
                            <div class="productTitle-r3">
                                关键字：<%# Eval("ProductKeywords")%>
                            </div>
                            <br />
                        </div>
                        <div class="productPrice price"><asp:Literal ID="ltPrice" runat="server"></asp:Literal></div>
                        <div class="productBrand">品牌：
                        <a href='product-brand&ID=<%# HHOnline.Framework.GlobalSettings.Encrypt(Eval("BrandID").ToString()) %>' target="_blank">
                            <%# Eval("BrandName")%>
                        </a></div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <div style="width:100%;float:left">
                <asp:DataList ID="dlProduct2" runat="server" OnItemDataBound="dlProduct_ItemDataBound"
                    RepeatDirection="Horizontal" RepeatLayout="Flow" >
                    <ItemTemplate>
                        <div class="productGridShow2">
                            <div class="imgborder"><asp:Image BorderWidth="4" BorderColor="#dedede" ID="imgProduct" runat="server" /></div>
                            <div class="productTitle">
                                    <a href='product-product&ID=<%# HHOnline.Framework.GlobalSettings.Encrypt(Eval("ProductID").ToString()) %>' target="_blank"><%# Eval("ProductName") %></a>
                            </div>
                            <div class="productPrice"><asp:Literal ID="ltPrice" runat="server"></asp:Literal></div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <hc:CollectionPager ID="cpProduct" PageSize="10" runat="server"></hc:CollectionPager>
    </div>
</div>
</asp:Content>

