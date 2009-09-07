/// <reference path="../jquery-vsdoc.js" />
function beginCallFunction(target) {
    var v = target.options[target.selectedIndex].value;

    window.location.href = _nativeUrl + "&sortby=" + v;
}