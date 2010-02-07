<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="UserControls_Footer" %>
<div class="main-r5">
    <div class="logo-bot">
        &nbsp;</div>
    <div class="text-bot">
        <ul>
            <li><a href='<%= _url %>pages/view.aspx?common-contactinfo' target="_blank">联系我们</a></li>
            <li><a href='<%= _url %>pages/view.aspx?common-aboutehuaho' target="_blank">了解华宏</a></li>
            <li><a href='<%= _url %>pages/view.aspx?common-honeruser' target="_blank">荣誉客户</a></li>
            <%--  <li><a href='<%= _url %>register.aspx' target="_blank">加盟华宏</a></li>--%>            
            <li><a href='<%= _url %>pages/view.aspx?common-recruitment' target="_blank">人员招聘</a></li>
            <li><a href='<%= _url %>pages/view.aspx?common-wflist' target="_blank">业务流程</a></li>
            <li><a href='<%= _url %>pages/view.aspx?common-friendlink' target="_blank">友情链接</a></li>
            <li><a href='<%= _url %>pages/view.aspx?common-sitemap' target="_blank">站点地图</a></li>
            <li class="last"><a href='<%= _url %>pages/view.aspx?common-rightnotice' target="_blank">版权声明</a></li>
        </ul>
        <h6>
            <asp:Literal ID="ltCopyRight" runat="server"></asp:Literal>&nbsp;&nbsp;<a href="http://www.inidc.net/" target="_blank">本站带宽由北京数据家公司提供</a>
        </h6>
    </div>
    <div class="icp-bot">
        <div class="icp-text">
           <asp:Literal ID="ltIcp" runat="server"></asp:Literal></div>
    </div>
</div>
