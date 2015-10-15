using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Economics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void DrawAll()
        {
            try
            {
                dataGridView1.Rows.Clear();
                Transaction[] list = MainManager.GetAllTransactions().ToArray();
                for (int i = 1; i < list.Length; i++)
                {
                    AddTransactionToTable(list[i]);
                }
                double sum = MainManager.GetBalance();
                string str = (Math.Abs(((int)(sum * 100)) % 100)).ToString();
                if (str.Length < 2) str = "0" + str;
                label2.Text = ((int)sum).ToString() + "." + str;
            }
            catch (System.Exception) { }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MainManager.FillAllTransactions();
            DrawAll();
        }

        private void AddTransactionToTable(Transaction tr)
        {
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[@"ID"].Value = tr.getID().ToString();
            string str = tr.getMonth().ToString();
            if (str.Length < 2) str = "0" + str;
            dataGridView1.Rows[index].Cells[@"Date"].Value = tr.getDay().ToString() + "." + str + "." + tr.getYear().ToString();
            if(tr.getType() == 1)
            dataGridView1.Rows[index].Cells[@"Type"].Value = "IN";
            else
            dataGridView1.Rows[index].Cells[@"Type"].Value = "OUT";
            double sum = tr.getSum();
            str = (Math.Abs(((int)(sum * 100)) % 100)).ToString();
            if (str.Length < 2) str = "0" + str;
            dataGridView1.Rows[index].Cells[@"Sum"].Value = ((int)sum).ToString() + "." + str;
            sum = tr.getBalance();
            str = (Math.Abs(((int)(sum * 100)) % 100)).ToString();
            if (str.Length < 2) str = "0" + str;
            dataGridView1.Rows[index].Cells[@"Balance"].Value = ((int)sum).ToString() + "." + str;
            dataGridView1.Rows[index].Cells[@"Coment"].Value = tr.getComent();
            
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrawAll();
        }
    }
}
