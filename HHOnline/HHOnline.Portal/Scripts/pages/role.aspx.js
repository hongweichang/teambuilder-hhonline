﻿$().ready(function() {
    $('a.view[rel=showdetails]').click(function() {
        var id = $(this).attr('href').replace(/javascript\:void\((\d+)\)/g, '$1');
        showPage({
            marginTop: 100,
            closable:true,
            title:'角色详细信息',
            url:'Permission/RoleDetail.aspx?ID=' + id + '&t=' + Math.random()
        });
    });
});