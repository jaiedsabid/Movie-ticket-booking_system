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
    public partial class AccountSetting : Form
    {
        databaseCon d1 = new databaseCon();
        public AccountSetting()
        {
            InitializeComponent();
        }

        public AccountSetting(string unm)
        {
            InitializeComponent();
            labelUsername.Text = unm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Control_panel(labelUsername.Text).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxPass.Text == textBoxCPass.Text)
            {
                d1.changeControl_UserPass(labelUsername.Text, textBoxPass.Text);
                MessageBox.Show("Password changed successfully!");
            }
            else { MessageBox.Show("Password don't match!"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ConfirmDelete(labelUsername.Text).Show();
        }
    }
}
