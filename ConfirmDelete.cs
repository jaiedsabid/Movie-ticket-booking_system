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
    public partial class ConfirmDelete : Form
    {
        private string unm = "";
        databaseCon d1 = new databaseCon();
        public ConfirmDelete()
        {
            InitializeComponent();
        }

        public ConfirmDelete(string uname)
        {
            InitializeComponent();
            unm = uname;
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            d1.deleteControl_user(unm);
            MessageBox.Show("Control account deleted!");
            this.Hide();
            new login().Show();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AccountSetting(unm).Show();
        }
    }
}
