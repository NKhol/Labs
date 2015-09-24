using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace MvcLibrary.Controllers
{
    public class ChairsController : Controller
    {
        //
        // GET: /Chairs/

        private DB_CyberneticsEntities ctx = new DB_CyberneticsEntities();
        public ActionResult AllChairs()
        {
            return View(ctx.DIC_CHAIRS.ToList());
        }

        //----------------


        public ActionResult Problem()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var ct = (from c in ctx.DIC_CHAIRS where c.DCH_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult RankEdit(int id, FormCollection collection)
        {
            var ct = (from c in ctx.DIC_CHAIRS where c.DCH_ID == id select c).First();
            try
            {
                var f = collection.GetValue("head");
                string head = f.AttemptedValue.ToString();
                if ((from c in ctx.PERSON_CHAIR where c.PC_CH == id select c).Any())
                {
                    var d = (from c in ctx.PERSON_CHAIR where c.PC_CH == id select c).First();
                    ctx.PERSON_CHAIR.Remove(d);
                    ctx.SaveChanges();
                }
                var p = (from c in ctx.PERSON where c.PR_NAME == head select c).First();
                
                PERSON_CHAIR w = new PERSON_CHAIR();
                w.PC_CH = id;
                w.PC_PR = p.PR_ID;
                w.PERSON = p;
                ctx.PERSON_CHAIR.Add(w);
                ctx.SaveChanges();
                    UpdateModel(ct);
                    ctx.SaveChanges();
                
                return RedirectToAction("AllChairs");
            }
            catch
            {
                return RedirectToAction("Problem");
            }
        }

        public ActionResult Create()
        {
            DIC_CHAIRS rank = new DIC_CHAIRS();
            return View(rank);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateRanks(DIC_CHAIRS rank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var s = rank.DCH_NAME;
                    if ((from c in ctx.DIC_CHAIRS where c.DCH_NAME == s select c).Any()) { }
                    else
                    {
                        ctx.DIC_CHAIRS.Add(rank);
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("AllChairs");
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
            var ct = (from c in ctx.DIC_CHAIRS where c.DCH_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRanks(int id, FormCollection collection)
        {
            try
            {
                DIC_CHAIRS rank = (from c in ctx.DIC_CHAIRS where c.DCH_ID == id select c).First();
                int t = rank.DCH_ID;
                
                if ((from c in ctx.PERSON where c.PR_DCH == t select c).Any() )
                    
                {
                    return RedirectToAction("Problem");
                }
                else
                {
                    var f = (from c in ctx.PERSON_CHAIR where c.PC_CH == id select c).ToList();
                    foreach (var item in f)
                    {
                        ctx.PERSON_CHAIR.Remove(item);
                        ctx.SaveChanges();
                    }
                    ctx.DIC_CHAIRS.Remove(rank);
                    ctx.SaveChanges();
                    return RedirectToAction("AllChairs");
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
