using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 组织机构
    /// </summary>
    [Serializable]
    public class Organization : ExtendedAttributes
    {
        #region --Private Members--
        private int _organizationID;
        private string _organizationName;
        private string _organizationDesc;
        private string _organizationMemo;
        private int _parentID;
        private int _displayOrder;
        private ComponentStatus _organizationStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public Organization()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///组织机构编号
        ///</summary>
        public int OrganizationID
        {
            get { return _organizationID; }
            set { _organizationID = value; }
        }

        ///<summary>
        ///组织机构名称
        ///</summary>
        public string OrganizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

        ///<summary>
        ///组织机构描述
        ///</summary>
        public string OrganizationDesc
        {
            get { return _organizationDesc; }
            set { _organizationDesc = value; }
        }

        ///<summary>
        ///组织机构备注
        ///</summary>
        public string OrganizationMemo
        {
            get { return _organizationMemo; }
            set { _organizationMemo = value; }
        }

        ///<summary>
        ///上级组织机构编号
        ///</summary>
        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        ///<summary>
        ///组织显示顺序（从1到n排序
        ///</summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        ///<summary>
        ///组织状态，1正常、2停用
        ///</summary>
        public ComponentStatus OrganizationStatus
        {
            get { return _organizationStatus; }
            set { _organizationStatus = value; }
        }

        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        ///<summary>
        ///创建操作人
        ///</summary>
        public int CreateUser
        {
            get { return _createUser; }
            set { _createUser = value; }
        }

        ///<summary>
        ///最后更新时间
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        ///<summary>
        ///最后更新操作人
        ///</summary>
        public int UpdateUser
        {
            get { return _updateUser; }
            set { _updateUser = value; }
        }
        #endregion
    }


}
