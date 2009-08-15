using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace HHOnline.Common
{
    /// <summary>
    /// DateRecord Help Class
    /// </summary>
    public static class DataRecordHelper
    {
        #region GetBoolean
        public static bool GetBoolean(IDataRecord dr, int ordinal)
        {
            return dr.GetBoolean(ordinal);
        }

        public static bool GetBoolean(IDataRecord dr, string name)
        {
            return GetBoolean(dr, dr.GetOrdinal(name));
        }

        public static bool GetBoolean(IDataRecord dr, int ordinal, bool defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetBoolean(ordinal);
            }
            return defaultValue;
        }

        public static bool GetBoolean(IDataRecord dr, string name, bool defaultValue)
        {
            return GetBoolean(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetByte
        public static byte GetByte(IDataRecord dr, int ordinal)
        {
            return GetByte(dr, ordinal, 0);
        }

        public static byte GetByte(IDataRecord dr, string name)
        {
            return GetByte(dr, dr.GetOrdinal(name));
        }

        public static byte GetByte(IDataRecord dr, int ordinal, byte defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetByte(ordinal);
            }
            return defaultValue;
        }

        public static byte GetByte(IDataRecord dr, string name, byte defaultValue)
        {
            return GetByte(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetByteArray
        public static byte[] GetByteArray(IDataRecord dr, int ordinal)
        {
            int length = Convert.ToInt32(dr.GetBytes(ordinal, 0L, null, 0, 0));
            byte[] buffer = new byte[length];
            dr.GetBytes(ordinal, 0L, buffer, 0, length);
            return buffer;
        }

        public static byte[] GetByteArray(IDataRecord dr, string name)
        {
            return GetByteArray(dr, dr.GetOrdinal(name));
        }

        public static byte[] GetByteArray(IDataRecord dr, int ordinal, byte[] defaultValue)
        {
            if (dr.IsDBNull(ordinal))
            {
                return defaultValue;
            }
            int length = Convert.ToInt32(dr.GetBytes(ordinal, 0L, null, 0, 0));
            byte[] buffer = new byte[length];
            dr.GetBytes(ordinal, 0L, buffer, 0, length);
            return buffer;
        }

        public static byte[] GetByteArray(IDataRecord dr, string name, byte[] defaultValue)
        {
            return GetByteArray(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetChar
        public static char GetChar(IDataRecord dr, int ordinal)
        {
            return GetChar(dr, ordinal, ' ');
        }

        public static char GetChar(IDataRecord dr, string name)
        {
            return GetChar(dr, dr.GetOrdinal(name));
        }

        public static char GetChar(IDataRecord dr, int ordinal, char defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetChar(ordinal);
            }
            return defaultValue;
        }

        public static char GetChar(IDataRecord dr, string name, char defaultValue)
        {
            return GetChar(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetDateTime
        public static DateTime GetDateTime(IDataRecord dr, int ordinal)
        {
            return GetDateTime(dr, ordinal, new DateTime(2000, 1, 1));
        }

        public static DateTime GetDateTime(IDataRecord dr, string name)
        {
            return GetDateTime(dr, dr.GetOrdinal(name));
        }

        public static DateTime GetDateTime(IDataRecord dr, int ordinal, DateTime defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetDateTime(ordinal);
            }
            return defaultValue;
        }

        public static DateTime GetDateTime(IDataRecord dr, string name, DateTime defaultValue)
        {
            return GetDateTime(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetDecimal
        public static decimal? GetNullableDecimal(IDataRecord dr, int ordinal)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetDecimal(ordinal);
            }
            return null;
        }

        public static decimal? GetNullableDecimal(IDataRecord dr, string name)
        {
            return GetDecimal(dr, dr.GetOrdinal(name));
        }

        public static decimal GetDecimal(IDataRecord dr, int ordinal)
        {
            return GetDecimal(dr, ordinal, 0);
        }

        public static decimal GetDecimal(IDataRecord dr, string name)
        {
            return GetDecimal(dr, dr.GetOrdinal(name));
        }

        public static decimal GetDecimal(IDataRecord dr, int ordinal, decimal defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetDecimal(ordinal);
            }
            return defaultValue;
        }

        public static decimal GetDecimal(IDataRecord dr, string name, decimal defaultValue)
        {
            return GetDecimal(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetDouble
        public static double GetDouble(IDataRecord dr, int ordinal)
        {
            return GetDouble(dr, ordinal, 0);
        }

        public static double GetDouble(IDataRecord dr, string name)
        {
            return GetDouble(dr, dr.GetOrdinal(name));
        }

        public static double GetDouble(IDataRecord dr, int ordinal, double defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetDouble(ordinal);
            }
            return defaultValue;
        }

        public static double GetDouble(IDataRecord dr, string name, double defaultValue)
        {
            return GetDouble(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetFloat
        public static float GetFloat(IDataRecord dr, int ordinal)
        {
            return GetFloat(dr, ordinal, 0);
        }

        public static float GetFloat(IDataRecord dr, string name)
        {
            return GetFloat(dr, dr.GetOrdinal(name));
        }

        public static float GetFloat(IDataRecord dr, int ordinal, float defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetFloat(ordinal);
            }
            return defaultValue;
        }

        public static float GetFloat(IDataRecord dr, string name, float defaultValue)
        {
            return GetFloat(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetGuid
        public static Guid GetGuid(IDataRecord dr, int ordinal)
        {
            return GetGuid(dr, ordinal, new Guid());
        }

        public static Guid GetGuid(IDataRecord dr, string name)
        {
            return GetGuid(dr, dr.GetOrdinal(name));
        }

        public static Guid GetGuid(IDataRecord dr, int ordinal, Guid defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetGuid(ordinal);
            }
            return defaultValue;
        }

        public static Guid GetGuid(IDataRecord dr, string name, Guid defaultValue)
        {
            return GetGuid(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetInt16
        public static short GetInt16(IDataRecord dr, int ordinal)
        {
            return GetInt16(dr, ordinal, 0);
        }

        public static short GetInt16(IDataRecord dr, string name)
        {
            return GetInt16(dr, dr.GetOrdinal(name));
        }

        public static short GetInt16(IDataRecord dr, int ordinal, short defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetInt16(ordinal);
            }
            return defaultValue;
        }

        public static short GetInt16(IDataRecord dr, string name, short defaultValue)
        {
            return GetInt16(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetInt32
        public static int? GetNullableInt32(IDataRecord dr, int ordinal)
        {
            if (dr.IsDBNull(ordinal))
                return null;
            else
                return dr.GetInt32(ordinal);
        }

        public static int? GetNullableInt32(IDataRecord dr, string name)
        {
            int ordinal = dr.GetOrdinal(name);
            if (ordinal >= 0)
                return GetNullableInt32(dr, ordinal);
            else
                return null;
        }

        public static int GetInt32(IDataRecord dr, int ordinal)
        {
            return GetInt32(dr, ordinal, 0);
        }

        public static int GetInt32(IDataRecord dr, string name)
        {
            return GetInt32(dr, dr.GetOrdinal(name));
        }

        public static int GetInt32(IDataRecord dr, int ordinal, int defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetInt32(ordinal);
            }
            return defaultValue;
        }

        public static int GetInt32(IDataRecord dr, string name, int defaultValue)
        {
            return GetInt32(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetInt64
        public static long GetInt64(IDataRecord dr, int ordinal)
        {
            return GetInt64(dr, ordinal, 0);
        }

        public static long GetInt64(IDataRecord dr, string name)
        {
            return GetInt64(dr, dr.GetOrdinal(name));
        }

        public static long GetInt64(IDataRecord dr, int ordinal, long defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetInt64(ordinal);
            }
            return defaultValue;
        }

        public static long GetInt64(IDataRecord dr, string name, long defaultValue)
        {
            return GetInt64(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion

        #region GetString
        public static string GetString(IDataRecord dr, int ordinal)
        {
            return GetString(dr, ordinal, string.Empty);
        }

        public static string GetString(IDataRecord dr, string name)
        {
            return GetString(dr, dr.GetOrdinal(name));
        }

        public static string GetString(IDataRecord dr, int ordinal, string defaultValue)
        {
            if (!dr.IsDBNull(ordinal))
            {
                return dr.GetString(ordinal);
            }
            return defaultValue;
        }

        public static string GetString(IDataRecord dr, string name, string defaultValue)
        {
            return GetString(dr, dr.GetOrdinal(name), defaultValue);
        }
        #endregion
    }
}
