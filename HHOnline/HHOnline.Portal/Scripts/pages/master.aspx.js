/// <reference path="../jquery-vsdoc.js" />
var nav = {
    main:'main.aspx',
    product:'pages/view.aspx?product-productlist',
    news:'pages/view.aspx?news-newslist'
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
function searchPruduct(searchText,maskText) {
    var _searchText = $.trim(searchText.val())
    if (_searchText == '' || _searchText == maskText) {
        return;
    }
    window.location.href = relativeUrl + 'pages/view.aspx?product-search&w=' + encodeURIComponent(_searchText);
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
    var mk1 = '直接输入关键字根据产品名称、概要、内容等信息进行产品相关搜索！';
    var mk2 = '直接输入关键字根据资讯名称、概要、内容等信息进行咨询相关搜索！';
    var s1 = $('input[rel=searchproduct]');
    s1.watermark({
        markText: mk1
    });
    s1.keydown(function(e) {
        if ((e.keyCode || e.which) == 13) {
            searchPruduct($(this), mk1);
            return false;
        }
    })
    var s2 = $('input[rel=searcharticle]');
    s2.watermark({
        markText: mk2
    });

    $('#searchProduct').click(function() {
        this.blur();
        searchPruduct(s1, mk1);
    })
});