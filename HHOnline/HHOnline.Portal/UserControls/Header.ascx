<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControls_Header" %>
<div class="main-r1">
    <div class="main-r1c1">
        &nbsp;</div>
    <div class="main-r1c2">
        <div class="main-r1c2-r1">
            <div class="main-r1c2-r1c1">
                服务热线:
                <asp:Literal ID="ltPhone" runat="server"></asp:Literal></div>
            <div class="main-r1c2-r1c2">
                <div class="top-nav">
                    <asp:LoginView ID="lvUser" runat="server">
                        <AnonymousTemplate>
                            <ul class="logged-ul">
                                <li><a href="#" class="shoppingcard">购物车</a></li>
                                <li><a href="#" class="basket">直接询价</a></li>
                            </ul>
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <ul class="logged-ul">
                                <li><a href="#" class="shoppingcard">购物车</a></li>
                                <li><a href="#" class="favorite">收藏夹</a></li>
                                <li><a href="#" class="basket">直接询价</a></li>
                                <li><a href="#" class="dashboard">我的华宏</a></li>
                            </ul>
                        </LoggedInTemplate>
                    </asp:LoginView>
                </div>
                <div class="top-nav1">
                    <asp:LoginView ID="lvWelcome" runat="server">
                        <LoggedInTemplate>
                            欢迎您,
                            <%= Profile.AccountInfo.DisplayName %>!&nbsp;&nbsp;
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            请您登录：
                        </AnonymousTemplate>
                    </asp:LoginView>
                    <asp:LoginStatus ID="lsQuit" runat="server" LogoutAction="Refresh" LoginText="[登录]"
                        LogoutText="[注销]" ForeColor="#ff0000" />
                    <asp:LoginView ID="lvManage" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="HHOnlineUser-View">
                                <ContentTemplate>
                                    <a href="controlpanel/controlpanel.aspx" style="color: #ff0000">[管理中心]</a>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </div>
            </div>
        </div>
        <div class="main-r1c2-r2">
            <div class="main-r1c2-r2r1">
                <ul class="nav-main">
                    <li><a href="#" class="selected">首页</a></li>
                    <li><a href="#">产品</a></li>
                    <li><a href="#">资讯</a></li>
                    <li><a href="#">解决方案</a></li>
                </ul>
            </div>
            <div class="main-r1c2-r2r2">
                工业自动化仪表及实验室分析仪器专业销售平台!
            </div>
        </div>
    </div>
</div>
