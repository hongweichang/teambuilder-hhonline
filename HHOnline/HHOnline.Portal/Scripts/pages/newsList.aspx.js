function cateShow(index) {
	var cateItem = document.getElementById("cate_item_" + index);
	var contentItem = document.getElementById("cate_content_block_" + index);

	if (contentItem)
	{
		contentItem.style.display = 'block';
		contentItem.style.top = cateItem.offsetTop + 'px';
		cateItem.className = 'cate_item_hover';
    }
}
function cateHidden(index) {
	var cateItem = document.getElementById("cate_item_" + index);
	var contentItem = document.getElementById("cate_content_block_" + index);

	if (contentItem)
	{
		contentItem.style.display = 'none';
		cateItem.className = '';
    }
}