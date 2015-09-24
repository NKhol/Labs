using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Data.Entity;

namespace ClassLibrary
{
    public class Manager
    {
        static DB_CyberneticsEntities ctx;

        static public bool LoadDataBase()
        {
            try
            {
                ctx = new DB_CyberneticsEntities();
                ctx.DIC_AUDIENCE.Load();
                ctx.DIC_CHAIRS.Load();
                ctx.DIC_DEGREE.Load();
                ctx.DIC_RANKS.Load();
                ctx.PERSON.Load();
                ctx.PERSON_AUDIENCE.Load();
                ctx.PERSON_CHAIR.Load();
                ctx.PERSON_RANKS.Load();
            }
            catch(System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        static public List<string> GetAllPeople()
        {
            ctx.PERSON.Load();
            List<string> people = new List<string>();
            var f = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToArray();
            for (int i = 0; i < f.Length; i++ ){
                people.Add(f[i].PR_NAME);
            }
                return people;
        }
        static public List<string> GetAllAudiences()
        {
            ctx.DIC_AUDIENCE.Load();
            List<string> aud = new List<string>();
            var f = (from c in ctx.DIC_AUDIENCE where c.DAU_ID == c.DAU_ID select c).ToArray();
            for (int i = 0; i < f.Length; i++ )
            {
                aud.Add(f[i].DAU_NAME);
            }
            return aud;
        }
        static public List<string> GetAllChairs()
        {
            ctx.DIC_CHAIRS.Load();
            List<string> ch = new List<string>();
            var f = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == c.DCH_NAME select c).ToArray();
            for (int i = 0; i < f.Length; i++)
            {
                ch.Add(f[i].DCH_NAME);
            }
            return ch;
        }
        static public List<string> GetAllRanks()
        {
            ctx.DIC_RANKS.Load();
            List<string> ch = new List<string>();
            var f = (from c in ctx.DIC_RANKS where c.DRK_ID == c.DRK_ID select c).ToArray();
            for (int i = 0; i < f.Length; i++)
            {
                ch.Add(f[i].DRK_NAME);
            }
            return ch;
        }
        static public List<string> GetAllDegrees()
        {
            ctx.DIC_DEGREE.Load();
            List<string> ch = new List<string>();
            var f = (from c in ctx.DIC_DEGREE where c.DDG_ID == c.DDG_ID select c).ToArray();
            for (int i = 0; i < f.Length; i++)
            {
                ch.Add(f[i].DDG_NAME);
            }
            return ch;
        }
        static public List<string> GetAllChairsAndHeads()
        {
            ctx.PERSON_CHAIR.Load();
            List<string> t = new List<string>();
            var ta = (from c in ctx.PERSON_CHAIR where c.PC_CH == c.PC_CH select c).ToArray();
            foreach(var c in ta)
            {
                string str = String.Empty;
                int i = c.PC_CH;
                str += (from d in ctx.DIC_CHAIRS where d.DCH_ID == i select d.DCH_NAME).First() + " -> ";
                i = c.PC_PR;
                str += (from d in ctx.PERSON where d.PR_ID == i select d.PR_NAME).First();
                t.Add(str);
            }
            return t;
        }
        static public List<string> GetAllPeopleAndRanks()
        {
            List<string> t = new List<string>();
            ctx.PERSON_RANKS.Load();
            ctx.PERSON.Load();
            ctx.DIC_RANKS.Load();

            var people = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToArray();
            foreach(var c in people)
            {
                if( (from d in ctx.PERSON_RANKS where d.PRS_PR == c.PR_ID select d).Any() )
                {
                    var ar = (from d in ctx.PERSON_RANKS where d.PRS_PR == c.PR_ID select d).ToArray();
                    string str = String.Empty;
                    str += c.PR_NAME + " ->";
                    foreach(var g in ar)
                    {
                        str += " " + (from w in ctx.DIC_RANKS where g.PRS_DRK == w.DRK_ID select w.DRK_NAME).First()+";";
                    }
                    t.Add(str);
                }
            }

            return t;
        }
        static public List<string> GetCheckedRanksByName(string name)
        {
            List<string> t = new List<string>();
            ctx.PERSON.Load();
            ctx.PERSON_RANKS.Load();
            ctx.DIC_RANKS.Load();

            int pr = (from c in ctx.PERSON where c.PR_NAME == name select c.PR_ID).First();
            var ar = (from c in ctx.PERSON_RANKS where c.PRS_PR == pr select c.PRS_DRK).ToArray();
            foreach(var c in ar)
            {
                t.Add((from d in ctx.DIC_RANKS where d.DRK_ID == c select d.DRK_NAME).First());
            }

            return t;
        }
        static public List<string> GetAllPeopleAndAudiences()
        {
            List<string> t = new List<string>();
            ctx.PERSON_AUDIENCE.Load();
            ctx.PERSON.Load();
            ctx.DIC_AUDIENCE.Load();

            var people = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToArray();
            foreach (var c in people)
            {
                if ((from d in ctx.PERSON_AUDIENCE where d.PAU_PR == c.PR_ID select d).Any())
                {
                    var ar = (from d in ctx.PERSON_AUDIENCE where d.PAU_PR == c.PR_ID select d).ToArray();
                    string str = String.Empty;
                    str += c.PR_NAME + " ->";
                    foreach (var g in ar)
                    {
                        str += " " + (from w in ctx.DIC_AUDIENCE where g.PAU_DAU == w.DAU_ID select w.DAU_NAME).First() + ";";
                    }
                    t.Add(str);
                }
            }

            return t;
        }
        static public List<string> GetCheckedAudiencesByName(string name)
        {
            List<string> t = new List<string>();
            ctx.PERSON.Load();
            ctx.PERSON_AUDIENCE.Load();
            ctx.DIC_AUDIENCE.Load();

            int pr = (from c in ctx.PERSON where c.PR_NAME == name select c.PR_ID).First();
            var ar = (from c in ctx.PERSON_AUDIENCE where c.PAU_PR == pr select c.PAU_DAU).ToArray();
            foreach (var c in ar)
            {
                t.Add((from d in ctx.DIC_AUDIENCE where d.DAU_ID == c select d.DAU_NAME).First());
            }

            return t;
        }

        static public bool ChairIsInBD(string str)
        {
            bool t = new bool();
            ctx.DIC_CHAIRS.Load();
            t = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == str select c).Any();
            return t;
        }
        static public void AddChair(string name)
        {
            try
            {
                DIC_CHAIRS ch = new DIC_CHAIRS();
                ch.DCH_NAME = name;
                ctx.DIC_CHAIRS.Add(ch);
                ctx.SaveChanges();
            }
            catch(System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public bool AudienceIsInBD(string str)
        {
            bool t = new bool();
            ctx.DIC_AUDIENCE.Load();
            t = (from c in ctx.DIC_AUDIENCE where c.DAU_NAME == str select c).Any();
            return t;
        }

        static public void AddAudience(string name)
        {
            DIC_AUDIENCE aud = new DIC_AUDIENCE();
            aud.DAU_NAME = name;
            ctx.DIC_AUDIENCE.Add(aud);
            ctx.SaveChanges();
        }

        static public bool DegreeIsInBD(string str)
        {
            bool t = new bool();
            ctx.DIC_DEGREE.Load();
            t = (from c in ctx.DIC_DEGREE where c.DDG_NAME == str select c).Any();
            return t;
        }
        static public void AddDegree(string name)
        {
            DIC_DEGREE dg = new DIC_DEGREE();
            dg.DDG_NAME = name;
            ctx.DIC_DEGREE.Add(dg);
            ctx.SaveChanges();
        }
        static public bool RankIsInBD(string str)
        {
            bool t = new bool();
            ctx.DIC_RANKS.Load();
            t = (from c in ctx.DIC_RANKS where c.DRK_NAME == str select c).Any();
            return t;
        }
        static public void AddRank(string name)
        {
            DIC_RANKS r = new DIC_RANKS();
            r.DRK_NAME = name;
            ctx.DIC_RANKS.Add(r);
            ctx.SaveChanges();
        }

        static public bool PersonIsInBD(string name)
        {
            bool t = new bool();
            ctx.PERSON.Load();
            t = (from c in ctx.PERSON where c.PR_NAME == name select c).Any();
            return t;
        }
        static public void AddPerson(string name,string chair, string position , string degree, string telephone, string interests)
        {
            PERSON p = new PERSON();
            p.PR_NAME = name;
            p.PR_POS = position;
            p.PR_PH = telephone;
            p.PR_INT = interests;
            
            int i = (from c in ctx.DIC_DEGREE where c.DDG_NAME == degree select c.DDG_ID).First();
            p.PR_DDG = i;
            
            //1
            i = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == chair select c.DCH_ID).First();
            p.PR_DCH = i;
            //2
            p.DIC_CHAIRS = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == chair select c).First();
            ctx.PERSON.Add(p);
            ctx.SaveChanges();
        }

