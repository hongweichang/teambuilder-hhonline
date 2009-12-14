function batchOPTs(action, ids) {
    var mb = $('#batchOpts');
    $.ajax({
        url: 'product.axd',
        dataType: 'json',
        data: { action: action, t: Math.random(), ids: ids },
        error: function(resp) {  mb.removeClass('batchOptsLoader').html('发生了错误，无法完成操作！') },
        success: function(json) {
            mb.removeClass('batchOptsLoader').html(json.msg);
            if (json.suc) { setTimeout('window.parent.location.href = window.parent.location.href;', 1000); }
        }
    });
}
var __opts = {
    'delete': 'batchDelete',
    'publish':'batchPublish',
    'copy':'batchCopy',
    'truncate':'batchTruncate'
};
$().ready(function() {
    batchOPTs(__opts[__action], __ids);
});