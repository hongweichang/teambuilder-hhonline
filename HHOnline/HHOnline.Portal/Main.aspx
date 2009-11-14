<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/HomeMasterPage.master"
    AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" runat="Server">
    <huc:Search ID="sMain" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <div class="main-r3">
        <div class="main-r3c1">
            <div class="main-r3c1-r1">
                <div class="main-r3c1-r1c1">
                </div>
                <div class="main-r3c1-r1c2">
                    <span class="service-title service-title1">&nbsp;</span></div>
                <div class="main-r3c1-r1c3">
                </div>
            </div>
            <div class="main-r3c1-r2">
                <asp:Literal ID="ltIdeal" runat="server"></asp:Literal></div>
            <div class="main-r3c1-r1 main-r3c1-r3">
                <div class="main-r3c1-r1c1">
                </div>
                <div class="main-r3c1-r1c2">
                    <span class="service-title service-title2">&nbsp;</span></div>
                <div class="main-r3c1-r1c3">
                </div>
            </div>
            <div class="main-r3c1-r2 main-r3c1-r4">
                <asp:Literal ID="ltService" runat="server"></asp:Literal></div>
        </div>
        <div class="main-r3c2 loader04" id="divAdLogo">
        </div>
        <div class="main-r3c3">
            <hc:HHLoginView ID="hhView" runat="server" PanelLeftCss="main-r3c3-c1 access-point"
                PanelMiddleCss="main-r3c3-c2 access-point" PanelRightCss="main-r3c3-c3 access-point"
                RegisterUrl="register.aspx" RegisterCss="login-access register-access" LoginUrl="login.aspx"
                LoginCss="login-access signup-access" InfoCss="info-access" ProfileUrl="pages/view.aspx?profiles-dashboard"
                MsgCss="msg-access" Message="抢先加盟，获取更多优惠和合作机会！" />
        </div>
    </div>
    <div class="main-r4">
        <div class="main-r4c1">
            <div class="tab-nav">
                <ul id="productNavigator1">
                    <li class="active" rel="product">产品</li>
                    <li rel="variety">品牌</li>
                    <li rel="trade">行业</li>
                </ul>
            </div>
            <div class="tab-content">
                <div id="productTabContent" class="productTabContent">
                    <div class="lclBorder">
                    <hc:LettersCollectionList ID="lclListCategory" LetterType="Category" runat="Server" CssName="lettersCL"></hc:LettersCollectionList>
                    </div>
                    <hc:CategoryList ID="clProduct" runat="server" CssClass="hpCategoryList" Max="100"></hc:CategoryList>
                </div>
                <div id="varietyTabContent" class="productTabContent" style="display:none">
                    <div class="lclBorder">
                    <hc:LettersCollectionList ID="LettersCollectionList1" LetterType="Brand" runat="Server" CssName="lettersCL"></hc:LettersCollectionList>
                    </div>
                    <hc:HPVarietyList ID="hpvlProduct" runat="server" CssClass="hpCategoryList"></hc:HPVarietyList>
                </div>
                <div id="tradeTabContent" class="productTabContent" style="display:none">
                    <div class="lclBorder">
                    <hc:LettersCollectionList ID="LettersCollectionList2" LetterType="Industry" runat="Server" CssName="lettersCL"></hc:LettersCollectionList>
                    </div>
                    <hc:HPIndustryList ID="hpilProduct" runat="server" CssClass="hpCategoryList"></hc:HPIndustryList>
                </div>
            </div>
            <br />
            <div class="tab-nav">
                <ul id="productNavigator2">
                    <li class="active" rel="promotion">促销</li>
                    <li rel="hot">热卖</li>
                    <li rel="new">新品</li>
                    <li rel="recommend">推荐</li>
                </ul>
            </div>
            <div class="tab-content">
                <div id="promotionTabContent" class="productTabContent">
                    <hc:ProductPromotionList ID="pplMain" runat="server" ProductType="Promotion" CssClass="productPromotionList" Columns="4" />
                </div>
                
                <div id="recommendTabContent" class="productTabContent" style="display:none">
                    <hc:ProductPromotionList ID="ProductPromotionList1" runat="server" ProductType="Recommend" CssClass="productPromotionList" Columns="4" />
                </div>
                
                <div id="hotTabContent" class="productTabContent" style="display:none">
                    <hc:ProductPromotionList ID="ProductPromotionList2" runat="server" ProductType="Hot" CssClass="productPromotionList" Columns="4" />
                </div>
                
                <div id="newTabContent" class="productTabContent" style="display:none">
                    <hc:ProductPromotionList ID="ProductPromotionList3" runat="server" ProductType="New" CssClass="productPromotionList" Columns="4" />
                </div>
            </div>
        </div>
        <div class="main-r4c2">
            <div class="list-title">
                <div class="title-left">
                </div>
                <div class="title-content">
                    推荐品牌</div>
                <div class="title-right">
                </div>
            </div>
            <div class="list-content">
                <hc:VarietyList ID="vlProduct" runat="server"></hc:VarietyList>
                <div class="list-more">
                    <a href="pages/view.aspx?product-brand" target="_blank" title="查看更多。。。">&nbsp;</a>
                </div>
            </div>
            <br />
            <div class="list-title">
                <div class="title-left">
                </div>
                <div class="title-content">
                    行业应用</div>
                <div class="title-right">
                </div>
            </div>
            <div class="list-content">
                <hc:IndustryList ID="ilProduct" runat="server" runat="server"></hc:IndustryList>
                <div class="list-more">
                    <a href="pages/view.aspx?product-industry" target="_blank" title="查看更多。。。">&nbsp;</a>
                </div>
            </div>
            <br />
            <div class="list-title">
                <div class="title-left">
                </div>
                <div class="title-content">
                    新闻资讯</div>
                <div class="title-right">
                </div>
            </div>
            <div class="list-content">
                <hc:ArticleList ID="alProduct" runat="server"></hc:ArticleList>
                <div class="list-more">
                    <a href="pages/view.aspx?news-newslist" target="_blank" title="查看更多。。。">&nbsp;</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
