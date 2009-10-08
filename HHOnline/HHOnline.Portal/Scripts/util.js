function correctPNG() {
    var arVersion = navigator.appVersion.split("MSIE");
    var version = parseFloat(arVersion[1]);
    if ((version >= 5.5) && (document.body.filters)) {
        for (var j = 0; j < document.images.length; j++) {
            var img = document.images[j];
            var imgName = img.src.toUpperCase();
            if (imgName.substring(imgName.length - 3, imgName.length) == "PNG") {
                var imgID = (img.id) ? "id='" + img.id + "' " : "";
                var imgClass = (img.className) ? "class='" + img.className + "' " : "";
                var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' ";
                var imgStyle = "display:inline-block;" + img.style.cssText;
                if (img.align == "left") { imgStyle = "float:left;" + imgStyle; }
                if (img.align == "right") {imgStyle = "float:right;" + imgStyle; }
                if (img.parentElement.href) {imgStyle = "cursor:hand;" + imgStyle; }
                var strNewHTML = "<span " + imgID + imgClass + imgTitle
                                             + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";"
                                             + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
                                             + "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>";
                img.outerHTML = strNewHTML;
                j = j - 1;
            }
        }
    }
}
///url,width,height,modalness,args
function popWin(setting) {
    var url = setting.url;
    do {
        url = url.replace('+', '%2b').replace('/', '%2f');
    }
    while (url.indexOf('+') >= 0 || url.indexOf('/') >= 0);

    var left = 0, top = 0;
    var feature1 = 'toolbar=no,resizable=no,location=no,status=yes,';
    var feature2 = 'toolbar=no;resizable=no;location=no;status=yes;';
    if (typeof setting.width != 'undefined') {
        left = (window.screen.width - setting.width) / 2;
        feature1 += 'left=' + left + ',width=' + setting.width + ',';
        feature2 += 'dialogWidth=' + setting.width + 'px;dialogLeft='+left+"px;";
    }
    if (typeof setting.height != 'undefined') {
        top = (window.screen.height - setting.height) / 3;
        feature1 += 'top=' + top + ',height=' + setting.height;
        feature2 += 'dialogHeight=' + setting.height + 'px;;dialogTop=' + top + "px;";
    }
    var popWindow = null;
    if (setting.modalness) {
        popWindow = window.showModalDialog(url, setting.args ? setting.args : '', feature2);
    }
    else {
        popWindow = window.open(url, '', feature1);
        popWindow.isPopup = true;
    }
    return popWindow;
}
//String Builder
function stringBuilder() {
    this.__S_B = new Array();
}
stringBuilder.prototype.append = function(value) {
    this.__S_B.push(value);
}
stringBuilder.prototype.appendNewLine = function(value) {
    this.__S_B.push('');
    this.__S_B.push(value);
}
stringBuilder.prototype.toString = function(splitExp) {
    return (typeof splitExp != 'undefined' ? this.__S_B.join(splitExp) : this.__S_B.join(''));
}
//String Formater
String.prototype.format = function() {
    var args = arguments;
    return this.replace(/{(\d{1})}/g, function() {
        return args[arguments[1]];
    });
};

//Template translator
window.__templateCache = {};
function templateTrans(template, json) {
    var foo = !/\W/.test(template) ?
         (__templateCache[template] = (__templateCache[template] || templateTrans(document.getElementById(template).innerHTML)))
         :
         (new Function("obj",
                            "var p=[];" +
                            "with(obj){p.push('" +
                             template.replace(/[\r\t\n]/g, " ")
                            .replace(/'(?=[^#]*#>)/g, "\t")
                            .split("'").join("\\'")
                            .split("\t").join("'")
                            .replace(/<#=(.+?)#>/g, "',$1,'")
                            .split("<#").join("');")
                            .split("#>").join("p.push('")
                            + "');}return p.join('');"));
    return json ? foo(json) : foo;
}

//弹出模态消息框
function showMsg(setting) {
    $.fn.jmodal({
        title: '提示信息',
        initWidth: 600,
        overlayColor: setting.bgColor ? setting.bgColor : '#fff',
        marginTop: setting.top ? setting.top : 0,
        iframe: false,
        content: function(sender) {
            sender.css('padding', '10px');
            sender.html(setting.msg);
        },
        onunload: setting.onunload
    });
}

//弹出模态页面
function showPage(setting) {
    $.fn.jmodal({
        title: setting.title,
        initWidth: setting.width ? setting.width : 600,
        overlayColor: setting.bgColor?setting.bgColor:'#fff',
        marginTop: setting.marginTop?setting.marginTop:0,
        closable: setting.closable ? true : false,
        content: function(sender, args) {
            sender.attr('src', setting.url);
            if (typeof window.$ohj == 'undefined') {
                window.$ohj = args.cancel;
            }
        }
    });
}
//format
String.prototype.format = function() {
    var args = arguments;
    return this.replace(/{(\d{1})}/g, function() {
        return args[arguments[1]];
    });
};
//cut
String.prototype.cut = function(len) {
    var position = 0;
    var result = [];
    var tale = '';
    for (var i = 0; i < this.length; i++) {
        if (position >= len) {
            tale = '...';
            break;
        }
        if (this.charCodeAt(i) > 255) {
            position += 2;
            result.push(this.substr(i, 1));
        }
        else {
            position++;
            result.push(this.substr(i, 1));
        }
    }
    return result.join("") + tale;
};
$().ready(function() {
    correctPNG();
})