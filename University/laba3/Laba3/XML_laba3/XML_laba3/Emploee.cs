using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_laba3
{
    class Emploee
    {
        public string Department { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Degree { get; set; }
        public List<string> rank = new List<string>();
        string audience;
        string letter = String.Empty;
        string phone;
        public string Interests { set; get; }
        public Emploee() 
        {
            Department = String.Empty;
            Name = String.Empty;
            Position = String.Empty;
            Degree = String.Empty;
            Interests = String.Empty;
        }
        public void SetAudience(string aud,string lett)
        {
            audience = aud;
            letter = lett;
            if (audience == null) { audience = String.Empty; }
            if (letter == null) { letter = String.Empty; }
        }
        public string GetNumberOfAudience()
        {
            if (audience == null) { return String.Empty; }
            return audience;
        }
        public string GetLetterOfAudience()
        {
            if (letter == null) { return String.Empty; }
            return letter;
        }
        public void SetPhone(string phone)
        {
            this.phone = GetOnlyNumbersFromPhone(phone); 
        }
        public string GetPhone()
        {
            if (phone == null) { return String.Empty; }
            return phone;
        }
        static public string GetOnlyNumbersFromPhone(string phone)
        {
            string newNumber = String.Empty;
            if(phone!=null)
            {
                for(int i = 0 ; i < phone.Length ; i++)
                {
                    if(phone[i]=='0'||phone[i]=='1'||phone[i]=='2'||phone[i]=='3'||phone[i]=='4'||phone[i]=='5'||phone[i]=='6'||
                        phone[i] == '7' || phone[i] == '8' || phone[i] == '9')
                    {
                        newNumber = newNumber + phone[i];
                    }
                }
            }
            return newNumber;
        }
        public bool Eq(Emploee e)
        {
            if(e.Department == Department&&
                e.Degree==Degree&&
                e.audience==audience              &&
                e.GetPhone()==GetPhone()&&
                e.Interests==Interests&&
                e.Name==Name&&
                e.Position==Position
                )
            {
                return true;
            }
            return false;
        }

    }
}
