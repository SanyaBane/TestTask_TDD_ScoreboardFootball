using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask_TDD_ScoreboardFootball
{
    public class Team
    {
        public string Title { get; }

        /// <param name="title">Case insensitive.</param>
        public Team(string title)
        {
            Title = title;
        }

        #region Equals() and GetHashCode()
        public override bool Equals(object obj)
        {
            return obj is Team team &&
                   Title.ToUpper() == team.Title.ToUpper();
        }

        public override int GetHashCode()
        {
            return 434131217 + EqualityComparer<string>.Default.GetHashCode(Title.ToUpper());
        }

        public static bool operator ==(Team left, Team right)
        {
            return EqualityComparer<Team>.Default.Equals(left, right);
        }

        public static bool operator !=(Team left, Team right)
        {
            return !(left == right);
        }
        #endregion
    }
}