        static public void AddChairAndHead(string chair, string head)
        {
            ctx.PERSON.Load();
            ctx.PERSON_CHAIR.Load();
            ctx.DIC_CHAIRS.Load();

            int ch = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == chair select c.DCH_ID).First();
            int pr = (from c in ctx.PERSON where c.PR_NAME == head select c.PR_ID).First();

            if( (from c in ctx.PERSON_CHAIR where c.PC_CH == ch select c).Any() )
            {
                var f = (from c in ctx.PERSON_CHAIR where c.PC_CH == ch select c).ToArray();
                foreach(var c in f)
                {
                    ctx.PERSON_CHAIR.Remove(c);
                }
            }
            PERSON_CHAIR prch = new PERSON_CHAIR();
            prch.PC_CH = ch;
            prch.PC_PR = pr;
            ctx.PERSON_CHAIR.Add(prch);
            ctx.SaveChanges();
        }
        static public void AddPersonAndRanks(string name, List<string> ranks)
        {
            ctx.DIC_RANKS.Load();
            ctx.PERSON.Load();
            ctx.PERSON_RANKS.Load();

            int pr = (from c in ctx.PERSON where c.PR_NAME == name select c.PR_ID).First();

            var oldRel = (from c in ctx.PERSON_RANKS where c.PRS_PR == pr select c).ToArray();
            foreach(var c in oldRel)
            {
                ctx.PERSON_RANKS.Remove(c);
            }
            ctx.SaveChanges();
            foreach(var s in ranks)
            {
                PERSON_RANKS rel = new PERSON_RANKS();
                rel.PRS_PR = pr;
                rel.PRS_DRK = (from c in ctx.DIC_RANKS where c.DRK_NAME == s select c.DRK_ID).First();
                rel.DIC_RANKS = (from c in ctx.DIC_RANKS where c.DRK_NAME == s select c).First();
                rel.PERSON = (from c in ctx.PERSON where c.PR_NAME == name select c).First();
                ctx.PERSON_RANKS.Add(rel);
                ctx.SaveChanges();
            }
            
        }
        static public void AddPersonAndAudiences(string name, List<string> audiences)
        {
            ctx.DIC_AUDIENCE.Load();
            ctx.PERSON.Load();
            ctx.PERSON_AUDIENCE.Load();

            int pr = (from c in ctx.PERSON where c.PR_NAME == name select c.PR_ID).First();

            var oldRel = (from c in ctx.PERSON_AUDIENCE where c.PAU_PR == pr select c).ToArray();
            foreach (var c in oldRel)
            {
                ctx.PERSON_AUDIENCE.Remove(c);
            }
            ctx.SaveChanges();
            foreach (var s in audiences)
            {
                PERSON_AUDIENCE rel = new PERSON_AUDIENCE();
                rel.PAU_PR = pr;
                rel.PAU_DAU = (from c in ctx.DIC_AUDIENCE where c.DAU_NAME == s select c.DAU_ID).First();
                rel.DIC_AUDIENCE = (from c in ctx.DIC_AUDIENCE where c.DAU_NAME == s select c).First();
                rel.PERSON = (from c in ctx.PERSON where c.PR_NAME == name select c).First();
                ctx.PERSON_AUDIENCE.Add(rel);
                ctx.SaveChanges();
            }
            
        }

