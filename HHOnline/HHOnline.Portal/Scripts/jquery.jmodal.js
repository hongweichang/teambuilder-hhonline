/// <reference path="jquery.js"/>
/*
 * jmodal
 * version: 2.0 (05/13/2009)
 * @ jQuery v1.3.*
 *
 * Licensed under the GPL:
 *   http://gplv3.fsf.org
 *
 * Copyright 2008, 2009 Jericho [ thisnamemeansnothing[at]gmail.com ] 
 *  
*/
$.extend($.fn, {
    jmodal: function(setting) {
        var ps = $.fn.extend({
            data: {},
            marginTop: 100,
            onunload: function(e) { },
            initWidth: 400,
            title: 'JModal Dialog',
            content: 'This is a jquery plugin!',
            overlayColor: '#888',
            closable: true,
            iframe: true
        }, setting);

        window.$hideJmodal =
            window.$hideJmodal ?
                window.$hideJmodal :
                new Function('overlay', 'jmodal', 'onunLoad', "overlay.animate({ opacity: 0 }, function() { $(this).css('display', 'none') });jmodal.animate({ opacity: 0 }, function() { $(this).css('display', 'none'); var c = onunLoad?onunLoad():true;});");
        window.$jmodalExecute = false;
        
        ps.docWidth = $(document).width();
        ps.docHeight = $(document).height();

        var jmodal = $('#jquery-jmodal');
        var overlay = $('#jmodal-overlay');

        if (jmodal.length == 0) {
            $('<div id="jmodal-overlay" class="jmodal-overlay"/>' +
                '<div class="jmodal-main" id="jquery-jmodal" >' +
                   '<div class="jmodal-r1c1" id="jmodal-title"></div>' +
                   '<div class="jmodal-r1c2" >&nbsp;</div>' +
                   '<div class="jmodal-r2" id="jmodal-content"></div>' +
                '</div>').appendTo($('form:first-child') || $(document.body));
            overlay = $('#jmodal-overlay');
            jmodal = $('#jquery-jmodal');
        }
        else {
            overlay = $('#jmodal-overlay').css({ opacity: 0, 'display': 'block' });
            jmodal = $('#jquery-jmodal').css({ opacity: 0, 'display': 'block' });
        }

        overlay.css({
            height: ps.docHeight,
            opacity: 0,
            backgroundColor: ps.overlayColor
        }).animate({ opacity: 0.5 });
        var c = $('#jmodal-content').html('');
        var close = c.next();
        var hide = function() {
            if (window.$jmodalExecute) { return; }
            ps.onunload(c, ps.data);
            window.$hideJmodal(overlay, jmodal, function() { window.$jmodalExecute = true });
        };
        if (ps.closable) {
            c.css('border-bottom', 0);
            if (close == null || close.length == 0) {
                $('<div class="jmodal-r3"><div class="jmodal-r3c1" id="jmodal-close">&nbsp;</div></div>').insertAfter(c);
                close = $('#jmodal-close').hover(function() {
                    $(this).css('background-position', 'center 0');
                }, function() {
                    $(this).css('background-position', 'center center');
                }).mousedown(function() {
                    $(this).css('background-position', 'center -48px');
                });
            }
            else {
                close.css('display', 'block');
            }
            close.unbind('click', hide).bind('click', hide);
        }
        else {
            if (close != null && close.length > 0) {
                close.css('display', 'none');
                c.css('border-bottom', 'solid 4px #1b376c');
            }
        }
        if (ps.iframe) {
            $('<iframe scrolling="no" frameborder="0" width="100%" />').appendTo(c);
            c = c.css('padding', '0').find('iframe:eq(0)');
        }
        jmodal.css({
            position: (ps.fixed ? 'fixed' : 'absolute'),
            width: ps.initWidth,
            left: (ps.docWidth - ps.initWidth) / 2,
            top: (ps.marginTop + document.documentElement.scrollTop)
        }).animate({ opacity: 1 }, function() {
            jmodal.css('opacity','').find('#jmodal-title').html(ps.title);

            if (typeof ps.content == 'string') {
                $('#jmodal-container-content').html(ps.content);
            }
            if (typeof ps.content == 'function') {
                ps.content(c, { cancel: function(onunLoad) { window.$hideJmodal(overlay, jmodal, onunLoad) } });
            }
        });
    }
});

