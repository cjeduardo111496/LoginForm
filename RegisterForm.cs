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

namespace LoginForm
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Email Must Be Indicated");
            }
            else
            if (txtUserName.Text.TrimStart() == string.Empty | txtUserName.Text.Length < 5)
            {
                MessageBox.Show("Username Must Contain at Least 5 Minimum Character!");
            }
            else
            if (txtPassWord.Text.TrimStart() == string.Empty | txtPassWord.Text.Length < 8)
            {
                MessageBox.Show("Password Must Contain at Least 8 Minimum Character!");
            }
            else
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-VAN28TB;Initial Catalog=VPRO;Integrated Security=True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from UserLogin where Username= @Username", conn);
                cmd.Parameters.AddWithValue("@Username", this.txtUserName.Text);
                
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(string.Format("Username {0} already exist", this.txtUserName.Text));
                    Clear();
                }

              
                /*SqlConnection conn = new SqlConnection("Data Source=VPRO-SRVR1;Initial Catalog=VPRO;Integrated Security=True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from UserLogin where Username= '" + txtUserName + "'", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                 
                    MessageBox.Show("Username already exists - please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                */
                
                else
                {
                        cmd = new SqlCommand("Register", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmailAdd", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassWord.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration is Successful");
                        LoginForm form1 = new LoginForm();
                        this.Hide();
                        form1.Show();

                }
               
                void Clear()
                {
                    txtUserName.Text = txtPassWord.Text = txtConfirmPassword.Text = "";

                }
           
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is LoginForm)
                {
                    frm.Show();
                }
            }
            Close();
            //Application.Restart();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (txtPassWord.PasswordChar == '\0')
            {
                button4.BringToFront();
                txtPassWord.PasswordChar = '*';
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtPassWord.PasswordChar == '*')
            {
                button3.BringToFront();
                txtPassWord.PasswordChar = '\0';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtConfirmPassword.PasswordChar == '*')
            {
                button1.BringToFront();
                txtConfirmPassword.PasswordChar = '\0';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtConfirmPassword.PasswordChar == '\0')
            {
                button2.BringToFront();
                txtConfirmPassword.PasswordChar = '*';
            }
        }
    }
}
