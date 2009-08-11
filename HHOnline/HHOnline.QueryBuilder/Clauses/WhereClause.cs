using System;
using System.Collections.Generic;
using System.Text;


namespace HHOnline.QueryBuilder
{
    /// <summary>
    ///Where�Ӿ�
    /// ʹ����һ�����Ͽ��԰�������Ƚ������
    /// eg:(UserID=1 or UserID=2 or UserID>100)
    /// WhereClause myWhereClause = new WhereClause("UserID", Comparison.Equals, 1);
    /// myWhereClause.AddClause(LogicOperator.Or, Comparison.Equals, 2);
    /// myWhereClause.AddClause(LogicOperator.Or, Comparison.GreaterThan, 100);
    /// </summary>
    public struct WhereClause
    {
        private string _fieldName;
        private Comparison _comparisonOperator;
        private object _value;

        /// <summary>
        /// �Ӿ�
        /// </summary>
        internal struct SubClause
        {
            public LogicOperator LogicOperator;
            public Comparison ComparisonOperator;
            public object Value;
            public SubClause(LogicOperator logic, Comparison compareOperator, object compareValue)
            {
                LogicOperator = logic;
                ComparisonOperator = compareOperator;
                Value = compareValue;
            }
        }
        internal List<SubClause> SubClauses;	// Array of SubClause

        /// <summary>
        /// ���ݿ��ֶ���
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        /// <summary>
        /// �Ƚ������
        /// </summary>
        public Comparison ComparisonOperator
        {
            get { return _comparisonOperator; }
            set { _comparisonOperator = value; }
        }

        /// <summary>
        ///ֵ
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public WhereClause(string field, Comparison firstCompareOperator, object firstCompareValue)
        {
            _fieldName = field;
            _comparisonOperator = firstCompareOperator;
            _value = firstCompareValue;
            SubClauses = new List<SubClause>();
        }
        public void AddClause(LogicOperator logic, Comparison compareOperator, object compareValue)
        {
            SubClause NewSubClause = new SubClause(logic, compareOperator, compareValue);
            SubClauses.Add(NewSubClause);
        }
    }

}
