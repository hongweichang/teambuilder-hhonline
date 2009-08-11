/// <reference path="jquery-vsdoc.js"/>
/*
* cookie
* @ jQuery v1.2.*
*
* Copyright 2008, 2009 Jericho [ thisnamemeansnothing[at]gmail.com ] 
*  
*/
$.fn.cookie = function(setting) {
    var ps = $.extend({
        //erase,get,set
        action: 'set',
        name: '',
        value: '',
        days: 1
    }, setting);
    var createCookie = function(name, value, expire) {
        document.cookie = ps.name + "=" + ps.value + "; expires=" + expire + "; path=/";
    };
    switch (ps.action) {
        case 'set':
            var expires = '';
            if (ps.days) {
                var date = new Date();
                date.setTime(date.getTime() + (ps.days * 24 * 60 * 60 * 1000));
                expires = date.toGMTString();
            }
            createCookie(ps.name, ps.value, expires);
            break;
        case 'get':
            var q = ps.name + '=';
            var cs = document.cookie.split(';');
            var c = '';
            for (var i = 0; i < cs.length; i++) {
                c = cs[i];
                while (c.charAt(0) == ' ') { c = c.substring(1, c.length); }
                if (c.indexOf(q) == 0) { return c.substring(q.length, c.length); }
            }
            return null;
            break;
        case 'erase':
            createCookie(ps.name, '', -1);
            break;
    }
}