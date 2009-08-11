///<reference path="jquery.js" />
/*
    copyright @ jericho 2009
    http://www.ajaxplaza.net
*/
$.fn.datepick = function(setting) {
    var opts = $.fn.extend({
        plugCss: {},
        popTo: null,
        topClass: 'jquery-datepick-top',
        todayClass: 'jquery-datepick-today',
        weekMonthClass: 'jquery-datepick-month',
        optsClass: 'jquery-datepick-opts',
        dayClass: 'jquery-datepick-days',
        weekClass: 'jquery-datepick-weeks',
        weeks: ['日', '一', '二', '三', '四', '五', '六'],
        lang: ['年', '月', '星期', '选择年月：', '今天'],
        year: '-10:0',
        dateFormat: 'yyyy/MM/dd'
    }, setting);
    opts.popTo = (opts.popTo == null ? this : opts.popTo);
    var dp = this;
    this.date = new Date();
    this.year = this.date.getFullYear();
    this.month = this.date.getMonth();
    this.today = this.date.getDate();
    this.week = this.date.getDay();
    var datepick = $('#jquery-datepick');
    if (datepick.length == 0) {
        datepick = $('<div id="jquery-datepick">' +
                '<div class="' + opts.topClass + '">' +
                    '<div class="' + opts.todayClass + '"><a href="javascript:void(0)" >' + opts.lang[4] + '</a></div>' +
                    '<div class="' + opts.weekMonthClass + '">' +
                        '<div class="week">' + opts.lang[3] + '</div>' +
                        '<div class="month">' +
                            '<select rel="year" id="jdSelYear"  /><span rel="lang">' + opts.lang[0] + '</span>' +
                            '<select rel="month" id="jdSelMonth"  /><span rel="lang">' + opts.lang[1] + '</span></div>' +
                    '</div>' +
                    '<div class="' + opts.optsClass + '">' +
                        '<div class="close"><a href="javascript:void(0)" /></div>' +
                        '<div class="monthview" />' +
                    '</div>' +
                '</div>' +
                '<div  class="' + opts.dayClass + '">' +
                    '<div class="' + opts.weekClass + '" />' +
                    '<table id="jquery-datepick-main" cellspacing="0" cellpadding="0" style="width:100%"></table>' +
                '</div>' +
            '</div>').css(opts.plugCss).hide().appendTo($(document.body));
        $.each(opts.weeks, function(i, n) {
            $('<span>' + n + '</span>').appendTo($('.' + opts.weekClass, datepick));
        });

        $('.close', datepick).click(hide);
    }

    function formatDate(f, date) {
        f = (typeof f == 'undefined' ? 'yyyy-MM-dd' : f);
        var year = date.getFullYear().toString();
        var month = (date.getMonth() + 1).toString();
        var day = date.getDate().toString();
        var ym = f.replace(/[^y|Y]/g, '');
        if (ym.length == 2) year = year.substring(2, 4);

        var mm = f.replace(/[^m|M]/g, '');
        if (mm.length > 1) {
            if (mm.length == 1) {
                month = "0" + month;
            }
        }
        var dm = f.replace(/[^d]/g, '');
        if (dm.length > 1) {
            if (day.length == 1) {
                day = "0" + day;
            }
        }
        return f.replace(ym, year).replace(mm, month).replace(dm, day);
    }
    function hide() {
        if (typeof datepick != 'undefined' && datepick != null && datepick.is(':visible')) {
            datepick.hide();
        }
    }
    function getMonthView(year, month) {
        var ma = new Array();
        var sdw = new Date(year, month, 1).getDay();
        var edw = new Date(year, month + 1, 0).getDate();
        for (var i = 0; i < 42; i++) {
            ma[i] = '';
        }
        for (var i = 0; i < edw; i++) {
            ma[i + sdw] = i + 1;
        }
        return ma;
    }
    function bind(sel, arr) {
        sel.html('');
        $.each(arr, function(i, n) {
            $('<option value="' + n + '">' + n + '</option>').appendTo(sel);
        });
    }
    function bindDate(curYear, curMonth) {
        $('#jdSelYear').val(curYear);
        $('#jdSelMonth').val(curMonth);
        var dma = getMonthView(curYear, curMonth - 1);
        var anchor = $('#jquery-datepick-main').find('a');
        for (var n in dma) {
            anchor.eq(n).html(dma[n]).attr('class', 'days').unbind('click').bind('click', function() {
                if (!$(this).hasClass('days') && !$(this).hasClass('today')) { return };

                hide();

                $(opts.popTo).val(formatDate(opts.dateFormat, new Date(
                                                            curYear,
                                                            curMonth - 1,
                                                            parseInt($(this).text()))));
            });
            if (dma[n] != '') {
                if (curYear == dp.year && curMonth == dp.month + 1 && dma[n] == dp.today) {
                    anchor.eq(n).removeClass('days').addClass('today');
                }
            }
            else {
                anchor.eq(n).removeClass('days');
            }
        }
    }
    function adapt() {
        datepick.find('.' + opts.todayClass).find('a').unbind('click').bind('click', function() {
            $(opts.popTo).val(formatDate(opts.dateFormat, new Date()));
            hide();
        });
        datepick.find('select').unbind('change').bind('change', function() {
            bindDate(parseInt($('#jdSelYear').val()), parseInt($('#jdSelMonth').val()));
        });
    }
    return this.each(function() {
        var me = this;

        datepick = (datepick == null ? $('#jquery-datepick') : datepick);

        var minusYear, plusYear;
        opts.year.replace(/\-(\d{0,2}):(\d)/g, function() {
            minusYear = parseInt(arguments[1]);
            plusYear = parseInt(arguments[2]);
        });

        this.startYear = dp.year - minusYear;
        this.endYear = dp.year + plusYear;

        var dayHtml = '';
        var arrYear = new Array();
        for (var i = me.startYear; i <= me.endYear; i++) {
            arrYear.push(i);
        }
        bind($('#jdSelYear'), arrYear);

        var arrMonth = new Array();
        for (var i = 1; i <= 12; i++) {
            arrMonth.push(i);
        }
        bind($('#jdSelMonth'), arrMonth);

        $(this).click(function(e) {
            var t = $(opts.popTo);
            e.stopPropagation();

            var v = $.trim(t.val());
            var y = dp.year;
            var m = dp.month + 1;

            if (v != '') {
                var r = opts.dateFormat.replace(/(dd)/g, '(\\d{2})', false)
                                                   .replace(/(yyyy|yy)/g, '(\\d{2,4})', false)
                                                   .replace(/(mm|MM|m|M)/g, '(\\d{1,2})', false)
                var reg = new RegExp(r, 'g');

                v.replace(reg, function() {
                    var a = arguments;
                    y = a[1];
                    m = a[2];
                });
            }
            datepick.css({
                left: t.offset().left,
                top: t.offset().top + t.innerHeight() + 5
            }).show();
            bindDate(y, m);
            adapt();
        });
        //create date unit
        for (var i = 0; i < 6; i++) {
            dayHtml += '<tr>';
            for (var j = 0; j < 7; j++) {
                dayHtml += '<td><a href="javascript:void(0)"></a></td>';
            }
            dayHtml += '</tr>';
        }
        $('#jquery-datepick-main', datepick).html('<tbody>' + dayHtml + '</tbody>');



        $(document).bind('mousedown', function(e) {
            if (e.pageX + document.documentElement.scrollLeft > datepick.offset().left + datepick.width() ||
                e.pageX + document.documentElement.scrollLeft < datepick.offset().left ||
                e.pageY + document.documentElement.scrollTop > datepick.offset().top + datepick.height() ||
                e.pageY + document.documentElement.scrollTop < datepick.offset().top
                ) {
                if (typeof e.target != 'undefined' && ($(e.target).attr('id') == 'jdSelYear' || $(e.target).parent().attr('id') == 'jdSelYear'))
                    return;
                hide();
            }
        });
    })
}