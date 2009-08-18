function CheckAttachmentType() {
    document.getElementById('divLocalAttachment').style.display = this.selectedIndex == 1 ? 'none' : 'block'; 
    document.getElementById('divServerAttachment').style.display = this.selectedIndex == 1 ? 'block' : 'none';
}
