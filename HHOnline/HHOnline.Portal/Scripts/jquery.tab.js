/// <reference path="jquery-vsdoc.js" />
$.fn.tab = function() {
    return this.each(function() {
        var me = $(this);
        var tabs = me.find('div.tabs');
        tabs.find('a').focus(function() { this.blur(); }).click(function() {
            tabs.find('li').removeClass('activeTab');
            tabs.find('li').each(function() {
                $('#' + $(this).attr('rel')).hide();
            })
            $(this).parent().addClass('activeTab');
            $('#' + $(this).parent().attr('rel')).show();
        })
    })
}
