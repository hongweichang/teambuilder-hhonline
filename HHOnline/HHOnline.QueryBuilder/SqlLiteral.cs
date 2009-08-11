using System;

namespace HHOnline.QueryBuilder
{
    /// <summary>
    /// 原样输出，不会添加上引号
    /// query = new SelectQueryBuilder();
    /// query.SelectFromTable("Orders");
    /// query.AddWhere("OrderDate", Comparison.LessOrEquals, new SqlLiteral("getDate()"));
    /// </summary>
    public class SqlLiteral
    {
        public static string StatementRowsAffected = "SELECT @@ROWCOUNT";

        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public SqlLiteral(string value)
        {
            _value = value;
        }
    }

}
