/// <reference path="jquery-vsdoc.js"/>
/// <reference path="util.js"/>
window.__Position = [];

$.extend($.fn, {
    autoComplete: function(setting) {
        var opts = $.extend({
            url: '',
            data: { action: 'getSuggestion' },
            highLight: '<b><u>{0}</u></b>',
            highLightable: true,
            template: '',
            splitExp: '$s$',
            callback: function() { },
            containerCssName: 'jquery-autoComplete',
            itemNormalCssName: 'itemNormal',
            itemHoverCssName: 'itemHover'
        }, setting);

        function brush(html, regExp) {
            var highLight = function(text) {
                return opts.highLight.format(text);
            };
            var parse = function(text) {
                return text.replace(new RegExp(regExp, 'ig'), highLight);
            };
            return '<ul>' + $(html).find("li").map(function() {
                var me = $(this);
                var v = this.innerHTML;
                var s = v.replace(/<.*?>/g, function() {
                    return opts.splitExp;
                }).split(opts.splitExp);
                for (var i = 0; i < s.length; i++) {
                    if ($.trim(s[i]) != '') {
                        v = v.replace(s[i], parse(s[i]));
                    }
                }
                return '<li ' + (typeof me.attr('dataKey') != 'undefined' ? 'dataKey="' + me.attr('dataKey') + '"' : '') + '>' + v + '</li>';
            }).get().join('') + '</ul>';
        }

        function suggestion(e) {
            //get the controller
            var me = $(e.data.controller);
            // initializes the ajax dada, such as:
            // {
            //     action:'[ACTION]',
            //     value:'[VALUE]'
            //  }
            opts.data.value = me.val();
            opts.data.tick = Math.random();
            if ($.trim(opts.data.value) == '') { $('#' + me.attr('ac')).hide(); return; }

            $.ajax({
                url: opts.url,
                data: opts.data,
                dataType: 'json',
                error: function(msg) { throw new Error('failed to load suggestions from server!!!'); },
                success: function(json) {
                    var id = '';
                    var autoComplete = null;
                    // set Attribute 'ac'[autoComplete] to the controller
                    if (typeof me.attr('ac') == 'undefined') {
                        // proccessed an unique id
                        id = 'ac' + new Date().valueOf();

                        me.attr('ac', id);
                        me.after('<div class="' + opts.containerCssName + '" id="' + id + '"></div>');
                        autoComplete = $('#' + id).css(opts.containerCss);
                    }
                    else {
                        id = me.attr('ac');
                        autoComplete = $('#' + id).css(opts.containerCss);
                    }

                    var sb = new stringBuilder();
                    sb.append(templateTrans(opts.template, json));
                    
                    //cache the position of controller
                    __Position[id]=(__Position[id]?__Position[id]:{
                        width:me.width(),
                        height:me.height(),
                        offset: me.offset(),
                        pos:me.position()
                    });
                    var p = eval(__Position[id]);

                    autoComplete.css({
                        minWidth: p.width + 10,
                        left: p.pos.left + document.documentElement.scrollLeft,
                        top: p.pos.top + p.height + document.documentElement.scrollTop + 11
                    });



                    if ($.trim(sb.toString()) == '') {
                        autoComplete.hide();
                    }
                    else {
                        autoComplete
                            .html(brush('<ul>' + sb.toString() + '</ul>', me.val()))
                            .find('li')
                            .hover(function() {
                                this.className = opts.itemHoverCssName;
                            }, function() {
                                this.className = opts.itemNormalCssName;
                            }).click(function(ev) {
                                ev.stopPropagation();
                                var t = $(this);
                                var k = t.attr('dataKey');
                                me.val(typeof k == 'undefined' ? t.text() : k);
                                autoComplete.hide();
                                opts.callback();
                            });
                        autoComplete.show();
                    }
                }
            })
        }
        return this.each(function() {
            $(this).bind('keyup', { controller: this }, suggestion)
                        .bind('blur', function(e) {
                            setTimeout("$('#" + $(this).attr('ac') + "').hide()", 500);
                        });
        })
    }
});