        static public void RemovePerson(string name)
        {
            ctx.PERSON.Load();
            ctx.PERSON_AUDIENCE.Load();
            ctx.PERSON_CHAIR.Load();
            ctx.PERSON_RANKS.Load();

            var pr = (from c in ctx.PERSON where c.PR_NAME == name select c.PR_ID).First();

            var a = (from c in ctx.PERSON_AUDIENCE where c.PAU_PR == pr select c).ToArray();
            foreach(var c in a)
            {
                ctx.PERSON_AUDIENCE.Remove(c);
                ctx.SaveChanges();
            }

            var b = (from c in ctx.PERSON_CHAIR where c.PC_PR == pr select c).ToArray();
            foreach(var c in b)
            {
                ctx.PERSON_CHAIR.Remove(c);
                ctx.SaveChanges();
            }

            var d = (from c in ctx.PERSON_RANKS where c.PRS_PR == pr select c).ToArray();
            foreach(var c in d)
            {
                ctx.PERSON_RANKS.Remove(c);
                ctx.SaveChanges();
            }

            ctx.PERSON.Remove((from c in ctx.PERSON where c.PR_ID==pr select c).First());
            ctx.SaveChanges();
        }

        static public bool IsAllowToRemoveChair(string name)
        {
            ctx.DIC_CHAIRS.Load();
            ctx.PERSON.Load();

            var ch = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == name select c.DCH_ID).First();

