using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.DAL.Entities
{
    public class Match
    {
        #region columns
        public int Id { get; set; }
        public int WhiteId { get; set; }
        public int BlackId { get; set; }
        public int TournamentId { get; set; }
        public int Round { get; set; }
        public MatchResultType Result { get; set; }
        #endregion

        #region Relations
        public Member White { get; set; }
        public Member Black { get; set; }
        public Tournament Tournament { get; set; }
        #endregion
    }
}
