using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace MvcLibrary.Controllers
{
    public class DegreeController : Controller
    {
        //
        // GET: /Degree/
        private DB_CyberneticsEntities ctx = new DB_CyberneticsEntities();
        public ActionResult AllDegrees()
        {
            return View();
        }

        //----------------


        public ActionResult Problem()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var ct = (from c in ctx.DIC_DEGREE where c.DDG_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult RankEdit(int id, FormCollection collection)
        {
            var ct = (from c in ctx.DIC_DEGREE where c.DDG_ID == id select c).First();
            try
            {
                
                {
                    UpdateModel(ct);
                    ctx.SaveChanges();
                }
                return RedirectToAction("AllDegrees");
            }
            catch
            {
                return RedirectToAction("Problem");
            }
        }

        public ActionResult Create()
        {
            DIC_DEGREE rank = new DIC_DEGREE();
            return View(rank);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateRanks(DIC_DEGREE rank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var s = rank.DDG_NAME;
                    if ((from c in ctx.DIC_DEGREE where c.DDG_NAME == s select c).Any()) { }
                    else
                    {
                        ctx.DIC_DEGREE.Add(rank);
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("AllDegrees");
                }
            }
            catch
            {
                return RedirectToAction("Problem");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var ct = (from c in ctx.DIC_DEGREE where c.DDG_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRanks(int id, FormCollection collection)
        {
            try
            {
                DIC_DEGREE rank = (from c in ctx.DIC_DEGREE where c.DDG_ID == id select c).First();
                int t = rank.DDG_ID;
                if ((from c in ctx.PERSON where c.PR_DDG == t select c).Any())
                {
                    return RedirectToAction("Problem");
                }
                else
                {
                    ctx.DIC_DEGREE.Remove(rank);
                    ctx.SaveChanges();
                    return RedirectToAction("AllDegrees");
                }
            }
            catch
            {

            }
            return RedirectToAction("Problem");
        }

        //=================

    }
}
