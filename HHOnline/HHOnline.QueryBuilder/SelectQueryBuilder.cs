using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace HHOnline.QueryBuilder
{
    /*例子：
     * SelectQueryBuilder query = new SelectQueryBuilder();
     * query.SelectFromTable("Customers");
     * query.AddJoin(JoinType.InnerJoin, "Customers", "CustomerID",Comparison.Equals,"Orders", "CustomerID");
     * query.SelectAllColumns();
     * query.TopRecords =10
     * query.AddWhere("CustomerID", Comparison.Equals, "VINET", 1);
     * query.AddWhere("OrderDate", Comparison.LessThan, new DateTime(2005, 1, 1), 1);
     * query.AddWhere("CustomerID", Comparison.Equals, "TOMSP", 2);
     * WhereClause clause =query.AddWhere("OrderDate", Comparison.LessThan, new DateTime(2004, 6, 30), 2);
     * clause.AddClause(LogicOperator.Or, Comparison.GreaterThan, new DateTime(2006, 1, 1));
     * 
     * 
     * output:select top 10 Customers.* from Customers inner join Orders on Customers.CustomerID = Orders.CustomerID
     * ((CustomerID = 'VINET') AND (OrderDate < '2005/01/01 12:00:00')) 
     * OR
     * ((CustomerID = 'TOMSP') AND (OrderDate < '2004/06/30 12:00:00' OR OrderDate > '2006/01/01 12:00:00'))  
     */

    /// <summary>
    /// Query生成器
    /// </summary>
    public class SelectQueryBuilder : IQueryBuilder
    {
        #region-- Private Members--
        protected bool _distinct = false;
        protected TopClause _topClause = new TopClause(100, TopUnit.Percent);
        protected List<string> _selectedColumns = new List<string>();	// array of string
        protected List<string> _selectedTables = new List<string>();	// array of string
        protected List<JoinClause> _joins = new List<JoinClause>();	// array of JoinClause
        protected WhereStatement _whereStatement = new WhereStatement();
        protected List<OrderByClause> _orderByStatement = new List<OrderByClause>();	// array of OrderByClause
        protected List<string> _groupByColumns = new List<string>();		// array of string
        protected WhereStatement _havingStatement = new WhereStatement();
        internal WhereStatement WhereStatement
        {
            get { return _whereStatement; }
            set { _whereStatement = value; }
        }
        #endregion

        #region --Constructor--
        public SelectQueryBuilder() { }
        public SelectQueryBuilder(DbProviderFactory factory)
        {
            _dbProviderFactory = factory;
        }

        private DbProviderFactory _dbProviderFactory;
        public void SetDbProviderFactory(DbProviderFactory factory)
        {
            _dbProviderFactory = factory;
        }
        #endregion

        #region --Distinct/Top--
        /// <summary>
        /// 是否包含Distince
        /// </summary>
        public bool Distinct
        {
            get { return _distinct; }
            set { _distinct = value; }
        }

        /// <summary>
        /// 记录数
        /// </summary>
        public int TopRecords
        {
            get { return _topClause.Quantity; }
            set
            {
                _topClause.Quantity = value;
                _topClause.Unit = TopUnit.Records;
            }
        }

        /// <summary>
        /// Top子句
        /// </summary>
        public TopClause TopClause
        {
            get { return _topClause; }
            set { _topClause = value; }
        }
        #endregion

        #region --SelectedColumn/Table--
        /// <summary>
        /// 选择的列
        /// </summary>
        public string[] SelectedColumns
        {
            get
            {
                if (_selectedColumns.Count > 0)
                    return _selectedColumns.ToArray();
                else
                    return new string[1] { "*" };
            }
        }
        /// <summary>
        /// 选择的表
        /// </summary>
        public string[] SelectedTables
        {
            get { return _selectedTables.ToArray(); }
        }

        /// <summary>
        /// 选择所有列
        /// </summary>
        public void SelectAllColumns()
        {
            _selectedColumns.Clear();
        }

        /// <summary>
        /// Count
        /// </summary>
        public void SelectCount()
        {
            SelectColumn("count(1)");
        }

        /// <summary>
        /// 选择列
        /// </summary>
        /// <param name="column">列名</param>
        public void SelectColumn(string column)
        {
            _selectedColumns.Clear();
            _selectedColumns.Add(column);
        }

        /// <summary>
        /// 选择列
        /// </summary>
        /// <param name="columns">列名</param>
        public void SelectColumns(params string[] columns)
        {
            _selectedColumns.Clear();
            foreach (string column in columns)
            {
                _selectedColumns.Add(column);
            }
        }

        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="table">表名</param>
        public void SelectFromTable(string table)
        {
            _selectedTables.Clear();
            _selectedTables.Add(table);
        }

        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="table">表名</param>
        public void SelectFromTables(params string[] tables)
        {
            _selectedTables.Clear();
            foreach (string Table in tables)
            {
                _selectedTables.Add(Table);
            }
        }
        #endregion

        #region --AddJoin--
        /// <summary>
        /// 添加连接子句
        /// </summary>
        /// <param name="newJoin"></param>
        public void AddJoin(JoinClause newJoin)
        {
            _joins.Add(newJoin);
        }

        /// <summary>
        /// 添加连接子句
        /// </summary>
        /// <param name="join"></param>
        /// <param name="toTableName"></param>
        /// <param name="toColumnName"></param>
        /// <param name="operator"></param>
        /// <param name="fromTableName"></param>
        /// <param name="fromColumnName"></param>
        public void AddJoin(JoinType join, string toTableName, string toColumnName, Comparison @operator, string fromTableName, string fromColumnName)
        {
            JoinClause NewJoin = new JoinClause(join, toTableName, toColumnName, @operator, fromTableName, fromColumnName);
            _joins.Add(NewJoin);
        }
        #endregion

        #region --AddWhere--
        /// <summary>
        /// Where表达式
        /// </summary>
        public WhereStatement Where
        {
            get { return _whereStatement; }
            set { _whereStatement = value; }
        }

        /// <summary>
        /// 添加Where子句
        /// </summary>
        /// <param name="clause"></param>
        public void AddWhere(WhereClause clause)
        {
            AddWhere(clause, 1);
        }

        /// <summary>
        /// 添加Where子句
        /// </summary>
        /// <param name="clause">子句</param>
        /// <param name="level">级别</param>
        public void AddWhere(WhereClause clause, int level)
        {
            _whereStatement.Add(clause, level);
        }

        /// <summary>
        /// 添加Where子句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="operator"></param>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue)
        {
            return AddWhere(field, @operator, compareValue, 1);
        }

        /// <summary>
        /// 添加Where子句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="operator"></param>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public WhereClause AddWhere(Enum field, Comparison @operator, object compareValue)
        {
            return AddWhere(field.ToString(), @operator, compareValue, 1);
        }

        /// <summary>
        /// 添加Where子句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="operator"></param>
        /// <param name="compareValue"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
            _whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        #endregion

        #region --AddOrderBy--
        /// <summary>
        /// 添加排序子句
        /// </summary>
        /// <param name="clause"></param>
        public void AddOrderBy(OrderByClause clause)
        {
            _orderByStatement.Add(clause);
        }

        /// <summary>
        /// 添加排序子句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="order"></param>
        public void AddOrderBy(Enum field, Sorting order)
        {
            this.AddOrderBy(field.ToString(), order);
        }

        /// <summary>
        /// 添加排序子句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="order"></param>
        public void AddOrderBy(string field, Sorting order)
        {
            OrderByClause NewOrderByClause = new OrderByClause(field, order);
            _orderByStatement.Add(NewOrderByClause);
        }
        #endregion

        #region--GroupBy--
        /// <summary>
        /// 添加分组列
        /// </summary>
        /// <param name="columns"></param>
        public void GroupBy(params string[] columns)
        {
            foreach (string Column in columns)
            {
                _groupByColumns.Add(Column);
            }
        }
        #endregion

        #region --AddHaving--
        /// <summary>
        /// 添加Having子句
        /// </summary>
        public WhereStatement Having
        {
            get { return _havingStatement; }
            set { _havingStatement = value; }
        }

        /// <summary>
        /// 添加Having子句
        /// </summary>
        /// <param name="clause"></param>
        public void AddHaving(WhereClause clause)
        {
            AddHaving(clause, 1);
        }

        /// <summary>
        /// 添加Having子句
        /// </summary>
        /// <param name="clause"></param>
        /// <param name="level"></param>
        public void AddHaving(WhereClause clause, int level)
        {
            _havingStatement.Add(clause, level);
        }

        /// <summary>
        /// 添加Having子句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="operator"></param>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public WhereClause AddHaving(string field, Comparison @operator, object compareValue)
        {
            return AddHaving(field, @operator, compareValue, 1);
        }

        /// <summary>
        /// 添加Having子句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="operator"></param>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public WhereClause AddHaving(Enum field, Comparison @operator, object compareValue)
        {
            return AddHaving(field.ToString(), @operator, compareValue, 1);
        }

        /// <summary>
        /// 添加Having子句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="operator"></param>
        /// <param name="compareValue"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public WhereClause AddHaving(string field, Comparison @operator, object compareValue, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
            _havingStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        #endregion

        #region--BuildQuery--
        /// <summary>
        /// BuildCommand
        /// </summary>
        /// <returns></returns>
        public DbCommand BuildCommand()
        {
            return (DbCommand)this.BuildQuery(true);
        }

        /// <summary>
        /// BuildQuery
        /// </summary>
        /// <returns></returns>
        public string BuildQuery()
        {
            return (string)this.BuildQuery(false);
        }

        /// <summary>
        /// 生成SQL
        /// </summary>
        /// <returns>生成SQL</returns>
        private object BuildQuery(bool buildCommand)
        {
            if (buildCommand && _dbProviderFactory == null)
                throw new Exception("Cannot build a command when the Db Factory hasn't been specified. Call SetDbProviderFactory first.");

            DbCommand command = null;
            if (buildCommand)
                command = _dbProviderFactory.CreateCommand();

            string Query = "SELECT ";

            // Output Distinct
            if (_distinct)
            {
                Query += "DISTINCT ";
            }

            // Output Top clause
            if (!(_topClause.Quantity == 100 & _topClause.Unit == TopUnit.Percent))
            {
                Query += "TOP " + _topClause.Quantity;
                if (_topClause.Unit == TopUnit.Percent)
                {
                    Query += " PERCENT";
                }
                Query += " ";
            }

            // Output column names
            if (_selectedColumns.Count == 0)
            {

                if (_selectedTables.Count == 1)
                    Query += GetColumnSql(_selectedTables[0], "*");
                //Query += _selectedTables[0] + ".";
                // By default only select * from the table that was selected. If there are any joins, it is the responsibility of the user to select the needed columns.
                else
                    Query += "*";
            }
            else
            {
                foreach (string ColumnName in _selectedColumns)
                {
                    Query += ColumnName + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
                Query += ' ';
            }
            // Output table names
            if (_selectedTables.Count > 0)
            {
                Query += " FROM ";
                foreach (string TableName in _selectedTables)
                {
                    Query += TableName + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
                Query += ' ';
            }

            // Output joins
            if (_joins.Count > 0)
            {
                foreach (JoinClause Clause in _joins)
                {
                    string JoinString = "";
                    switch (Clause.JoinType)
                    {
                        case JoinType.InnerJoin: JoinString = "INNER JOIN"; break;
                        case JoinType.OuterJoin: JoinString = "OUTER JOIN"; break;
                        case JoinType.LeftJoin: JoinString = "LEFT JOIN"; break;
                        case JoinType.RightJoin: JoinString = "RIGHT JOIN"; break;
                    }
                    JoinString += " " + Clause.ToTable + " ON ";

                    JoinString += WhereStatement.CreateComparisonClause(GetColumnSql(Clause.FromTable, Clause.FromColumn), Clause.ComparisonOperator, new SqlLiteral(GetColumnSql(Clause.ToTable, Clause.ToColumn)));
                    Query += JoinString + ' ';
                }
            }

            // Output where statement
            if (_whereStatement.ClauseLevels > 0)
            {
                if (buildCommand)
                    Query += " WHERE " + _whereStatement.BuildWhereStatement(true, ref command);
                else
                    Query += " WHERE " + _whereStatement.BuildWhereStatement();
            }

            // Output GroupBy statement
            if (_groupByColumns.Count > 0)
            {
                Query += " GROUP BY ";
                foreach (string Column in _groupByColumns)
                {
                    Query += Column + ',';
                }
                Query = Query.TrimEnd(',');
                Query += ' ';
            }

            // Output having statement
            if (_havingStatement.ClauseLevels > 0)
            {
                // Check if a Group By Clause was set
                if (_groupByColumns.Count == 0)
                {
                    throw new Exception("Having statement was set without Group By");
                }
                if (buildCommand)
                    Query += " HAVING " + _havingStatement.BuildWhereStatement(true, ref command);
                else
                    Query += " HAVING " + _havingStatement.BuildWhereStatement();
            }

            // Output OrderBy statement
            if (_orderByStatement.Count > 0)
            {
                Query += " ORDER BY ";
                foreach (OrderByClause Clause in _orderByStatement)
                {
                    string OrderByClause = "";
                    switch (Clause.SortOrder)
                    {
                        case Sorting.Ascending:
                            OrderByClause = Clause.FieldName + " ASC"; break;
                        case Sorting.Descending:
                            OrderByClause = Clause.FieldName + " DESC"; break;
                    }
                    Query += OrderByClause + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last AND inserted by foreach loop
                Query += ' ';
            }

            if (buildCommand)
            {
                // Return the build command
                command.CommandText = Query;
                return command;
            }
            else
            {
                // Return the built query
                return Query;
            }
        }

        private string GetColumnSql(string tableName, string columnName)
        {
            if (columnName.Contains("."))
            {
                return columnName;
            }
            else
            {
                if (tableName.Trim().Contains(" "))
                {
                    //匹配 tablename t 或者 tablename as t
                    string pattern = "(\\w+)\\s*(as)?\\s*(\\w+)";
                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                    MatchCollection matchs = regex.Matches(tableName);
                    foreach (Match match in matchs)
                    {
                        if (match.Success)
                        {
                            GroupCollection gc = match.Groups;
                            tableName = gc[3].Value;
                            break;
                        }
                    }
                }
                return tableName + "." + columnName;
            }
        }
        #endregion
    }

}
