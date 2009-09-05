/// <reference path="../jquery-vsdoc.js" />
var navigates = {
    product: 'pages/home/productcategories.aspx',
    variety: 'pages/home/productvariety.aspx',
    trade: 'pages/home/producttrade.aspx'
};
function changeTab() {
    var me = $(this);
    var tabName = me.attr('rel');
    var last = $('#productNavigator1').find('li.active');
    if (me != last) {
        last.removeClass('active');
        me.addClass('active');
        document.getElementById('frameProduct1').src = eval('(navigates.' + tabName + ')')+'?t='+Math.random();
    }
}
$().ready(function() {
    $('#btnSearchTrade').hover(function() {
        $(this).css('background-position', 'center -20px');
    }, function() {
        $(this).css('background-position', 'center 0');
    }).mousedown(function() {
        $(this).css('background-position', 'center -40px');
    }).mouseup(function() {
        $(this).css('background-position', 'center 0');
    });

    $('#txtSearchTrade').watermark({
        markText: '搜索行业相关信息。。'
    });
    $('input[rel=searchproduct]').watermark({
        markText: '选择搜索范围，输入名称、品牌、型号等信息搜索产品！'
    });

    $('#divAdLogo').hrzAccordion({
        pictures: _showPictures
    });

    $('#productNavigator1').find('li').click(changeTab);
});