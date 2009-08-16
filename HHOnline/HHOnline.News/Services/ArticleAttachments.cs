using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;

namespace HHOnline.News.Services
{
	public class ArticleAttachments
	{
		public static string FileStoreKey = "ArticleAttachmentPicture";

		//public static IStorageFile GetDefaultPicture(int productID)
		//{
		//    return ShopDataProvider.Instance.GetDefaultPicture(productID);
		//}

		public static string MakePath(int productID)
		{
			return GlobalSettings.MakePath(productID);
		}

	}
}
