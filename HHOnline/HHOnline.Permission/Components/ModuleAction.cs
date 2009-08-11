using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Permission.Components
{
    /// <summary>
    /// Module与Action关联关系
    /// </summary>
    [Serializable]
    public class ModuleAction
    {
        #region -Constructor-
        /// <summary>
        /// 创建<see cref="HHOnline.Permission.Components.ModuleAction"/>实例
        /// </summary>
        public ModuleAction()
        { }
        /// <summary>
        /// 创建<see cref="HHOnline.Permission.Components.ModuleAction"/>实例
        /// </summary>
        /// <param name="_ModuleId"></param>
        /// <param name="_ActionId"></param>
        /// <param name="_ModuleActionName"></param>
        /// <param name="_Description"></param>
        public ModuleAction(int _ModuleId, int _ActionId, string _ModuleActionName, string _Description)
            : this(0, _ModuleId, _ActionId, _ModuleActionName, _Description)
        { }
        /// <summary>
        ///  创建<see cref="HHOnline.Permission.Components.ModuleAction"/>实例
        /// </summary>
        /// <param name="_ModuleActionId"></param>
        /// <param name="_ModuleId"></param>
        /// <param name="_ActionId"></param>
        /// <param name="_ModuleActionName"></param>
        /// <param name="_Description"></param>
        public ModuleAction(int _ModuleActionId,int _ModuleId,int _ActionId,string _ModuleActionName,string _Description) {
            this._ModuleActionId = _ModuleActionId;
            this._ModuleId=_ModuleId;
            this._ActionId = _ActionId;
            this._ModuleActionName = _ModuleActionName;
            this._Description = _Description;
        }

        #endregion

        #region -Properties-
        private int _ModuleActionId;
        /// <summary>
        /// Module Action ID
        /// </summary>
        public int ModuleActionId
        {
            get { return _ModuleActionId; }
            set { _ModuleActionId = value; }
        }
        private int _ModuleId;
        /// <summary>
        /// Module ID
        /// </summary>
        public int ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }
        private int _ActionId;
        /// <summary>
        /// Action ID
        /// </summary>
        public int ActionId
        {
            get { return _ActionId; }
            set { _ActionId = value; }
        }
        private string _ModuleActionName;
        /// <summary>
        /// Module Action名称
        /// </summary>
        public string ModuleActionName
        {
            get { return _ModuleActionName; }
            set { _ModuleActionName = value; }
        }
        private string _Description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        #endregion
    }
}
