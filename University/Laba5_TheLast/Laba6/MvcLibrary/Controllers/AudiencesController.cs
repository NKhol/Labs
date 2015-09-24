using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace MvcLibrary.Controllers
{
    public class AudiencesController : Controller
    {
        //
        // GET: /Audiences/
        private DB_CyberneticsEntities ctx = new DB_CyberneticsEntities();
        public ActionResult AllAudiences()
        {
            return View(ctx.DIC_AUDIENCE.ToList());
        }

        //------------------------

        public ActionResult Problem()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            DIC_AUDIENCE ct = (from c in ctx.DIC_AUDIENCE where c.DAU_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult AudiencesEdit(int id, FormCollection collection)
        {
            DIC_AUDIENCE ct = (from c in ctx.DIC_AUDIENCE where c.DAU_ID == id select c).First();
            try
            {
                UpdateModel(ct);
                ctx.SaveChanges();
                return RedirectToAction("AllAudiences");
            }
            catch
            {
                return RedirectToAction("Problem");
            }
        }

        public ActionResult Create()
        {
            DIC_AUDIENCE aud = new DIC_AUDIENCE();
            return View(aud);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateAudiences(DIC_AUDIENCE rank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var s = rank.DAU_NAME;
                    if ((from c in ctx.DIC_AUDIENCE where c.DAU_NAME == s select c).Any()) { }
                    else
                    {
                        ctx.DIC_AUDIENCE.Add(rank);
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("AllAudiences");
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
            DIC_AUDIENCE ct = (from c in ctx.DIC_AUDIENCE where c.DAU_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAudiences(int id, FormCollection collection)
        {
            try
            {
                DIC_AUDIENCE rank = (from c in ctx.DIC_AUDIENCE where c.DAU_ID == id select c).First();
                int t = rank.DAU_ID;
                if ((from c in ctx.PERSON_AUDIENCE where c.PAU_DAU == t select c).Any())
                {
                    return RedirectToAction("Problem");
                }
                else
                {
                    ctx.DIC_AUDIENCE.Remove(rank);
                    ctx.SaveChanges();
                    return RedirectToAction("AllAudiences");
                }
            }
            catch
            {

            }
            return RedirectToAction("Problem");
        }

        //========================

    }
}
