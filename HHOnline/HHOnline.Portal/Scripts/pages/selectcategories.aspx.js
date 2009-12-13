///<reference path="../jquery-vsdoc.js" />
var htmlTemp = ' <div class="sc_list_item"><input type="checkbox" text="{2}" value="{0}" /><a title="{2}" onclick="callServer({0});" href="javascript:{}">{1}</a></div>';
function renderDataToClient(data) {
    var pId = 0;
    if (data.cats != '') {
        var fp = $('#sc_list');
        fp.html('加载中。。。');
        var sb = new stringBuilder();
        $(data.cats).each(function(i, n) {
            pId = n.ParentID;
            sb.append(htmlTemp.format(n.CategoryID, n.CategoryName.cut(16), n.CategoryName));
        });
        fp.html(sb.toString());
    }
    
    var sl = $('#sc_select');
    if (data.nav != '') {
        var html = '';
        $(data.nav).each(function(i, n) {
            if (i == 0) {
                html += ('<span title="{0}">{1}</span>').format(n.CategoryName, n.CategoryName.cut(15));
            }
            else {
                html = ('<a title="{2}" onclick="callServer({0});" href="javascript:{}">{1}</a>&gt;&gt;').format(n.CategoryID, n.CategoryName, n.CategoryName.cut(15)) + html;
            }
        });
        html = ('<a title="全部" onclick="callServer(0);" href="javascript:{}">全部</a>&gt;&gt;') + html;
        sl.html(html);
    }
    else {
        sl.html('<span>全部</span>');
    }
}
function insertCats() {
    var fp = $('#sc_list');
    fp.find('input[checked]').each(function() {
        var m = $(this);
        parent.addCategories(m.val(), m.attr('text'));
    });
    //parent.addCategories();
}
function refreshBinder(result, content) {
    renderDataToClient(eval('(' + result + ')'));
}
$().ready(function() {
    callServer('0');
})