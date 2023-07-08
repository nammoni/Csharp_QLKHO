using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QUANLYKHOHANG
{
    public partial class FmPhieu : Form
    {
        SqlConnection _cnn = new SqlConnection("Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True");
        SQL dt = new SQL();
        SqlCommand cmd;
        Modify modify = new Modify();
        string sql;
        SqlConnection ketnoi;
        string chuoiketnoi = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        public FmPhieu()
        {
            _cnn.Open();
            InitializeComponent();
            
        }

        private void FmPhieu_Load(object sender, EventArgs e)
        {


            //Load proc Phieu Nhap
            //string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            //SqlConnection sqlconn = new SqlConnection(stringconn);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter sda = new SqlDataAdapter();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "danhsachphieunhap";
            //cmd.Connection = sqlconn;
            //sda.SelectCommand = cmd;
            //DataTable da = new DataTable();
            //sda.Fill(da);
            //dataPN.DataSource = da;


            //Load proc Phieu Xuất
            //string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            //SqlConnection sqlconn = new SqlConnection(stringconn);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter sda = new SqlDataAdapter();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "danhsachphieuxuat";
            //cmd.Connection = sqlconn;
            //sda.SelectCommand = cmd;
            //DataTable da = new DataTable();
            //sda.Fill(da);
            //dataPX.DataSource = da;
            //dataPN.DataSource = dt.LoadPhieu();
            //dataPX.DataSource = dt.LoadPhieuX();
            LoadComBoxNV();
            LoadComBoxKHX();
            LoadComBoxNVX();
            LoadDulieuN();
            LoadDulieuX();
          
        }

        void LoadComBoxNV()
        {

            var cmd = new SqlCommand("select MANV from NHAN_VIEN", _cnn);
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cmMaNvN.DisplayMember = "MANV";
            cmMaNvN.DataSource = dt;
        }
        void LoadComBoxNVX()
        {

            var cmd = new SqlCommand("select MANV from NHAN_VIEN", _cnn);
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cmMaNVX.DisplayMember = "MANV";
            cmMaNVX.DataSource = dt;
        }
        void LoadComBoxKHX()
        {

            var cmd = new SqlCommand("select MAKH from KHACH_HANG", _cnn);
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cmMaKh.DisplayMember = "MAKH";
            cmMaKh.DataSource = dt;
        }
        public bool KTThongTinN()
        {
            if (txtMaPnN.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã Phiếu Xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaPnN.Focus();
                return false;
            }
            if (cmMaNvN.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmMaNvN.Focus();
                return false;
            }
            return true;
        }
        void LoadDulieuN()
        {
            string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            SqlConnection sqlconn = new SqlConnection(stringconn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "danhsachphieunhaphang";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            dataPN.DataSource = da;
        }
        void LoadDulieuX()
        {
            string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            SqlConnection sqlconn = new SqlConnection(stringconn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "danhsachphieuxuathang";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            dataPX.DataSource = da;
        }

        private void btnThemN_Click(object sender, EventArgs e)
        {
            btnSuaN.Enabled = true;
            btnChiTietPN.Enabled = true;
            btnTKn.Enabled = true;
            btnXoaN.Enabled = true;
            if (KTThongTinN())
            {
              
                if (MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.ThemPhieu(txtMaPnN.Text, dateNgayN.Value, txtTongTienN.Text, cmMaNvN.Text))
                    //{
                    //    MessageBox.Show("Thêm Thành Công", "Thông Báo");
                    //}

                    try
                    {
                        SqlCommand cmd = new SqlCommand("INSERT_PHIEU_NHAPHANG", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MAPHIEU_NH", SqlDbType.Char).Value = txtMaPnN.Text;
                        cmd.Parameters.AddWithValue("@NGAY_NH", SqlDbType.Date).Value = dateNgayN.Value;
                        cmd.Parameters.AddWithValue("@TONGTIEN_NH", SqlDbType.Float).Value = txtTongTienN.Text;
                        cmd.Parameters.AddWithValue("@MANV", SqlDbType.NChar).Value = cmMaNvN.Text;
                        cmd.ExecuteNonQuery();
                        LoadDulieuN();
                        MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
            }
        }

        private void btnTKn_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "MAPHIEU_NH", "*" + txtTimKiemN.Text + "*");
            (dataPN.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void btnSuaN_Click(object sender, EventArgs e)
        {
            if(KTThongTinN())
            {
                if (MessageBox.Show("Bạn có chắc chắn Sửa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.SuaPhieuN(txtMaPnN.Text, dateNgayN.Value, txtTongTienN.Text ,cmMaNvN.Text))
                    //{
                    //    MessageBox.Show("Sửa Thành Công", "Thông Báo");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE_PHIEU_NHAPHANG", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MAPHIEU_NH", SqlDbType.Char).Value = txtMaPnN.Text;
                        cmd.Parameters.Add("@NGAY_NH", SqlDbType.Date).Value = dateNgayN.Value;
                        cmd.Parameters.Add("@TONGTIEN_NH", SqlDbType.Float).Value = txtTongTienN.Text;
                        cmd.Parameters.Add("@MANV", SqlDbType.NChar).Value = cmMaNvN.Text;
                        cmd.ExecuteNonQuery();
                        LoadDulieuN();
                        MessageBox.Show("Đã sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void btnXoaN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn Xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //if (dt.XoaPhieuN(txtMaPnN.Text))
                //{
                //    MessageBox.Show("Xóa Thành Công", "Thông Báo");
                //}
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE_PHIEU_NHAPHANG", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MAPHIEU_NH", SqlDbType.Char).Value = txtMaPnN.Text;
                    cmd.ExecuteNonQuery();
                    LoadDulieuN();
                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void dataPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPnN.Text = dataPN.SelectedRows[0].Cells[0].Value.ToString();
            dateNgayN.Text = dataPN.SelectedRows[0].Cells[1].Value.ToString();
            txtTongTienN.Text = dataPN.SelectedRows[0].Cells[2].Value.ToString();
            cmMaNvN.Text = dataPN.SelectedRows[0].Cells[3].Value.ToString();
        }

       
      

        private void dataPX_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtMaPX.Text = dataPX.SelectedRows[0].Cells[0].Value.ToString();
            dataNgayX.Text = dataPX.SelectedRows[0].Cells[1].Value.ToString();
            cmMaKh.Text = dataPX.SelectedRows[0].Cells[3].Value.ToString();
            txtTongTienX.Text = dataPX.SelectedRows[0].Cells[2].Value.ToString();
            cmMaNVX.Text = dataPX.SelectedRows[0].Cells[4].Value.ToString();

        }

        private void btnTimKiemX_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "MAPH_XH", "*" + txtTKX.Text + "*");
            (dataPX.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }
        
       
        private void txtTTX_TextChanged(object sender, EventArgs e)
        {
            string query = "Select sum(SOLUONG_XH*GIA) from SAN_PHAM,PHIEU_XUAT_HANG where SAN_PHAM.MA_SP =PHIEU_XUAT_HANG.MA_SP";
        }

        private void btnInPN_Click(object sender, EventArgs e)
        {
            FmChiTietPhieuNhap fmChiTietPhieuNhap = new FmChiTietPhieuNhap();
            fmChiTietPhieuNhap.Show();
        }

        private void btnINp_Click(object sender, EventArgs e)
        {
            FmChiTietPhieuXuat fmChiTietPhieuXuat = new FmChiTietPhieuXuat();
            fmChiTietPhieuXuat.Show();
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnXoaX_Click(object sender, EventArgs e)
        {
           if (MessageBox.Show("Bạn có chắc chắn Xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //if (dt.XoaPhieuX(txtMaPX.Text))
                //{
                //    MessageBox.Show("Xóa Thành Công", "Thông Báo");

                //}
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE_PHIEU_XUATHANG", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MAPH_XH", SqlDbType.Char).Value = txtMaPX.Text;
                    cmd.ExecuteNonQuery();
                    LoadDulieuX();
                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }  
        }

        private void btnSuaX_Click(object sender, EventArgs e)
        {
            if (KTThongTinX())
            {
                if (MessageBox.Show("Bạn có chắc chắn Sửa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {

                    //if (dt.SuaPhieuX(txtMaPX.Text, dataNgayX.Value, txtTongTienX.Text, cmMaKh.Text, cmMaNVX.Text))
                    //{
                    //    MessageBox.Show("Sửa Thành Công", "Thông Báo");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE_PHIEU_XUATHANG", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MAPH_XH", SqlDbType.Char).Value = txtMaPX.Text;
                        cmd.Parameters.Add("@NGAY_XH", SqlDbType.Date).Value = dataNgayX.Value;
                        cmd.Parameters.Add("@TONGTIEN_XH", SqlDbType.Float).Value = txtTongTienX.Text;
                        cmd.Parameters.Add("@MAKH", SqlDbType.NChar).Value = cmMaKh.Text;
                        cmd.Parameters.Add("@MANV", SqlDbType.NChar).Value = cmMaNVX.Text;
                        cmd.ExecuteNonQuery();
                        LoadDulieuX();
                        MessageBox.Show("Đã sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }
        public bool KTThongTinX()
        {
            if (txtMaPX.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã Phiếu Xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaPX.Focus();
                return false;
            }
            if (cmMaKh.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Mã Khách Hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmMaKh.Focus();
                return false;
            }
            if (cmMaNVX.Text == "")
            {
                MessageBox.Show("Vui lòng chọn mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmMaNVX.Focus();
                return false;
            }
            return true;
        }
        private void btnThemX_Click(object sender, EventArgs e)
        {
            btnXoaX.Enabled = true;
            btnSuaX.Enabled = true;
            btnTkX.Enabled = true;
            btnChiTietPhieuX.Enabled = true;
            if (KTThongTinX())
            {
                
                if (MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.ThemPhieuX(txtMaPX.Text, dataNgayX.Value, txtTongTienX.Text, cmMaKh.Text, cmMaNVX.Text))
                    //{
                    //    MessageBox.Show("Thêm Thành Công", "Thông Báo");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("INSERT_PHIEU_XUATHANG", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MAPH_XH", SqlDbType.Char).Value = txtMaPX.Text;
                        cmd.Parameters.AddWithValue("@NGAY_XH", SqlDbType.Date).Value = dataNgayX.Value;
                        cmd.Parameters.AddWithValue("@TONGTIEN_XH", SqlDbType.Float).Value = txtTongTienX.Text;
                        cmd.Parameters.AddWithValue("@MAKH", SqlDbType.NChar).Value = cmMaKh.Text;
                        cmd.Parameters.AddWithValue("@MANV", SqlDbType.NChar).Value = cmMaNVX.Text;
                        cmd.ExecuteNonQuery();
                        LoadDulieuX();
                        MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cmMaKh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTkX_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "MAPH_XH", "*" + txtTKX.Text + "*");
            (dataPX.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void btnChiTietPN_Click(object sender, EventArgs e)
        {
            FmChiTietNH fmChiTietNH = new FmChiTietNH();
            fmChiTietNH.Show();
        }

        private void btnChiTietPhieuX_Click(object sender, EventArgs e)
        {
            FmChiTietXH fmChiTietXH = new FmChiTietXH();
            fmChiTietXH.Show();
        }
    }
}
