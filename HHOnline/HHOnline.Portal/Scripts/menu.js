/// <reference path="jquery-vsdoc.js" />
/// <reference path="jquery.cookie.js" />
var menu = {
    initMenu: function(m) {
        var c = null, mc = null;
        // $('a.active').removeClass('active');
        //$('dl.cp-menu').removeAttr('rel').css({ height: '30px' });
        if (!m) {
            c = $.fn.cookie({ action: 'get', name: 'hhonline_menu' });
            if (c != null && $.trim(c) != '') {
                mc = $('#' + c).attr('class', 'active');
                m = $('#' + mc.attr('parentId'));
            }
            if (!m||m == null || m.length == 0) {
                m = $('dl.cp-menu:first-child');
            }
        }

        var h = m.find('li').length * 26;
        m.animate({ height: h + 35 }, 'fast', function() {
            $(this).attr('rel', 'expand');
        });
    },
    adaptMenu: function(obj) {
        var me = $(obj).parent();
        if (me.attr('rel') == 'expand') { return; }
        $('dl.cp-menu[rel=expand]').animate({ height: 30 }, 'fast', function() {
            $(this).removeAttr('rel');
        });
        menu.initMenu(me);
    },
    adaptItem: function(obj) {
        var me = $(obj);
        $.fn.cookie({ action: 'set', name: 'hhonline_menu', value: me.attr('id') });
    }
};
$().ready(function() {
    var w = window.location.href.split('/');
    if (w != null && w.length > 0) {
        if (w[w.length - 1].toLowerCase() == 'controlpanel.aspx') {
            $.fn.cookie({ action: 'erase', name: 'hhonline_menu' });
        }
    }
    menu.initMenu();
    $('a').focus(function() { this.blur(); });
});