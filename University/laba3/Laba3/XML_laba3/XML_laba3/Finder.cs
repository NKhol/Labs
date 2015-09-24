using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Xml.Xsl;


namespace XML_laba3
{
    class Finder
    {
        // ON Load

        static public string[] GetAllDepartments()
        {
            List<string> list = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\Base.xml");
            XmlNodeList nodes = xmlDoc.SelectNodes("//department/@NANE");
            foreach(XmlNode n in nodes)
            {
                if (list.Contains(n.Value) != true) { list.Add(n.Value); }
            }
            return list.ToArray();
        }
        static public string[] GetAllPositions()
        {
            List<string> list = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\Base.xml");
            XmlNodeList nodes = xmlDoc.SelectNodes("//employee/@POSITION");
            foreach (XmlNode n in nodes)
            {
                if (list.Contains(n.Value) != true && n.Value!=String.Empty) { list.Add(n.Value); }
            }
            return list.ToArray();
        }
        static public string[] GetAllDegree()
        {
            List<string> list = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\Base.xml");
            XmlNodeList nodes = xmlDoc.SelectNodes("//employee/@DEGREE");
            foreach (XmlNode n in nodes)
            {
                if (list.Contains(n.Value) != true && n.Value != String.Empty) { list.Add(n.Value); }
            }
            return list.ToArray();
        }
        static public string[] GetAllRank()
        {
            List<string> list = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\Base.xml");
            XmlNodeList nodes = xmlDoc.SelectNodes("//rank/@RANK");
            foreach (XmlNode n in nodes)
            {
                if (list.Contains(n.Value) != true && n.Value != String.Empty) { list.Add(n.Value); }
            }
            return list.ToArray();
        }
        // end of ON Load
        static private List<Emploee> CrossingOfResults(List<List<Emploee>> list)
        {
            List<Emploee> result = new List<Emploee>();
            try
            {
                if (list != null)
                {
                    Emploee[] em = list[0].ToArray();
                    if (em != null)
                    {
                        foreach (Emploee ele in em)
                        {
                            bool isIN = true;
                            foreach (List<Emploee> t in list)
                            {
                                if (t.Count <= 0) { return new List<Emploee>(); }
                                foreach (Emploee e in t)
                                {
                                    isIN = false;
                                    if (ele.Eq(e)) { isIN = true; break; }
                                }
                                if (isIN == false) { break; }
                            }
                            if (isIN == true) { result.Add(ele); }
                        }
                    }
                }
            }
            catch { }
            return result;
        }
        static public List<Emploee> SearchByLINQ(Emploee empl)
        {
            List<Emploee> results = new List<Emploee>();
            XDocument docXML = XDocument.Load(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\Base.xml");
            var result = (from obj in docXML.Descendants("employee")    
                          where (
                          
                          (empl.Name == String.Empty || empl.Name == obj.Attribute("NAME").Value) &&
                          (empl.Position == String.Empty || empl.Position == obj.Attribute("POSITION").Value) &&
                          (empl.Degree == String.Empty || empl.Degree == obj.Attribute("DEGREE").Value) &&
                          ((empl.GetLetterOfAudience() == CutNumbers(obj.Attribute("AUDIENCE").Value) &&
                          empl.GetNumberOfAudience() == Emploee.GetOnlyNumbersFromPhone(obj.Attribute("AUDIENCE").Value)) ||
                          empl.GetNumberOfAudience() == String.Empty) &&
                          (empl.GetPhone() == String.Empty || empl.GetPhone() == Emploee.GetOnlyNumbersFromPhone( obj.Attribute("PHONE").Value)) &&
                          (empl.Interests == String.Empty || empl.Interests == obj.Attribute("INTERESTS").Value) &&
                          (empl.Department == String.Empty || empl.Department == obj.Parent.Attribute("NANE").Value) &&
                          (empl.rank.Count <= 0 || obj.Descendants().Any(elem => (elem.Attribute("RANK").Value == empl.rank[0])))
                          
                          )
                          
                          select obj
                                      ).ToList();

            foreach(var r in result)
            {
                Emploee e = new Emploee();
                e.Name = r.Attribute("NAME").Value;
                e.Position = r.Attribute("POSITION").Value;
                e.Degree = r.Attribute("DEGREE").Value;
                e.Department = r.Parent.Attribute("NANE").Value;
                e.SetAudience(Emploee.GetOnlyNumbersFromPhone(r.Attribute("AUDIENCE").Value), CutNumbers(r.Attribute("AUDIENCE").Value));
                e.SetPhone(r.Attribute("PHONE").Value);
                e.Interests = r.Attribute("INTERESTS").Value;
                var ranks = r.Descendants();
                foreach(var rank in ranks)
                {
                    e.rank.Add(rank.Attribute("RANK").Value);
                }
                results.Add(e);
            }

            return results;
        }
        static public List<Emploee> SearchBySAX(Emploee empl)
        {
            List<Emploee> results = new List<Emploee>();
            string dep = String.Empty;
            var xmlReader = new XmlTextReader(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\Base.xml");
            Emploee betw = null; 
            while(xmlReader.Read())
            { 
                if (xmlReader.Name == "department") { while (xmlReader.MoveToNextAttribute())
                { if (xmlReader.Name == "NANE") { dep = xmlReader.Value;
                
                } }
                }
                if(xmlReader.Name == "employee")
                {
                    if(betw == null)
                    {
                        betw = new Emploee();
                        betw.Department = dep;
                    }
                    else
                    {
                        if(betw.Name!=String.Empty)
                        results.Add(betw);
                        betw = new Emploee();
                        betw.Department = dep;
                    }
                    if(xmlReader.HasAttributes)
                    while(xmlReader.MoveToNextAttribute())
                    {
                        if (xmlReader.Name == "NAME") {if(xmlReader.HasValue) betw.Name = xmlReader.Value; }
                        if (xmlReader.Name == "DEGREE") { if (xmlReader.HasValue) betw.Degree = xmlReader.Value; }
                        if (xmlReader.Name == "POSITION") { if (xmlReader.HasValue) betw.Position = xmlReader.Value; }
                        if (xmlReader.Name == "INTERESTS") { if (xmlReader.HasValue) betw.Interests = xmlReader.Value; }
                        if (xmlReader.Name == "PHONE") { if (xmlReader.HasValue)betw.SetPhone(xmlReader.Value); }
                        if (xmlReader.Name == "AUDIENCE") { if (xmlReader.HasValue) betw.SetAudience(Emploee.GetOnlyNumbersFromPhone(xmlReader.Value), CutNumbers(xmlReader.Value)); }
                    }
                }
              
                
                    if (xmlReader.Name == "rank")
                    {
                        while (xmlReader.MoveToNextAttribute())
                        {
                            if (xmlReader.HasValue)
                                betw.rank.Add(xmlReader.Value);
                        }
                    }
                

            }
            // now we have all employees



            return FiltrByEmployee(results,empl);
        }
        static List<Emploee> FiltrByEmployee(List<Emploee> allEmpl, Emploee param)
        {
            List<Emploee> result = new List<Emploee>();
            if(allEmpl!=null)
            {
                foreach(Emploee e in allEmpl)
                {
                    try
                    {
                        if ((e.Name == param.Name || param.Name == String.Empty) &&
                            (e.Interests == param.Interests || param.Interests == String.Empty) &&
                            (e.Position == param.Position || param.Position == String.Empty) &&
                            (e.Department == param.Department || param.Department == String.Empty) &&
                            (e.Degree == param.Degree || param.Degree == String.Empty) &&
                            (e.GetPhone() == param.GetPhone() || param.GetPhone() == String.Empty) &&
                            ((e.GetLetterOfAudience() == param.GetLetterOfAudience() && e.GetNumberOfAudience() == param.GetNumberOfAudience()) || (param.GetNumberOfAudience() == String.Empty))

                            )
                        {
                            if (e.rank.Count <= 0 || param.rank.Count <=0)
                            {
                                result.Add(e);
                            }
                            else
                            {
                                if(e.rank.Contains(param.rank[0]))
                                    result.Add(e);
                            }
                        }
                    }
                    catch { }
                }
            }
            return result;
        }
        //DOM
        static public List<Emploee> SearchByDOM(Emploee empl)
        {
            List<List<Emploee>> allResults = new List<List<Emploee>>();
             try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\Base.xml");
                allResults.Add(SearchByDomDepartment(empl.Department, xmlDoc));
                allResults.Add(SearchByDomName(empl.Name, xmlDoc));
                allResults.Add(SearchByDomPosition(empl.Position, xmlDoc));
                allResults.Add(SearchByDomDegree(empl.Degree, xmlDoc));
                allResults.Add(SearchByDomAudience(empl.GetNumberOfAudience(), empl.GetLetterOfAudience(), xmlDoc));
                allResults.Add(SearchByDomPhone(empl.GetPhone(), xmlDoc));
                allResults.Add(SearchByDomInterests(empl.Interests, xmlDoc));
                if (empl.rank.Count > 0) { allResults.Add(SearchByDomRank((empl.rank.ToArray())[0], xmlDoc)); }
                else
                {
                    allResults.Add(SearchByDomRank(String.Empty, xmlDoc));
                }
            }
            catch { }
            return CrossingOfResults(allResults);
        }
        static public List<Emploee> SearchByDomDepartment(string param,XmlDocument xmlDoc)
        {
            List<Emploee> r = new List<Emploee>();
            if (param != null)
            {
                if (param != String.Empty)
                {
                    XmlNodeList ele = xmlDoc.SelectNodes("//department[@NANE=\""+param+"\"]");
                    XmlNodeList elem = ele[0].ChildNodes;
                    try
                    {
                        foreach (XmlNode e in elem)
                        {
                            r.Add(GetDataAboutEmploee(e));
                        }
                    }
                    catch { }
                    return r;
                }
            }
            return GiveAllEmploeeDOM(xmlDoc.DocumentElement);
        }
        static public List<Emploee> SearchByDomName(string param , XmlDocument xmlDoc)
        {
            List<Emploee> r = new List<Emploee>();
            if (param != null)
            {
                if (param != String.Empty)
                {
                    XmlNodeList elem = xmlDoc.SelectNodes("//employee[@NAME=\"" + param + "\"]");
                    try
                    {
                        foreach (XmlNode e in elem)
                        {
                            r.Add(GetDataAboutEmploee(e));
                        }
                    }
                    catch { }
                    return r;
                }
            }
            return GiveAllEmploeeDOM(xmlDoc.DocumentElement);
        }
        static public List<Emploee> SearchByDomPosition(string param, XmlDocument xmlDoc)
        {
            List<Emploee> r = new List<Emploee>();
            if (param != null)
            {
                if (param != String.Empty)
                {
                    XmlNodeList elem = xmlDoc.SelectNodes("//employee[@POSITION=\"" + param + "\"]");
                    try
                    {
                        foreach (XmlNode e in elem)
                        {
                            r.Add(GetDataAboutEmploee(e));
                        }
                    }
                    catch { }
                    return r;
                }
            }
            return GiveAllEmploeeDOM(xmlDoc.DocumentElement);
        }
        static public List<Emploee> SearchByDomDegree(string param, XmlDocument xmlDoc)
        {
            List<Emploee> r = new List<Emploee>();
            if (param != null)
            {
                if (param != String.Empty)
                {
                    XmlNodeList elem = xmlDoc.SelectNodes("//employee[@DEGREE=\"" + param + "\"]");
                    try
                    {
                        foreach (XmlNode e in elem)
                        {
                            r.Add(GetDataAboutEmploee(e));
                        }
                    }
                    catch { }
                    return r;
                }
            }
            return GiveAllEmploeeDOM(xmlDoc.DocumentElement);
        }
        static public List<Emploee> SearchByDomAudience(string param,string let, XmlDocument xmlDoc)
        {
            List<Emploee> r = new List<Emploee>();
            if (param != null)
            {
                if (param != String.Empty)
                {
                    if (let != String.Empty) { param = param + "-" + let; }
                    XmlNodeList elem = xmlDoc.SelectNodes("//employee[@AUDIENCE=\"" + param + "\"]");
                    try
                    {
                        foreach (XmlNode e in elem)
                        {
                            r.Add(GetDataAboutEmploee(e));
                        }
                    }
                    catch { }
                    return r;
                }
            }
            return GiveAllEmploeeDOM(xmlDoc.DocumentElement);
        }
        static public List<Emploee> SearchByDomPhone(string param, XmlDocument xmlDoc)
        {
            List<Emploee> r = new List<Emploee>();
            if (param != null)
            {
                if (param != String.Empty)
                {
                    XmlNodeList ele = xmlDoc.SelectNodes("//employee");
                    
                    try
                    {
                        foreach (XmlNode e in ele)
                        {
                            if(GetDataAboutEmploee(e).GetPhone() == Emploee.GetOnlyNumbersFromPhone(param))
                            r.Add(GetDataAboutEmploee(e));
                        }
                    }
                    catch { }
                    return r;
                }
            }
            return GiveAllEmploeeDOM(xmlDoc.DocumentElement);
        }
        static public List<Emploee> SearchByDomInterests(string param, XmlDocument xmlDoc)
        {
            List<Emploee> r = new List<Emploee>();
            if (param != null)
            {
                if (param != String.Empty)
                {
                    XmlNodeList elem = xmlDoc.SelectNodes("//employee[@INTERESTS=\"" + param + "\"]");
                    try
                    {
                        foreach (XmlNode e in elem)
                        {
                            r.Add(GetDataAboutEmploee(e));
                        }
                    }
                    catch { }
                    return r;
                }
            }
            return GiveAllEmploeeDOM(xmlDoc.DocumentElement);
        }
        static public List<Emploee> SearchByDomRank(string param, XmlDocument xmlDoc)
        {
            List<Emploee> r = new List<Emploee>();
            if (param != null)
            {
                if (param != String.Empty)
                {
                    XmlNodeList ele = xmlDoc.SelectNodes("//rank[@RANK=\"" + param + "\"]");
                    List<XmlNode> el = new List<XmlNode>();
                    foreach(XmlNode t in ele)
                    {
                        el.Add(t.ParentNode);
                    }
                    XmlNode[] elem = el.ToArray();
                    try
                    {
                        foreach (XmlNode e in elem)
                        {
                            r.Add(GetDataAboutEmploee(e));
                        }
                    }
                    catch { }
                    return r;
                }
            }
            return GiveAllEmploeeDOM(xmlDoc.DocumentElement);
        }
        static public List<Emploee> GiveAllEmploeeDOM(XmlNode root)
        {
            List<Emploee> result = new List<Emploee>();
            XmlNodeList departments = root.ChildNodes;
            try
            {
                foreach (XmlNode dep in departments)
                {
                    XmlNodeList empl = dep.ChildNodes;
                    foreach (XmlNode em in empl)
                    {
                        result.Add(GetDataAboutEmploee(em));
                    }
                }
            }
            catch { }

            return result;
        }
        private static Emploee GetDataAboutEmploee(XmlNode em)
        {
            Emploee e = new Emploee();
            XmlAttributeCollection at = em.Attributes;
            e.Department = em.ParentNode.Attributes[1].Value;
            e.Name = at[1].Value;
            e.Position = at[2].Value;
            e.Degree = at[3].Value;
            e.SetAudience(Emploee.GetOnlyNumbersFromPhone(at[4].Value), CutNumbers(at[4].Value));
            e.SetPhone(at[5].Value);
            e.Interests = at[6].Value;
            XmlNodeList r = em.ChildNodes;
            foreach (XmlNode v in r)
            {
                e.rank.Add(v.Attributes[0].Value);
            }
            return e;
        }
        private static string CutNumbers(string n)
        {
            string st = String.Empty;
            if (n != null)
            {
                for(int i = 0 ; i < n.Length ; i++)
                {
                    if(n[i]!='0'&&n[i]!='1'&&n[i]!='2'&&n[i]!='3'&&n[i]!='4'&&n[i]!='5'&&n[i]!='6'&&n[i]!='7'&&n[i]!='8'&&n[i]!='9'&&n[i]!='-')
                    st = st + n[i];
                }
            }
            return st;
        }

        static public void Transform()
        {
            XslCompiledTransform transf = new XslCompiledTransform();
            transf.Load(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\transformation.xsl");
            transf.Transform(@"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\Base.xml", @"C:\Users\nazar\Documents\Visual Studio 2013\Projects\Laba3\newBase.html");
        }
        
    }
}
