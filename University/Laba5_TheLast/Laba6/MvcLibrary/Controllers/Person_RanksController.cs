using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace MvcLibrary.Controllers
{
    public class Person_RanksController : Controller
    {
        //
        // GET: /Person_Ranks/
        private DB_CyberneticsEntities ctx = new DB_CyberneticsEntities();
        public ActionResult AllRelations()
        {
            return View(ctx.PERSON_RANKS.ToList());
        }

        //----------------


        public ActionResult Problem()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var ct = (from c in ctx.PERSON_RANKS where c.PRS_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult RankEdit(int id, FormCollection collection)
        {
            var rank = (from c in ctx.PERSON_RANKS where c.PRS_ID == id select c).First();
            try
            {

                var f = collection.GetValue("head");
                string head = f.AttemptedValue.ToString();
                var au = collection.GetValue("rank");
                string audi = au.AttemptedValue.ToString();

                if ((from c in ctx.PERSON_RANKS where c.PERSON.PR_NAME == head && c.DIC_RANKS.DRK_NAME == audi select c).Any())
                {
                    ctx.PERSON_RANKS.Remove(rank);
                    ctx.SaveChanges();
                    return RedirectToAction("AllRelations");
                }
                else
                {
                    var person = (from c in ctx.PERSON where c.PR_NAME == head select c).First();
                    var audience = (from c in ctx.DIC_RANKS where c.DRK_NAME == audi select c).First();

                    rank.DIC_RANKS = audience;
                    rank.PERSON = person;
                    rank.PRS_PR = person.PR_ID;
                    rank.PRS_DRK = audience.DRK_ID;

                    ctx.SaveChanges();

                }

                return RedirectToAction("AllRelations");

            }
            catch
            {
                return RedirectToAction("Problem");
            }
            return View();
        }

        public ActionResult Create()
        {
            PERSON_RANKS rank = new PERSON_RANKS();
            return View(rank);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateRanks(PERSON_RANKS rank, FormCollection collection)
        {
            try
            {

                var f = collection.GetValue("head");
                string head = f.AttemptedValue.ToString();
                var au = collection.GetValue("rank");
                string audi = au.AttemptedValue.ToString();

                if ((from c in ctx.PERSON_RANKS where c.PERSON.PR_NAME == head && c.DIC_RANKS.DRK_NAME == audi select c).Any())
                {
                    return RedirectToAction("AllRelations");
                }
                else
                {
                    var person = (from c in ctx.PERSON where c.PR_NAME == head select c).First();
                    var audience = (from c in ctx.DIC_RANKS where c.DRK_NAME == audi select c).First();

                    PERSON_RANKS rel = new PERSON_RANKS();

                    rel.PERSON = person;
                    rel.DIC_RANKS = audience;

                    ctx.PERSON_RANKS.Add(rel);
                    ctx.SaveChanges();

                }

                return RedirectToAction("AllRelations");

            }
            catch
            {
                return RedirectToAction("Problem");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var ct = (from c in ctx.PERSON_RANKS where c.PRS_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRanks(int id, FormCollection collection)
        {
            try
            {
                PERSON_RANKS rank = (from c in ctx.PERSON_RANKS where c.PRS_ID == id select c).First();

                ctx.PERSON_RANKS.Remove(rank);
                ctx.SaveChanges();
                return RedirectToAction("AllRelations");

            }
            catch
            {

            }
            return RedirectToAction("Problem");
        }

        //=================

    }
}
