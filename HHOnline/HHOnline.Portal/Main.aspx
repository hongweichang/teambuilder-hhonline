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
        <div class="main-r3c2" id="divAdLogo">
        </div>
        <div class="main-r3c3">
            <hc:HHLoginView ID="hhView" runat="server" PanelLeftCss="main-r3c3-c1 access-point"
                PanelMiddleCss="main-r3c3-c2 access-point" PanelRightCss="main-r3c3-c3 access-point"
                RegisterUrl="register.aspx" RegisterCss="login-access register-access" LoginUrl="login.aspx"
                LoginCss="login-access signup-access" InfoCss="info-access" ProfileUrl="Profile.aspx"
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
                    <hc:CategoryList ID="clProduct" runat="server" CssClass="hpCategoryList"></hc:CategoryList>
                </div>
            </div>
            <br />
            <div class="tab-nav">
                <ul>
                    <li class="active">促销</li>
                    <li>推荐</li>
                    <li>热卖</li>
                </ul>
            </div>
            <div class="tab-content">
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
                    <a href="#" title="查看更多。。。">&nbsp;</a>
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
                    <a href="#" title="查看更多。。。">&nbsp;</a>
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
                    <a href="#" title="查看更多。。。">&nbsp;</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
