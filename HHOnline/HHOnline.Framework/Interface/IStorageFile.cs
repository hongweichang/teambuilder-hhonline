using System;
using System.IO;

namespace HHOnline.Framework
{
    /// <summary>
    /// 服务器端文件接口
    /// </summary>
    public interface IStorageFile
    {
        /// <summary>
        ///文件大小
        /// </summary>
        int ContentLength { get; }

        /// <summary>
        /// 文件名称
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// 文件路径
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// 文件存储Key
        /// </summary>
        string FileStoreKey { get; }

        /// <summary>
        /// 读取文件流
        /// </summary>
        /// <returns></returns>
        Stream OpenReadStream();

        /// <summary>
        /// 获取客户端下载路径
        /// </summary>
        /// <returns></returns>
        string GetDownloadUrl();


    }
}
