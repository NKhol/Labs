using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace MvcLibrary.Controllers
{
    public class RanksController : Controller
    {
        //
        // GET: /Ranks/
        private DB_CyberneticsEntities ctx = new DB_CyberneticsEntities();
        public ActionResult AllRanks()
        {
            return View(ctx.DIC_RANKS.ToList());
        }

        public ActionResult Cant(DIC_RANKS rank)
        {
            return View(rank);
        }

        public ActionResult Problem()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var ct = (from c in ctx.DIC_RANKS where c.DRK_ID == id select c).First();
            return View(ct);
        }

        [HttpPost,ActionName("Edit")]
        public ActionResult RankEdit(int id,FormCollection collection)
        {
            var ct = (from c in ctx.DIC_RANKS where c.DRK_ID == id select c).First();
            try
            {
                UpdateModel(ct);
                ctx.SaveChanges();
                return RedirectToAction("AllRanks");
            }catch
            {
                return Cant(ct);
            }
        }

        public ActionResult Create()
        {
            DIC_RANKS rank = new DIC_RANKS();
            return View(rank);
        }

        [HttpPost,ActionName("Create")]
        public ActionResult CreateRanks(DIC_RANKS rank)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var s = rank.DRK_NAME;
                    if ((from c in ctx.DIC_RANKS where c.DRK_NAME == s select c).Any()) { }
                    else
                    {
                        ctx.DIC_RANKS.Add(rank);
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("AllRanks");
                }
            }
            catch
            {
                return RedirectToAction("Cant");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var ct = (from c in ctx.DIC_RANKS where c.DRK_ID == id select c).First();
            return View(ct);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteRanks(int id, FormCollection collection)
        {
            try
            {
                DIC_RANKS rank = (from c in ctx.DIC_RANKS where c.DRK_ID == id select c).First();
                int t = rank.DRK_ID;
                if((from c in ctx.PERSON_RANKS where c.PRS_DRK == t select c).Any())
                {
                   return RedirectToAction("Problem");
                }
                else
                {
                    ctx.DIC_RANKS.Remove(rank);
                    ctx.SaveChanges();
                    return RedirectToAction("AllRanks");
                }
            }
            catch
            { 

            }
            return RedirectToAction("Problem");
        }
    }
}
