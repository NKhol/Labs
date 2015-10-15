using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Economics
{
    class MainManager
    {
        static string ar = 
               "<?xml version=\"1.0\" encoding=\"utf-8\"?>"+
               "<transactionDataBase>"+
               "<transaction ID=\"0\" TYPE=\"IN\" COMENT=\"It's example\" SUM=\"0\" BALANCE=\"0\">"+
               "<date DAY=\"14\" MONTH=\"4\" YEAR=\"2015\">"+
               "</date>"+
               "</transaction>"+
               "</transactionDataBase>";
        static List<Transaction> _history = new List<Transaction>();
        static string pathToContent = "dataBaseEconomics.xml";
        static XmlDocument dataBase = new XmlDocument();
        static int maxId = 0;
        static double balance = 0;

        public static bool FillAllTransactions()
        {
            try
            {
                _history.Clear();
                dataBase.Load(@"" + pathToContent);
                XmlNodeList trans = dataBase.SelectNodes(@"//transaction");
                foreach(XmlNode node in trans)
                {
                    Transaction transact = new Transaction();
                    XmlAttributeCollection atributes = node.Attributes;
                    int id = Convert.ToInt32(atributes[@"ID"].Value.ToString());
                    if (id > maxId) maxId = id;
                    transact.setID(id);
                    string type = (atributes[@"TYPE"].Value.ToString());
                    if (type == "IN")
                        transact.setType(1);
                    else
                        transact.setType(0);
                    transact.setSum(Convert.ToDouble(atributes[@"SUM"].Value.ToString()));
                    balance = Convert.ToDouble(atributes[@"BALANCE"].Value.ToString());
                    transact.setBalance(balance);
                    transact.setComent(atributes[@"COMENT"].Value.ToString());
                    XmlNodeList childs =  node.ChildNodes;
                    transact.setDate(Convert.ToInt32(childs[0].Attributes[@"DAY"].Value.ToString()),
                                     Convert.ToInt32(childs[0].Attributes[@"MONTH"].Value.ToString()),
                                     Convert.ToInt32(childs[0].Attributes[@"YEAR"].Value.ToString())
                                    );
                    _history.Add(transact);
                }
            }
            catch (System.Exception) { 
            MessageBox.Show("New DataBase was created", "Problems with database connection");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ar);
            doc.Save(@"" + pathToContent);
            //File.SetAttributes(@"" + pathToContent, File.GetAttributes(@"" + pathToContent) | FileAttributes.Hidden | (~FileAttributes.ReadOnly));
            FillAllTransactions();
            }
            return true;
        }
        public static double GetBalance()
        {
            return balance;
        }
        public static List<Transaction> GetAllTransactions()
        {
            return _history;
        }

        public static bool AddTransaction(Transaction tr)
        {
            File.SetAttributes(@"" + pathToContent, File.GetAttributes(@"" + pathToContent) & (~ FileAttributes.Hidden) & (~FileAttributes.ReadOnly));
            dataBase.Load(@"" + pathToContent);
            XmlNodeList trans = dataBase.SelectNodes(@"//transaction");
            XmlNode node = trans[0].Clone();
            XmlAttributeCollection atributes = node.Attributes;
            maxId++;
            atributes[@"ID"].Value = maxId.ToString();
            if (tr.getType() == 1)
            {
                atributes[@"TYPE"].Value = "IN";
                balance += tr.getSum();
            }
            else
            {
                atributes[@"TYPE"].Value = "OUT";
                balance -= tr.getSum();
            }
            atributes[@"COMENT"].Value = tr.getComent();
            atributes[@"SUM"].Value = tr.getSum().ToString();
            atributes[@"BALANCE"].Value = balance.ToString();
            DateTime day = DateTime.Now;
            XmlNodeList childs = node.ChildNodes;
            childs[0].Attributes[@"DAY"].Value = day.Day.ToString();
            childs[0].Attributes[@"MONTH"].Value = day.Month.ToString();
            childs[0].Attributes[@"YEAR"].Value = day.Year.ToString();
            XmlNode root = trans[0].ParentNode;
            root.AppendChild(node);
            try
            {
                dataBase.Save(@"" + pathToContent);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            File.SetAttributes(@"" + pathToContent, File.GetAttributes(@"" + pathToContent) | FileAttributes.Hidden);
    
            return true;
        }
    }
}