            if((from c in ctx.PERSON where c.PR_DCH == ch select c).Any())
            {
                return false;
            }

            return true;
        }
        static public void RemoveChair(string name)
        {
            ctx.DIC_CHAIRS.Load();
            ctx.PERSON_CHAIR.Load();

            var ch = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == name select c.DCH_ID).First();

            var w = (from c in ctx.PERSON_CHAIR where c.PC_CH == ch select c).ToArray();
            foreach(var c in w)
            {
                ctx.PERSON_CHAIR.Remove(c);
                ctx.SaveChanges();
            }
            ctx.DIC_CHAIRS.Remove((from c in ctx.DIC_CHAIRS where c.DCH_NAME == name select c).First());
            ctx.SaveChanges();
        }
        static public bool IsAllowToRemoveAudience(string name)
        {
            ctx.DIC_AUDIENCE.Load();
            ctx.PERSON_AUDIENCE.Load();

            var au = (from c in ctx.DIC_AUDIENCE where c.DAU_NAME == name select c.DAU_ID).First();

            if( (from c in ctx.PERSON_AUDIENCE where c.PAU_DAU == au select c).Any() )
            {
                return false;
            }

            return true;
        }

        static public void RemoveAudience(string name)
        {
            ctx.DIC_AUDIENCE.Load();
            ctx.DIC_AUDIENCE.Remove((from c in ctx.DIC_AUDIENCE where c.DAU_NAME == name select c).First());
            ctx.SaveChanges();
        }

        static public bool IsAllowToRemoveDegree(string name)
        {
            ctx.PERSON.Load();
            ctx.DIC_DEGREE.Load();

            var dg = (from c in ctx.DIC_DEGREE where c.DDG_NAME == name select c.DDG_ID).First();

            if( (from c in ctx.PERSON where c.PR_DDG == dg select c).Any() )
            {
                return false;
            }

            return true;
        }
        static public void RemoveDegeree(string name)
        {
            ctx.DIC_DEGREE.Load();
            ctx.DIC_DEGREE.Remove((from c in ctx.DIC_DEGREE where c.DDG_NAME == name select c).First());
            ctx.SaveChanges();
        }
        static public bool IsAllowToRemoveRank(string name)
        {
            ctx.DIC_RANKS.Load();
            ctx.PERSON_RANKS.Load();

            var au = (from c in ctx.DIC_RANKS where c.DRK_NAME == name select c.DRK_ID).First();

            if ((from c in ctx.PERSON_RANKS where c.PRS_DRK == au select c).Any())
            {
                return false;
            }

            return true;
        }

        static public void RemoveRank(string name)
        {
            ctx.DIC_RANKS.Load();
            ctx.DIC_RANKS.Remove((from c in ctx.DIC_RANKS where c.DRK_NAME == name select c).First());
            ctx.SaveChanges();
        }
        static public List<string> GetInfoAboutPerson(string name)
        {
            List<string> res = new List<string>();
            string str = String.Empty;
            ctx.DIC_AUDIENCE.Load();
            ctx.DIC_CHAIRS.Load();
            ctx.DIC_DEGREE.Load();
            ctx.DIC_RANKS.Load();
            ctx.PERSON.Load();
            ctx.PERSON_AUDIENCE.Load();
            ctx.PERSON_CHAIR.Load();
            ctx.PERSON_RANKS.Load();

            var person = (from c in ctx.PERSON where c.PR_NAME == name select c).First();
            int i = person.PR_ID;

            res.Add("Name: " + person.PR_NAME);
            i = person.PR_DCH;
            res.Add("Chair: " + (from c in ctx.DIC_CHAIRS where c.DCH_ID == i select c ).First().DCH_NAME);
            res.Add("Position: " + person.PR_POS);
            i = person.PR_DDG;
            res.Add("Degree: "+ (from c in ctx.DIC_DEGREE where c.DDG_ID == i select c).First().DDG_NAME);
            res.Add("Phone number: " + person.PR_PH);
            res.Add("Interests: " + person.PR_INT);
            var r = GetCheckedRanksByName(name);
            str = "Ranks:";
            foreach(var c in r)
            {
                str += " " + c + ";";
            }
            res.Add(str);
            str = "Audiences:";
            var d = GetCheckedAudiencesByName(name);
            foreach(var c in d)
            {
                str += " " + c + ";";
            }
            res.Add(str);
            res.Add("--------------------");
            return res;
        }

        static public List<string> Search(string name,string chair,string degree,string rank, string audience)
        {
            List<string> result = new List<string>();

            List<PERSON> list1 = new List<PERSON>();
            List<PERSON> list2 = new List<PERSON>();
            List<PERSON> list3 = new List<PERSON>();
            List<PERSON> list4 = new List<PERSON>();
            List<PERSON> list5 = new List<PERSON>();

            if(name == String.Empty)
            {
                list1 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                list1 = (from c in ctx.PERSON where c.PR_NAME == name select c).ToList();
            }

            if(chair == String.Empty)
            {
                list2 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                int ch = (from c in ctx.DIC_CHAIRS where c.DCH_NAME == chair select c.DCH_ID).First();
                list2 = (from c in ctx.PERSON where ch == c.PR_DCH select c).ToList();
            }

            if(degree == String.Empty)
            {
                list3 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                int dg = (from c in ctx.DIC_DEGREE where c.DDG_NAME == degree select c.DDG_ID).First();
                list3 = (from c in ctx.PERSON where c.PR_DDG == dg select c).ToList();
            }

            if(rank == String.Empty)
            {
                list4 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                int r = (from c in ctx.DIC_RANKS where c.DRK_NAME == rank select c.DRK_ID).First();
                var pr = (from c in ctx.PERSON_RANKS where c.PRS_DRK == r select c.PRS_PR).ToList();
                list4 = (from c in ctx.PERSON where pr.Contains(c.PR_ID) select c).ToList();
            }

            if(audience == String.Empty)
            {
                list5 = (from c in ctx.PERSON where c.PR_ID == c.PR_ID select c).ToList();
            }
            else
            {
                int au = (from c in ctx.DIC_AUDIENCE where c.DAU_NAME == audience select c.DAU_ID).First();
                var pr = (from c in ctx.PERSON_AUDIENCE where c.PAU_DAU == au select c.PAU_PR).ToList();
                list5 = (from c in ctx.PERSON where pr.Contains(c.PR_ID) select c).ToList();
            }

            foreach(var c in list1)
            {
                if(list2.Contains(c)&&list3.Contains(c)&&list4.Contains(c)&&list5.Contains(c))
                {
                    var st = GetInfoAboutPerson(c.PR_NAME);
                    foreach(var g in st)
                    {
                        result.Add(g);
                    }
                }
            }
             

            return result;
        }
        static public void CreateXML(string name, string chair, string degree, string rank, string audience,string nameXML)
        {
            List<PERSON> result = new List<PERSON>();

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

            foreach (var c in list1)
            {
                if (list2.Contains(c) && list3.Contains(c) && list4.Contains(c) && list5.Contains(c))
                {
                    result.Add(c);
                }
            }
            // we know result. so we have to create XML

            if(System.IO.File.Exists(nameXML))
            {
                System.IO.File.Delete(nameXML);
            }
             string ar = 
               "<?xml version=\"1.0\" encoding=\"utf-8\"?>"+
"<transactionDataBase>"+
    
        "<employee ID =\"0\" "+
           "NAME=\"example\" " +
           "CHAIR=\"example\" " +
           "POSITION =\"example\" "+ 
           "DEGREE =\"example\" "+  
           "PHONE=\"example\" "+
           "INTERESTS =\"example\" >" +
        "</employee>" +
            "<rank RANK=\"example\"></rank>"+
            "<audience NAME=\"example\"></audience>" +
        
    
"</transactionDataBase>";

             XmlDocument doc_that_we_create = new XmlDocument();
             doc_that_we_create.LoadXml(ar);
             doc_that_we_create.Save(@"" + nameXML + ".xml");

             XmlDocument doc = new XmlDocument();
             doc.Load(@"" + nameXML + ".xml");
             XmlNode parent = doc.SelectNodes("//transactionDataBase")[0];

             XmlNode etPerson = doc.SelectNodes("//employee")[0];
             XmlNode etRank = doc.SelectNodes("//rank")[0];
             XmlNode etAudience = doc.SelectNodes("//audience")[0];

             var res = result.ToArray();

             for (int i = 0; i < res.Length; i++ )
             {
                 XmlNode _newPerson = etPerson.Clone();
                 _newPerson.Attributes["ID"].Value = (i + 1).ToString();
                 _newPerson.Attributes["NAME"].Value = res[i].PR_NAME;
                 _newPerson.Attributes["PHONE"].Value = res[i].PR_PH;
                 _newPerson.Attributes["INTERESTS"].Value = res[i].PR_INT;
                 _newPerson.Attributes["POSITION"].Value = res[i].PR_POS;
                 int t = res[i].PR_DCH;
                 _newPerson.Attributes["CHAIR"].Value = (from c in ctx.DIC_CHAIRS where c.DCH_ID == t select c.DCH_NAME).First().ToString();
                  t = res[i].PR_DDG;
                  _newPerson.Attributes["DEGREE"].Value = (from c in ctx.DIC_DEGREE where c.DDG_ID == t select c.DDG_NAME).First().ToString();

                  List<XmlNode> ranks = new List<XmlNode>();
                  List<XmlNode> aud = new List<XmlNode>();
                  t = res[i].PR_ID;

                  var au = (from c in ctx.PERSON_AUDIENCE where c.PAU_PR == t select c.DIC_AUDIENCE.DAU_NAME).ToList();
                  var r = (from c in ctx.PERSON_RANKS where c.PRS_PR == t select c.DIC_RANKS.DRK_NAME).ToList();

                 foreach( var s in au)
                 {
                     var _newaud = etAudience.Clone();
                     _newaud.Attributes["NAME"].Value = s;
                     aud.Add(_newaud);
                 }

                 foreach (var s in r)
                 {
                     var _newaud = etRank.Clone();
                     _newaud.Attributes["RANK"].Value = s;
                     ranks.Add(_newaud);
                 }

                 foreach(var s in ranks)
                 {
                     _newPerson.AppendChild(s);
                 }
                 foreach (var s in aud)
                 {
                     _newPerson.AppendChild(s);
                 }
                 parent.AppendChild(_newPerson);
                 doc.Save(@"" + nameXML + ".xml");
                 
             }

             XmlNode r1 = doc.SelectNodes("//employee")[0]; 
             XmlNode r2 = doc.SelectNodes("//rank")[0]; 
             XmlNode r3 = doc.SelectNodes("//audience")[0];


             parent.RemoveChild(r1);
             parent.RemoveChild(r2);
             parent.RemoveChild(r3);
             doc.Save(@"" + nameXML + ".xml");

                 return;  
        }
        
    }
}
