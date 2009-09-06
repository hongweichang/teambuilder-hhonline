<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Pages_Product_Category" %>

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
        <hc:CategoryNavigate ID="cnProduct" runat="server"></hc:CategoryNavigate>
    </div>
    <div style="padding-left:20px;">
        <hc:CategoryList ID="clProduct" runat="server" CssClass="hpCategoryList pdCategoryList" Columns="1" />
    </div>
    <div class="navigateData">相关类别</div>
    <div>
        <hc:CategoryLikeList ID="cllProduct" runat="server" CssClass="cllProduct"></hc:CategoryLikeList>
    </div>
     <div class="navigateData">子分类信息</div>
    <div>
        <hc:CategorySubList ID="cslProduct" runat="server" CssClass="cllProduct"></hc:CategorySubList>
    </div>
    <asp:Panel ID="pnlSort" runat="server">
    <div class="sortList">
        <asp:LinkButton ID="lnkGrid" OnClick="linkButton_Click"  CssClass="showByGrid showBy" runat="server" Text=" " ToolTip="按列表显示"></asp:LinkButton>
        <asp:LinkButton ID="lnkList" OnClick="linkButton_Click" CssClass="showByList showBy" runat="server" ToolTip="按大图显示"></asp:LinkButton>
    </div>
    </asp:Panel>
    <div style="padding:5px;">
        <asp:Literal ID="ltProductList" runat="server"></asp:Literal>
        <asp:DataList ID="dlProduct" runat="server" OnItemDataBound="dlProduct_ItemDataBound" RepeatColumns="1" 
            RepeatDirection="Horizontal" RepeatLayout="Flow" >
            <ItemTemplate>
                <div class="productGridShow">
                    <asp:Image BorderWidth="4" BorderColor="#dedede" ID="imgProduct" runat="server" />
                    <div class="productTitle">
                        <div class="productTitle-r1">
                            <a href='product-product&ID=<%# Eval("ProductID") %>' target="_blank"><%# Eval("ProductName") %></a>
                        </div>
                        <div class="productTitle-r2">
                            <%# Eval("ProductAbstract")%>
                        </div>
                        <div class="productTitle-r3">
                            关键字：<%# Eval("ProductKeywords")%>
                        </div>
                        <br />
                    </div>
                    <div class="productPrice"><asp:Literal ID="ltPrice" runat="server"></asp:Literal></div>
                    <div class="productBrand">品牌：
                    <a href='product-variety&ID=<%# Eval("BrandID") %>' target="_blank">
                        <%# Eval("BrandName")%>
                    </a></div>
                </div>
            </ItemTemplate>
        </asp:DataList>
        <asp:DataList ID="dlProduct2" runat="server" OnItemDataBound="dlProduct_ItemDataBound"
            RepeatDirection="Horizontal" RepeatLayout="Flow" >
            <ItemTemplate>
                <div class="productGridShow2">
                    <div class="imgborder"><asp:Image BorderWidth="4" BorderColor="#dedede" ID="imgProduct" runat="server" /></div>
                    <div class="productTitle">
                            <a href='product-product&ID=<%# Eval("ProductID") %>' target="_blank"><%# Eval("ProductName") %></a>
                    </div>
                    <div class="productPrice"><asp:Literal ID="ltPrice" runat="server"></asp:Literal></div>
                </div>
            </ItemTemplate>
        </asp:DataList>
        <hc:CollectionPager ID="cpProduct" PageSize="10" runat="server"></hc:CollectionPager>
    </div>
</div>
</asp:Content>

