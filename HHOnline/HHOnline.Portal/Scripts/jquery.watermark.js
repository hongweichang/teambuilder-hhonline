/*
* watermark
* @ jQuery v1.2.*
*
* Copyright 2008, 2009 Jericho [ thisnamemeansnothing[at]gmail.com ] 
*  
*/
(function($) {
    $.fn.watermark = function(opts) {
        var p = $.fn.extend({}, $.fn.watermark.params, opts);
        return this.each(function() {
            var target = $(this);
            function focus() {
                if (target.val() == p.markText && target.hasClass(p.markCss)) {
                    target.val('').removeClass(p.markCss);
                }
            }
            function render() {
                var v = $.trim(target.val());
                if (v == '' || v == p.markText)
                    target.val(p.markText).addClass(p.markCss);
            }
            target.focus(focus);
            target.blur(render);
            target.change(render);
            render();
        });
    }
    $.fn.watermark.params = {
        markCss: 'watermaskcss',
        markText: 'Water mask'
    };
})(jQuery);