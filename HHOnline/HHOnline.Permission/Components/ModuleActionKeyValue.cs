using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Permission.Components
{
    /// <summary>
    /// 由[ModuleName,ActionName]组成的键值对
    /// </summary>
    [Serializable]
    public class ModuleActionKeyValue
    {
        /// <summary>
        /// 创建<see cref="HHOnline.Permission.Components.ModuleActionKeyValue"/>的新实例
        /// </summary>
        public ModuleActionKeyValue() { }
        /// <summary>
        /// 创建<see cref="HHOnline.Permission.Components.ModuleActionKeyValue"/>的新实例
        /// </summary>
        /// <param name="_ModuleName"></param>
        /// <param name="_ActionName"></param>
        public ModuleActionKeyValue(string _ModuleName, string _ActionName)
        {
            this._ModuleName = _ModuleName;
            this._ActionName = _ActionName;
        }
        private string _ModuleName;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }
        private string _ActionName;
        /// <summary>
        /// 操作名称
        /// </summary>
        public string ActionName
        {
            get { return _ActionName; }
            set { _ActionName = value; }
        }
    }
}
