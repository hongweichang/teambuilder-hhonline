<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="UserControls_Footer" %>
<div class="main-r5">
    <div class="logo-bot">
        &nbsp;</div>
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
            <asp:Literal ID="ltCopyRight" runat="server"></asp:Literal>
        </h6>
    </div>
    <div class="icp-bot">
        <div class="icp-text">
           <asp:Literal ID="ltIcp" runat="server"></asp:Literal></div>
    </div>
</div>
