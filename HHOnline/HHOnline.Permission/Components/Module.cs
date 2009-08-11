using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Permission.Components
{
    /// <summary>
    /// 功能模块
    /// </summary>
    [Serializable]
    public class Module
    {
        #region -Constructor-
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Module"/>的实例
        /// </summary>
        public Module()
            : this(0, string.Empty, string.Empty, string.Empty)
        { }
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Module"/>的实例
        /// </summary>
        /// <param name="_ModuleShortName"></param>
        /// <param name="_ModuleName"></param>
        /// <param name="_Description"></param>
        public Module(string _ModuleShortName,string _ModuleName, string _Description)
            : this(0,_ModuleShortName, _ModuleName, _Description)
        { }
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Module"/>的实例
        /// </summary>
        /// <param name="_ModuleID"></param>
        /// <param name="_ModuleShortName"></param>
        /// <param name="_ModuleName"></param>
        /// <param name="_Description"></param>
        public Module(int _ModuleID,string _ModuleShortName, string _ModuleName, string _Description)
        {
            this._ModuleShortName = _ModuleShortName;
            this._ModuleID = _ModuleID;
            this._ModuleName = _ModuleName;
            this._Description = _Description;
        }
        #endregion

        #region -Properties-
        private string _ModuleShortName;
        /// <summary>
        /// 模块简称
        /// </summary>
        public string ModuleShortName
        {
            get { return _ModuleShortName; }
            set { _ModuleShortName = value; }
        }
        private int _ModuleID;
        /// <summary>
        /// 功能模块ID
        /// </summary>
        public int ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        private string _ModuleName;
        /// <summary>
        /// 功能模块名称
        /// </summary>
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }
        private string _Description;
        /// <summary>
        /// 功能模块描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        #endregion
    }
}
