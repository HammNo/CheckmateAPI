using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.DAL.Entities
{
    [Flags]
    public enum TournamentCategoryType
    {
        Junior = 1,
        Senior = 2,
        Veteran = 4
    }
}
