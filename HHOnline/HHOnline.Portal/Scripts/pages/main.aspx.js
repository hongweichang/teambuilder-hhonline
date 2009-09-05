/// <reference path="../jquery-vsdoc.js" />
function changeTab() {
    var me = $(this);
    if (me.hasClass('active')) { return; }
    var tabName = me.attr('rel');
    var last = $('#productNavigator1').find('li.active');

    last.removeClass('active');
    me.addClass('active');
    $('#' + last.attr('rel') + 'TabContent').hide();
    $('#' + tabName + 'TabContent').show();
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