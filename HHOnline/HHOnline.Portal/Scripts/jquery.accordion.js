/// <reference path="jquery.js" />
$.fn.hrzAccordion = function(settings) {
    var ps = $.extend({
        speed: 500,
        swapSpeed: 5000,
        titleHeight: 40,
        width: 448,
        height: 183,
        pictures: [],
        timerId: '__globaltimer1'
    }, settings);
    return this.each(function() {
        var me = $(this);
        var outer = me.find('div.jquery-accordion'), pictures = null, count = 0, title = null, navigator = null;

        if (outer.length == 0) {
            $('<div class="jquery-accordion">' +
                '<div class="jquery-accordion-pictures" />' +
                '<div class="jquery-accordion-titles" />' +
                '<div class="jquery-accordion-navigate" ></div>' +
                '</div>').appendTo(me);
            outer = me.find('div.jquery-accordion');
        }
        outer.css({
            width: ps.width,
            height: ps.height
        });
        pictures = me.find('div.jquery-accordion-pictures');
        navigator = me.find('div.jquery-accordion-navigate');

        var html = '', b = false;
        $.each(ps.pictures, function(i, n) {
            count = i + 1;
            b = ($.trim(n.Link) != '');
            html += '<a index="' + i + '"' + (b ? ' href="' + n.Link + '" style="cursor:pointer" target="_blank" ' : ' style="cursor:default" ') +
                         'title="' + n.Title + '" description="' + n.Description + '">' +
                         '<img src="' + n.Url + '" style="width:' + ps.width + 'px;height:' + ps.height + 'px;"  /></a>';

        });
        if (count == 0) { return; }
        pictures.html(html);
        navigator.css({ width: count * 26 });
        html = '';
        if (count > 1) {
            for (var i = 0; i < count; i++) {
                html += '<div class="jquery-accordion-nav ' + (i == 0 ? 'jquery-accordion-navHover' : '') + '" style="left:' + i * 26 + 'px">' + (i + 1) + '</div>';
            }
            navigator.html(html);
            navigator.find('div').css('opacity',0.7).hover(function() {
                $(this).addClass('jquery-accordion-navHover');
                clearInterval(window[ps.timerId]);
                if (parseInt(pictures.find('a:visible').attr('index')) != parseInt(this.innerHTML) - 1) {
                    pictureBox(pictures.find('a:eq(' + (parseInt(this.innerHTML) - 1) + ')'));
                }
            }, function() {
                if (parseInt(pictures.find('a:visible').attr('index')) != parseInt(this.innerHTML) - 1) {
                    $(this).removeClass('jquery-accordion-navHover');
                }
                window[ps.timerId] = setInterval(pictureBox, ps.swapSpeed);
            });
        }
        if (ps.titleHeight != 0) {
            title = pictures.next().css('opacity', 0.7).height(ps.titleHeight);
        }
        else {
            pictures.next().hide();
        }
        pictures.find('a:not(:first)').css('display', 'none');
        var pf = pictures.find('a:first');
        if (ps.titleHeight != 0) {
            title.html('<b>' + pf.attr('title') + '</b>' + pf.attr('description'));
        }

        var _First = pictures.find('a:first');
        var _Last = pictures.find('a:last');

        function pictureBox(p) {
            if (!p) {
                if (_Last.is(':visible')) {
                    p = _First.fadeIn(ps.speed).addClass('jquery-accordion-visible');
                    _Last.hide();
                } else {
                    p = pictures.find('a:visible').hide().next().fadeIn(ps.speed);
                }
            }
            else {
                pictures.find('a:visible').hide();
                p.fadeIn(ps.speed);
            }
            navigator.find('.jquery-accordion-navHover').removeClass('jquery-accordion-navHover');
            navigator.children(':eq(' + p.attr('index') + ')').addClass('jquery-accordion-navHover');

            if (ps.titleHeight != 0) {
                title.height(0);
                title.animate({ height: ps.titleHeight }, ps.speed);
                title.html('<b>' + p.attr('title') + '</b>' + p.attr('description'));
            }
        }
        if (count > 1) {
            window[ps.timerId] = setInterval(pictureBox, ps.swapSpeed);
        }
    });
}