<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Login</title>
</head>
<body style="background:#1D3647;">
    <form id="form1" runat="server">
    <div style="width:99%;margin:auto;">
        <div class="page-top"></div>
        <div class="page-bg">
            <div class="login-bg">
                <div class="login-main">
                    <div id="loginOverlay"></div>
                    <div class="membership" id="userLogin">
                            <div class="loading" id="divLoading">
                                <p class="loading-text">正在验证</p>
                                <p><img src="Images/Default/loading.gif" alt="Loading" /></p>
                            </div>
                            <div class="error-msg" id="errorMsg">
                                Error Message!
                            </div>
                        <div class="forms" id="loginForm"> 
                            <a href="Register.aspx" class="registerLink">注册</a>                           
                            <div id="loginType" class="loginType">
                                企业用户登录
                            </div>
                            <p class="loginP">
                                <label>用户</label>
                                <input type="text" maxlength="50" id="txtUserName" class="text-box normalbg" />
                                <span title="不能为空！" style="color:#ff0000;display:none;">*</span>
                            </p>
                            <p class="loginP">
                                <label>密码</label>
                                <input type="password" maxlength="50" id="txtPassword" class="text-box normalbg" />
                                <span title="不能为空！" style="color:#ff0000;display:none;">*</span>
                            </p>
                            <p class="loginP">
                                <label>验证码</label>
                                <input type="text" id="txtValidCode" maxlength="4" class="text-box normalbg" />
                                <span title="不能为空！" style="color:#ff0000;display:none;">*</span><br />
                            </p>
                            <p class="loginP1">                                
                               <img src="validcode.axd" id="imgValidCode" alt="单击换一张！" onclick="this.src='validcode.axd?t='+Math.random()" />
                            </p>
                             <p class="chk-container">
                                <input type="checkbox" id="chkRememberMe" value="0" /><label>下次记住我。</label>
                            </p>
                            <p class="button-container">
                                <a href="javascript:void(0)" onclick="signUp();" id="login" ></a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="page-bottom">
                    <%= this.CopyRight %></div>
    </div>
    </form>
</body>
</html>
