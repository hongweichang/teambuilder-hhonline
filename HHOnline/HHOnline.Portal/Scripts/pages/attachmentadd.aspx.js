function CheckAttachmentType() {
    var sel = $('#tdTypeSelector').find('select')[0];
    document.getElementById('divLocalAttachment').style.display = sel.selectedIndex == 1 ? 'none' : 'block';
    document.getElementById('divServerAttachment').style.display = sel.selectedIndex == 1 ? 'block' : 'none';
}
