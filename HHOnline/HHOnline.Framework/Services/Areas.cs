using System;
using System.Collections.Generic;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 地区管理
    /// </summary>
    public class Areas
    {
        /// <summary>
        /// 获取所有地区信息
        /// </summary>
        /// <returns></returns>
        public static List<Area> GetAllAreas()
        {
            List<Area> areas = null;
            string cacheKey = CacheKeyManager.AreasKey;
            if (HttpContext.Current != null)
                areas = HttpContext.Current.Items[cacheKey] as List<Area>;

            if (areas != null)
                return areas;

            areas = HHCache.Instance.Get(cacheKey) as List<Area>;
            if (areas == null)
            {
                areas = CommonDataProvider.Instance.GetAreas();
                HHCache.Instance.Max(cacheKey, areas);
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[cacheKey] = areas;
                }
            }
            return areas;
        }

        /// <summary>
        /// 根据地区ID获取子地区
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static List<Area> GetChildAreas(int areaID)
        {
            List<Area> child = new List<Area>();
            Area area = GetArea(areaID);
            if (area != null)
            {
                if (GlobalSettings.IsNullOrEmpty(area.DistrictCode))
                {
                    foreach (Area a in GetAllAreas())
                    {
                        if (a.RegionCode == area.RegionCode && !GlobalSettings.IsNullOrEmpty(a.DistrictCode))
                        {
                            child.Add(a);
                        }
                    }
                }
                else if (area.RegionType == AreaType.TerritoryArea)
                {
                    foreach (Area a in GetAllAreas())
                    {
                        if (a.RegionCode == area.DistrictCode && !GlobalSettings.IsNullOrEmpty(a.DistrictCode))
                        {
                            child.Add(a);
                        }
                    }
                }
            }
            return child;
        }

        /// <summary>
        /// 根据地区ID获取所有子地区(管理区域下所有地区）
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static List<Area> GetAllChildAreas(int areaID)
        {
            List<Area> childs = GetChildAreas(areaID);
            List<Area> allChilds = new List<Area>();
            allChilds.AddRange(childs);
            foreach (Area area in childs)
            {
                if (area.RegionType == AreaType.TerritoryArea)
                {
                    Area a = GetArea(area.DistrictCode);
                    allChilds.AddRange(GetChildAreas(a.RegionID));
                }
            }
            return allChilds;
        }

        /// <summary>
        /// 根据地区类型获取大区/省份
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Area> GetAreasByType(AreaType type)
        {
            List<Area> areas = new List<Area>();
            foreach (Area area in GetAllAreas())
            {
                if (area.RegionType == type && GlobalSettings.IsNullOrEmpty(area.DistrictCode))
                    areas.Add(area);
            }
            return areas;
        }

        /// <summary>
        /// 获取行政省份
        /// </summary>
        /// <returns></returns>
        public static List<Area> GetDistinctAreas()
        {
            return GetAreasByType(AreaType.DistinctArea);
        }

        /// <summary>
        /// 获取管理大区
        /// </summary>
        /// <returns></returns>
        public static List<Area> GetTerritoryAreas()
        {
            return GetAreasByType(AreaType.TerritoryArea);
        }

        /// <summary>
        /// 根据AreaID获取Area
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static Area GetArea(int areaID)
        {
            Area area = null;
            string cacheKey = CacheKeyManager.GetAreaKey(areaID);
            area = HHCache.Instance.Get(cacheKey) as Area;

            if (area == null)
            {
                foreach (Area a in GetAllAreas())
                {
                    if (a.RegionID == areaID)
                    {
                        area = a;
                        break;
                    }
                }
            }

            if (area != null)
                HHCache.Instance.Max(cacheKey, area);
            return area;
        }

        /// <summary>
        /// 根据DistrictCode获取Area
        /// </summary>
        /// <param name="distinctCode"></param>
        /// <returns></returns>
        public static Area GetArea(string distinctCode)
        {
            Area area = null;
            string cacheKey = CacheKeyManager.GetAreaKey(distinctCode);
            area = HHCache.Instance.Get(cacheKey) as Area;

            if (area == null)
            {
                foreach (Area a in GetAllAreas())
                {
                    if (a.DistrictCode == distinctCode ||
                        (GlobalSettings.IsNullOrEmpty(a.DistrictCode) && a.RegionCode == distinctCode))
                    {
                        area = a;
                        break;
                    }
                }
            }

            if (area != null)
                HHCache.Instance.Max(cacheKey, area);
            return area;
        }

        /// <summary>
        /// 获取地区下拉框值
        /// </summary>
        /// <returns></returns>
        public static List<KeyValue> GetValueRange()
        {
            List<Area> areaList = Areas.GetTerritoryAreas();
            List<KeyValue> valueRange = new List<KeyValue>();
            foreach (Area area in areaList)
            {
                valueRange.Add(new KeyValue(area.RegionID.ToString(), area.RegionName));
                AddChildRegion(area, 0, ref valueRange);
            }
            return valueRange;
        }

        private static void AddChildRegion(Area parent, int deps, ref List<KeyValue> valueRange)
        {
            string block = "┗";
            for (int i = 0; i < deps; i++)
            {
                block = "　" + block;
            }
            foreach (Area chid in Areas.GetChildAreas(parent.RegionID))
            {
                valueRange.Add(new KeyValue(chid.RegionID.ToString(), block + chid.RegionName));
                AddChildRegion(chid, deps + 1, ref valueRange);
            }
        }
    }
}
