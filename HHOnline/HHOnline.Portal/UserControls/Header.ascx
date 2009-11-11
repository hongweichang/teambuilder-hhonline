<%@ control language="C#" autoeventwireup="true" inherits="UserControls_Header, App_Web_5f7hrmcw" %>
<div class="main-r1">
    <div class="main-r1c1">
        &nbsp;</div>
    <div class="main-r1c2">
            <div class="main-r1c2-r1c1">
                服务热线:
                <asp:Literal ID="ltPhone" runat="server"></asp:Literal></div>
            <div class="main-r1c2-r1c2">
                <div class="top-nav">
                    <asp:LoginView ID="lvUser" runat="server">
                        <AnonymousTemplate>
                            <ul class="logged-ul">
                                <li><a href='<%= HHOnline.Framework.GlobalSettings.RelativeWebRoot %>pages/view.aspx?shopcart-cartitems' class="shoppingcard" target="_blank">购物车</a></li>
                                <li><a href="javascript:{}" class="basket">直接询价</a></li>
                            </ul>
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <ul class="logged-ul">
                                <li><a href='<%= HHOnline.Framework.GlobalSettings.RelativeWebRoot %>pages/view.aspx?shopcart-cartitems' target="_blank" class="shoppingcard">购物车</a></li>
                                <li><a href='<%= HHOnline.Framework.GlobalSettings.RelativeWebRoot %>pages/view.aspx?profiles-favlist' target="_blank" class="favorite">收藏夹</a></li>
                                <li><a href="javascript:{}" class="basket">直接询价</a></li>
                                <li><a href='<%= HHOnline.Framework.GlobalSettings.RelativeWebRoot %>pages/view.aspx?profiles-dashboard' class="dashboard">我的华宏</a></li>
                            </ul>
                        </LoggedInTemplate>
                    </asp:LoginView>
                </div>
                <div class="top-nav1">
                    <asp:Literal ID="ltDescriptions" runat="server"></asp:Literal>
                </div>
            </div>
        <div class="main-r1c2-r2">
            <div class="main-r1c2-r2r1">
                <ul class="nav-main" id="headerNav">
                    <li><a href="javascript:void(0)" class="selected" rel="main">首页</a></li>
                    <li><a href="javascript:void(0)" rel="product">产品</a></li>
                    <li><a href="javascript:void(0)" rel="news">资讯</a></li>
                    <%--<li><a href="javascript:void(0)">解决方案</a></li>--%>
                </ul>
            </div>
            <div class="main-r1c2-r2r2" id="headerDesc">
                工业自动化仪表及实验室分析仪器专业销售平台!
            </div>
        </div>
    </div>
</div>
