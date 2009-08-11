<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="main-r1">
            <div class="main-r1c1">&nbsp;</div>
            <div class="main-r1c2">
                <div class="main-r1c2-r1">
                    <div class="main-r1c2-r1c1">服务热线: <asp:Literal ID="ltPhone" runat="server"></asp:Literal></div>
                    <div class="main-r1c2-r1c2">
                        <div class="top-nav">                            
                            <asp:LoginView ID="lvUser" runat="server">
                                <AnonymousTemplate><ul class="logged-ul">
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
                                    欢迎您, <%= Profile.AccountInfo.DisplayName %>!&nbsp;&nbsp;
                                </LoggedInTemplate>
                                <AnonymousTemplate>
                                    请您登录：
                                </AnonymousTemplate>
                            </asp:LoginView>
                            <asp:LoginStatus ID="lsQuit" runat="server" LogoutAction="Refresh" LoginText="[登录]" LogoutText="[注销]" ForeColor="#ff0000" />
                            <asp:LoginView ID="lvManage" runat="server">                                
                                <RoleGroups>
                                    <asp:RoleGroup Roles="HHOnlineUser-View">
                                        <ContentTemplate>
                                            <a href="controlpanel/controlpanel.aspx" style="color:#ff0000">[管理中心]</a>                                    
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
        <div class="main-r2">
            <div class="main-r2c1">
                <div class="search-bg">
                    <div class="search-left search"></div>
                     <div class="search-title search-m search">产品搜索：</div>
                    <div class="search-type search-m">
                        <asp:DropDownList ID="ddlSearchType" runat="server">
                            <asp:ListItem Text="全部范围" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="产品名称" Value="1"></asp:ListItem>
                            <asp:ListItem Text="产品品牌" Value="2"></asp:ListItem>
                            <asp:ListItem Text="产品型号" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="search-input search-m">
                        <asp:TextBox ID="txtSearch" rel="searchproduct" runat="server" Width="350px" MaxLength="255" style="padding:3px;"></asp:TextBox>
                    </div>
                    <div class="search-search search-m search">
                        <div class="search-icon">&nbsp;</div>
                        <asp:LinkButton ID="lnkSearch" runat="server" Text="搜索"></asp:LinkButton>
                    </div>
                    <div class="search-right search"></div>
                </div>
                <div class="search-hot">
                    热门搜索：<a href="#" >AND</a><a href="#">精密天平</a>
                </div>
            </div>
        </div>
        <div class="main-r3">
            <div class="main-r3c1">
                <div class="main-r3c1-r1">
                    <div class="main-r3c1-r1c1"></div>
                    <div class="main-r3c1-r1c2"><span class="service-title service-title1">&nbsp;</span></div>
                    <div class="main-r3c1-r1c3"></div>    
                </div>
                <div class="main-r3c1-r2"><asp:Literal ID="ltIdeal" runat="server"></asp:Literal></div>
                <div class="main-r3c1-r1 main-r3c1-r3">
                    <div class="main-r3c1-r1c1"></div>
                    <div class="main-r3c1-r1c2"><span class="service-title service-title2">&nbsp;</span></div>
                    <div class="main-r3c1-r1c3"></div>    
                </div>
                <div class="main-r3c1-r2 main-r3c1-r4"><asp:Literal ID="ltService" runat="server"></asp:Literal></div>
            </div>
            <div class="main-r3c2" id="divAdLogo">
               
            </div>
            <div class="main-r3c3">
                <hc:HHLoginView ID="hhView" runat="server" PanelLeftCss="main-r3c3-c1 access-point"
                         PanelMiddleCss="main-r3c3-c2 access-point" PanelRightCss="main-r3c3-c3 access-point"
                         RegisterUrl="register.aspx" RegisterCss="login-access register-access"
                         LoginUrl="login.aspx" LoginCss="login-access signup-access"
                         InfoCss="info-access" ProfileUrl="Profile.aspx"
                         MsgCss="msg-access" Message="抢先加盟，获取更多优惠和合作机会！" />
            </div>
        </div>
        <div class="main-r4">
            <div class="main-r4c1">
                <div class="tab-nav">
                    <ul>
                        <li class="active">产品</li>
                        <li>品牌</li>
                        <li>行业</li>
                    </ul>
                </div>
                <div class="tab-content"></div>
                <br />
                <div class="tab-nav">
                    <ul>
                        <li class="active">促销</li>
                        <li>推荐</li>
                        <li>热卖</li>
                    </ul>
                </div>
                <div class="tab-content"></div>
            </div>
            <div class="main-r4c2">
                <div class="list-title">
                    <div class="title-left"></div>
                    <div class="title-content">推荐品牌</div>
                    <div class="title-right"></div>
                </div>
                <div class="list-content">
                    <ul>
                        <li><a href="#" runat="server">日本AND</a></li>
                        <li><a id="A1" href="#" runat="server">泰华塑胶</a></li>
                        <li><a id="A2" href="#" runat="server">台湾Spped</a></li>
                    </ul>
                    <div class="list-more">
                        <a href="#" title="查看更多。。。">&nbsp;</a>
                    </div>
                </div>
                <br />
                <div class="list-title">
                    <div class="title-left"></div>
                    <div class="title-content">行业应用</div>
                    <div class="title-right"></div>
                </div>
                <div class="list-content">
                    <ul>
                        <li><a id="A6" href="#" runat="server">AND</a></li>
                        <li><a id="A7" href="#" runat="server">Speedo</a></li>
                        <li><a id="A8" href="#" runat="server">Jacko</a></li>
                    </ul>
                    <div class="list-more">
                        <a href="#" title="查看更多。。。">&nbsp;</a>
                    </div>
                </div>
                <br />
                 <div class="list-title">
                    <div class="title-left"></div>
                    <div class="title-content">新闻资讯</div>
                    <div class="title-right"></div>
                </div>
                <div class="list-content">
                    <ul>
                        <li><a id="A3" href="#" runat="server">中央。。。</a></li>
                        <li><a id="A4" href="#" runat="server">这是一次。。</a></li>
                        <li><a id="A5" href="#" runat="server">革命性的成功。</a></li>
                    </ul>
                    <div class="list-more">
                        <a href="#" title="查看更多。。。">&nbsp;</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="main-r5">
           <div class="logo-bot">&nbsp;</div>
           <div class="text-bot">
                <ul>
                    <li><a href="#" target="_blank">联系我们</a></li>
                    <li><a href="#" target="_blank">了解华宏</a></li>
                    <li><a href="#" target="_blank">荣誉客户</a></li>
                    <li><a href="#" target="_blank">加盟华宏</a></li>
                    <li><a href="#" target="_blank">业务流程</a></li>
                    <li><a href="#" target="_blank">友情链接</a></li>
                    <li class="last"><a href="#" target="_blank">版权申明</a></li>
                </ul>
                <h6>
                    <%= this.CopyRight %>
                </h6>
           </div>
           <div class="icp-bot"><div class="icp-text">苏ICP备07008095</div></div>
        </div>
    </div>
    </form>
</body>
</html>
