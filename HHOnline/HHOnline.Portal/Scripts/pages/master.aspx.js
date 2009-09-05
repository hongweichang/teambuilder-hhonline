/// <reference path="../jquery-vsdoc.js" />
var nav = {
    main:'main.aspx',
    product:'pages/product-productlist.aspx',
    news:'pages/news-newslist.aspx'
};
function changeHeaderTab(navigate,tabName) {
    navigate.find('a.selected').removeClass('selected');
    navigate.find('a[rel=' + tabName + ']').addClass('selected');
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