<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="Pages_Common_SiteMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>站点地图 - 华宏在线</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="description" content="华宏在线网站地图，华宏在线 www.eHUAHO.com,华宏仪表 工业自动化仪表及实验室分析仪器专业供应商,华宏HUAHO 打造中国最优秀仪器仪表服务品牌">
    <meta name="keywords" content="网站地图,sitemap,华宏在线,eHUAHO,华宏仪表,华宏仪器,华宏HUAHO">
    <link type="image/x-icon" rel="shortcut icon" title="ICON" href="/images/favicon.ico" />
    <style type="text/css" media="all">
        a:link
        {
            color: #000000;
        }
        a:visited
        {
            color: #000000;
        }
        a:hover
        {
            color: #990000;
        }
        .s
        {
            font-size: 12px;
        }
        .c
        {
            font-size: 14px;
        }
        .m
        {
            font-size: 14px;
            font-weight: bold;
        }
        .headborder
        {
            border-right: #990000 1px solid;
            border-top: #990000 1px solid;
            font-weight: bold;
            border-left: #990000 1px solid;
            padding-top: 2px;
            border-bottom: #990000 3px solid;
            height: 31px;
            text-align: center;
        }
        .headonborder
        {
            border-right: #990000 1px solid;
            border-top: #990000 1px solid;
            font-weight: bold;
            background: #990000;
            border-left: #990000 1px solid;
            padding-top: 2px;
            border-bottom: #990000 3px solid;
            height: 31px;
            text-align: center;
        }
        .black-bar-redline
        {
            border-top: 3px solid #990000;
            background: #666666;
            color: #fff;
            height: 26px;
            border-bottom: 2px solid #333333;
            font: bold;
            padding-left: 15px;
            padding-top: 2px;
        }
        div.listcallingsort
        {
            width: 100%;
            padding: 0px;
        }
        ul.callinglayout
        {
            display: block;
            margin: 10px 0px;
            list-style: none;
            text-align: left;
            width: 100%;
            margin-left: 0px;
            padding: 0px;
        }
        ul.callinglayout li
        {
            float: left;
            width: 110px;
            margin-right: 10px;
            margin-bottom: 10px;
            white-space: nowrap;
            font-size: 13px;
        }
        ul.callinglayouthead
        {
            display: block;
            margin: 20px 0px;
            list-style: none;
            text-align: left;
            width: 100%;
            margin-left: 0px;
            padding: 0px;
        }
        ul.callinglayouthead li
        {
            float: left;
            width: 100px;
            margin-right: 10px;
            white-space: nowrap;
            font-size: 14px;
            font-weight: bold;
        }
        div.hackbox
        {
            clear: both;
        }
    </style>
</head>
<body bgcolor="#ffffff" text="#000000" topmargin="0" marginheight="0" leftmargin="0"
    marginwidth="0">
    <form id="myForm" runat="server">
    <table class="C" cellspacing="0" cellpadding="5" width="750" align="center" border="0">
        <tr>
            <td width="175px" valign="middle" class="s">
                <a href="http://www.ehuaho.com/">
                    <img border="0" src="http://www.ehuaho.com/WebAdmin/eHUAHO_LOGO.gif" /></a>
            </td>
            <td>
                <div class="listcallingsort">
                    <ul class="callinglayouthead" style="text-align: right; font-size: larger;">
                        <li><a href="http://www.ehuaho.com/main.aspx">首页</a></li><li><a href="http://www.ehuaho.com/pages/view.aspx?product-productlist">
                            产品</a></li><li><a href="http://www.ehuaho.com/pages/view.aspx?news-newslist">资讯</a></li><li>
                                <a href="http://www.ehuaho.com/login.aspx">登录</a></li><li><a href="http://www.ehuaho.com/register.aspx">
                                    注册</a></li></ul>
                </div>
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="8" cellspacing="0">
        <tr>
            <td bgcolor="#fffff0" style="border: #FFE28C 1px solid;">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="30" class="m">
                            华宏在线 网站地图
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="105" height="28" align="center" bgcolor="990000" class="m" style="border-top: #ff3300 3px solid;
                color: #ffffff">
                <a href="http://www.ehuaho.com/pages/view.aspx?product-brand" style="color: #ffffff;">
                    所有品牌</a><a name="3"></a>
            </td>
            <td width="645" class="m" style="border-bottom: #990000 3px solid;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="8" cellspacing="0">
        <tr>
            <td height="50" style="border: #e0e0e0 1px solid;">
                <div class="listcallingsort">
                    <ul class="callinglayout">
                        <asp:Literal runat="server" ID="ltBrand"></asp:Literal>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <br>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="105" height="28" align="center" bgcolor="990000" class="m" style="border-top: #ff3300 3px solid;
                color: #ffffff">
                <a href="http://www.ehuaho.com/pages/view.aspx?product-category" style="color: #ffffff;">
                    产品分类</a><a name="3"></a>
            </td>
            <td width="645" class="m" style="border-bottom: #990000 3px solid;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="8" cellspacing="0">
        <tr>
            <td height="50" style="border: #e0e0e0 1px solid;">
                <div class="listcallingsort">
                    <ul class="callinglayout">
                        <asp:Literal runat="server" ID="ltCategory"></asp:Literal>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <br>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="105" height="28" align="center" bgcolor="990000" class="m" style="border-top: #ff3300 3px solid;
                color: #ffffff">
                <a href="http://www.ehuaho.com/pages/view.aspx?product-industry" style="color: #ffffff;">
                    行业应用</a><a name="3"></a>
            </td>
            <td width="645" class="m" style="border-bottom: #990000 3px solid;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="8" cellspacing="0">
        <tr>
            <td height="50" style="border: #e0e0e0 1px solid;">
                <div class="listcallingsort">
                    <ul class="callinglayout">
                        <asp:Literal runat="server" ID="ltIndustry"></asp:Literal>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <br>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="105" height="28" align="center" bgcolor="990000" class="m" style="border-top: #ff3300 3px solid;
                color: #ffffff">
                <a href="http://www.ehuaho.com/pages/view.aspx?product-productlist" style="color: #ffffff;">
                    产品列表</a><a name="3"></a>
            </td>
            <td width="645" class="m" style="border-bottom: #990000 3px solid;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="8" cellspacing="0">
        <tr>
            <td height="50" style="border: #e0e0e0 1px solid;">
                <div class="listcallingsort">
                    <ul class="callinglayout">
                        <asp:Literal runat="server" ID="ltProduct"></asp:Literal>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <br>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="105" height="28" align="center" bgcolor="990000" class="m" style="border-top: #ff3300 3px solid;
                color: #ffffff">
                <a href="http://www.ehuaho.com/pages/view.aspx?news-newslist" style="color: #ffffff;">
                    资讯中心</a><a name="3"></a>
            </td>
            <td width="645" class="m" style="border-bottom: #990000 3px solid;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="8" cellspacing="0">
        <tr>
            <td height="50" style="border: #e0e0e0 1px solid;">
                <div class="listcallingsort">
                    <ul class="callinglayout">
                        <asp:Literal runat="server" ID="ltNews"></asp:Literal>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <br>
    <br>
    <hr align="center" width="98%" color="#990000" style="height: 3px">
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="5">
        <tbody>
            <tr>
                <td height="13" align="middle" class="s">
                    <div align="center">
                        华宏在线 版权所有 | <a href="http://www.ehuaho.com/pages/view.aspx?common-contactinfo" target="_blank">
                            联系我们</a>| <a href="http://www.ehuaho.com/Pages/Common/SiteMap.aspx" target="_blank">
                                网站地图</a></div>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
