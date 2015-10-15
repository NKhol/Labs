using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economics
{
    struct MyDate{
        public int day;
        public int month;
        public int year;
    };
    class Transaction
    {
        int _id;
        int _type;              //   0 - out  , 1 - in 
        string _coment;
        MyDate _date;
        double _sum;
        double _balance;
        public Transaction()
        { }
        public bool setID (int id)
        {
            _id = id;
            return true;
        }
        public bool setType(int type)
        {
            _type = type;
            return true;
        }
        public bool setComent(string coment)
        {
            _coment = coment;
            return true;
        }
        public bool setDate(int day , int month , int year)
        {
            if (day > 31 || month > 12) return false;
           // DateTime now = DateTime.Now;
            //if (year > now.Year) return false;
            _date.day = day;
            _date.month = month;
            _date.year = year;
            return true;
        }
        public bool setSum(double sum)
        {
            _sum = sum;
            return true;
        }
        public bool setBalance(double balance)
        {
            _balance = balance;
            return true;
        }

        public int getID()
        {
            return _id;
        }
        public int getType()
        {
            return _type;
        }
        public string getComent()
        {
            return _coment;
        }
        public int getDay()
        {
            return _date.day;
        }
        public int getMonth()
        {
            return _date.month;
        }
        public int getYear()
        {
            return _date.year;
        }
        public double getSum()
        {
            return _sum;
        }
        public double getBalance()
        {
            return _balance ;
        }
    }
}
