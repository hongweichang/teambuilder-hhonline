/// <reference path="../jquery-vsdoc.js" />
function changeTab() {
    var me = $(this);
    if (me.hasClass('active')) { return; }
    var tabName = me.attr('rel');
    var last = me.parent().find('li.active');

    last.removeClass('active');
    me.addClass('active');
    $('#' + last.attr('rel') + 'TabContent').hide();
    $('#' + tabName + 'TabContent').show();
}
function loadPictures() {
    var picsContainer = $('#divAdLogo');
    $.ajax({
        dataType: 'json',
        url: 'ads.axd',
        error: function(error) {
            var msg = error.responseText != '' ? error.responseText : '图片加载失败';
            picsContainer.html(msg);
        },
        success: function(json) {
            picsContainer.hrzAccordion({
                pictures: json
            });
        }
    })    
}
$().ready(function() {
    loadPictures();

    $('#productNavigator1').find('li').click(changeTab);
    $('#productNavigator2').find('li').click(changeTab);
});