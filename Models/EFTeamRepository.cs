using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Models
{
    public class EFTeamRepository : ITeamRepository
    {
        private BowlingLeagueDbContext _context { get; set; }
        public EFTeamRepository (BowlingLeagueDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Team> Teams => _context.Teams;

         
    }
}
