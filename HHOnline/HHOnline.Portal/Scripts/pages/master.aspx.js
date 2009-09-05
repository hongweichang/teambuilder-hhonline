/// <reference path="../jquery-vsdoc.js" />
var nav = {
    main:'main.aspx',
    product:'pages/product-productlist.aspx',
    news:'pages/news-newslist.aspx'
};
var desc = {
    main: '工业自动化仪表及实验室分析仪器专业销售平台!',
    product:'产品浏览以及搜索显示页！',
    news:'显示资讯相关信息，了解业内最新消息！'
};
function changeHeaderTab(navigate,tabName) {
    navigate.find('a.selected').removeClass('selected');
    navigate.find('a[rel=' + tabName + ']').addClass('selected');
    $('#headerDesc').html(eval('(desc.' + tabName + ')'))
}
$().ready(function() {
    var n = $('#headerNav');
    n.find('a').click(function() {
        var m = $(this);
        if (typeof activeTab != 'undefined' && activeTab == m.attr('rel')) { return; }
        window.location.href = relativeUrl + eval('(nav.' + m.attr('rel') + ')');
        m.blur();
    });
    if (typeof activeTab != 'undefined') {
        changeHeaderTab(n, activeTab);
    }
});