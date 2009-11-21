/// <reference path="jquery.js" />
var MARQUE_CACHE = [];
$.fn.marque = function(settings) {
    var ps = $.extend({
        //second
        speed: 3,
        width: 950,
        step: 0,
        height: 20,
        //0:vertical,1:horizontal.
        direction: 1
    }, settings);
    return this.each(function() {
        var me = $(this);
        me.css({
            'position': 'relative',
            'overflow': 'hidden',
            'width': ps.width,
            'height': ps.height
        });
        me.find('ul').css({
            'position': 'relative',
            'list-style-type': 'none',
            'margin': 0
        });
        var lis = me.find('li').css({
            'margin': 0,
            'line-height': '100%',
            'padding': '4px 5px'
        });
        if (ps.direction == 1) {
            lis.css({ 'float': 'left' });
        }
        this.$pause = false;
        this.$setting = ps;
        this.$delay = ps.speed;
        var t = $(this);
        t.hover(function() { t.marqueInitial(true); }, function() { t.marqueInitial(false); });
        MARQUE_CACHE.push(this);
        setInterval(__MARQUE_START, 1000);

    });
}
$.fn.marqueInitial = function(isPause) {
    return this.each(function() { this.$pause = isPause });
}
function __MARQUE_START() {
    for (var i = 0; i < MARQUE_CACHE.length; i++) {
        var elm = MARQUE_CACHE[i];
        if (elm && !elm.$pause) {
            if (elm.$delay == 0) {
                var v = (elm.$setting.direction == 0);
                var ul = $('> ul:first-child', elm);
                if (!elm.$steps) {
                    if (v) {
                        elm.$steps = $('> li:first-child', ul).outerHeight();
                    } else {
                        elm.$steps = $('> li:first-child', ul).outerWidth();
                    }
                    elm.$step = 0;
                }
                if ((elm.$steps + elm.$step) <= 0) {
                    elm.$delay = elm.$setting.speed;
                    elm.$steps = false;
                    $(ul).css((v ? 'top' : 'left'), '0').find('> li:last-child').after($('> li:first-child', ul));
                    $('> *', ul).not('li').remove();
                } else {
                    elm.$step -= elm.$steps;
                    if (-elm.$step > elm.$steps) {
                        elm.$step = -elm.$steps;
                    }
                    if (v) {
                        ul.css('top', elm.$step + 'px');
                    }
                    else {
                        ul.css('left', elm.$step + 'px');
                    }
                }
            } else {
                elm.$delay--;
            }
        }
    }
};