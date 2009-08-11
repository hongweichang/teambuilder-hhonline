<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogOut.aspx.cs" Inherits="ControlPanel_LogOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户注销</title>
    <style type="text/css">
    html
    {
        background-color:#E6E6FA;
    }
    .logout
    {
        width:300px;
        height:50px;
        border:solid 1px #708090;
        background:#FFFFF0;
        margin:100px auto; 
        font-family:Verdana,宋体;
        font-size:11px;
        position:relative;
        }
    .loader
    {
        position:absolute;
        top:13px;
        left:13px;
        height:24px;
        width:24px;
        background:url(../images/default/indicator.gif) no-repeat center center;
        }
    .text
    {
        position:absolute;
        left:50px;
        height:30px;
        width:250px;
        padding:10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <div class="logout">
            <div class="loader">&nbsp;</div>
            <div class="text">
                正在注销。。。<br />
                Cancellation of the application...   
            </div>
        </div>
    </div>
    </form>
</body>
</html>
