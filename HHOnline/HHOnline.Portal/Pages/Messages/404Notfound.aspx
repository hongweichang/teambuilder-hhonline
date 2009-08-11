<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/MsgMasterPage.master" AutoEventWireup="true" CodeFile="404Notfound.aspx.cs" Inherits="Pages_Messages_404Notfound" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMsg" Runat="Server">
<div class="error404">
<div class="error404-r1c1">&nbsp;</div>
<div class="error404-r2c1">
    <p>没有找到您要访问的页面，请检查您是否输入正确URL。</p>
    <div>请尝试以下操作：</div>

    <ol>
    <li>如果您已经在地址栏中输入该网页的地址，请确认其拼写正确。</li>
    <li>打开<a class="home" href="../../Index.aspx">首页</a>，然后查找指向您感兴趣信息的链接。</li>
    <li>单击<a class="goback" href='<%= _goback %>'>后退</a>链接，尝试其他链接。</li>
    <li>单击单击搜索链接，寻找Internet上的信息。</li>
    </ol>
    <p>如果您在浏览本站时，多次出现此错误，请与管理员联系。</p>
</div>
</div>
</asp:Content>

