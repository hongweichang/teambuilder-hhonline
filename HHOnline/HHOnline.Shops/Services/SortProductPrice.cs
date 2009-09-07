using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Framework;

namespace HHOnline.Shops
{
    public class SortProductByPrice : IComparer<Product>
    {
        public SortProductByPrice(SortOrder _sortOrder, int _userID, bool _isAuthencated)
        {
            sortOrder = _sortOrder;
            userID = _userID;
            isAuthencated = _isAuthencated;
        }
        #region -Members-
        private SortOrder sortOrder;
        public SortOrder SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }
        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private bool isAuthencated;
        public bool IsAuthencated
        {
            get { return isAuthencated; }
            set { isAuthencated = value; }
        }
        #endregion

        #region IComparer<Product> -Member-
        public int Compare(Product x, Product y)
        {
            decimal? price1 = 0;
            decimal? price2 = 0;


            decimal? price11 = 0;
            decimal? price12 = 0;
            decimal? price21 = 0;
            decimal? price22 = 0;
            if (isAuthencated)
            {
                price11 = ProductPrices.GetPriceMarket(userID, x.ProductID);
                price12 = ProductPrices.GetPriceMember(userID, x.ProductID);
                if (price11 == null)
                {
                    price1 = price12;
                }
                else
                {
                    if (price12 == null)
                        price1 = price11;
                    else
                    {
                        price1 = (price11 > price12 ? price12 : price11);
                    }
                }
                price21 = ProductPrices.GetPriceMarket(userID, y.ProductID);
                price22 = ProductPrices.GetPriceMember(userID, y.ProductID);
                if (price21 == null)
                {
                    price2 = price22;
                }
                else
                {
                    if (price22 == null)
                        price2 = price21;
                    else
                    {
                        price2 = (price21 > price22 ? price22 : price21);
                    }
                }
            }
            else
            {
                price1 = ProductPrices.GetPriceDefault(x.ProductID);
                price2 = ProductPrices.GetPriceDefault(y.ProductID);
            }
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    if (price1 == null)
                    {
                        if (price2 != null) return -1;
                        else return 0;
                    }
                    else
                    {
                        if (price2 == null) return 1;
                        else
                        {
                            if (price1 > price2) return 1;
                            else if (price1 == price2) return 0;
                            else return -1;
                        }
                    }
                case SortOrder.Descending:
                    if (price1 == null)
                    {
                        if (price2 != null) return 1;
                        else return 0;
                    }
                    else
                    {
                        if (price2 == null) return -1;
                        else
                        {
                            if (price1 > price2) return -1;
                            else if (price1 == price2) return 0;
                            else return 1;
                        }
                    }
            }
            return 0;
        }

        #endregion
    }
}
