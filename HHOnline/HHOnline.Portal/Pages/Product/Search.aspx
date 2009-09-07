<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

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
    <div class="searchDuration"><asp:Literal ID="ltSearchDuration" runat="server"></asp:Literal></div>
    
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
            <hc:CollectionPager ID="cpProduct" PageSize="10" runat="server"></hc:CollectionPager>
        </div>
</div>
</asp:Content>

