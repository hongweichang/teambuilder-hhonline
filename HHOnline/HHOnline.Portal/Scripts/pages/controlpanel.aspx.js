function resolvePendingCompany(nav) {
    $.fn.cookie({ action: 'set', name: 'hhonline_menu', value: 'item_companyuser' });
    window.location.href = nav;
}
function resolvePending(nav) {
    $.fn.cookie({ action: 'set', name: 'hhonline_menu', value: 'item_companyuserpend' });
    window.location.href = nav;
}