/// <reference path="../jquery-vsdoc.js" />
$().ready(function() {
    $('a.edit[rel=editcompany]').click(function() {
        var inputs = $(this).parent().find('input[type=hidden]');
        var comid = inputs.filter('.first').val();
        var pid = inputs.filter('.second').val();
        showPage({
            marginTop: 20,
            width: 800,
            title: '审批公司类型更改申请',
            url: 'Users/CompanyPendingEdit.aspx?CompanyID=' + comid + '&PendingID=' + pid + '&t=' + Math.random(),
            bgColor: '#888'
        });
    });
});