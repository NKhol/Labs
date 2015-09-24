using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using ClassLibrary;

namespace Laba5_TheLast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Manager.LoadDataBase();
            FillPeople();
            FillAudiences();
            FillDegrees();
            FillChairs();
            FillRanks();
            FillChairsAndHeads();
            FillPeopleAndRanks();
            FillPeopleAndAudiences();
        }
        private void DrawAll()
        {
            FillPeople();
            FillAudiences();
            FillDegrees();
            FillChairs();
            FillRanks();
            FillChairsAndHeads();
            FillPeopleAndRanks();
            FillPeopleAndAudiences();
        }
        private void FillPeople()
        {
            var people = Manager.GetAllPeople().ToArray();

            richTextBoxPeopleWithNew.ReadOnly = false;
            richTextBoxPeopleWithNew.Lines = people;
            richTextBoxPeopleWithNew.ReadOnly = true;

            this.comboBoxChAndHPersonNew.Items.Clear();
            foreach(var c in people)
            {
                comboBoxChAndHPersonNew.Items.Add(c);
            }

            this.comboBoxPAR_PersonNew.Items.Clear();
            foreach (var c in people)
            {
                this.comboBoxPAR_PersonNew.Items.Add(c);
            }

            this.comboBoxPAA_PrsonNew.Items.Clear();
            foreach (var c in people)
            {
                this.comboBoxPAA_PrsonNew.Items.Add(c);
            }

            this.comboBoxPersonRemove.Items.Clear();
            foreach (var c in people)
            {
                this.comboBoxPersonRemove.Items.Add(c);
            }

            this.comboBoxViewPerson.Items.Clear();
            foreach (var c in people)
            {
                this.comboBoxViewPerson.Items.Add(c);
            }

            this.comboBoxSearchByPerson.Items.Clear();
            foreach (var c in people)
            {
                this.comboBoxSearchByPerson.Items.Add(c);
            }
        }

        private void FillAudiences()
        {
            var au = Manager.GetAllAudiences().ToArray();

            richTextBoxAudienceWithNew.ReadOnly = false;
            richTextBoxAudienceWithNew.Lines =au;
            richTextBoxAudienceWithNew.ReadOnly = true;

            this.checkedListBoxPAA_AudiencesNew.Items.Clear();
            foreach(var c in au)
            {
                this.checkedListBoxPAA_AudiencesNew.Items.Add(c);
            }

            this.comboBoxAudienceRemove.Items.Clear();
            foreach (var c in au)
            {
                this.comboBoxAudienceRemove.Items.Add(c);
            }

            this.comboBoxSearchByAudience.Items.Clear();
            foreach (var c in au)
            {
                this.comboBoxSearchByAudience.Items.Add(c);
            }
        }
        private void FillDegrees()
        {
            var dg = Manager.GetAllDegrees().ToArray();

            richTextBoxDegreesWithNew.ReadOnly = false;
            richTextBoxDegreesWithNew.Lines = dg;
            richTextBoxDegreesWithNew.ReadOnly = true;

            comboBoxDegreeOfNewPerson.Items.Clear();
            foreach(var c in dg)
            {
                comboBoxDegreeOfNewPerson.Items.Add(c);
            }

            this.comboBoxDegreeRemove.Items.Clear();
            foreach (var c in dg)
            {
                this.comboBoxDegreeRemove.Items.Add(c);
            }

            this.comboBoxSearchByDegree.Items.Clear();
            foreach (var c in dg)
            {
                this.comboBoxSearchByDegree.Items.Add(c);
            }

        }

        
        private void FillChairs()
        {
            var ch = Manager.GetAllChairs().ToArray();

            richTextBoxChairsWithNew.ReadOnly = false;
            richTextBoxChairsWithNew.Lines = ch;
            richTextBoxChairsWithNew.ReadOnly = true;

            comboBoxChairOfNewPerson.Items.Clear();
            foreach(var c in ch)
            {
                comboBoxChairOfNewPerson.Items.Add(c);
            }

            this.comboBoxChAndHChairNew.Items.Clear();
            foreach (var c in ch)
            {
                comboBoxChAndHChairNew.Items.Add(c);
            }

            this.comboBoxChairRemove.Items.Clear();
            foreach (var c in ch)
            {
                this.comboBoxChairRemove.Items.Add(c);
            }

            this.comboBoxSearchByChair.Items.Clear();
            foreach (var c in ch)
            {
                this.comboBoxSearchByChair.Items.Add(c);
            }
        }
        private void FillRanks()
        {
            var ranks = Manager.GetAllRanks().ToArray();
             
            richTextBoxRanksWithNew.ReadOnly = false;
            richTextBoxRanksWithNew.Lines = ranks;
            richTextBoxRanksWithNew.ReadOnly = true;

            this.checkedListBoxPAR_RanksNew.Items.Clear();
            foreach(var c in ranks)
            {
                this.checkedListBoxPAR_RanksNew.Items.Add(c);
            }

            this.comboBoxRankRemove.Items.Clear();
            foreach (var c in ranks)
            {
                this.comboBoxRankRemove.Items.Add(c);
            }

            this.comboBoxSearchByRank.Items.Clear();
            foreach (var c in ranks)
            {
                this.comboBoxSearchByRank.Items.Add(c);
            }
        }

        private void FillChairsAndHeads()
        {
            var t = Manager.GetAllChairsAndHeads().ToArray();

            richTextBoxChairsAndHeadWhithNew.ReadOnly = false;
            richTextBoxChairsAndHeadWhithNew.Lines = t;
            richTextBoxChairsAndHeadWhithNew.ReadOnly = true;
        }
        private void FillPeopleAndRanks()
        {
            var pr = Manager.GetAllPeopleAndRanks().ToArray();

            richTextBoxPAR_WithNew.ReadOnly = false;
            richTextBoxPAR_WithNew.Lines = pr;
            richTextBoxPAR_WithNew.ReadOnly = true;
        }
        private void FillPeopleAndAudiences()
        {
            var pr = Manager.GetAllPeopleAndAudiences().ToArray();

            richTextBoxPAA_WithNew.ReadOnly = false;
            this.richTextBoxPAA_WithNew.Lines = pr;
            richTextBoxPAA_WithNew.ReadOnly = true;
        }

        private void buttonAddNewChair_Click(object sender, EventArgs e)
        {
            if(textBoxNewChair.Text == String.Empty)
            {
                MessageBox.Show("Fill name!!!", "Problem");
            }
            else
            {
                if(Manager.ChairIsInBD(textBoxNewChair.Text))
                {
                    MessageBox.Show("There is this chair", "Error");
                }
                else
                {
                    Manager.AddChair(textBoxNewChair.Text);
                    this.FillChairs();
                }
            }
        }

        private void buttonAddNewAudience_Click(object sender, EventArgs e)
        {
            if(textBoxNewAudience.Text == String.Empty)
            {
                MessageBox.Show("Fill name", "Error");
            }else
            {
                try
                {
                    if(Manager.AudienceIsInBD(textBoxNewAudience.Text))
                    {
                        MessageBox.Show("There is this audience","Problem");
                    }
                    else
                    {
                        Manager.AddAudience(textBoxNewAudience.Text);
                        this.FillAudiences();
                    }
                }
                catch(System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Problem");
                }
            }
        }

        private void buttonAddNewDegree_Click(object sender, EventArgs e)
        {
            if (this.textBoxNewDegree.Text == String.Empty)
            {
                MessageBox.Show("Fill name", "Error");
            }
            else
            {
                try
                {
                    if (Manager.DegreeIsInBD(textBoxNewDegree.Text))
                    {
                        MessageBox.Show("There is this degree", "Problem");
                    }
                    else
                    {
                        Manager.AddDegree(textBoxNewDegree.Text);
                        this.FillDegrees();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Problem");
                }
            }
        }

        private void buttonAddNewRank_Click(object sender, EventArgs e)
        {
            if (this.textBoxNewRank.Text == String.Empty)
            {
                MessageBox.Show("Fill name", "Error");
            }
            else
            {
                try
                {
                    if (Manager.RankIsInBD(textBoxNewRank.Text))
                    {
                        MessageBox.Show("There is this rank", "Problem");
                    }
                    else
                    {
                        Manager.AddRank(textBoxNewRank.Text);
                        this.FillRanks();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Problem");
                }
            }
        }

        private void buttonAddPerson_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBoxNameOfNew.Text == String.Empty || textBoxPositionOfNewPerson.Text == String.Empty || 
                    comboBoxChairOfNewPerson.SelectedItem == null || comboBoxDegreeOfNewPerson.SelectedItem == null)
                {
                    MessageBox.Show("Fill all", "Problem");
                }
                else{
                    if(Manager.PersonIsInBD(textBoxNameOfNew.Text))
                    {
                        MessageBox.Show("There is this person","Error");
                    }
                    else
                    {
                        Manager.AddPerson(textBoxNameOfNew.Text, comboBoxChairOfNewPerson.SelectedItem.ToString(), textBoxPositionOfNewPerson.Text,
                           comboBoxDegreeOfNewPerson.SelectedItem.ToString(), textBoxPhoneOfNewPerson.Text, textBoxInterestsOfNewPerson.Text);
                        this.FillPeople();
                    }
                }

            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            
        }

        private void buttonAddChairAndHead_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.comboBoxChAndHChairNew.SelectedItem==null || this.comboBoxChAndHPersonNew.SelectedItem ==null)
                {
                    MessageBox.Show("Fill all", "Problem");
                }
                else
                {
                    Manager.AddChairAndHead(comboBoxChAndHChairNew.SelectedItem.ToString(), comboBoxChAndHPersonNew.SelectedItem.ToString());
                    this.FillChairsAndHeads();
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void comboBoxPAR_PersonNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(this.comboBoxPAR_PersonNew.SelectedItem!=null)
            {
                var t = Manager.GetCheckedRanksByName(this.comboBoxPAR_PersonNew.SelectedItem.ToString());
                bool l = new bool();
                for(int i =0; i< this.checkedListBoxPAR_RanksNew.Items.Count ;i++)
                {
                    if(t.Contains(this.checkedListBoxPAR_RanksNew.Items[i].ToString()))
                    {
                        l = true;
                        this.checkedListBoxPAR_RanksNew.SetItemChecked(i, l);
                    }
                    else
                    {
                        l = false;
                        this.checkedListBoxPAR_RanksNew.SetItemChecked(i, l);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxPAR_PersonNew.SelectedItem == null)
                {
                    MessageBox.Show("Choose person", "Problem");
                }
                else
                {
                    List<string> t = new List<string>();
                    foreach (var c in this.checkedListBoxPAR_RanksNew.Items)
                    {
                        if(this.checkedListBoxPAR_RanksNew.CheckedItems.Contains(c))
                        t.Add(c.ToString());
                    }
                    Manager.AddPersonAndRanks(this.comboBoxPAR_PersonNew.SelectedItem.ToString(), t);
                    this.FillPeopleAndRanks();
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void comboBoxPAA_PrsonNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBoxPAA_PrsonNew.SelectedItem!=null)
            {
                var t = Manager.GetCheckedAudiencesByName(this.comboBoxPAA_PrsonNew.SelectedItem.ToString());
                bool l = new bool();
                for (int i = 0; i < this.checkedListBoxPAA_AudiencesNew.Items.Count; i++)
                {
                    if (t.Contains(this.checkedListBoxPAA_AudiencesNew.Items[i].ToString()))
                    {
                        l = true;
                        this.checkedListBoxPAA_AudiencesNew.SetItemChecked(i, l);
                    }
                    else
                    {
                        l = false;
                        this.checkedListBoxPAA_AudiencesNew.SetItemChecked(i, l);
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose person", "Error");
            }
        }

        private void buttonAddPAA_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxPAA_PrsonNew.SelectedItem == null)
                {
                    MessageBox.Show("Choose person", "Problem");
                }
                else
                {
                    List<string> t = new List<string>();
                    foreach (var c in this.checkedListBoxPAA_AudiencesNew.Items)
                    {
                        if (this.checkedListBoxPAA_AudiencesNew.CheckedItems.Contains(c))
                            t.Add(c.ToString());
                    }
                    Manager.AddPersonAndAudiences(this.comboBoxPAA_PrsonNew.SelectedItem.ToString(), t);
                    this.FillPeopleAndAudiences();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonRemovePerson_Click(object sender, EventArgs e)
        {
            if(this.comboBoxPersonRemove.SelectedItem == null)
            {
                MessageBox.Show("Choose person","Problem");
            }
            else
            {
                Manager.RemovePerson(this.comboBoxPersonRemove.SelectedItem.ToString());
                DrawAll();
            }
        }

        private void buttonChairRemove_Click(object sender, EventArgs e)
        {
            if(this.comboBoxChairRemove.SelectedItem == null)
            {
                MessageBox.Show("Choose chair", "Problem");
            }
            else
            {
                if(Manager.IsAllowToRemoveChair(this.comboBoxChairRemove.SelectedItem.ToString()))
                {
                    Manager.RemoveChair(this.comboBoxChairRemove.SelectedItem.ToString());
                    DrawAll();
                }
                else
                {
                    MessageBox.Show("There are people", "Problem");
                }
            }
        }

        private void buttonRemoveAudience_Click(object sender, EventArgs e)
        {
            if (this.comboBoxAudienceRemove.SelectedItem == null)
            {
                MessageBox.Show("Choose audience", "Problem");
            }
            else
            {
                if (Manager.IsAllowToRemoveAudience(this.comboBoxAudienceRemove.SelectedItem.ToString()))
                {
                    Manager.RemoveAudience(this.comboBoxAudienceRemove.SelectedItem.ToString());
                    DrawAll();
                }
                else
                {
                    MessageBox.Show("There are people", "Problem");
                }
            }
        }

        private void buttonRemoveDegree_Click(object sender, EventArgs e)
        {
            if (this.comboBoxDegreeRemove.SelectedItem == null)
            {
                MessageBox.Show("Choose degree", "Problem");
            }
            else
            {
                if (Manager.IsAllowToRemoveDegree(this.comboBoxDegreeRemove.SelectedItem.ToString()))
                {
                    Manager.RemoveDegeree(this.comboBoxDegreeRemove.SelectedItem.ToString());
                    DrawAll();
                }
                else
                {
                    MessageBox.Show("There are people with this degree", "Problem");
                }
            }
        }

        private void buttonRemoveRank_Click(object sender, EventArgs e)
        {
            if (this.comboBoxRankRemove.SelectedItem == null)
            {
                MessageBox.Show("Choose rank", "Problem");
            }
            else
            {
                if (Manager.IsAllowToRemoveRank(this.comboBoxRankRemove.SelectedItem.ToString()))
                {
                    Manager.RemoveRank(this.comboBoxRankRemove.SelectedItem.ToString());
                    DrawAll();
                }
                else
                {
                    MessageBox.Show("There are people with this rank", "Problem");
                }
            }
        }

        private void comboBoxViewPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBoxViewPerson.SelectedItem!=null)
            {
                this.richTextBoxViewPerson.ReadOnly = false;
                this.richTextBoxViewPerson.Lines = Manager.GetInfoAboutPerson(this.comboBoxViewPerson.SelectedItem.ToString()).ToArray();
                this.richTextBoxViewPerson.ReadOnly = true;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string name = String.Empty, chair = String.Empty, degree = String.Empty, rank = String.Empty, audience = String.Empty;

            if(checkBoxChair.Checked && this.comboBoxSearchByChair.SelectedItem!=null)
            {
                chair = this.comboBoxSearchByChair.SelectedItem.ToString();
            }

            if(checkBoxDegree.Checked && this.comboBoxSearchByDegree.SelectedItem!=null)
            {
                degree = this.comboBoxSearchByDegree.SelectedItem.ToString();
            }

            if(checkBoxAudience.Checked && this.comboBoxSearchByAudience.SelectedItem!=null)
            {
                audience = this.comboBoxSearchByAudience.SelectedItem.ToString();
            }

            if(checkBoxPerson.Checked && this.comboBoxSearchByPerson.SelectedItem!=null)
            {
                name = this.comboBoxSearchByPerson.SelectedItem.ToString();
            }

            if(checkBoxRank.Checked && this.comboBoxSearchByRank.SelectedItem!=null)
            {
                rank = this.comboBoxSearchByRank.SelectedItem.ToString();
            }

            this.richTextBoxResultOfSearch.ReadOnly = false;
            this.richTextBoxResultOfSearch.Lines = Manager.Search(name, chair, degree, rank, audience).ToArray();
            this.richTextBoxResultOfSearch.ReadOnly = true;
        }

        private void buttonConvertToXML_Click(object sender, EventArgs e)
        {
            if (textBoxXML.Text != String.Empty)
            {
                string name = String.Empty, chair = String.Empty, degree = String.Empty, rank = String.Empty, audience = String.Empty;

                if (checkBoxChair.Checked && this.comboBoxSearchByChair.SelectedItem != null)
                {
                    chair = this.comboBoxSearchByChair.SelectedItem.ToString();
                }

                if (checkBoxDegree.Checked && this.comboBoxSearchByDegree.SelectedItem != null)
                {
                    degree = this.comboBoxSearchByDegree.SelectedItem.ToString();
                }

                if (checkBoxAudience.Checked && this.comboBoxSearchByAudience.SelectedItem != null)
                {
                    audience = this.comboBoxSearchByAudience.SelectedItem.ToString();
                }

                if (checkBoxPerson.Checked && this.comboBoxSearchByPerson.SelectedItem != null)
                {
                    name = this.comboBoxSearchByPerson.SelectedItem.ToString();
                }

                if (checkBoxRank.Checked && this.comboBoxSearchByRank.SelectedItem != null)
                {
                    rank = this.comboBoxSearchByRank.SelectedItem.ToString();
                }
                Manager.CreateXML(name, chair, degree, rank, audience, textBoxXML.Text);
            }else
            {
                MessageBox.Show("Write the name of future file","Problem");
            }

        }
    }
}
