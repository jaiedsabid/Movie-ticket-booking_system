using MySql.Data.MySqlClient;
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
    public partial class Add_Movie : Form
    {
        private databaseCon d1 = new databaseCon();
        public Add_Movie()
        {
            InitializeComponent();
        }

        public void loadData()
        {
            comboBox1.SelectedIndex = 0;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string show_time = comboBox1.SelectedItem.ToString();
            string movie = textBoxMovie.Text;
            string movie_id = textBoxMovieID.Text;
            d1.addMovieAndShowTime(movie_id, movie, show_time);                
        }

        private void buttonGen_Click(object sender, EventArgs e)
        {
            Random rndm = new Random();
            textBoxMovieID.Text = rndm.Next(1000, 9999).ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
