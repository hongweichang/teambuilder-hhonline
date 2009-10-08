/// <reference path="jquery.js"/>
(function($) {
    $.fn.rating = function(options) {
        var opts = $.extend({}, $.fn.rating.defaults, options);
        return this.each(function() {
            var me = this;
            var target = $(this);
            var rp = null;
            switch (opts.style) {
                case 'basic':
                    rp = '<ul class="star-rating" style="width:' + opts.maxvalue * 25 + 'px">';
                    break;
                case 'small':
                    rp = '<ul class="star-rating small-star" style="width:' + opts.maxvalue * 10 + 'px">';
                    break;
                case 'in-line':
                    rp = '<span class="inline-rating"><ul class="star-rating small-star" style="width:' + opts.maxvalue * 10 + 'px">';
                    break;
            }
            $(target).append(rp);
            var li = '';
            var sw = null;
            var si = null;
            var t = null;
            var cw = Math.floor(100 / opts.maxvalue * opts.curvalue);
            for (var i = 0; i <= opts.maxvalue; i++) {
                if (i == 0)
                    li += '<li class="current-rating" style="width:' + cw + '%;">' + opts.curvalue + '/' + opts.maxvalue + '</li>';
                else {
                    sw = Math.floor(100 / opts.maxvalue * i);
                    si = (opts.maxvalue - i) + 2;
                    if (opts.descrip != undefined) t = opts.descrip[i - 1];
                    else t = i + '/' + opts.maxvalue;
                    li += '<li class="star"><a href="javascript:void(0)" rel="' + i + '" title="' + t + '" style="width:' + sw + '%;z-index:' + si + '">' + i + '</a></li>';
                }
            }
            $(target).find('.star-rating').append(li);
            if (opts.maxvalue > 1)
                $(target).append('<span class="star-rating-result" />');
            var stars = $(target).find('.star-rating').children('.star');
            if (opts.enable)
                stars.bind('click', rated);
            else {
                bindRate();
                if (opts.onRating != null)
                    opts.onRating(opts.curvalue, me);
            }
            me.reset = function() {
                stars.bind('click', rated);
                opts.curvalue = 0;
                $(target).find('a').each(function() {
                    $(this).removeClass();
                });
                $(target).find('.star-rating').children('.current-rating').css({ width: (opts.curvalue * 100) + '%' });
                opts.enable = true;
            }
            function rated() {
                if (opts.maxvalue == 1) {
                    opts.curvalue = (opts.curvalue == 0) ? 1 : 0;
                    $(target).find('.star-rating').children('.current-rating').css({ width: (opts.curvalue * 100) + '%' });
                }
                else {
                    opts.curvalue = stars.index(this) + 1;
                }
                if (opts.onRating != null)
                    opts.onRating(opts.curvalue, me);
                bindRate();
                return true;
            }
            function bindRate() {
                //stars.unbind('click');
                $(target).find('a').each(function() {
                    if ($(this).attr('rel') <= opts.curvalue)
                        $(this).attr('class', 'sel');
                    else
                        $(this).attr('class', 'disabled');
                    //opts.enable = false;
                });
            }
        })
    }
    $.fn.rating.defaults = {
        onRating: null,
        maxvalue: 5,
        curvalue: 0,
        style: 'basic',
        descrip: ['普通收藏', '以后有用', '不错的产品', '非常不错, 值得收藏', '很好的产品，我会长期关注的'],
        enable: true
    };
})(jQuery);