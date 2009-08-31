using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 客户级别（企业客户）
    /// </summary>
    public class CustomerGrade
    {
        #region --Private Members--
        private int _gradeID;
        private int _companyID;
        private UserLevel _gradeLevel = UserLevel.E;
        private string _gradeLimit;
        private string _gradeName;
        private string _gradeDesc;
        private string _gradeMemo;
        private ComponentStatus _gradeStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public CustomerGrade()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///等级编号
        ///</summary>
        public int GradeID
        {
            get { return _gradeID; }
            set { _gradeID = value; }
        }

        ///<summary>
        ///客户编号
        ///</summary>
        public int CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }

        ///<summary>
        ///等级数值，1到5级对应E到A级
        ///</summary>
        public UserLevel GradeLevel
        {
            get { return _gradeLevel; }
            set { _gradeLevel = value; }
        }

        ///<summary>
        ///等级约束（数据范围约定）
        ///</summary>
        public string GradeLimit
        {
            get { return _gradeLimit; }
            set { _gradeLimit = value; }
        }

        ///<summary>
        ///等级名称
        ///</summary>
        public string GradeName
        {
            get { return _gradeName; }
            set { _gradeName = value; }
        }

        ///<summary>
        ///等级描述
        ///</summary>
        public string GradeDesc
        {
            get { return _gradeDesc; }
            set { _gradeDesc = value; }
        }

        ///<summary>
        ///等级备注
        ///</summary>
        public string GradeMemo
        {
            get { return _gradeMemo; }
            set { _gradeMemo = value; }
        }

        ///<summary>
        ///等级状态，1启用、2停用
        ///</summary>
        public ComponentStatus GradeStatus
        {
            get { return _gradeStatus; }
            set { _gradeStatus = value; }
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
