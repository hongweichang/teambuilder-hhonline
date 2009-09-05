function cateShow(index) {
    document.getElementById("cate_content_block_" + index).style.display = 'block';
    document.getElementById("cate_item_" + index).className = 'cate_item_hover';
}
function cateHidden(index) {
    document.getElementById("cate_content_block_" + index).style.display = 'none';
    document.getElementById("cate_item_" + index).className = '';
}