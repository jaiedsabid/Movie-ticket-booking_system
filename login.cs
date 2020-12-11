using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using System.Windows.Forms;

namespace FinalTerm_project
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool success_login = new databaseCon().loginUser(userNameText.Text, userPassText.Text);
            if(success_login == true)
            {
                new Control_panel(userNameText.Text).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Register().Show();
        }
    }
}
