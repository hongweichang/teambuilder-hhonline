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
    $('#divAdLogo').hrzAccordion({
        pictures: _showPictures
    });

    $('#productNavigator1').find('li').click(changeTab);
});