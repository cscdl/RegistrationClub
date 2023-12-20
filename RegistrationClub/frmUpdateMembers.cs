using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RegistrationClub
{
    public partial class frmUpdateMembers : Form
    {
        public frmUpdateMembers()
        {
            InitializeComponent();
            displayStudentID();
        }

        public void displayStudentID()
        {
            SqlConnection sqlconn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\RegistrationClub\RegistrationClub\DBClub.mdf;Integrated Security=True");

            string studentID = "SELECT * FROM ClubMembers";

            SqlCommand sqlcomm = new SqlCommand(studentID, sqlconn);
            SqlDataReader myReader;

            try
            {
                sqlconn.Open();
                myReader = sqlcomm.ExecuteReader();

                while (myReader.Read())
                {
                    Int64 StudID = myReader.GetInt64(1);
                    cbStudentID.Items.Add(StudID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sqlconn.Close();
        }
        private void cbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\RegistrationClub\RegistrationClub\DBClub.mdf;Integrated Security=True");

            string StudentID = "SELECT * FROM ClubMembers WHERE StudentId = '"+cbStudentID.Text+"';";

            SqlCommand sqlcomm = new SqlCommand(StudentID, sqlconn);
            SqlDataReader myReader;

            try
            {
                sqlconn.Open();
                myReader = sqlcomm.ExecuteReader();

                while (myReader.Read())
                {
                    string fname = myReader.GetString(2);
                    string mname = myReader.GetString(3);
                    string lname = myReader.GetString(4);
                    string age = myReader.GetInt32(5).ToString();
                    string gender = myReader.GetString(6);
                    string program = myReader.GetString(7);

                    txtFirstname.Text = fname;
                    txtMiddlename.Text = mname;
                    txtLastname.Text = lname;
                    txtAge.Text = age;
                    cbGender.Text = gender;
                    cbProgram.Text = program;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sqlconn.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\RegistrationClub\RegistrationClub\DBClub.mdf;Integrated Security=True");

            try
            {
                sqlconn.Open();

                string updatedQuery = "UPDATE ClubMembers SET StudentID=@StudentId, FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, Age=@Age, Gender=@Gender, Program=@Program WHERE ID=@ID";

                using (SqlCommand sqlcomm = new SqlCommand(updatedQuery, sqlconn))
                {
                    int selectedID = 2;

                    sqlcomm.Parameters.AddWithValue("@ID", selectedID);
                    sqlcomm.Parameters.AddWithValue("@StudentId", cbStudentID.Text);
                    sqlcomm.Parameters.AddWithValue("@FirstName", txtFirstname.Text);
                    sqlcomm.Parameters.AddWithValue("@MiddleName", txtMiddlename.Text);
                    sqlcomm.Parameters.AddWithValue("@LastName", txtLastname.Text);
                    sqlcomm.Parameters.AddWithValue("@Age", Convert.ToInt32(txtAge.Text));
                    sqlcomm.Parameters.AddWithValue("@Gender", cbGender.Text);
                    sqlcomm.Parameters.AddWithValue("@Program", cbProgram.Text);

                    MessageBox.Show("Record Updated");

                    sqlcomm.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sqlconn.Close();
        }
    }
}
