using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML_laba3
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Emploee ReadParameters()
        {
            Emploee empl = new Emploee();
            if (departmentCheckBox.Checked)
            {
                if (comboBox1.SelectedItem != null)
                {
                    empl.Department = comboBox1.SelectedItem.ToString();
                }
            }
            if (nameCheckBox.Checked) { empl.Name = nameTextBox1.Text; }
            if (degreeCheckBox1.Checked) { empl.Degree = degreeTextBox1.Text; }
            if (positionCheckBox.Checked) { empl.Position = positionTextBox2.Text; }
            if (rankCheckBox1.Checked)
            {
                string[]ranks = rankTextBox1.Text.Split(new Char[]{',','.',';'});
                if(ranks!=null)
                {
                    foreach(string s in ranks)
                    {
                        empl.rank.Add(s);
                    }
                }
                else
                {
                    empl.rank.Add(String.Empty);
                }
            }
            if (audienceCheckBox1.Checked)
            { 
                empl.SetAudience(audienceNumericUpDown1.Value.ToString(), null);
                if (letterCheckBox1.Checked)
                {
                    empl.SetAudience(audienceNumericUpDown1.Value.ToString(), letterTextBox2.Text);
                }
            }
            if (phoneCheckBox1.Checked)
            {
                empl.SetPhone(phoneTextBox2.Text);
            }
            if (interestsCheckBox1.Checked)
            {
                empl.Interests = interestsTextBox1.Text;
            }


            return empl;
        }
        private void ShowEmploee(Emploee empl)
        {
            List<string> data = new List<string>();
            string s = String.Empty;
            string t = String.Empty;
            //department
            s = "Department: ";
            t = empl.Department;
            if (t == String.Empty) { t = "-"; }
            s = s + t;
            data.Add(s);
            //name
            s = "Name: ";
            t = empl.Name;
            if (t == String.Empty) { t = "-"; }
            s = s + t;
            data.Add(s);
            //position
            t = empl.Position;
            s = "Position: ";
            if (t == String.Empty) { t = "-"; }
            s = s + t;
            data.Add(s);
            //degree
            s = "Degree: ";
            t = empl.Degree;
            if (t == String.Empty) { t = "-"; }
            s = s + t;
            data.Add(s);
            //rank
            s = "Rank(s): ";
            t = String.Empty;
            string[] ar = empl.rank.ToArray();
            if(ar!=null)
            {
                foreach(string st in ar)
                {
                    if (t != String.Empty) { t = t + ", "; }
                    t = t + st;
                }
            }
            if (t == String.Empty) { t = "-"; }
            s = s + t;
            data.Add(s);
            //audience
            s = "Audience: ";
            t = empl.GetNumberOfAudience();
            if (empl.GetLetterOfAudience() != String.Empty)
            {
                t = t + " (" + empl.GetLetterOfAudience() + ")";
            }
            if (t == String.Empty) { t = "-"; }
            s = s + t;
            data.Add(s);
            //phone
            s = "Phone: ";
            t = empl.GetPhone();
            if (t == String.Empty) { t = "-"; }
            s = s + t;
            data.Add(s);
            //interests
            s = "Interests: ";
            t = empl.Interests;
            if (t == String.Empty) { t = "-"; }
            s = s + t;
            data.Add(s);
            // data is seted
            string[] str = data.ToArray();
            foreach(string h in str)
            {
                resultRichTextBox1.AppendText(h+'\n');
            }
            resultRichTextBox1.AppendText("---------------------------------------------------------------------------\n");
        }

        private void searchButton1_Click(object sender, EventArgs e)
        {
            resultRichTextBox1.Clear();
            if (domRadioButton1.Checked)
            {
                List<Emploee> r = Finder.SearchByDOM(ReadParameters());
                Emploee[] t = r.ToArray();
                foreach (Emploee empl in t)
                {
                    ShowEmploee(empl);
                }
            }
            if(linqRadioButton1.Checked)
            {
                List<Emploee> r = Finder.SearchByLINQ(ReadParameters());
                Emploee[] t = r.ToArray();
                foreach (Emploee empl in t)
                {
                    ShowEmploee(empl);
                }
            }
            if(saxRadioButton1.Checked)
            {
                List<Emploee> r = Finder.SearchBySAX(ReadParameters());
                Emploee[] t = r.ToArray();
                foreach (Emploee empl in t)
                {
                    ShowEmploee(empl);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resultRichTextBox1.Clear();
        }



        // prepearing
        private void MainWindow_Load(object sender, EventArgs e)
        {
            AddDepartments();
            AddPositions();
            AddDegree();
            AddRank();
        }
        private void AddDepartments()
        {
            string[] s = Finder.GetAllDepartments();
            if(s!=null)
            {
                comboBox1.Items.Clear();
                foreach(string st in s )
                {
                    comboBox1.Items.Add(st);
                }
            }
        }
        private void AddPositions()
        {
            string[] s = Finder.GetAllPositions();
            if (s != null)
            {
                comboBox2.Items.Clear();
                foreach (string st in s)
                {
                    comboBox2.Items.Add(st);
                }
            }
        }
        private void AddDegree()
        {
            string[] s = Finder.GetAllDegree();
            if (s != null)
            {
                comboBox3.Items.Clear();
                foreach (string st in s)
                {
                    comboBox3.Items.Add(st);
                }
            }
        }
        private void AddRank()
        {
            string[] s = Finder.GetAllRank();
            if (s != null)
            {
                comboBox4.Items.Clear();
                foreach (string st in s)
                {
                    comboBox4.Items.Add(st);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedItem!=null)
            {
                positionTextBox2.Text = comboBox2.SelectedItem.ToString();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                degreeTextBox1.Text = comboBox3.SelectedItem.ToString();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                rankTextBox1.Text = comboBox4.SelectedItem.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Finder.Transform();
        }
    }
}
