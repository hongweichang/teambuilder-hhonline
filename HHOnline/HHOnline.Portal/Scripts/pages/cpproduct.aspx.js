/// <reference path="../jquery-vsdoc.js" />
function showSupply(id) {
    showPage({
        marginTop: 120,
        width: 800,
        closable:true,
        title: '编辑公司信息',
        url: 'Product/SupplyDetails.aspx?ID=' + id + '&t=' + Math.random(),
        bgColor: '#888'
    });
}
function showBatch(url,closable) {
    showPage({
        marginTop: 120,
        width: 800,
        closable: closable==undefined?true:closable,
        title: '批量操作',
        url: url + '&t=' + Math.random(),
        bgColor: '#888'
    });
}
$().ready(function() {
    var cc = $('#cmContainer');
    cc.contextMenu({
        items: [
            { itemText: '复制产品', itemTag: 'first' },
            { itemText: '-' },
            { itemText: '批量发布', itemTag: 'second' },
            { itemText: '产品停用', itemTag: 'third' },
            { itemText: '-' },
            { itemText: '栏目批量更新', itemTag: 'fourth' },
            { itemText: '-' },
            { itemCss: 'batchDelete', itemText: '产品批量删除', itemTag: 'fifth' }
        ],
        onItemClick: function(sender, e) {
            var ids = cc.find('input[name=chkSelProduct][checked]')
                .map(function() { return this.value; })
                    .get()
                    .join(',');
            if (ids == '') {
                showMsg({ msg: '当前操作不能完成，请至少选择一个产品！', top: 150, bgColor: '#888' });
                return;
            }
            switch (sender.getAttribute('itemTag')) {
                case 'first':
                    showBatch('productmodal/batchcopy.aspx?ids=' + ids);
                    break;
                case 'second':
                    showBatch('productmodal/batchpublish.aspx?ids=' + ids);
                    break;
                case 'third':
                    showBatch('productmodal/batchdelete.aspx?ids=' + ids);
                    break;
                case 'fourth':
                    showBatch('productmodal/batchsetfocus.aspx?ids=' + ids,false);
                    break;
                case 'fifth':
                    showBatch('productmodal/batchtruncate.aspx?ids=' + ids);
                    break;
            }
        }
    });

    $('#chkSelAll').click(function() {
        cc.find('input[name=chkSelProduct]').attr('checked', this.checked);
    })
});