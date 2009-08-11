using System;


namespace HHOnline.QueryBuilder
{
    /// <summary>
    /// Top�Ӿ�
    /// </summary>
    public struct TopClause
    {
        public int Quantity;
        public TopUnit Unit;
        public TopClause(int nr)
        {
            Quantity = nr;
            Unit = TopUnit.Records;
        }
        public TopClause(int nr, TopUnit aUnit)
        {
            Quantity = nr;
            Unit = aUnit;
        }
    }

}
