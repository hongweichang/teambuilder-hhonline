/// <reference path="../jquery-vsdoc.js" />
function addSearchWord() {
    showPage({
        marginTop: 120,
        width: 500,
        title: '新增搜索关键词',
        url: 'site/searchwordadd.aspx?t=' + Math.random(),
        bgColor: '#888'
    });
    return false;
}