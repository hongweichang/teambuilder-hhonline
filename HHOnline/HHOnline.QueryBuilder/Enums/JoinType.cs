using System;

namespace HHOnline.QueryBuilder
{
    /// <summary>
    /// ���Ӳ����� JOIN�Ӿ�
    /// </summary>
    public enum JoinType
    {
        /// <summary>
        /// ������
        /// </summary>
        InnerJoin,

        /// <summary>
        ///������ 
        /// </summary>
        OuterJoin,

        /// <summary>
        /// ������
        /// </summary>
        LeftJoin,

        /// <summary>
        /// ������
        /// </summary>
        RightJoin
    }
}
