using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySqlConnector;
using Renci.SshNet.Security.Cryptography;
using Ubiety.Dns.Core.Records.NotUsed;

namespace FinalTerm_project
{
    class databaseCon
    {
        static MySqlConnector.MySqlConnection connectify;
        public void Connect()
        {
            connectify = new MySqlConnection();
            try
            {
                connectify.ConnectionString = "server= localhost; User Id = " + "root" + ";" + "database= onlineticket; Password=" + "jaied";
                connectify.Open();
            }
            catch (Exception e)
            {
            }
        }

        public bool closeConnection()
        {
            try
            {
                connectify.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool loginUser(string user, string pass)
        {
            string query = "SELECT * FROM users where username='" + user + "' and password='" + pass + "'";


            try
            {
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                MySqlDataReader login_usr = cmd.ExecuteReader();

                login_usr.Read();

                if (login_usr["username"].ToString() == user && login_usr["password"].ToString() == pass)
                {
                    closeConnection();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!");
                return false;
            }
        }

        public List<string[]> ShowTime()
        {
            string query = "SELECT * FROM show_time";
            List<string[]> rData = new List<string[]>();

            try
            {
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                MySqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    rData.Add(new string[] { data["id"].ToString(), data["movie"].ToString(), data["time"].ToString() });
                }

                closeConnection();
                return rData;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                closeConnection();
                return rData;
            }
        }

        public bool chkCustomer_id(string id)
        {
            string chkUsrInfo_query = String.Format("SELECT * FROM customers where id='{0}'", id);
            try
            {
                Connect();
                MySqlCommand cmd = new MySqlCommand(chkUsrInfo_query, connectify);
                MySqlDataReader data = cmd.ExecuteReader();

                if (data.Read())
                {
                    closeConnection();
                    return true;
                }
                else
                {
                    closeConnection();
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Customer ID Already Exists!");
                return false;
            }
        }

        public void confirmBooking(string cid, string date, string name, string gndr, string addr, string phn, string shw_id, List<string> sits)
        {
            try
            {
                if (chkCustomer_id(cid) == false)
                {
                    Connect();
                    string usrInfo_query = String.Format("INSERT INTO customers VALUES('{0}', '{1}', '{2}', '{3}', '{4}')", cid, name, gndr, addr, phn);
                    MySqlCommand cmd = new MySqlCommand(usrInfo_query, connectify);
                    cmd.ExecuteNonQuery();
                }
                Connect();
                for (int i = 0; i < sits.Count; i++)
                {
                    string query = String.Format("INSERT INTO sold_tickets (sit_id, customer_id, date, show_id) VALUES ('{0}', '{1}', '{2}', '{3}')", sits[i], cid, date, shw_id);
                    MySqlCommand cmd = new MySqlCommand(query, connectify);
                    cmd.ExecuteNonQuery();
                }
                closeConnection();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string getShowID(string movie, string shwTime)
        {
            string shwID = "";
            string query = String.Format("SELECT * FROM show_time WHERE movie='{0}' AND time='{1}'", movie, shwTime);
            try
            {
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                MySqlDataReader data = cmd.ExecuteReader();
                data.Read();
                shwID = data["id"].ToString();
                closeConnection();
                return shwID;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                closeConnection();
                return shwID;
            }
        }

        public List<string> getBookedSits(string movie, string shw_time)
        {
            List<string> booked_sits = new List<string>();
            string query = String.Format("SELECT sit_id FROM sold_tickets WHERE show_id=(SELECT id FROM show_time WHERE movie='{0}' AND time='{1}')", movie, shw_time);
            try
            {
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                MySqlDataReader data = cmd.ExecuteReader();
                while(data.Read())
                {
                    booked_sits.Add(data["sit_id"].ToString());
                }
                return booked_sits;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return booked_sits;
            }
        }

        public void addMovieAndShowTime(string id, string mvName, string shw_time)
        {
            try
            {
                string query = String.Format("INSERT INTO show_time VALUES ('{0}', '{1}', '{2}')", id, mvName, shw_time);
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                cmd.ExecuteNonQuery();
                closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeConnection();
            }
        }

        public void registerAccount(string username, string password)
        {
            try
            {
                string query = String.Format("INSERT INTO users VALUES ('{0}', '{1}')", username, password);
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                cmd.ExecuteNonQuery();
                closeConnection();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeConnection();
            }
        }

        public List<string> getCustomerInfo(string sitID)
        {
            List<string> usrInfo = new List<string>();

            try
            {
                Connect();
                string query = String.Format("SELECT * FROM customers WHERE id=(SELECT MAX(customer_id) FROM sold_tickets WHERE sit_id='{0}')", sitID);
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                MySqlDataReader data = cmd.ExecuteReader();
                data.Read();
                usrInfo.Add(data["id"].ToString());
                usrInfo.Add(data["name"].ToString());
                usrInfo.Add(data["gender"].ToString());
                usrInfo.Add(data["address"].ToString());
                usrInfo.Add(data["phone"].ToString());
                closeConnection();
                return usrInfo;
            }

            catch(Exception e)
            {
                closeConnection();
                MessageBox.Show(e.Message);
                return usrInfo;
            }
        }

        public void removeSit(string sitID, string cid, string mv, string tm)
        {
            try
            {
                string query = String.Format("DELETE FROM sold_tickets WHERE sit_id='{0}' AND customer_id='{1}' AND show_id=(SELECT id FROM show_time WHERE movie='{2}' AND time='{3}')", sitID, cid, mv, tm);
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                cmd.ExecuteNonQuery();
                closeConnection();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeConnection();
            }
        }

        public void removeCustomerInfo(string id)
        {
            try
            {
                string query = String.Format("DELETE FROM customers WHERE id='{0}'", id);
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                cmd.ExecuteNonQuery();
                closeConnection();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeConnection();
            }
        }

        public void removeMovieAndShowtime(string movie, string time)
        {
            try
            {
                string swid = getShowID(movie, time);
                string query = String.Format("DELETE FROM show_time WHERE id='{0}'", swid);
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                cmd.ExecuteNonQuery();
                closeConnection();
            }

            catch(Exception ex)
            {
                closeConnection();
                MessageBox.Show(ex.Message);
            }
        }

        public void changeControl_UserPass(string uname, string pass)
        {
            try
            {
                string query = String.Format("UPDATE users SET password='{0}' WHERE username='{1}'", pass, uname);
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                cmd.ExecuteNonQuery();
                closeConnection();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeConnection();
            }
        }

        public void deleteControl_user(string username)
        {
            try
            {
                string query = String.Format("DELETE FROM users WHERE username='{0}'", username);
                Connect();
                MySqlCommand cmd = new MySqlCommand(query, connectify);
                cmd.ExecuteNonQuery();
                closeConnection();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeConnection();
            }
        }
    }
}
