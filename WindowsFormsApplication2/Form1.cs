using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
            student student=new student();
            student.name=nameTextBox.Text;
            student.email = emailTextBox.Text;
            student.address = addressTextBox.Text;

            string conn = @"server=BITM-401-PC07\SQLEXPRESS;database=ABC_University;integrated security=true";
            SqlConnection connection =new SqlConnection();
            connection.ConnectionString = conn;
            connection.Open();
            string query = string.Format("INSERT INTO student VALUES('{0}','{1}','{2}')", student.name, student.email, student.address);

            SqlCommand command = new SqlCommand(query, connection);
            int affectedrow = command.ExecuteNonQuery();
            connection.Close();
            if (affectedrow > 0)
            {
                MessageBox.Show("data Insert Successfully");
            }
            else
            {
                MessageBox.Show("problem");

            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            string conn = @"server=BITM-401-PC07\SQLEXPRESS;database=ABC_University;integrated security=true";
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = conn;
            connection.Open();
            string query = string.Format("SELECT * FROM student");
                SqlCommand command=new SqlCommand(query,connection);
            SqlDataReader aReader = command.ExecuteReader();
            List< student> students=new List<student>();
            if (aReader.HasRows)
            {
                while (aReader.Read())
                {
                    student astudent=new student();
                    astudent.studentID = (int)aReader[0];
                    astudent.name = aReader[1].ToString();
                    astudent.email = aReader[2].ToString();
                    astudent.address = aReader[3].ToString();
                    students.Add(astudent);
                }
                connection.Close();
                studentGridView.DataSource = students;
            }

        }
    }
}
