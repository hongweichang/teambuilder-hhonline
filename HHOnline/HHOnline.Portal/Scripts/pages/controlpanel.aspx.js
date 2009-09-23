function resolvePendingCompany(nav) {
    $.fn.cookie({ action: 'set', name: 'hhonline_menu', value: 'item_companyuser' });
    window.location.href = nav;
}