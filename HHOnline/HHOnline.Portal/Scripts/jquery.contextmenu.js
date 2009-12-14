/// <reference path="jquery-vsdoc.js" />
/// <reference path="util.js" />
/*
* context menu
* version: 1.0.0 (11/25/2009)
* @ jQuery v1.2.*
*
* Licensed under the GPL:
*   http://gplv3.fsf.org
*
* Copyright 2008, 2009 Jericho [ thisnamemeansnothing[at]gmail.com ] 
*  
*/
(function($) {
    var $contextMenu, $contextMenuShadow, $jqcmCache = [];
    $.fn.extend($.fn, {
        contextMenu: function(settings) {
            var ps = $.extend({
                menuCss: 'jqcmMenu',
                shadowCss: 'jqcmShadow',
                itemCss: 'jqcmItem',
                itemHoverCss: 'jqcmItemHover',
                shadow: true,
                shadowRelative: 2,
                items: [], // serialize as  [itemCss:'ITEMCSS',itemText:'ITEMTEXT',itemTag:'ITEMTAG']
                onContextMenu: function(sender, e) { },
                onItemClick: function(sender, e) { }
            }, settings);

            if (!$contextMenu) {
                $contextMenu = $('<div id="jerichoContextMenu" />')
                            .hide()
                            .addClass(ps.menuCss)
                            .appendTo('body')
                            .bind('click', function(e) {
                                e.stopPropagation();
                            });
            }

            if (!$contextMenuShadow) {
                $contextMenuShadow = $('<div id="jcmShadow" />')
                                  .hide()
                                  .addClass(ps.shadowCss)
                                  .css('opacity', .4)
                                  .appendTo('body');
            }
            $jqcmCache = $jqcmCache || [];
            var doc = $(document);
            ps.size = { docWidth: doc.width(), docHeight: doc.height() };
            $jqcmCache.push(ps);

            return this.each(function() {
                $(this).bind('contextmenu', function(e) {
                    __displayContextMenu($jqcmCache.length - 1, this, e)
                    return false;
                });
            });
        }
    });


    function __displayContextMenu(index, target, e) {
        var cur = $jqcmCache[index];
        cur.onContextMenu(target, e);
        var sb = new stringBuilder();
        sb.append('<ul>');
        $.each(cur.items, function(i, n) {
            if (n.itemText == '-') { sb.append('<li class="jqcmSeperate">&nbsp;</li>'); }
            else { sb.append('<li class="' + n.itemCss + '" itemTag="' + n.itemTag + '">' + n.itemText + '</li>'); }
        });
        sb.append('</ul>');

        $contextMenu.html(sb.toString());
        $contextMenu.find('li').each(function(i) {
            if (this.className != 'jqcmSeperate') {
                $(this).addClass(cur.itemCss).hover(function() {
                    $(this).addClass(cur.itemHoverCss);
                }, function() {
                    $(this).removeClass(cur.itemHoverCss);
                })
            .click(function(e) {
                __hideContextMenu();
                cur.onItemClick(this, e);
            });
            }
        });
        var x = e.pageX, y = e.pageY, eleWidth = $contextMenu.width(), eleHeight = $contextMenu.height();
        if (e.pageX + eleWidth > cur.size.docWidth) {
            x = cur.size.docWidth - eleWidth - 10;
        }
        if (e.pageY + eleHeight > cur.size.docHeight) {
            y = cur.size.docHeight - eleHeight - 10;
        }
        $contextMenu.css({ left: x, top: y }).show();
        if (cur.shadow) {
            $contextMenuShadow.css({
                width: $contextMenu.width(),
                height: $contextMenu.height(),
                left: x + cur.shadowRelative,
                top: y + cur.shadowRelative
            }).show();
        }
        $(document).one('click', __hideContextMenu);
    }
    function __hideContextMenu() {
        $contextMenu.hide();
        $contextMenuShadow.hide();
    }
})(jQuery);