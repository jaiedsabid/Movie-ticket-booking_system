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
    public partial class Register : Form
    {
        databaseCon d1 = new databaseCon();
        public Register()
        {
            InitializeComponent();
        }

        private void buttoRegister_Click(object sender, EventArgs e)
        {
            if(textBoxPass.Text == textBoxCPass.Text)
            {
                d1.registerAccount(textBoxUsername.Text, textBoxPass.Text);
                MessageBox.Show("Account Created!");
            }
            else { MessageBox.Show("Password don't match."); }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }
    }
}
