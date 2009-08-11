/// <reference path="jquery-vsdoc.js"/>
/*
* zoombmp
* @ jQuery v1.2.*
*
* Copyright 2008, 2009 Jericho [ thisnamemeansnothing[at]gmail.com ] 
*  
*/
$.fn.zoomBmp = function(settings) {
    var p = $.extend({
        urlAttr: 'zoomUrl',
        offset: { x: 10, y: 30 }
    }, settings);
    function createPreview(holder) {
        var h = $(holder);
        h.append('<p id="jpreviewImage"><img src=""></p>');
        return h.find('#jpreviewImage');
    }
    function getScroll() {
        return { top: (parent.document.documentElement.scrollTop || document.documentElement.scrollTop),
            left: (parent.document.documentElement.scrollLeft || document.documentElement.scrollLeft)
        }
    }
    return this.each(function() {
        $(this).hover(function(e) {
            var t = $.trim(this.title);
            if (t != undefined && t != '') {
                this.title = '';
                this.pit = t;
            }
            var h = null;
            h = createPreview(window != parent ?
                                            parent.document.body
                                                :
                                                    document.body);
            var g = getScroll();
            h.css({
                left: e.pageX + p.offset.x + g.left,
                top: e.pageY + p.offset.y + g.top
            }).fadeIn('fast').find('img').attr('src', this.getAttribute('zoomUrl'));
        }, function() {
            if (this.pit != undefined) {
                this.title = this.pit;
            }
            $('#jpreviewImage', parent.document.body || document.body).remove();
        });
    });
}