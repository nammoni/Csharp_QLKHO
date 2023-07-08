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
namespace QUANLYKHOHANG
{
    public partial class FmNCC : Form
    {
        SqlConnection _cnn = new SqlConnection("Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True");
        SQL dt = new SQL();
        public FmNCC()
        {
            InitializeComponent();
        }


        private void FmNCC_Load(object sender, EventArgs e)
        {
            //string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            //SqlConnection sqlconn = new SqlConnection(stringconn);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter sda = new SqlDataAdapter();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "danhsachnhacungcap";
            //cmd.Connection = sqlconn;
            //sda.SelectCommand = cmd;
            //DataTable da = new DataTable();
            //////sda.Fill(da);
            //DataNCC.DataSource = da;
           // DataNCC.DataSource = dt.LoadNCC();
            _cnn.Open();
            LoadDulieu();
        }

        private void DataNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNCC.Text = DataNCC.SelectedRows[0].Cells[0].Value.ToString();
            txtTenNCC.Text = DataNCC.SelectedRows[0].Cells[1].Value.ToString();
            txtDcNCC.Text = DataNCC.SelectedRows[0].Cells[2].Value.ToString();
        }
        public bool KTThongTin()
        {
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return false;
            }
            if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCC.Focus();
                return false;
            }
            if (txtDcNCC.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDcNCC.Focus();
                return false;
            }

            return true;
        }
        void LoadDulieu()
        {
            string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            SqlConnection sqlconn = new SqlConnection(stringconn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "danhsachnhacungcap";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            DataNCC.DataSource = da;
        }
        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            btnSuaNCC.Enabled = true;
            btnXoaNCC.Enabled = true;
            if (KTThongTin())
            {
               
                if (MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.ThemNCC(txtMaNCC.Text, txtTenNCC.Text, txtDcNCC.Text))
                    //{
                    //    MessageBox.Show("Thêm Thành Công");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("INSERT_NHA_CUNG_CAP", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MA_NCC", SqlDbType.NChar).Value = txtMaNCC.Text;
                        cmd.Parameters.AddWithValue("@TEN_NCC", SqlDbType.NVarChar).Value = txtTenNCC.Text;
                        cmd.Parameters.AddWithValue("@DICHI_NCC", SqlDbType.NVarChar).Value = txtDcNCC.Text;
                        cmd.ExecuteNonQuery();
                        LoadDulieu();
                        MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnXoaNCC_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc chắn Xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //if (dt.XoaNCC(txtMaNCC.Text))
                //{
                //    MessageBox.Show("Xóa Thành Công");
                //}
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE_NHA_CUNG_CAP", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MA_NCC", SqlDbType.NChar).Value = txtMaNCC.Text;
                    cmd.ExecuteNonQuery();
                    LoadDulieu();
                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            if (KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn Sửa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.SuaNCC(txtMaNCC.Text, txtTenNCC.Text, txtDcNCC.Text))
                    //{
                    //    MessageBox.Show("Sửa Thành Công");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE_NHA_CUNG_CAP", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MA_NCC", SqlDbType.NChar).Value = txtMaNCC.Text;
                        cmd.Parameters.Add("@TEN_NCC", SqlDbType.NVarChar).Value = txtTenNCC.Text;
                        cmd.Parameters.Add("@DICHI_NCC", SqlDbType.NVarChar).Value = txtDcNCC.Text;
                        cmd.ExecuteNonQuery();
                        LoadDulieu();
                        MessageBox.Show("Đã sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}


    

