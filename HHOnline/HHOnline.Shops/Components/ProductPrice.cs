using System;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    /// <summary>
    /// 产品价格
    /// </summary>
    public class ProductPrice : ExtendedAttributes
    {
        #region --Private Members--
        private int _priceID;
        private int _productID;
        private int _modelID;
        private string _deliverySpan;
        private string _warrantySpan;
        private PriceIncludeType _includeFreight = PriceIncludeType.Include;
        private PriceIncludeType _includeTax = PriceIncludeType.Include;
        private decimal _applyTaxRate;
        private int _quoteMOQ;
        private decimal _priceMarket;
        private decimal _pricePromotion;
        private decimal _priceFloor;
        private decimal _pricePurchase;
        private decimal _priceGradeA;
        private decimal _priceGradeB;
        private decimal _priceGradeC;
        private decimal _priceGradeD;
        private decimal _priceGradeE;
        private int _supplyRegion;
        private DateTime _quoteFrom;
        private DateTime _quoteEnd;
        private int _quoteRenewal = 1;
        private ComponentStatus _supplyStatus = ComponentStatus.Enabled;
        private DateTime _createTime;
        private int _createUser;
        private DateTime _updateTime;
        private int _updateUser;
        #endregion

        #region --Constructor--
        public ProductPrice()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///报价编号
        ///</summary>
        public int PriceID
        {
            get { return _priceID; }
            set { _priceID = value; }
        }

        ///<summary>
        ///产品编号
        ///</summary>
        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        ///<summary>
        ///产品型号编号
        ///</summary>
        public int ModelID
        {
            get { return _modelID; }
            set { _modelID = value; }
        }

        ///<summary>
        ///最短供货时间（时长格式）
        ///</summary>
        public string DeliverySpan
        {
            get { return _deliverySpan; }
            set { _deliverySpan = value; }
        }

        ///<summary>
        ///产品保修期（时长格式）
        ///</summary>
        public string WarrantySpan
        {
            get { return _warrantySpan; }
            set { _warrantySpan = value; }
        }

        ///<summary>
        ///是否包含运费，1包含、2不包含
        ///</summary>
        public PriceIncludeType IncludeFreight
        {
            get { return _includeFreight; }
            set { _includeFreight = value; }
        }

        ///<summary>
        ///是否包含税，1包含、2不包含
        ///</summary>
        public PriceIncludeType IncludeTax
        {
            get { return _includeTax; }
            set { _includeTax = value; }
        }

        ///<summary>
        ///供货税率（显示为百分数）
        ///</summary>
        public decimal ApplyTaxRate
        {
            get { return _applyTaxRate; }
            set { _applyTaxRate = value; }
        }

        ///<summary>
        ///最小订货量
        ///</summary>
        public int QuoteMOQ
        {
            get { return _quoteMOQ; }
            set { _quoteMOQ = value; }
        }

        ///<summary>
        ///市场价格
        ///</summary>
        public decimal PriceMarket
        {
            get { return _priceMarket; }
            set { _priceMarket = value; }
        }

        ///<summary>
        ///促销价格（为0时表示赠送）
        ///</summary>
        public decimal PricePromotion
        {
            get { return _pricePromotion; }
            set { _pricePromotion = value; }
        }

        ///<summary>
        ///保底价格
        ///</summary>
        public decimal PriceFloor
        {
            get { return _priceFloor; }
            set { _priceFloor = value; }
        }

        ///<summary>
        ///采购价格
        ///</summary>
        public decimal PricePurchase
        {
            get { return _pricePurchase; }
            set { _pricePurchase = value; }
        }

        ///<summary>
        ///五级会员价格
        ///</summary>
        public decimal PriceGradeA
        {
            get { return _priceGradeA; }
            set { _priceGradeA = value; }
        }

        ///<summary>
        ///四级会员价格
        ///</summary>
        public decimal PriceGradeB
        {
            get { return _priceGradeB; }
            set { _priceGradeB = value; }
        }

        ///<summary>
        ///三级会员价格
        ///</summary>
        public decimal PriceGradeC
        {
            get { return _priceGradeC; }
            set { _priceGradeC = value; }
        }

        ///<summary>
        ///二级会员价格
        ///</summary>
        public decimal PriceGradeD
        {
            get { return _priceGradeD; }
            set { _priceGradeD = value; }
        }

        ///<summary>
        ///一级会员价格
        ///</summary>
        public decimal PriceGradeE
        {
            get { return _priceGradeE; }
            set { _priceGradeE = value; }
        }

        ///<summary>
        ///供货区域（报价有效区域，空值表示全国）
        ///</summary>
        public int SupplyRegion
        {
            get { return _supplyRegion; }
            set { _supplyRegion = value; }
        }

        ///<summary>
        ///报价起始日期
        ///</summary>
        public DateTime QuoteFrom
        {
            get { return _quoteFrom; }
            set { _quoteFrom = value; }
        }

        ///<summary>
        ///报价截止日期
        ///</summary>
        public DateTime QuoteEnd
        {
            get { return _quoteEnd; }
            set { _quoteEnd = value; }
        }

        ///<summary>
        ///报价自动续期周期（单位自然月、0表示不延期）
        ///</summary>
        public int QuoteRenewal
        {
            get { return _quoteRenewal; }
            set { _quoteRenewal = value; }
        }

        ///<summary>
        ///供应状态，1启用、2停用
        ///</summary>
        public ComponentStatus SupplyStatus
        {
            get { return _supplyStatus; }
            set { _supplyStatus = value; }
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
