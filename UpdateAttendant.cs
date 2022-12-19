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
    public partial class UpdateAttendant : Form
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
        public UpdateAttendant()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static void update(string sql)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = get_SqlCommand(sql);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("成功更新：" + rows + "条管理员信息");
                }
                else
                {
                    MessageBox.Show("更新失败");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错原因：" + ex.Message);
            }
        }//Update
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length != 11)
            {
                MessageBox.Show("联系方式错误,请输入正确的联系方式");
                textBox3.Focus();
                return;
            }
            string sql1 = "select * from 管理员 where 管理员编号='" + textBox2.Text + "'";
            SqlCommand cmd1 = get_SqlCommand(sql1);
            SqlDataReader r = cmd1.ExecuteReader();
            if (r.Read())
            {
                MessageBox.Show("输入的新管理员编号重复，请重新输入");
                textBox2.Focus();
                return;
            }
            string oldATid = this.textBox1.Text;
            string newATid = this.textBox2.Text;
            string CIF = this.textBox3.Text;
            string place = this.comboBox1.Text;
            string sql = "update 管理员 set 管理员编号='" + newATid + "',联系方式='" + CIF + "',管理区域='" + place + "' where 管理员编号='" + oldATid + "'";//更新语句
            update(sql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
