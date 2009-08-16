/// <reference path="jquery.js" />
$.fn.hrzAccordion = function(settings) {
    var ps = $.fn.extend({
        speed: 500,
        swapSpeed: 5000,
        titleHeight: 40,
        width: 448,
        height: 183,
        pictures: []
    }, settings);
    return this.each(function() {
        var me = $(this);
        var outer = me.find('div.jquery-accordion'), pictures = null, count = 0, title = null, navigator = null, nav = null;

        if (outer.length == 0) {
            $('<div class="jquery-accordion">' +
                '<div class="jquery-accordion-pictures" />' +
                '<div class="jquery-accordion-titles" />' +
                '<div class="jquery-accordion-navigate" ><div class="jquery-accordion-nav" /></div>' +
                '</div>').appendTo(me);
            outer = me.find('div.jquery-accordion');
        }
        outer.css({
            width: ps.width,
            height: ps.height
        });
        pictures = me.find('div.jquery-accordion-pictures');
        navigator = me.find('div.jquery-accordion-navigate');
        nav = navigator.children(':first');

        var html = '', b = false;
        $.each(ps.pictures, function(i, n) {
            count = i;
            b = ($.trim(n.Link) != '');
            html += '<a index="' + i + '"' + (b ? ' href="' + n.Link + '" style="cursor:pointer" target="_blank" ' : ' style="cursor:default" ') +
                         'title="' + n.Title + '" description="' + n.Description + '">' +
                         '<img src="' + n.Url + '" style="width:' + ps.width + 'px;height:' + ps.height + 'px;"  /></a>';

        });
        if (count == 0) { return; }
        pictures.html(html);
        navigator.width((count + 1) * 16);
        title = pictures.next().css('opacity', 0.7).height(ps.titleHeight);
        pictures.find('a:not(:first)').css('display', 'none');
        var pf = pictures.find('a:first');
        title.html('<b>' + pf.attr('title') + '</b>' + pf.attr('description'));

        var _First = pictures.find('a:first');
        var _Last = pictures.find('a:last');

        function pictureBox() {
            title.height(0);
            title.animate({ height: ps.titleHeight }, ps.speed);
            var p = null;
            if (_Last.is(':visible')) {
                p = _First.fadeIn(ps.speed).addClass('jquery-accordion-visible');
                _Last.hide();
            } else {
                p = pictures.find('a:visible').hide().next().fadeIn(ps.speed);
            }
            title.html('<b>' + p.attr('title') + '</b>' + p.attr('description'));
            nav.animate({ left: 16 * p.attr('index') });

        }
        if (count != 1) {
            setInterval(pictureBox, ps.swapSpeed);
        }
    })
}