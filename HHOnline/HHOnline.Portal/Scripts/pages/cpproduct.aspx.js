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