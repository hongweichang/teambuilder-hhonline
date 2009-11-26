/// <reference path="../jquery-vsdoc.js" />
function changeTab() {
    var me = $(this);
    if (me.hasClass('active')) { return; }
    var tabName = me.attr('rel');
    var last = me.parent().find('li.active');

    last.removeClass('active');
    me.addClass('active');
    $('#' + last.attr('rel') + 'TabContent').hide();
    $('#' + tabName + 'TabContent').show();
}
function loadPictures() {
    var picsContainer = $('#divAdLogo');
    $.ajax({
        dataType: 'json',
        url: 'ads.axd',
        error: function(error) {
            var msg = error.responseText != '' ? error.responseText : '图片加载失败';
            picsContainer.html(msg);
            picsContainer.removeClass('loader04');
        },
        success: function(json) {
            picsContainer.hrzAccordion({
                pictures: json
            });
            picsContainer.removeClass('loader04');
        }
    })
}
function loadFriendLinks() {
    var flm = $('#friendLinkMarque');
    $.ajax({
        dataType: 'json',
        url: 'product.axd',
        data: { action: 'getFriendLinks', t: Math.random() },
        error: function(error) { flm.html("加载失败！"); },
        success: function(json) {
            if (json.suc) {
                var fls = json.msg;
                if (fls != null && fls.length > 0) {
                    var sb = new stringBuilder();
                    $.each(eval('(' + fls + ')'), function(i, n) {
                        sb.append('<a href="');
                        sb.append(n.Url);
                        sb.append('" target="_blank">');
                        sb.append(n.Title);
                        sb.append('</a>');
                    });
                    flm.html(sb.toString());
                }
            }
            else {
                flm.html("暂无！");
            }
        }
    })
}
$().ready(function() {
    loadPictures();

    $('#productNavigator1').find('li').click(changeTab);
    $('#productNavigator2').find('li').click(changeTab);
    //loadFriendLinks();

    $('#articleListContainer').marque({
        width:280,
        direction:0,
        step:26,
        height:260,
        speed:2
    })
});