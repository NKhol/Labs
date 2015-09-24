using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace MvcLibrary.Controllers
{
    public class vm_PersonList
    {
        public PERSON Person { get; set; }
    }

    public class PersonController : Controller
    {
        //
        // GET: /Person/

        private DB_CyberneticsEntities ctx = new DB_CyberneticsEntities();
        public List<PERSON> result = new List<PERSON>();
        
        public ActionResult AllPeople()
        {
            return View(ctx.PERSON.ToList());
        }


        public ActionResult Details(int id)
        {
            var ct = (from c in ctx.PERSON where c.PR_ID == id select c).First();
            return View(ct);
        }

        public ActionResult Search()
        {

            return View();
        }

        [HttpPost, ActionName("Search")]
        public ActionResult PersonSearch(FormCollection collection)
        {
            var f = collection.GetValue("name");
            string name = f.AttemptedValue.ToString();
            var f1 = collection.GetValue("chair");
            string chair = f1.AttemptedValue.ToString();
            var f2 = collection.GetValue("degree");
            string degree = f2.AttemptedValue.ToString();
            var f3 = collection.GetValue("rank");
            string rank = f3.AttemptedValue.ToString();
            var f4 = collection.GetValue("audience");
            string audience = f4.AttemptedValue.ToString();

            //===============
           // List<PERSON> result = new List<PERSON>();
            result.Clear();
            List<PERSON> list1 = new List<PERSON>();
            List<PERSON> list2 = new List<PERSON>();
            List<PERSON> list3 = new List<PERSON>();
            List<PERSON> list4 = new List<PERSON>();
            List<PERSON> list5 = new List<PERSON>();

            if (name == String.Empty)
            {
                list1 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                list1 = (from c in ctx.PERSON where c.PR_NAME == name select c).ToList();
            }

            if (chair == String.Empty)
            {
                list2 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                int ch = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == chair select c.DCH_ID).First();
                list2 = (from c in ctx.PERSON where ch == c.PR_DCH select c).ToList();
            }

            if (degree == String.Empty)
            {
                list3 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                int dg = (from c in ctx.DIC_DEGREE where c.DDG_NAME == degree select c.DDG_ID).First();
                list3 = (from c in ctx.PERSON where c.PR_DDG == dg select c).ToList();
            }

            if (rank == String.Empty)
            {
                list4 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                int r = (from c in ctx.DIC_RANKS where c.DRK_NAME == rank select c.DRK_ID).First();
                var pr = (from c in ctx.PERSON_RANKS where c.PRS_DRK == r select c.PRS_PR).ToList();
                list4 = (from c in ctx.PERSON where pr.Contains(c.PR_ID) select c).ToList();
            }

            if (audience == String.Empty)
            {
                list5 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                int au = (from c in ctx.DIC_AUDIENCE where c.DAU_NAME == audience select c.DAU_ID).First();
                var pr = (from c in ctx.PERSON_AUDIENCE where c.PAU_DAU == au select c.PAU_PR).ToList();
                list5 = (from c in ctx.PERSON where pr.Contains(c.PR_ID) select c).ToList();
            }

            var universum = (from c in ctx.PERSON select c).ToList();

            foreach (var c in universum)
            {
                if (list1.Contains(c)&&list2.Contains(c) && list3.Contains(c) && list4.Contains(c) && list5.Contains(c))
                {
                    PERSON tr = new PERSON();
                    tr = c;
                    result.Add(tr);
                }
            }
            //==========

            return RedirectToAction("Result");// problem
           // return View(result);
        }

        //----------------
        public ActionResult Result()
        {
            return View(result);
        }

        public ActionResult Problem()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var ct = (from c in ctx.PERSON where c.PR_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult PersonEdit(int id, FormCollection collection)
        {
            var p = (from c in ctx.PERSON where c.PR_ID == id select c).First();
            try
            {

                var f = collection.GetValue("chair");
                string chair = f.AttemptedValue.ToString();
                var au = collection.GetValue("degree");
                string degree = au.AttemptedValue.ToString();
                string name = p.PR_NAME;

                PERSON per = new PERSON();
                UpdateModel(per);
                string w = per.PR_NAME;

                if ((from c in ctx.PERSON where c.PR_ID!= id && c.PR_NAME == w select c ).Any())
                {
                    return RedirectToAction("Problem");
                }
                else
                {
                    
                    var ch = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == chair select c).First();
                    var dg = (from c in ctx.DIC_DEGREE where c.DDG_NAME == degree select c).First();
                    UpdateModel(p);
                    p.PR_DCH = ch.DCH_ID;
                    p.PR_DDG = dg.DDG_ID;
                    p.DIC_CHAIRS = ch;

                    ctx.SaveChanges();

                }
                
                return RedirectToAction("AllPeople");

            }
            catch
            {
                return RedirectToAction("Problem");
            }
            return View();
        }

        public ActionResult Create()
        {
            PERSON rank = new PERSON();
            return View(rank);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateRanks(PERSON rank, FormCollection collection)
        {
            try
            {

                var f = collection.GetValue("chair");
                string chair = f.AttemptedValue.ToString();
                var au = collection.GetValue("degree");
                string degree = au.AttemptedValue.ToString();

                if(ModelState.IsValid)
                {
                    string n = rank.PR_NAME;
                    if((from c in ctx.PERSON where c.PR_NAME == n select c).Any())
                    {
                        return RedirectToAction("Problem");
                    }
                    else
                    {
                        var ch = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == chair select c).First();
                        var dg = (from c in ctx.DIC_DEGREE where c.DDG_NAME == degree select c).First();
                        rank.PR_DDG = dg.DDG_ID;
                        rank.PR_DCH = ch.DCH_ID;
                        rank.DIC_CHAIRS = ch;

                        ctx.PERSON.Add(rank);
                        ctx.SaveChanges();

                    }
                }

                return RedirectToAction("AllPeople");

            }
            catch
            {
                return RedirectToAction("Problem");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var ct = (from c in ctx.PERSON where c.PR_ID == id select c).First();
            return View(ct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRanks(int id, FormCollection collection)
        {
            try
            {
                PERSON rank = (from c in ctx.PERSON where c.PR_ID == id select c).First();

                if ((from c in ctx.PERSON_AUDIENCE where c.PERSON.PR_ID == id select c).Any() ||
                    (from c in ctx.PERSON_CHAIR where c.PERSON.PR_ID == id select c).Any() ||
                    (from c in ctx.PERSON_RANKS where c.PERSON.PR_ID == id select c).Any())
                {
                    return RedirectToAction("Problem");
                }
                else
                {
                    ctx.PERSON.Remove(rank);
                    ctx.SaveChanges();
                    return RedirectToAction("AllPeople");
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
