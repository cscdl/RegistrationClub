using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationClub
{
    public partial class frmRegistrationClub : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int count = 0;
        private int ID, Age;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentId;

        public frmRegistrationClub()
        {
            InitializeComponent();

            // Initialize ClubRegistrationQuery
            clubRegistrationQuery = new ClubRegistrationQuery();

            RefreshListOfClubMembers();
        }

        private int RegistrationID()
        {
            // Increment count by 1 for each transaction
            count++;
            return count;
        }
        private void RefreshListOfClubMembers()
        {
            // Call DisplayList method of ClubRegistrationQuery
            clubRegistrationQuery.DisplayList();

            // Set DataGridView DataSource to BindingSource
            dgvListClubMembers.DataSource = clubRegistrationQuery.bindingSource;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Call RegisterStudent method with parameters
            ID = RegistrationID();
            StudentId = long.Parse(txtStudentID.Text);
            FirstName = txtFirstname.Text;
            MiddleName = txtMiddlename.Text;
            LastName = txtLastname.Text;
            Age = int.Parse(txtAge.Text);
            Gender = cbGender.Text;
            Program = cbProgram.Text;

            if (clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program))
            {
                MessageBox.Show("Student registered successfully!");

            }
            else
            {
                MessageBox.Show("Error registering student!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmUpdateMembers fum = new frmUpdateMembers();
            fum.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Call RefreshListOfClubMembers to display updated data
            RefreshListOfClubMembers();
        }
    }
}
