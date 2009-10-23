/// <reference path="../jquery-vsdoc.js" />

$().ready(function() {
    var t = $('#swChart');
    $.ajax({
        url: 'product.axd',
        dataType: 'json',
        data: { action: 'getStatistic', t: Math.random() },
        error: function(err) {
            t.removeClass('swCanvasLoad').html('无法获取搜索关键字信息！');
        },
        success: function(json) {
            if (json.suc) {
                var allColors = ['0x0000CD', '0xA52A2A', '0x7FFF00', '0xDC143C', '0x8B008B',
                                 '0xFF8C00', '0x00BFFF', '0xFF00FF', '0x4B0082', '0x800080'];
                var colors = [];
                var title = [], percentage = [], showName = [], defaultState = [], amount = 0;
                $.each(eval('('+json.msg+')'), function(i, n) {
                    title.push(n.SearchWord + '(' + n.HitCount + '次)');
                    percentage.push(n.HitCount);
                    showName.push('true');
                    defaultState.push('false');
                    amount += n.HitCount;
                });
                if (title.length > 0) {
                    for (var i in percentage) {
                        percentage[i] = parseInt(parseInt(percentage[i]) * 100 / amount);
                        colors.push(allColors[i]);
                    }
                    defaultState[0] = 'true';
                    t.flash({
                        url: '../images/flash/pie_chart.swf?t='+Math.random(),
                        width: 800,
                        height: 400,
                        flashVars: {
                            pie_num: percentage.join(','),
                            pie_name: title.join(','),
                            show_name: showName.join(','),
                            colorstr: colors.join(','),
                            defaultsate: defaultState.join(',')
                        }
                    });
                }
                else {
                    t.html('没有相关搜索关键字信息！');
                }
            }
            else {
                t.html('无法获取搜索关键字信息！');
            }

            t.removeClass('swCanvasLoad');
        }
    })
})