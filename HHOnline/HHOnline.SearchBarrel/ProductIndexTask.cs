using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using HHOnline.Task;
using HHOnline.Framework;

namespace HHOnline.SearchBarrel
{
    public class ProductIndexTask : ITask
    {
        /*
        /// <summary>
        /// 每次更新索引的最大数目
        /// </summary>
        private int count = 1000;
         * */

        public void Execute(System.Xml.XmlNode node)
        {
            /*
            XmlAttribute countNode = node.Attributes["count"];
            if (countNode != null)
            {
                try
                {
                    count = int.Parse(countNode.Value);
                }
                catch
                {
                    count = 1000;
                }
            }
             */
            new HHException(ExceptionType.UnknownError, "ProductIndexTask Start").Log();
            ProductSearchManager.InitializeIndex();
            new HHException(ExceptionType.UnknownError, "ProductIndexTask End").Log();
        }
    }
}
