using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Models
{
    public class EFBowlerRepository : IBowlerRepository
    {
        private BowlingLeagueDbContext _context { get; set; }
        public EFBowlerRepository(BowlingLeagueDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Bowler> Bowlers =>  _context.Bowlers;
        public void CreateBowler(Bowler b)
        {
            _context.Add(b);
            _context.SaveChanges();
        }
        public void SaveBowler(Bowler b)
        {
            _context.Update(b);
            _context.SaveChanges();
        }
        public void DeleteBowler(Bowler b)
        {
            _context.Remove(b);
            _context.SaveChanges();
        }
    }
}
