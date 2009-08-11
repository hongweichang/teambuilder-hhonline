using System;

namespace HHOnline.QueryBuilder
{
    /// <summary>
    ///  �Ƚϲ����� WHERE, HAVING and JOIN �Ӿ�
    /// </summary>
    public enum Comparison
    {
        /// <summary>
        /// ����
        /// </summary>
        Equals,

        /// <summary>
        /// ������
        /// </summary>
        NotEquals,

        /// <summary>
        /// ������
        /// </summary>
        Like,

        /// <summary>
        /// ������
        /// </summary>
        NotLike,

        /// <summary>
        /// ����
        /// </summary>
        GreaterThan,

        /// <summary>
        /// ���ڵ���
        /// </summary>
        GreaterOrEquals,

        /// <summary>
        /// С��
        /// </summary>
        LessThan,

        /// <summary>
        /// С�ڵ���
        /// </summary>
        LessOrEquals,

        /// <summary>
        /// ������
        /// </summary>
        In,

        /// <summary>
        /// ��������
        /// </summary>
        NotIn,
    }
}
