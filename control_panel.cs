using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalTerm_project
{
    public partial class Control_panel : Form
    {
        private string uname_c = "";
        private List<string> sits = new List<string>();
        private string rmv_booked = "";
        private databaseCon d1 = new databaseCon();
        private bool[] btn_clk = new bool[15];
        private bool[] booked_chk = new bool[15];

        public Control_panel()
        {
            InitializeComponent();
            loadAllData();
        }

        public Control_panel(string unm)
        {
            InitializeComponent();
            loadAllData();
            uname_c = unm;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (btn_clk[0] == false && booked_chk[0] == false)
            {
                btn_clk[0] = true;
                sits.Add("A1");
                buttonA1.BackColor = Color.Cyan;
            }
            else if (btn_clk[0] == true && booked_chk[0] == false)
            {
                btn_clk[0] = false;
                sits.Remove("A1");
                buttonA1.BackColor = DefaultBackColor;
            }

            if(booked_chk[0] == true)
            {
                rmv_booked = "A1";
                loadCustomerInfo("A1");
            }
            else if(booked_chk[0] == false)
            {
                resetUIs();
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxName.Text = "";
            textBoxAddress.Text = "";
            textBoxPhone.Text = "";

            comboBoxMovie.SelectedIndex = 0;
            comboBoxShow.SelectedIndex = 0;

            radioButton1.Select();
            sits.Clear();
            loadAllData();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        private void loadAllData()
        {
            textBoxID.Text = "";
            textBoxName.Text = "";
            textBoxAddress.Text = "";
            textBoxPhone.Text = "";

            comboBoxMovie.Items.Clear();
            comboBoxShow.Items.Clear();
            comboBoxMovie.Items.Add("Select Movie...");
            comboBoxShow.Items.Add("Select Show Time...");
            comboBoxShow.SelectedIndex = 0;
            comboBoxMovie.SelectedIndex = 0;


            radioButton1.Select();

            List<string[]> showTimes = d1.ShowTime();

            foreach (string[] info in showTimes)
            {
                comboBoxMovie.Items.Add(info[1]);
                comboBoxShow.Items.Add(info[2]);
            }

            load_bookedSits();
        }

        public void resetUIs()
        {
            textBoxID.Text = "";
            textBoxName.Text = "";
            textBoxAddress.Text = "";
            textBoxPhone.Text = "";
            radioButton1.Select();
        }

        private void reload_sitStatus()
        {
            buttonA1.BackColor = DefaultBackColor;
            buttonA2.BackColor = DefaultBackColor;
            buttonA3.BackColor = DefaultBackColor;
            buttonB1.BackColor = DefaultBackColor;
            buttonB2.BackColor = DefaultBackColor;
            buttonB3.BackColor = DefaultBackColor;
            buttonC1.BackColor = DefaultBackColor;
            buttonC2.BackColor = DefaultBackColor;
            buttonC3.BackColor = DefaultBackColor;
            buttonD1.BackColor = DefaultBackColor;
            buttonD2.BackColor = DefaultBackColor;
            buttonD3.BackColor = DefaultBackColor;
            buttonE1.BackColor = DefaultBackColor;
            buttonE2.BackColor = DefaultBackColor;
            buttonE3.BackColor = DefaultBackColor;
        }

        public void load_bookedSits()
        {
            Array.Clear(booked_chk, 0, booked_chk.Length);
            Array.Clear(btn_clk, 0, btn_clk.Length);
            rmv_booked = "";

            List<string> bookedSits = d1.getBookedSits(comboBoxMovie.Text, comboBoxShow.Text);

            for (int i = 0; i < bookedSits.Count; i++)
            {
                if (bookedSits[i] == "A1") { booked_chk[0] = true; buttonA1.BackColor = Color.Cyan; }
                if (bookedSits[i] == "A2") { booked_chk[1] = true; buttonA2.BackColor = Color.Cyan; }
                if (bookedSits[i] == "A3") { booked_chk[2] = true; buttonA3.BackColor = Color.Cyan; }
                if (bookedSits[i] == "B1") { booked_chk[3] = true; buttonB1.BackColor = Color.Cyan; }
                if (bookedSits[i] == "B2") { booked_chk[4] = true; buttonB2.BackColor = Color.Cyan; }
                if (bookedSits[i] == "B3") { booked_chk[5] = true; buttonB3.BackColor = Color.Cyan; }
                if (bookedSits[i] == "C1") { booked_chk[6] = true; buttonC1.BackColor = Color.Cyan; }
                if (bookedSits[i] == "C2") { booked_chk[7] = true; buttonC2.BackColor = Color.Cyan; }
                if (bookedSits[i] == "C3") { booked_chk[8] = true; buttonC3.BackColor = Color.Cyan; }
                if (bookedSits[i] == "D1") { booked_chk[9] = true; buttonD1.BackColor = Color.Cyan; }
                if (bookedSits[i] == "D2") { booked_chk[10] = true; buttonD2.BackColor = Color.Cyan; }
                if (bookedSits[i] == "D3") { booked_chk[11] = true; buttonD3.BackColor = Color.Cyan; }
                if (bookedSits[i] == "E1") { booked_chk[12] = true; buttonE1.BackColor = Color.Cyan; }
                if (bookedSits[i] == "E2") { booked_chk[13] = true; buttonE2.BackColor = Color.Cyan; }
                if (bookedSits[i] == "E3") { booked_chk[14] = true; buttonE3.BackColor = Color.Cyan; }
            }
        }

        private void loadCustomerInfo(string sitID)
        {
            List<string> usrInfo = d1.getCustomerInfo(sitID);
            textBoxID.Text = usrInfo[0];
            textBoxName.Text = usrInfo[1];
            textBoxAddress.Text = usrInfo[3];
            textBoxPhone.Text = usrInfo[4];

            if (usrInfo[2] == "Male")
            {
                radioButton1.Checked = true;
            }
            else if (usrInfo[2] == "Female")
            {
                radioButton2.Checked = true;
            }
        }

        private void comboBoxMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = comboBoxMovie.SelectedIndex;
            comboBoxShow.SelectedIndex = indx;
            reload_sitStatus();
            load_bookedSits();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (btn_clk[1] == false && booked_chk[1] == false)
            {
                btn_clk[1] = true;
                sits.Add("A2");
                buttonA2.BackColor = Color.Cyan;
            }
            else if (btn_clk[1] == true && booked_chk[1] == false)
            {
                btn_clk[1] = false;
                sits.Remove("A2");
                buttonA2.BackColor = DefaultBackColor;
            }

            if (booked_chk[1] == true)
            {
                rmv_booked = "A2";
                loadCustomerInfo("A2");
            }
            else if (booked_chk[1] == false)
            {
                resetUIs();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (btn_clk[2] == false && booked_chk[2] == false)
            {
                btn_clk[2] = true;
                sits.Add("A3");
                buttonA3.BackColor = Color.Cyan;
            }
            else if (btn_clk[2] == true && booked_chk[2] == false)
            {
                btn_clk[2] = false;
                sits.Remove("A3");
                buttonA3.BackColor = DefaultBackColor;
            }

            if (booked_chk[2] == true)
            {
                rmv_booked = "A3";
                loadCustomerInfo("A3");
            }
            else if (booked_chk[2] == false)
            {
                resetUIs();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (btn_clk[3] == false && booked_chk[3] == false)
            {
                btn_clk[3] = true;
                sits.Add("B1");
                buttonB1.BackColor = Color.Cyan;
            }
            else if (btn_clk[3] == true && booked_chk[3] == false)
            {
                btn_clk[3] = false;
                sits.Remove("B1");
                buttonB1.BackColor = DefaultBackColor;
            }

            if (booked_chk[3] == true)
            {
                rmv_booked = "B1";
                loadCustomerInfo("B1");
            }
            else if (booked_chk[3] == false)
            {
                resetUIs();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (btn_clk[4] == false && booked_chk[4] == false)
            {
                btn_clk[4] = true;
                sits.Add("B2");
                buttonB2.BackColor = Color.Cyan;
            }
            else if (btn_clk[4] == true && booked_chk[4] == false)
            {
                btn_clk[4] = false;
                sits.Remove("B2");
                buttonB2.BackColor = DefaultBackColor;
            }

            if (booked_chk[4] == true)
            {
                rmv_booked = "B2";
                loadCustomerInfo("B2");
            }
            else if (booked_chk[4] == false)
            {
                resetUIs();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (btn_clk[5] == false && booked_chk[5] == false)
            {
                btn_clk[5] = true;
                sits.Add("B3");
                buttonB3.BackColor = Color.Cyan;
            }
            else if (btn_clk[5] == true && booked_chk[5] == false)
            {
                btn_clk[5] = false;
                sits.Remove("B3");
                buttonB3.BackColor = DefaultBackColor;
            }

            if (booked_chk[5] == true)
            {
                rmv_booked = "B3";
                loadCustomerInfo("B3");
            }
            else if (booked_chk[5] == false)
            {
                resetUIs();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (btn_clk[6] == false && booked_chk[6] == false)
            {
                btn_clk[6] = true;
                sits.Add("C1");
                buttonC1.BackColor = Color.Cyan;
            }
            else if (btn_clk[6] == true && booked_chk[6] == false)
            {
                btn_clk[6] = false;
                sits.Remove("C1");
                buttonC1.BackColor = DefaultBackColor;
            }

            if (booked_chk[6] == true)
            {
                rmv_booked = "C1";
                loadCustomerInfo("C1");
            }
            else if (booked_chk[6] == false)
            {
                resetUIs();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (btn_clk[7] == false && booked_chk[7] == false)
            {
                btn_clk[7] = true;
                sits.Add("C2");
                buttonC2.BackColor = Color.Cyan;
            }
            else if (btn_clk[7] == true && booked_chk[7] == false)
            {
                btn_clk[7] = false;
                sits.Remove("C2");
                buttonC2.BackColor = DefaultBackColor;
            }

            if (booked_chk[7] == true)
            {
                rmv_booked = "C2";
                loadCustomerInfo("C2");
            }
            else if (booked_chk[7] == false)
            {
                resetUIs();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (btn_clk[8] == false && booked_chk[8] == false)
            {
                btn_clk[8] = true;
                sits.Add("C3");
                buttonC3.BackColor = Color.Cyan;
            }
            else if (btn_clk[8] == true && booked_chk[8] == false)
            {
                btn_clk[8] = false;
                sits.Remove("C3");
                buttonC3.BackColor = DefaultBackColor;
            }

            if (booked_chk[8] == true)
            {
                rmv_booked = "C3";
                loadCustomerInfo("C3");
            }
            else if (booked_chk[8] == false)
            {
                resetUIs();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (btn_clk[9] == false && booked_chk[9] == false)
            {
                btn_clk[9] = true;
                sits.Add("D1");
                buttonD1.BackColor = Color.Cyan;
            }
            else if (btn_clk[9] == true && booked_chk[9] == false)
            {
                btn_clk[9] = false;
                sits.Remove("D1");
                buttonD1.BackColor = DefaultBackColor;
            }

            if (booked_chk[9] == true)
            {
                rmv_booked = "D1";
                loadCustomerInfo("D1");
            }
            else if (booked_chk[9] == false)
            {
                resetUIs();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (btn_clk[10] == false && booked_chk[10] == false)
            {
                btn_clk[10] = true;
                sits.Add("D2");
                buttonD2.BackColor = Color.Cyan;
            }
            else if (btn_clk[10] == true && booked_chk[10] == false)
            {
                btn_clk[10] = false;
                sits.Remove("D2");
                buttonD2.BackColor = DefaultBackColor;
            }

            if (booked_chk[10] == true)
            {
                rmv_booked = "D2";
                loadCustomerInfo("D2");
            }
            else if (booked_chk[10] == false)
            {
                resetUIs();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (btn_clk[11] == false && booked_chk[11] == false)
            {
                btn_clk[11] = true;
                sits.Add("D3");
                buttonD3.BackColor = Color.Cyan;
            }
            else if (btn_clk[11] == true && booked_chk[11] == false)
            {
                btn_clk[11] = false;
                sits.Remove("D3");
                buttonD3.BackColor = DefaultBackColor;
            }

            if (booked_chk[11] == true)
            {
                rmv_booked = "D3";
                loadCustomerInfo("D3");
            }
            else if (booked_chk[11] == false)
            {
                resetUIs();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btn_clk[12] == false && booked_chk[12] == false)
            {
                btn_clk[12] = true;
                sits.Add("E1");
                buttonE1.BackColor = Color.Cyan;
            }
            else if (btn_clk[12] == true && booked_chk[12] == false)
            {
                btn_clk[12] = false;
                sits.Remove("E1");
                buttonE1.BackColor = DefaultBackColor;
            }

            if (booked_chk[12] == true)
            {
                rmv_booked = "E1";
                loadCustomerInfo("E1");
            }
            else if (booked_chk[12] == false)
            {
                resetUIs();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (btn_clk[13] == false && booked_chk[13] == false)
            {
                btn_clk[13] = true;
                sits.Add("E2");
                buttonE2.BackColor = Color.Cyan;
            }
            else if (btn_clk[13] == true && booked_chk[13] == false)
            {
                btn_clk[13] = false;
                sits.Remove("E2");
                buttonE2.BackColor = DefaultBackColor;
            }

            if (booked_chk[13] == true)
            {
                rmv_booked = "E2";
                loadCustomerInfo("E2");
            }
            else if (booked_chk[13] == false)
            {
                resetUIs();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (btn_clk[14] == false && booked_chk[14] == false)
            {
                btn_clk[14] = true;
                sits.Add("E3");
                buttonE3.BackColor = Color.Cyan;
            }
            else if (btn_clk[14] == true && booked_chk[14] == false)
            {
                btn_clk[14] = false;
                sits.Remove("E3");
                buttonE3.BackColor = DefaultBackColor;
            }

            if (booked_chk[14] == true)
            {
                rmv_booked = "E3"; ;
                loadCustomerInfo("E3");
            }
            else if (booked_chk[14] == false)
            {
                resetUIs();
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            string id, name, gender = "", address, phn, date_x, shw_id;
            id = textBoxID.Text;
            name = textBoxName.Text;
            address = textBoxAddress.Text;
            phn = textBoxPhone.Text;
            date_x = dateTimePicker.Value.ToString();
            shw_id = d1.getShowID(comboBoxMovie.SelectedItem.ToString(), comboBoxShow.SelectedItem.ToString());

            if (radioButton1.Checked)
            {
                gender = "Male";
            }
            else if (radioButton2.Checked)
            {
                gender = "Female";
            }
            MessageBox.Show("Sit Booked Successfully!");
            d1.confirmBooking(id, date_x, name, gender, address, phn, shw_id, sits);
            loadAllData();
        }

        private void buttonGenID_Click(object sender, EventArgs e)
        {
            Random rndm = new Random();
            int number = rndm.Next(10000, 99999);
            textBoxID.Text = Convert.ToString(number);
        }

        private void buttonAddMovie_Click(object sender, EventArgs e)
        {
            new Add_Movie().Show();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if(rmv_booked == "")
            {
                MessageBox.Show("Invalid Operation!");
            }
            else
            {
                d1.removeSit(rmv_booked, textBoxID.Text, comboBoxMovie.Text, comboBoxShow.Text);
                reload_sitStatus();
                loadAllData();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            d1.removeCustomerInfo(textBoxID.Text);
            reload_sitStatus();
            loadAllData();
        }

        private void buttonRmMovie_Click(object sender, EventArgs e)
        {
            d1.removeMovieAndShowtime(comboBoxMovie.Text, comboBoxShow.Text);
            reload_sitStatus();
            loadAllData();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AccountSetting(uname_c).Show();
        }
    }
}
