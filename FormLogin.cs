using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        //preventing sql injection
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Invalid Username or Password!");
            }
            else if (txtPassword.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Please Enter Password!");
            }
            else
            {
                SqlConnection con = new SqlConnection("Data Source=VPRO-SRVR1;Initial Catalog=VPRO;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("select * from UserLogin where Username=@Username and Password =@Password", con);
                cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //Connection open here  
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if(dt.Rows.Count > 0)
                {
                    MessageBox.Show("Successfully logged in");
                    WelcomePage form1 = new WelcomePage();
                    this.Hide();
                    form1.Show();
                }
                else
                {
                    MessageBox.Show("Please Enter Correct Username or Password");
                    Clear();
                }
            }
        }

        private void Clear()
        {
            txtPassword.Text = "";

        }
        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister Rs = new FormRegister();
            this.Hide();
            Rs.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                button4.BringToFront();
                txtPassword.PasswordChar = '*';
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                button3.BringToFront();
                txtPassword.PasswordChar = '\0';
            }
        }
    }
}
//sql injection with concatenate
/*
private void btnLogin_Click(object sender, EventArgs e)
{
    if (IsValid())
    {
        using (SqlConnection conn = new SqlConnection("Data Source=VPRO-SRVR1;Initial Catalog=VPRO;Integrated Security=True"))
        {
            string query = "select * from  Login where UserName = '" + txtUserName.Text.Trim() + "' and Password = '" + txtPassword.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                Form form1 = new Form();
                this.Hide();
                form1.Show();
            }
        }
    }

}*/