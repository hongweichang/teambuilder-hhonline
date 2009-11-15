<%@ Page Language="C#" MasterPageFile="~/Masters/DetailMasterPage.master" AutoEventWireup="true" CodeFile="ProductPictureManager.aspx.cs" Inherits="ControlPanel_product_ProductPictureManager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDetail" runat="Server">
    <script type="text/javascript">
        var t, c, f, h, p, pw = 70;
        var fp;
        $().ready(function() {
            f = $('#ju-foot');
            t = $('#fileOutput');

            $('#divUpload').flash({
                url: flashSrc,
                width: 200,
                height: 30,
                flashVars: {
                    exts: '*.gif;*.png;*.jpg;',
                    fileCount: 20,
                    maxSize: 1 * 1024 * 1024,
                    pushFiles: 'pushFiles',
                    postUrl: (parent.window.first ? 'upload.axd?uid=' + __uid : 'updatepicture.axd?pictureid=' + parent.window.productId),
                    onUploadStart: 'onUploadStart',
                    onFileOpen: 'onFileOpen',
                    onCompleted: 'onCompleted',
                    onProgress: 'onProgress',
                    onHttpError: 'onHttpError',
                    onIOError: 'onIOError',
                    onSecurityError: 'onSecurityError',
                    onSuccess: 'onSuccess',
                    onCancel: 'onCancel',
                    onSizeFlowed: 'onSizeFlowed',
                    onCountFlowed: 'onCountFlowed'
                }
            });
        });
        
        function pushFiles(files) {
            var html = '';
            $.each(eval(files), function(i, n) {
                html += '<tr>' +
                                '<th title="' + n.name + '">' + n.name.cut(30) + '</th>' +
                                '<td class="ju-size">' + getSize(n.size) + '</td>' +
                                '<td class="ju-status ju-wait" title="等待上传。。。">' +
                                    '<div class="ju-pb"><div class="ju-pbvalue"></div></div>' +
                                '</td>' +
                             '</tr>';
            });
            t.html(html);
            c = t.find('td.ju-status');
        }
        function getSize(s) {
            if (s < 1024) {
                return '<1kb';
            }
            else {
                if (s < 1024 * 1024) {
                    return Math.ceil(s / 1024) + ' kb';
                }
                else {
                    return Math.round(s / (1024 * 1024)) + ' M';
                }
            }
        }
        
        function onUploadStart() {
            f.attr('class', 'ju-uploading').html('正在上传。。。');
        }
        function onFileOpen(index) {
            h = c.eq(index).removeClass('ju-wait').find('div').show();
            p = h.find('div.ju-pbvalue');
        }
        function onCompleted(index) {
            c.eq(index).addClass('ju-finished').attr('title','上传成功！').find('div').hide();
        }
        function onProgress(percentage, index) {
            p.width(percentage * pw);
        }
        function onHttpError(code, index) {
            c.eq(index).addClass('ju-warn').attr('title', 'Http错误, Code:' + code + '！').find('div').hide();
        }
        function onIOError(fileName, index) {
            c.eq(index).addClass('ju-warn').attr('title', '读写文件时发生了错误！').find('div').hide();
        }
        function onSecurityError(error, index) {
            c.eq(index).addClass('ju-warn').attr('title', '没有读写操作权限, 详细信息：' + error + '！').find('div').hide();
        }
        function onSuccess(totalBytes) {
            if (totalBytes != 0) {
                f.attr('class', 'ju-foot').html('总共上传文件: ' + getSize(totalBytes));
                parent.window.callServer('refreshBinder');
            }
        }
        function onCancel(fileName, index) {
            c.eq(index).removeClass('ju-wait').addClass('ju-cancel').attr('title','上传操作被取消。').find('div').hide();
            f.attr('class','ju-cancel').html('上传操作已经被取消！ ' );
        }
        function onCountFlowed(fileCount) {
            alert('上传的文件总数超过了' + fileCount + '个');
        }
        function onSizeFlowed(maxSize, index) {
            f.attr('class', 'ju-warn').html('部分上传文件大小超出最大限定，将被自动取消！ ');
        }
        
        
    </script>
    <div style="padding:10px;height:400px">   
        <div id="jerichoUpload" class="jerichoUpload">
            <div class="ju-top">
                <div class="ju-title">上传图片：</div>
                <div class="ju-upload" id="divUpload"></div>
            </div>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <th class="ju-gridhead">已使用</th>
                    <td class="ju-gridhead ju-size">大小</td>
                    <td class="ju-gridhead">状态</td>
                </tr>
            </table>
            <div class="ju-content">
                <table cellpadding="0" cellspacing="0">
                    <tbody id="fileOutput">
                        
                    </tbody>
                </table>
            </div>
            <div id="ju-foot" class="ju-foot">设备已就绪, 请选择需要上传的文件！</div>
        </div>
    </div>
    </asp:Content>
