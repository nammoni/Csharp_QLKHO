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
    public partial class FmLoaiSP : Form
    {
        SqlConnection _cnn = new SqlConnection("Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True");
        SQL dt = new SQL();
        public FmLoaiSP()
        {
            InitializeComponent();
        }
        public bool KTThongTin()
        {
            if (txtMaLoai.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoai.Focus();
                return false;
            }
            if (txtTenLoai.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenLoai.Focus();
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
            cmd.CommandText = "danhsachloaisp";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            DataLSP.DataSource = da;

        }
        private void btnThemLSP_Click(object sender, EventArgs e)
        {
            btnXoaLSP.Enabled = true;
            btnSuaLSP.Enabled = true;
            if (KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.ThemLSP(txtMaLoai.Text, txtTenLoai.Text))
                    //{
                    //    MessageBox.Show("Thêm Thành Công");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("INSERT_LOAISP", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MALOAI", SqlDbType.NChar).Value = txtMaLoai.Text;
                        cmd.Parameters.AddWithValue("@TENLOAI", SqlDbType.NVarChar).Value = txtTenLoai.Text;
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
        private void btnXoaLSP_Click(object sender, EventArgs e)
        {
            if (KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn Xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.XoaLSP(txtMaLoai.Text))
                    //{
                    //    MessageBox.Show("Xóa Thành Công");
                    //}

                    try
                    {
                        SqlCommand cmd = new SqlCommand("DELETE_LOAISP", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MALOAI", SqlDbType.NChar).Value = txtMaLoai.Text;
                        cmd.ExecuteNonQuery();
                        LoadDulieu();
                        MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa Không thành công");
                    }

                }
            }
        }

        private void btnSuaLSP_Click(object sender, EventArgs e)
        {
            if (KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn Sửa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.SuaLSP(txtMaLoai.Text, txtTenLoai.Text))
                    //{
                    //    MessageBox.Show("Thêm Thành Công");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE_LOAISP", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MALOAI", SqlDbType.NChar).Value = txtMaLoai.Text;
                        cmd.Parameters.Add("@TENLOAI", SqlDbType.NVarChar).Value = txtTenLoai.Text;
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

        private void FmLoaiSP_Load(object sender, EventArgs e)
        {
            //string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            //SqlConnection sqlconn = new SqlConnection(stringconn);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter sda = new SqlDataAdapter();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "danhsachloaisp";
            //cmd.Connection = sqlconn;
            //sda.SelectCommand = cmd;
            //DataTable da = new DataTable();
            //////sda.Fill(da);
            //DataLSP.DataSource = da;
            //DataLSP.DataSource = dt.LoadLSP();
            _cnn.Open();
            LoadDulieu();
        }

        private void DataLSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLoai.Text = DataLSP.SelectedRows[0].Cells[0].Value.ToString();
            txtTenLoai.Text = DataLSP.SelectedRows[0].Cells[1].Value.ToString();
        }

      
    }
}
