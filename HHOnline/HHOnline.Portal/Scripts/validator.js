/// <reference path="jquery-vsdoc.js" />

if (typeof (window.$validatorparams) == "undefined") {
    alert("初始化Js失败，无法自定义验证控件外观！")
}

function hideValidator(tagId) {
    var target = $('#' + tagId);
    if ($validatorparams.isFilterClose) {
        target.fadeOut();
    }
    else {
        target.hide();
    }
}
//override asp.net Validate Controls
function ValidatorUpdateDisplay(val) {
    var target = $(val);
    
    if (typeof val.display != 'undefined') {
        var dis = val.display.toLowerCase();
        if (dis == "none") {
            return;
        }
        if (dis == "dynamic") {
            target.css({ dispaly: (target.attr('IsValid') ? "none" : "inline") });
        }
    }
    target.css({ display: 'inline' , position: 'absolute' })
                .attr('class','aspx-validator')
                    .html('<div class="aspx-validator-bg" id="' + val.id + '_sub">' +
                                '<div class="aspx-validator-text">' + val.errormessage + '</div>' +
                             '</div>');
    //var renderTo = $('#' + val.controltovalidate);

    //var WinElementPos = getWinElementPos(obj)
    //val.style.left = (parseInt(WinElementPos.x + obj.offsetWidth)).toString() + "px";
    //val.style.top = (parseInt(WinElementPos.y)).toString() + "px";
    target.css({ visibility: (val.isvalid ? 'hidden' : 'visible'), opacity: 0 });

    if (val.isvalid) {
        target.animate({ opacity: 0 });
    }
    else {
        target.animate({ opacity: 1 });
    }

    if ($validatorparams.isAutoClose) {
        setTimeout("hideValidator('" + val.id + "')", $validatorparams.duration);
    }
}