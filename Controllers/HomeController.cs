using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private IBowlerRepository _repo { get; set; }
        private ITeamRepository _repoteam { get; set; }
        public HomeController (IBowlerRepository temp, ITeamRepository tempteam)
        {
            _repo = temp;
            _repoteam = tempteam;
        }
        public IActionResult Index(string teamName)
        {
            var blah = _repo.Bowlers
                .Include("Team")
                .Where(b => b.Team.TeamName == teamName || teamName == null)
                .ToList();
            ViewData["TeamName"] = teamName;
            return View(blah) ;
        }
        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _repoteam.Teams.ToList();
            var blah = _repo.Bowlers.Select(x => x.BowlerID);
            
            var hah = new Bowler
            {
                BowlerID = blah.Max() + 1
            };

            return View("BowlerForm", hah);
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {
            var blah = _repo.Bowlers.Select(x=>x.BowlerID);

            if (blah.Contains(b.BowlerID))
            {
                _repo.SaveBowler(b);
            }
            else
            {
                _repo.CreateBowler(b);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit (int bowlerid)
        {
            var blah = _repo.Bowlers.Include("Team").Where(x => x.BowlerID == bowlerid).FirstOrDefault();
            ViewBag.Teams = _repoteam.Teams.ToList();
            return View("BowlerForm", blah);
        }
        
        public IActionResult Delete (Bowler b)
        {
            var blah = _repo.Bowlers.Where(x => x.BowlerID == b.BowlerID).FirstOrDefault();
            _repo.DeleteBowler(blah);
            return RedirectToAction("Index");
        }
    }
}
