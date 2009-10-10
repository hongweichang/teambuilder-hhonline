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
function clearArea() {
	var sa = $('#showArea');
	sa.prev().val('全国');
	sa.next().val('0');
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
    
    $('input[rel=quotefromdate]').datepick({
        year: '-40:5',
        popTo: 'input[rel=quotefromdate]',
        dateFormat: 'yyyy年MM月dd日'
    });
    
    $('input[rel=quoteenddate]').datepick({
        year: '-40:5',
        popTo: 'input[rel=quoteenddate]',
        dateFormat: 'yyyy年MM月dd日'
    });

    var sa = $('#regionViewer');
    $('#showArea').click(selectArea);
	$('#clearArea').click(clearArea);

    $(document).mousedown(function(e) {
        if ((e.pageX < sa.offset().left || e.pageX > sa.offset().left + sa.width()) ||
            e.pageY < sa.offset().top || e.pageY > sa.offset().top + sa.height()) {
            sa.hide();
        }
    });
});