///<reference path="../jquery-vsdoc.js" />
$().ready(function() {
    var anchors = $('#sitemapContainer').find('.smc_r2').find('a:first');
    var loader = $('#smLoader');
    anchors.click(function() {
        var m = $(this);

        if (!m.hasClass('disabled')) {
            loader.show();
            anchors.addClass('disabled');

            $.ajax({
                url: 'sitemap.axd',
                dataType: 'json',
                data: { action: m.attr('ref'), t: Math.random() },
                error: function(err) {
                    anchors.removeClass('disabled');
                    loader.hide();
                    showMsg({ msg: '生成过程中发生了错误，请重试！', top: 100, bgColor: '#888' });
                },
                success: function(json) {
                    showMsg({ msg: json.msg, top: 100, bgColor: '#888' });
                    loader.hide();
                    anchors.removeClass('disabled');
                }
            });
        }
    })
})