using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace MvcLibrary.Controllers
{
    public class Person_AudienceController : Controller
    {
        //
        // GET: /Person_Audience/
        private DB_CyberneticsEntities ctx = new DB_CyberneticsEntities();
        public ActionResult AllRelations()
        {
            return View(ctx.PERSON_AUDIENCE.ToList());
        }

        //----------------


        public ActionResult Problem()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var ct = (from c in ctx.PERSON_AUDIENCE where c.PAU_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult RankEdit(int id, FormCollection collection)
        {
            var rank = (from c in ctx.PERSON_AUDIENCE where c.PAU_ID == id select c).First();
            try
            {

                var f = collection.GetValue("head");
                string head = f.AttemptedValue.ToString();
                var au = collection.GetValue("audience");
                string audi = au.AttemptedValue.ToString();

                if ((from c in ctx.PERSON_AUDIENCE where c.PERSON.PR_NAME == head && c.DIC_AUDIENCE.DAU_NAME == audi select c).Any())
                {
                    ctx.PERSON_AUDIENCE.Remove(rank);
                    ctx.SaveChanges();
                    return RedirectToAction("AllRelations");
                }
                else
                {
                    var person = (from c in ctx.PERSON where c.PR_NAME == head select c).First();
                    var audience = (from c in ctx.DIC_AUDIENCE where c.DAU_NAME == audi select c).First();

                    rank.DIC_AUDIENCE = audience;
                    rank.PERSON = person;
                    rank.PAU_PR = person.PR_ID;
                    rank.PAU_DAU = audience.DAU_ID;
                   
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
            PERSON_AUDIENCE rank = new PERSON_AUDIENCE();
            return View(rank);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateRanks(PERSON_AUDIENCE rank, FormCollection collection)
        {
            try
            {
                
                    var f = collection.GetValue("head");
                    string head = f.AttemptedValue.ToString();
                    var au = collection.GetValue("audience");
                    string audi = au.AttemptedValue.ToString();
                   
                    if((from c in ctx.PERSON_AUDIENCE where c.PERSON.PR_NAME == head && c.DIC_AUDIENCE.DAU_NAME == audi select c).Any())
                    {
                        return RedirectToAction("AllRelations");
                    }else
                    {
                        var person = (from c in ctx.PERSON where c.PR_NAME == head select c).First();
                        var audience = (from c in ctx.DIC_AUDIENCE where c.DAU_NAME == audi select c).First();

                        rank.DIC_AUDIENCE = audience;
                        rank.PERSON = person;
                        rank.PAU_PR = person.PR_ID;
                        rank.PAU_DAU = audience.DAU_ID;
                        ctx.PERSON_AUDIENCE.Add(rank);
                        ctx.SaveChanges();
                       // return RedirectToAction("AllRelations");
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
            var ct = (from c in ctx.PERSON_AUDIENCE where c.PAU_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRanks(int id, FormCollection collection)
        {
            try
            {
                PERSON_AUDIENCE rank = (from c in ctx.PERSON_AUDIENCE where c.PAU_ID == id select c).First();
                
                    ctx.PERSON_AUDIENCE.Remove(rank);
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
