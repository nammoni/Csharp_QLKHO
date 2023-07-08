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
using System.Configuration;

namespace QUANLYKHOHANG
{
    public partial class FmChiTietNH : Form
    {
        SqlConnection _cnn = new SqlConnection("Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True");
        SQL dt = new SQL();
        SqlCommand cm = new SqlCommand();
        public FmChiTietNH()
        {
            InitializeComponent();
        }
        public bool KTThongTin()
        {
            if (cmMaPN.Text == "")
            {
                MessageBox.Show("Vui lòng chọn mã phiếu nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmMaPN.Focus();
                return false;
            }
            if (cmMaSp.Text == "")
            {
                MessageBox.Show("Vui lòng chọn mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmMaSp.Focus();
                return false;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return false;
            }
            return true;
        }
        private void btnThemPhieu_Click(object sender, EventArgs e)
        {
            btnSuaPhieu.Enabled = true;
            btnXoaPhieu.Enabled = true;
            btnInPN.Enabled = true;
            if (KTThongTin())
            {

                //string sql = "insert into CHITIET_NH values(@MAPHIEU_NH,@MA_SP,@SOLUONG,@THANHTIEN)";

                //SqlCommand cm = new SqlCommand(sql, _cnn);
                //cm.Parameters.AddWithValue("MAPHIEU_NH", cmMaPN.Text);
                //cm.Parameters.AddWithValue("MA_SP", cmMaSp.Text);
                //cm.Parameters.AddWithValue("SOLUONG", txtSoLuong.Text);
                //cm.Parameters.AddWithValue("THANHTIEN", txtTongTien.Text);
                //cm.ExecuteNonQuery();
                //LoadDulieu();
            }

            {
                try
                {

                    SqlCommand cmd = new SqlCommand("INSERT_CHITIET_NH", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MAPHIEU_NH", SqlDbType.Char).Value = cmMaPN.Text;
                    cmd.Parameters.AddWithValue("@MA_SP", SqlDbType.Char).Value = cmMaSp.Text;
                    cmd.Parameters.AddWithValue("@SOLUONG", SqlDbType.Int).Value = txtSoLuong.Text;
                    cmd.Parameters.AddWithValue("@THANHTIEN", SqlDbType.Float).Value = txtTongTien.Text;
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
        void LoadComBoxSPN()
        {
            var cmd = new SqlCommand("select MA_SP from SAN_PHAM", _cnn);
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cmMaSp.DisplayMember = "MA_SP";
            cmMaSp.DataSource = dt;
        }
        void LoadComBoxPN()
        {
            var cmd = new SqlCommand("select MAPHIEU_NH from PHIEU_NHAP_HANG", _cnn);
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cmMaPN.DisplayMember = "MAPHIEU_NH";
            cmMaPN.DataSource = dt;
        }
        void LoadDulieu()
        {
            string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            SqlConnection sqlconn = new SqlConnection(stringconn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "danhsachchitietphieunhap";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            DataCTPN.DataSource = da;
        }
        private void FmChiTietNH_Load(object sender, EventArgs e)
        {
            btnSuaPhieu.Enabled = true;
            btnXoaPhieu.Enabled = true;
            btnInPN.Enabled = true;
            _cnn.Open();
            LoadComBoxPN();
            LoadComBoxSPN();
            LoadDulieu();
            //DataCTPN.DataSource = dt.LoadLCHITIETNH();
        }

        private void btnXoaPhieu_Click(object sender, EventArgs e)
        {
            //string sqlXoa = "Delete From CHITIET_NH where MAPHIEU_NH = @MAPHIEU_NH and MA_SP =@MA_SP ";
            //SqlCommand cm = new SqlCommand(sqlXoa, _cnn);
            //cm.Parameters.AddWithValue("MAPHIEU_NH", cmMaPN.Text);
            //cm.Parameters.AddWithValue("MA_SP", cmMaSp.Text);
            //cm.Parameters.AddWithValue("SOLUONG", txtSoLuong.Text);
            //cm.Parameters.AddWithValue("THANHTIEN", txtTongTien.Text);
            //cm.ExecuteNonQuery();
            //LoadDulieu();
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE_CHITIET_NH", _cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MAPHIEU_NH", SqlDbType.Char).Value = cmMaPN.Text;
                cmd.Parameters.Add("@MA_SP", SqlDbType.Char).Value = cmMaSp.Text;
                cmd.ExecuteNonQuery();
                LoadDulieu();
                MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa không Thành Công!!!", "Thông Báo");
            }
        }

        private void btnSuaPhieu_Click(object sender, EventArgs e)
        {

                //string sqlSua = "update CHITIET_NH set SOLUONG = @SOLUONG,THANHTIEN = @THANHTIEN where MAPHIEU_NH = @MAPHIEU_NH and MA_SP = @MA_SP";
                //SqlCommand cm = new SqlCommand(sqlSua, _cnn);
                //cm.Parameters.AddWithValue("MAPHIEU_NH", cmMaPN.Text);
                //cm.Parameters.AddWithValue("MA_SP", cmMaSp.Text);
                //cm.Parameters.AddWithValue("SOLUONG", txtSoLuong.Text);
                //cm.Parameters.AddWithValue("THANHTIEN", txtTongTien.Text);
                //cm.ExecuteNonQuery();
                //LoadDulieu();


            if (KTThongTin())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("UPDATE_CHITIET_NH", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MAPHIEU_NH", SqlDbType.Char).Value = cmMaPN.Text;
                    cmd.Parameters.Add("@MA_SP", SqlDbType.Char).Value = cmMaSp.Text;
                    cmd.Parameters.Add("@SOLUONG", SqlDbType.Int).Value = txtSoLuong.Text;
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
    

        private void DataCTPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmMaPN.Text = DataCTPN.SelectedRows[0].Cells[0].Value.ToString();
            cmMaSp.Text = DataCTPN.SelectedRows[0].Cells[1].Value.ToString();
            txtSoLuong.Text = DataCTPN.SelectedRows[0].Cells[2].Value.ToString();
            txtTongTien.Text = DataCTPN.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btnInPN_Click(object sender, EventArgs e)
        {
            FmChiTietPhieuNhap fmChiTietPhieuNhap = new FmChiTietPhieuNhap();
            fmChiTietPhieuNhap.Show();
        }
    }
}
