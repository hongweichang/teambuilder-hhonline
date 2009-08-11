using System;

namespace HHOnline.Framework
{
    public class Area
    {
        #region --Private Members--
        private int _regionID;
        private AreaType _regionType = AreaType.DistinctArea;
        private ComponentStatus _regionStatus = ComponentStatus.Enabled;
        private string _regionCode;
        private string _districtCode;
        private string _regionName;
        private string _regionDesc;
        private string _regionMemo;

        private DateTime _createTime;
        private DateTime _updateTime;
        #endregion

        #region --Constructor--
        public Area()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///区域编号
        ///</summary>
        public int RegionID
        {
            get { return _regionID; }
            set { _regionID = value; }
        }

        ///<summary>
        ///区域类型，1行政区域（省份）、2管理区域（大区）
        ///</summary>
        public AreaType RegionType
        {
            get { return _regionType; }
            set { _regionType = value; }
        }

        ///<summary>
        ///区域编码
        ///</summary>
        public string RegionCode
        {
            get { return _regionCode; }
            set { _regionCode = value; }
        }

        ///<summary>
        ///地市区编码（为空表示区域、非空表示地市区）
        ///</summary>
        public string DistrictCode
        {
            get { return _districtCode; }
            set { _districtCode = value; }
        }

        ///<summary>
        ///区域名称
        ///</summary>
        public string RegionName
        {
            get { return _regionName; }
            set { _regionName = value; }
        }

        ///<summary>
        ///区域描述
        ///</summary>
        public string RegionDesc
        {
            get { return _regionDesc; }
            set { _regionDesc = value; }
        }

        ///<summary>
        ///区域备注
        ///</summary>
        public string RegionMemo
        {
            get { return _regionMemo; }
            set { _regionMemo = value; }
        }

        ///<summary>
        ///区域状态，1启用、2停用
        ///</summary>
        public ComponentStatus RegionStatus
        {
            get { return _regionStatus; }
            set { _regionStatus = value; }
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
        ///最后更新时间
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }
        #endregion
    }
}
