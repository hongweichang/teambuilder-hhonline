/// <reference path="../jquery-vsdoc.js" />
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
    /*
    $('#divAdLogo').hrzAccordion({
        
    });
    */
});