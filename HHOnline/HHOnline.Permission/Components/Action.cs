using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Permission.Components
{
    /// <summary>
    /// 操作功能
    /// </summary>
    [Serializable]
    public class Action
    {
        #region -Constructor-
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Action"/>的实例
        /// </summary>
        public Action()
            : this(0, string.Empty, string.Empty)
        { }
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Action"/>的实例
        /// </summary>
        /// <param name="_ActionName"></param>
        /// <param name="_Description"></param>
        public Action(string _ActionName, string _Description)
            : this(0, _ActionName, _Description)
        { }
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Action"/>的实例
        /// </summary>
        /// <param name="_ActionID"></param>
        /// <param name="_ActionName"></param>
        /// <param name="_Description"></param>
        public Action(int _ActionID, string _ActionName, string _Description)
        {
            this._ActionID = _ActionID;
            this._ActionName = _ActionName;
            this._Description = _Description;
        }
        #endregion

        #region -Properties-
        private int _ActionID;
        /// <summary>
        /// 功能ID
        /// </summary>
        public int ActionID
        {
            get { return _ActionID; }
            set { _ActionID = value; }
        }
        private string _ActionName;
        /// <summary>
        /// 功能名称
        /// </summary>
        public string ActionName
        {
            get { return _ActionName; }
            set { _ActionName = value; }
        }
        private string _Description;
        /// <summary>
        /// 功能描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        #endregion
    }
}
