using System;
using System.Data;
using System.Data.Common;

namespace HHOnline.Data
{
    /// <summary>
    /// 对参数进行封装
    /// </summary>
    public class ELParameter
    {
        public string ParameterName { get; set; }

        public DbType DbType { get; set; }

        public object Value { get; set; }

        public int Size { get; set; }

        public ParameterDirection Direction { get; set; }

        public ELParameter() { }

        public ELParameter(string parameterName, DbType dbType)
        {
            this.ParameterName = parameterName;
            this.DbType = dbType;
            this.Direction = ParameterDirection.Input;
        }

        public ELParameter(string parameterName, DbType dbType, int size, ParameterDirection direction)
        {
            this.ParameterName = parameterName;
            this.DbType = dbType;
            this.Size = size;
            this.Direction = direction;
        }

        public ELParameter(string parameterName, DbType dbType, object value)
        {
            this.ParameterName = parameterName;
            this.DbType = dbType;
            this.Value = value == null ? DBNull.Value : value;
            this.Direction = ParameterDirection.Input;
        }

        public ELParameter(string parameterName, DbType dbType, int size, object value)
        {
            this.ParameterName = parameterName;
            this.DbType = dbType;
            this.Value = value == null ? DBNull.Value : value;
            this.Size = size;
            this.Direction = ParameterDirection.Input;
        }
    }
}
