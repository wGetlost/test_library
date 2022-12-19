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
    public partial class AddAttendant : Form
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
        public AddAttendant()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static void insert(string sql)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = get_SqlCommand(sql);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("成功添加" + rows + "条管理员信息");
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错原因：" + ex.Message);
            }
        }//insert
        private void button1_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from 管理员 where 管理员编号='" + textBox2.Text + "'";
            SqlCommand cmd1 = get_SqlCommand(sql1);
            SqlDataReader r = cmd1.ExecuteReader();
            if (r.Read())
            {
                MessageBox.Show("管理员编号重复，请重新输入");
                textBox2.Focus();
                return;
            }
            if (textBox5.Text.Length != 11)
            {
                MessageBox.Show("联系方式错误,请输入正确的联系方式");
                textBox5.Focus();
                return;
            }
            if (textBox2.Text == "" || textBox3.Text == "" || textBox5.Text == "" || comboBox1.Text == "" )
            {
                MessageBox.Show("提示：输入不能为空");
                return;
            }
            string sql = string.Format("insert into 管理员 values('{0}','{1}','{2}','{3}','{4}')", textBox2.Text, textBox3.Text, radioButton1.Checked ? "男" : "女", textBox5.Text, comboBox1.Text);
            SqlCommand cmd = get_SqlCommand(sql);
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                MessageBox.Show("成功添加" + rows + "条管理员信息");
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败");
            }
            conn.Close();
            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
