/// <reference path="../jquery-vsdoc.js" />
$().ready(function() {
    $('a.edit[rel=editcompany]').click(function() {
        var id = $(this).parent().find('input[type=hidden]').val();
        showPage({
            marginTop: 20,
            width: 800,
            title: '编辑公司信息',
            url: 'Users/CompanyEdit.aspx?ID=' + id + '&t=' + Math.random(),
            bgColor: '#888'
        });
    });
});