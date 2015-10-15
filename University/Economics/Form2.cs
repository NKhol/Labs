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
    public partial class Form2 : Form
    {
        Form1 par;
        public Form2(Form1 p)
        {
            par = p;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if((radioButton1.Checked) || (radioButton2.Checked))
            {
                double sum = (double)numericUpDown1.Value + ((double)numericUpDown2.Value) / 100.0;
                if(sum > 0 )
                {
                    Transaction tr = new Transaction();
                    tr.setSum(sum);
                    if (radioButton1.Checked)
                        tr.setType(1);
                    else tr.setType(0);
                    tr.setComent(textBox1.Text);
                    MainManager.AddTransaction(tr);
                    MainManager.FillAllTransactions();
                    par.DrawAll();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong transaction", "Error");
                }
            }
            else { MessageBox.Show("Fill Direction", "Error"); }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
