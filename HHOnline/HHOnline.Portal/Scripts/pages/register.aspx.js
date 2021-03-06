﻿/// <reference path="../jquery-vsdoc.js" />
function showTip(text, style) {
    $('#msgTip').show().attr('class', 'msgTip ' + style).text(text);
}
function validateName() {
    var m = $(this);
    var v = $.trim(m.val());
    if (v == '') { return; }
    else {
        showTip('正在验证用户名。。', 'loadTip');
        $.ajax({
            dataType: 'json',
            data: { action: 'ValidName', name: v, t: Math.random() },
            url: 'organize.axd',
            error: function(msg) { showTip('无法连接到服务器！', 'errorTip'); },
            success: function(json) {
                showTip(json.msg, json.suc ? 'rightTip' : 'errorTip');
            }
        })
    }
}
function loadRegion(data,callback ) {
    $.ajax({
        url: 'organize.axd',
        dataType: 'json',
        data: data,
        error: function(err) { alert(err.responseText); },
        success: function(json) {
            callback(json);
        }
    });
}
function getRegion(target) {
    var sa = $('#showArea');
    sa.prev().val(target.attr('title'));
    sa.next().val(target.attr('regionId'));
}
function selectArea() {
    var me = $(this);
    var t = $('#regionViewer');
    t.css({
        left: me.offset().left - 300,
        top: me.offset().top - 20
    }).addClass('viewerLoader').html('正在加载...').show();
    loadRegion({ action: 'GetRegion', type: '1', t: Math.random() }, function(json) {
        var sb = new stringBuilder();
        $.each(eval(json.msg), function(i, n) {
            sb.append('<a href="javascript:{}" title="' + n.RegionName + '" regionId="' + n.RegionID + '" regionCode="' + n.RegionCode +
             '" isArea="' + ($.trim(n.DistrictCode) != '') + '">' + n.RegionName.cut(13) + '</a>');
        });
        t.html(sb.toString()).removeClass('viewerLoader');

        t.find('a').click(function() {
            t.addClass('viewerLoader').html('正在加载...');
            var _me = $(this);
            loadRegion({ action: 'GetRegion', type: '2', areaId: _me.attr('regionId'), t: Math.random() }, function(json2) {
                var sb2 = new stringBuilder();
                $.each(eval(json2.msg), function(i2, n2) {
                    sb2.append('<a href="javascript:{}"  title="' + n2.RegionName + '" regionId="' + n2.RegionID + '" regionCode="' + n2.RegionCode +
                 '" isArea="' + ($.trim(n2.DistrictCode) != '') + '">' + n2.RegionName.cut(13) + '</a>');
                });
                if (sb2.toString() == '') {
                    getRegion(_me);
                    t.hide();
                }
                else {
                    t.html(sb2.toString()).removeClass('viewerLoader');
                    t.find('a').click(function() {
                        getRegion($(this));
                        t.hide();
                    })
                }
            });
        })
    });
}
$().ready(function() {
    $('#tdPassword').find('input:first').pstrength({
        'minCharText': '[%d个字符以上]',
        'verdicts': ['简单', '一般', '中等难度', '复杂', '非常复杂']
    });
    var register = $('input[rel=register]');
    $('#validateName').find('input:first').blur(validateName);
    $('#chkAgree').click(function() {
        var m = $(this);
        register.attr('disabled', !m.attr('checked'));
    });


    var sa = $('#regionViewer');
    $('#showArea').click(selectArea);

    $(document).mousedown(function(e) {
        if ((e.pageX < sa.offset().left || e.pageX > sa.offset().left + sa.width()) ||
            e.pageY < sa.offset().top || e.pageY > sa.offset().top + sa.height()) {
            sa.hide();
        }
    })
});