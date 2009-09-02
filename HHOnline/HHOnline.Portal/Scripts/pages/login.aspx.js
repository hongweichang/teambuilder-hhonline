/// <reference path="../jquery-vsdoc.js" />
// 2. 内部用户
// 1. 企业用户
function signUp() {
    var inputs = $('#loginForm').find('input');
    var isvalid = true;
    inputs.each(function() {
        var me = $(this);
        if (!me.is('[type=checkbox]')) {
            if ($.trim(me.val()) == '') {
                me.next().show();
                isvalid = false;
            }
            else {
                me.next().hide();
            }
        }
    });
    if (isvalid) {
        var error = $('#errorMsg').hide();
        var loading = $('#divLoading');
        var userLogin = $('#userLogin');
        var overlay = $('#loginOverlay');
        var vc = $('#txtValidCode');
        var data = {
            userName: $('#txtUserName').val(),
            password: $('#txtPassword').val(),
            rememberMe: $('#chkRememberMe').is('[checked=true]'),
            validCode: vc.val(),
            action: 'ValidUser',
            t: Math.random()
        };
        $.ajax({
            data: data,
            dataType: 'json',
            url: 'organize.axd',
            error: function(msg) {
                error.html(msg.responseText).show();
            },
            success: function(json) {
                if (json.suc) {
                    window.location.href = json.msg;
                }
                else {
                    overlay.hide();
                    loading.hide();
                    $('#imgValidCode').attr('src', 'validcode.axd?t' + Math.random);
                    error.html(json.msg).show();
                    vc.val('').focus();
                }
            }
        });
        
        overlay.css('opacity',0.3).show();
        loading.show();
    }
}
window.onload = function() {
    if (typeof window.$userinfo != 'undefined') {
        $('#txtUserName').val($userinfo.uid);
        $('#txtPassword').val($userinfo.pwd);
        $('#chkRememberMe').attr('checked', true);
    }
    $('#userLogin').find('input').keyup(function(e) {
        if (e.keyCode == 13) {
            signUp();
        }
    })
}