/// <reference path="../jquery-vsdoc.js" />
var nav = {
    main: 'main.aspx',
    product: 'pages/view.aspx?product-productlist',
    news: 'pages/view.aspx?news-newslist'
};
var desc = {
    main: '工业自动化仪表及实验室分析仪器专业销售平台!',
    product: '产品浏览以及搜索显示页！',
    news: '显示资讯相关信息，了解业内最新消息！'
};
var mk1 = '直接输入关键字根据产品名称、概要、内容等信息进行产品相关搜索！';
var mk2 = '直接输入关键字根据资讯名称、概要、内容等信息进行咨询相关搜索！';
function changeHeaderTab(navigate, tabName) {
    navigate.find('a.selected').removeClass('selected');
    navigate.find('a[rel=' + tabName + ']').addClass('selected');
    $('#headerDesc').html(eval('(desc.' + tabName + ')'));
    return false;
}
function searchPruduct(searchText) {
    var _searchText = $.trim($(searchText).val())
    if (_searchText == '' || _searchText == mk1) {
        _searchText = '';
    }
    window.location.href = relativeUrl + 'pages/view.aspx?product-search&w=' + encodeURIComponent(_searchText);
}
function searchArticle(searchText) {
    var _searchText = $.trim($(searchText).val())
    if (_searchText == '' || _searchText == mk2) {
        return;
    }
    window.location.href = relativeUrl + 'pages/view.aspx?news-search&w=' + encodeURIComponent(_searchText);
}
function navGuid(t) {
    window.location.href = relativeUrl + eval('(nav.' + t + ')');
}
$().ready(function() {
    var n = $('#headerNav');
    n.find('a').each(function() { var m = $(this); m.attr('href', 'javascript:navGuid(\'' + m.attr('rel') + '\');'); });
    if (typeof activeTab != 'undefined') {
        changeHeaderTab(n, activeTab);
    }
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
    s1.autoComplete({
        highLight: '<b class="needed"><u>{0}</u></b>',
        template: 'Script1',
        url: 'product.axd',
        data: { action: 'getPSuggestion' }
    });

    var s2 = $('input[rel=searcharticle]');
    s2.watermark({
        markText: mk2
    });
    s2.autoComplete({
        highLight: '<b class="needed"><u>{0}</u></b>',
        template: 'Script1',
        url: 'product.axd',
        data: { action: 'getPSuggestion' }
    });
    s2.keydown(function(e) {
        if ((e.keyCode || e.which) == 13) {
            searchArticle($(this), mk2);
            return false;
        }
    });
    $('#searchProduct').attr('href', 'javascript:searchPruduct(\'#searchproduct\')');

    $('#searchArticle').click(function() {
        this.blur();
        searchArticle(s2, mk2);
    });

});