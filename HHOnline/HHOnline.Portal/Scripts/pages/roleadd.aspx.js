/// <reference path="../jquery-vsdoc.js" />
var treeview = {
    CheckSubNodes: function() {
        $('div.treeviewpermission').find('input[type=checkbox]').click(function() {
            treeview.parentCheckStateChanged(this);
            treeview.childCheckStateChanged(this);
        });
    },
    parentCheckStateChanged: function(obj) {
        var me = $(obj);
        var CheckState = true;
        var p = me;
        do {
            p = p.parent()
            if (p.attr('class').toLowerCase() == 'treeviewpermission') {
                return;
            }
        }
        while (p.attr('tagName').toLowerCase() != "div");
        if (p != null && p.length > 0) {
            CheckState = !(p.find('input[checked=false]').length > 0)
        }
        p = p.prev();
        if (p != null &&p.length>0&& p.attr('tagName').toUpperCase() == "TABLE") {
            p.find('input[type=checkbox]').attr('checked', CheckState);
        }
    },
    childCheckStateChanged: function(obj) {
        var me = $(obj);
        var CheckState = me.attr('checked');
        var p = me;
        do {
            p = p.parent()
        }
        while (p.attr('tagName').toLowerCase() != "table");


        p = p.next();
        if (p != null && p.length > 0 && p.attr('tagName').toUpperCase() == "DIV") {
            p.find('input[type=checkbox]').attr('checked', CheckState);
        }
    }
};
$().ready(function() {
    treeview.CheckSubNodes();
});