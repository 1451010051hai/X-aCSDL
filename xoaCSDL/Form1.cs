using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace xoaCSDL
{
    public partial class Form1 : Form
    {
        private SqlConnection cn=null;// khai báo
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // chuối kết nối
            string cnstr = "Server=. ;Database=QLBanHang;Integrated security=true;";
            cn = new SqlConnection(cnstr);
            dtgv.DataSource = GetData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void connect() {
            try
            {
                if (cn != null &&  cn.State != ConnectionState.Open) {
                    cn.Open();
                    
                }


            }
            catch (InvalidOperationException ex)
            {

                MessageBox.Show("không thể kết nối"+ ex.Message);
            }
            
        }
        //đóng kết nối
        public void disConnect() {
            if (cn != null && cn.State != ConnectionState.Closed)
                cn.Close();
        }
        private List<Object>GetData(){
            connect();
            string sql = "select * from LoaiSP";
            List<Object> list = new List<object>();
            try
            {
                // dùng đối tượng sql Command
                SqlCommand cm = new SqlCommand(sql,cn);
                SqlDataReader dr = cm.ExecuteReader();
                // khai báo các kiểu dữ liệu trong csdl
                string  ten;
              
                int ma;
                // bắt đầu đọc từ csdl lên grv
                while(dr.Read()){
                    // bắt đầu lấy dữ liệu từng cột
                    ma = dr.GetInt32(0);
                    ten = dr.GetString(1);
                   
                    //
                    var prod = new
                    {
                        
                        TenSP = ten,
                        maLoai = ma
                    };
                    list.Add(prod);

                }
                
                dr.Close();
                

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally{
                disConnect();
            }
            return list;
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            string query = "Delete from LoaiSP where MaLoaiSp = " + txtMaLoai.Text.ToString();
            SqlCommand cmd = new SqlCommand(query,cn);
            int NumberOfRows = 0;
            connect();
            NumberOfRows = cmd.ExecuteNonQuery();
            MessageBox.Show("Số dòng đã xóa là "+ NumberOfRows.ToString());
            disConnect();
            dtgv.DataSource = GetData();

        }

    }
}
