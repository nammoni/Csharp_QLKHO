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
    public partial class FmChiTietXH : Form
    {
        SqlConnection _cnn = new SqlConnection("Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True");
        SQL dt = new SQL();
        public FmChiTietXH()
        {
            InitializeComponent();
        }
        void LoadComBoxSPX()
        {
            var cmd = new SqlCommand("select MA_SP from SAN_PHAM", _cnn);
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cmMaSpX.DisplayMember = "MA_SP";
            cmMaSpX.DataSource = dt;
        }
        void LoadComBoxPX()
        {
            var cmd = new SqlCommand("select MAPH_XH from PHIEU_XUAT_HANG", _cnn);
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cmMaPX.DisplayMember = "MAPH_XH";
            cmMaPX.DataSource = dt;
        }
        public bool KTThongTin()
        {
            if (cmMaPX.Text == "")
            {
                MessageBox.Show("Vui lòng chọn mã phiếu xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmMaPX.Focus();
                return false;
            }
            if (cmMaSpX.Text == "")
            {
                MessageBox.Show("Vui lòng chọn mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmMaSpX.Focus();
                return false;
            }
            if (txtSoLuongX.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongX.Focus();
                return false;
            }
            return true;
        }
        private void FmChiTietXH_Load(object sender, EventArgs e)
        {
            _cnn.Open();
            LoadComBoxPX();
            LoadComBoxSPX();
            //string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            //SqlConnection sqlconn = new SqlConnection(stringconn);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter sda = new SqlDataAdapter();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "danhsachchitietphieuxuat";
            //cmd.Connection = sqlconn;
            //sda.SelectCommand = cmd;
            //DataTable da = new DataTable();
            //sda.Fill(da);
            //DataCTPX.DataSource = da;
            LoadDulieu();
            
        }
        void LoadDulieu()
        {
            string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            SqlConnection sqlconn = new SqlConnection(stringconn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "danhsachchitietphieuxuat";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            DataCTPX.DataSource = da;
        }
        private void btnThemPhieuX_Click(object sender, EventArgs e)
        {
            btnSuaPhieuX.Enabled = true;
            btnXoaPhieuX.Enabled = true;
            btnInPX.Enabled = true;
            if (KTThongTin())
            {
                try
                {
                    //string sql = "insert into CHITIET_NH values(@MAPH_XH,@MA_SP,@SOLUONG,@THANHTIEN)";

                    //SqlCommand cm = new SqlCommand(sql, _cnn);
                    //cm.Parameters.AddWithValue("MAPH_XH", cmMaPX);
                    //cm.Parameters.AddWithValue("MA_SP", cmMaSpX.Text);
                    //cm.Parameters.AddWithValue("SOLUONG", txtSoLuongX.Text);
                    //cm.Parameters.AddWithValue("THANHTIEN", txtTongTien.Text);
                    //cm.ExecuteNonQuery();


                    SqlCommand cmd = new SqlCommand("INSERT_CHITIET_XH", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MAPH_XH", SqlDbType.Char).Value = cmMaPX.Text;
                    cmd.Parameters.AddWithValue("@MA_SP", SqlDbType.Char).Value = cmMaSpX.Text;
                    cmd.Parameters.AddWithValue("@SOLUONG", SqlDbType.Int).Value = txtSoLuongX.Text;
                    cmd.Parameters.AddWithValue("@THANHTIEN", SqlDbType.Float).Value = txtTongTien.Text;
                    cmd.ExecuteNonQuery();
                    LoadDulieu();
                    MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    MessageBox.Show("Thêm Thành Công", "Thông báo");
                    LoadDulieu();

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Thêm thất bại", "Thông Báo");
                }
            }

        }

        private void btnXoaPhieuX_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE_CHITIET_XH", _cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MAPH_XH", SqlDbType.Char).Value = cmMaPX.Text;
                cmd.Parameters.Add("@MA_SP", SqlDbType.Char).Value = cmMaSpX.Text;
                cmd.ExecuteNonQuery();
                LoadDulieu();
                MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa không Thành Công!!!", "Thông Báo");
            }
        }

        private void btnSuaPhieuX_Click(object sender, EventArgs e)
        {

            //string sqlSua = "update CHITIET_NH set SOLUONG = @SOLUONG,THANHTIEN = @THANHTIEN where MAPH_XH = @MAPH_XH and MA_SP = @MA_SP";
            //SqlCommand cm = new SqlCommand(sqlSua, _cnn);
            //cm.Parameters.AddWithValue("MAPH_XH", cmMaPX);
            //cm.Parameters.AddWithValue("MA_SP", cmMaSpX.Text);
            //cm.Parameters.AddWithValue("SOLUONG", txtSoLuongX.Text);
            //cm.Parameters.AddWithValue("THANHTIEN", txtTongTien.Text);
            //cm.ExecuteNonQuery();
            //LoadDulieu();


            if (KTThongTin())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("UPDATE_CHITIET_XH", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MAPH_XH", SqlDbType.Char).Value = cmMaPX.Text;
                    cmd.Parameters.Add("@MA_SP", SqlDbType.Char).Value = cmMaSpX.Text;
                    cmd.Parameters.Add("@SOLUONG", SqlDbType.Int).Value = txtSoLuongX.Text;
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

        private void DataCTPX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmMaPX.Text = DataCTPX.SelectedRows[0].Cells[0].Value.ToString();
            cmMaSpX.Text = DataCTPX.SelectedRows[0].Cells[1].Value.ToString();
            txtSoLuongX.Text = DataCTPX.SelectedRows[0].Cells[2].Value.ToString();
            txtTongTien.Text = DataCTPX.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btnInPX_Click(object sender, EventArgs e)
        {
            FmChiTietPhieuXuat fmChiTietPhieuXuat = new FmChiTietPhieuXuat();
            fmChiTietPhieuXuat.Show();
        }
    }
}
