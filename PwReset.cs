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

namespace 信息管理
{
    public partial class PwReset : Form
    {
        public static string str = "Data Source=LAPTOP-VK4UBVJN;Initial Catalog=图书管理系统;Integrated Security=True;MultipleActiveResultSets=true";
        public static SqlConnection conn = null;
        public static void initConn()
        {
            if (conn == null)
            {
                conn = new SqlConnection(str);
            }
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == System.Data.ConnectionState.Broken)
            {
                conn.Close();
                conn.Open();
            }
        }//initConn
        public static SqlCommand get_SqlCommand(string sql)
        {
            initConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd;
        }
        public PwReset()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public static void resetps(string sql)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = get_SqlCommand(sql);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("密码重置成功");
                }
                else
                {
                    MessageBox.Show("密码重置失败，请输入正确的学工号");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错原因：" + ex.Message);
            }
        }//resetps
        private void button1_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from 用户 where 学工号='" + textBox1.Text + "'";
            SqlCommand cmd1 = get_SqlCommand(sql1);
            SqlDataReader r = cmd1.ExecuteReader();
            if (!r.Read())
            {
                MessageBox.Show("该用户不存在，请重新输入");
                textBox1.Focus();
                return;
            }
            string stid = this.textBox1.Text;
            string password1 = this.textBox2.Text;
            string password2 = this.textBox3.Text;
            string sql = "update 用户 set 登录密码='" + password1 + "' where 学工号='"+stid+"'";
            if (password1==password2)
            {
                resetps(sql);
            }
            else
            {
                MessageBox.Show("密码不一致，请重新输入");
                return;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
