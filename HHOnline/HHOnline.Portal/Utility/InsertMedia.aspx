<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertMedia.aspx.cs" Inherits="Utility_InsertMedia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>插入媒体</title>

    <script type="text/javascript">
        function ProcessContent(content,args)
        {
            if(window.parent.InsertContentToHtmlEditor)
                window.parent.InsertContentToHtmlEditor(content);
            parent.window.$mceHideJModal();
        }
        function apdatePanel(id) {
            document.getElementById(id).style.height = 210 + 'px';
        }
        window.onload = function() {
            apdatePanel('uploadPanel');
        }
    </script>

    <style type="text/css">
        body, html
        {
            margin: 0;
            padding: 0;
            font-size: 12px;
            font-family: Verdana;
        }
        .panel
        {
            height: 200px;
        }
        .tab1
        {
            padding: 3px 10px;
        }
    </style>

    <script type="text/javascript" src="../tiny_mce/tiny_mce_popup.js"></script>

    <script type="text/javascript" src="../tiny_mce/utils/mctabs.js"></script>

    <script type="text/javascript" src="../tiny_mce/utils/form_utils.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="tabs" class="tabs">
        <ul>
            <li id="tabs_uploadfile" class="current"><span><a href="javascript:mcTabs.displayTab('tabs_uploadfile','uploadPanel');apdatePanel('uploadPanel')">
                上传文件</a></span></li>
            <li id="tabs_sitefile"><span><a href="javascript:mcTabs.displayTab('tabs_sitefile','sitefilePanel');apdatePanel('sitefilePanel')">
                站点文件</a></span></li>
            <li id="tabs_filelink"><span><a href="javascript:mcTabs.displayTab('tabs_filelink','filelinkPanel');apdatePanel('filelinkPanel');">
                文件链接</a></span></li>
        </ul>
    </div>
    <div class="panel_wrapper">
        <div id="uploadPanel" class="panel current">
            <div class="tab1">
                <h4>
                    上传文件</h4>
                <span>从本地选择需要使用的文件</span>
            </div>
            <div class="tab1">
                <asp:FileUpload ID="fuFile" runat="server" />
                <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="fuFile" runat="server" ErrorMessage="选择需上传的文件"></asp:RequiredFieldValidator>
            </div>
            <div class="tab1">
                <h4>
                    尺寸</h4>
                <span>生成图片尺寸</span>
            </div>
            <div class="tab1">
                <asp:TextBox ID="txtWidth" runat="server" Width="40" Text="200"></asp:TextBox>
                x
                <asp:TextBox ID="txtHeight" Width="40" runat="server" Text="200"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtWidth"
                    ValidationExpression="(\d)*" ErrorMessage="输入数字"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="txtHeight"
                    ValidationExpression="(\d)*" ErrorMessage="输入数字"></asp:RegularExpressionValidator>
            </div>
            <div class="tab1">
                <asp:Button ID="btnUpload" runat="server" Text="插入" OnClick="btnUpload_Click" />
                <input type="button" onclick="parent.window.$mceHideJModal();return false;" value="关闭" />
            </div>
        </div>
        <div id="sitefilePanel" class="panel">
            tabs_sitefile
        </div>
        <div id="filelinkPanel" class="panel">
            tabs_filelink
        </div>
    </div>
    </form>
</body>
</html>
