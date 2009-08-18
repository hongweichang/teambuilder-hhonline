/// <reference path="../jquery-vsdoc.js" />
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

$().ready(function() {
    $('#tdPassword').find('input:first').pstrength({
        'minCharText': '[%d个字符以上]',
        'verdicts': ['简单', '一般', '中等难度', '复杂', '非常复杂']
    });

    $('#validateName').find('input:first').blur(validateName);
});