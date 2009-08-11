using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using HHOnline.Framework;

namespace HHOnline.Data
{
    /// <summary>
    /// 对数据库访问进行封装
    /// </summary>
    public class DataHelper
    {
        private static readonly Database db = DatabaseFactory.CreateDatabase();

        private DataHelper()
        { }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params ELParameter[] commandParameters)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PrepareCommand(db, cmdType, cmdText);
            BuildParameters(db, cmd, commandParameters);
            int value = db.ExecuteNonQuery(cmd);
            AssignOutParameter(cmd, commandParameters);
            return value;
        }

        public static IDataReader ExecuteReader(CommandType cmdType, string cmdText, params ELParameter[] commandParameters)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PrepareCommand(db, cmdType, cmdText);
            BuildParameters(db, cmd, commandParameters);
            IDataReader dr = db.ExecuteReader(cmd);
            AssignOutParameter(cmd, commandParameters);
            return dr;
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params ELParameter[] commandParameters)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = PrepareCommand(db, cmdType, cmdText);
            BuildParameters(db, cmd, commandParameters);
            object value = db.ExecuteScalar(cmd);
            AssignOutParameter(cmd, commandParameters);
            return value;
        }

        public static void AssignOutParameter(DbCommand cmd, params ELParameter[] commandParameters)
        {
            foreach (ELParameter param in commandParameters)
            {
                if (param.Direction == ParameterDirection.Output)
                {
                    param.Value = cmd.Parameters[param.ParameterName].Value;
                }
            }
        }

        public static void BuildParameters(Database db, DbCommand dbCommand, params ELParameter[] commandParameters)
        {
            foreach (ELParameter parameter in commandParameters)
            {
                if (parameter.Direction == ParameterDirection.Input)
                    db.AddInParameter(dbCommand, parameter.ParameterName, parameter.DbType, parameter.Value);
                else
                    db.AddOutParameter(dbCommand, parameter.ParameterName, parameter.DbType, parameter.Size);
            }
        }

        private static DbCommand PrepareCommand(Database db, CommandType cmdType, string cmdText)
        {
            DbCommand cmd = null;
            switch (cmdType)
            {
                case CommandType.StoredProcedure:
                    cmd = db.GetStoredProcCommand(cmdText);
                    break;
                case CommandType.Text:
                    cmd = db.GetSqlStringCommand(cmdText);
                    break;
                default:
                    cmd = db.GetStoredProcCommand(cmdText);
                    break;
            }
            return cmd;
        }

        public static string GetSafeSqlDateTimeFormat(DateTime date)
        {
            return date.ToString(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.SortableDateTimePattern);
        }

        public static DateTime GetSafeSqlDateTime(DateTime date)
        {
            if (date < (DateTime)SqlDateTime.MinValue)
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            else if (date > (DateTime)SqlDateTime.MaxValue)
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
            return date;
        }

        public static int GetSafeSqlInt(int i)
        {
            if (i <= (int)System.Data.SqlTypes.SqlInt32.MinValue)
                return (int)System.Data.SqlTypes.SqlInt32.MinValue + 1;
            else if (i >= (int)System.Data.SqlTypes.SqlInt32.MaxValue)
                return (int)System.Data.SqlTypes.SqlInt32.MaxValue - 1;
            return i;

        }

        public static object StringOrNull(string text)
        {
            if (GlobalSettings.IsNullOrEmpty(text))
                return DBNull.Value;

            return text;
        }

        public static object IntOrNull(int i)
        {
            if (i == 0)
                return DBNull.Value;
            return i;
        }

        public static object IntOrNull(decimal i)
        {
            if (i == 0)
                return DBNull.Value;
            return i;
        }
    }
}
