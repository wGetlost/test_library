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
    public partial class DeleteAttendant : Form
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
        public DeleteAttendant()
        {
            InitializeComponent();
        }
        public static void delete(string sql)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = get_SqlCommand(sql);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("成功删除" + rows + "条管理员信息");
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错原因：" + ex.Message);
            }
        }//delete
        private void button1_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from 管理员 where 管理员编号='" + textBox1.Text + "'";
            SqlCommand cmd1 = get_SqlCommand(sql1);
            SqlDataReader r = cmd1.ExecuteReader();
            if (!r.Read())
            {
                MessageBox.Show("该管理员不存在，请重新输入");
                textBox1.Focus();
                return;
            }
            string attendantid = this.textBox1.Text;
            string sql = "delete from 管理员 where 管理员编号='" + attendantid + "'";
            delete(sql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
