/// <reference path="jquery-vsdoc.js"/>
/*
* flash
* @ jQuery v1.2.*
*
* Copyright 2008, 2009 Jericho [ thisnamemeansnothing[at]gmail.com ] 
*  
*/
$.fn.flash = function(settings) {
    if (typeof settings == 'string') {
        settings = { url: settings };
    }
    settings.url = settings.url + '?t=' + Math.random();
    var p = $.extend($.fn.flash.defaults, settings);
    var flashTreeIE = '<object classid="clsid:D27CDB6E-AE6D-11CF-96B8-444553540000" id="{0}" ' +
                                'codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version={1},0,40,0" ' +
                                'border="0" width="{2}px" height="{3}px">' +
                               '<param name="movie" value="{4}">';

    var flashTreeMozilla = '<embed src="{4}" pluginspage="http://www.macromedia.com/go/getflashplayer" ' +
                                        'type="application/x-shockwave-flash" name="{0}" width="{2}px" height="{3}px"';
    var ppIE = '<param name="{0}" value="{1}" />';
    var ppMozilla = ' {0}="{1}" ';
    return this.each(function() {
        try {
            for (var prop in p.params) {
                flashTreeIE += ppIE.format(prop, p.params[prop]);
                flashTreeMozilla += ppMozilla.format(prop, p.params[prop]);
            }
            var vars = '';
            for (var v in p.flashVars) {
                vars += v + '=' + p.flashVars[v] + '&';
            }
            
            vars += 't=' + Math.random();
            flashTreeIE += ppIE.format('FlashVars', vars);
            flashTreeMozilla += ppMozilla.format('FlashVars', vars);
            flashTreeMozilla += '></embed>';
            flashTreeIE = flashTreeIE + flashTreeMozilla + "</object>";

            this.innerHTML = flashTreeIE.format((($.trim(p.uniqueId) == '') ? 'f' + (new Date().valueOf()).toString() : p.uniqueId),
                                                                 p.version,
                                                                 p.width,
                                                                 p.height,
                                                                 p.url);
        }
        catch (ex) {
            this.innerHtml = '<b style="color:#ff0000">' + ex.message + '</b>';
        }
    });
};
$.fn.flash.defaults = {
    version: '6',
    uniqueId: '',
    width: 300,
    height: 200,
    flashVars: {},
    url: '',
    params:
        {
            'quality': 'high',
            'wmode': 'transparent',
            'menu': 'false',
            'allowScriptAccess': 'sameDomain',
            'scale': 'noScale'
        }
};