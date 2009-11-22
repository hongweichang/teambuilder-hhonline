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
            url: window.$url,
            action: 'ValidUser',
            t: Math.random()
        };
        $.ajax({
            data: data,
            dataType: 'json',
            url: 'organize.axd',
            error: function(msg) {
                error.html(msg.responseText).show();
                $('#imgValidCode').attr('src', 'validcode.axd?t=' + Math.random);
            },
            success: function(json) {
                if (json.suc) {
                    var _loc = json.msg.split('?');
                    if (_loc.length > 1) {
                        var _url = _loc[0] + "?";
                        var params = _loc[1].split('&');
                        var _q = '';
                        var _index = -1;
                        var _pa = '';
                        for (var i in params) {
                            _pa = '';
                            _index = params[i].indexOf('=');
                            if (_index < 0) { _q += params[i] + "&"; }
                            else {
                                _q += params[i].substring(0, _index)+'=';
                                _q += escape(params[i].substring(_index+1, params[i].length - _index + 2)) + '&';
                            }
                        }
                        _url += _q;
                        _url = _url.substring(0, _url.length - 1);
                        window.location.href = _url;
                    } else {
                        window.location.href = json.msg;
                    }
                }
                else {
                    overlay.hide();
                    loading.hide();
                    $('#imgValidCode').attr('src', 'validcode.axd?t=' + Math.random);
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
    var un = $('#txtUserName');
    var pwd = $('#txtPassword');
    var vc = $('#txtValidCode');
    un.focus(function() {
        $(this).select();
    });
    pwd.focus(function() {
        $(this).select();
    });
    vc.focus(function() {
        $(this).select();
    });
    if (typeof window.$userinfo != 'undefined') {
        un.val($userinfo.uid);
        pwd.val($userinfo.pwd);
        un.keyup(function() {
            if (un.val() != $userinfo.uid) { pwd.val(''); }
            else { pwd.val($userinfo.pwd); }
        })
        $('#chkRememberMe').attr('checked', true);
    }
    $('#userLogin').find('input').keyup(function(e) {
        if (e.keyCode == 13) {
            signUp();
        }
    })
}