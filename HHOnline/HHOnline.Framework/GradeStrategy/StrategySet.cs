using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HHOnline.Framework
{
    /// <summary>
    /// 策略集合
    /// </summary>
    public class StrategySet : List<IGradeStrategy>
    {
        /// <summary>
        /// 生成Query
        /// </summary>
        /// <returns></returns>
        public string BuildQuery()
        {
            string query = string.Empty;
            for (int i = 0; i < this.Count; i++)
            {
                if (i != this.Count - 1)
                {
                    query += this[i].BuildQuery() + " AND ";
                }
                else
                {
                    query += this[i].BuildQuery();
                }
            }
            return query;
        }

        /// <summary>
        /// 通过Query 填充
        /// </summary>
        /// <param name="query"></param>
        public void ReFill(string query)
        {
            string pattern = "(\\w+)\\s*(=|like|is)\\s*('?%?\\w+%?'?)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchs = regex.Matches(query);
            foreach (Match match in matchs)
            {
                if (match.Success)
                {
                    GroupCollection gc = match.Groups;
                    Add(gc[1].Value, gc[3].Value);
                }
            }
        }

        /// <summary>
        /// 添加策略
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Add(string name, object value)
        {
            IGradeStrategy strategy = StrategyFactory.GetGradeStrategy(name.Trim());
            strategy.Value = value;
            Add(strategy);
        }
    }
}
