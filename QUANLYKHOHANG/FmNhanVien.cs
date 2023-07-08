using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QUANLYKHOHANG
{
    public partial class FmNhanVien : Form
    {
        SqlConnection _cnn = new SqlConnection("Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True");
        SQL dt = new SQL();
        public FmNhanVien()
        {
            InitializeComponent();
        }

        


        private void FmNhanVien_Load(object sender, EventArgs e)
        {
            //Load proc Nhan Vien
            //string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            //SqlConnection sqlconn = new SqlConnection(stringconn);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter sda = new SqlDataAdapter();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "danhsachnhanvien";
            //cmd.Connection = sqlconn;
            //sda.SelectCommand = cmd;
            //DataTable da = new DataTable();
            //sda.Fill(da);
            //DataNhanVien.DataSource = da;
           // DataNhanVien.DataSource = dt.LoadNV();
            _cnn.Open();
            LoadDulieu();
        }

        private void DataNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNv.Text = DataNhanVien.SelectedRows[0].Cells[0].Value.ToString();
            txtTenNv.Text = DataNhanVien.SelectedRows[0].Cells[1].Value.ToString();
            txtMailNv.Text = DataNhanVien.SelectedRows[0].Cells[2].Value.ToString();
            dateNv.Text = DataNhanVien.SelectedRows[0].Cells[3].Value.ToString();
           cmGtNv.SelectedItem = DataNhanVien.SelectedRows[0].Cells[4].Value.ToString();
            txtDtNv.Text = DataNhanVien.SelectedRows[0].Cells[5].Value.ToString();
            txtCvNv.Text = DataNhanVien.SelectedRows[0].Cells[6].Value.ToString();
            txtDiaChiNv.Text = DataNhanVien.SelectedRows[0].Cells[7].Value.ToString();
            txtLuongNv.Text = DataNhanVien.SelectedRows[0].Cells[8].Value.ToString();
            cmBp.SelectedItem = DataNhanVien.SelectedRows[0].Cells[9].Value.ToString();
        }

        private void txtMaNv_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
            //this.errorProvider1.Clear();
        }

        private void txtMailNv_TextChanged(object sender, EventArgs e)
        {
            if (!(new Regex(@"^[\w\.]+@([\w]+\.)+[\w]{2,4}$").IsMatch(txtMailNv.Text)))
            {
                errorProvider1.SetError(txtMailNv, "Định dạng email sai!");
                return;
            }
            this.errorProvider1.Clear();
        }

        private void txtDtNv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "MANV", "*" + txtTimKiem.Text + "*");
            (DataNhanVien.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dt.XoaNV(txtMaNv.Text))
            {
                MessageBox.Show("Xóa Thành Công", "Thông Báo");
            }
        }

        public bool KTThongTin()
        {
            if (txtMaNv.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhà nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNv.Focus();
                return false;
            }
            if (txtTenNv.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNv.Focus();
                return false;
            }
            if (txtMailNv.Text == "")
            {
                MessageBox.Show("Vui lòng nhập gmail", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMailNv.Focus();
                return false;
            }
            if (cmGtNv.Text == "")
            {
                MessageBox.Show("Vui lòng chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmGtNv.Focus();
                return false;
            }
            if (txtDtNv.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDtNv.Focus();
                return false;
            }
            if (txtCvNv.Text == "")
            {
                MessageBox.Show("Vui lòng nhập chức vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCvNv.Focus();
                return false;
            }
            if (txtDiaChiNv.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChiNv.Focus();
                return false;
            }
            if (txtLuongNv.Text == "")
            {
                MessageBox.Show("Vui lòng nhập lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLuongNv.Focus();
                return false;
            }
            if (cmBp.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bộ phận", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmBp.Focus();
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
            cmd.CommandText = "danhsachnhanvien";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            DataNhanVien.DataSource = da;
        }
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoaNV.Enabled = true;
            btnTimKiem.Enabled = true;
            if (KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.ThemNV(txtMaNv.Text, txtTenNv.Text, txtMailNv.Text, dateNv.Value, cmGtNv.Text, txtDtNv.Text, txtCvNv.Text, txtDiaChiNv.Text, txtLuongNv.Text, cmBp.Text))
                    //{
                    //    MessageBox.Show("Thêm Thành Công", "Thông Báo");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("INSERT_NHAN_VIEN", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MANV", SqlDbType.NChar).Value = txtMaNv.Text;
                        cmd.Parameters.AddWithValue("@TEN_NV", SqlDbType.NVarChar).Value = txtTenNv.Text;
                        cmd.Parameters.AddWithValue("@EMAIL_NV", SqlDbType.NVarChar).Value = txtMailNv.Text;
                        cmd.Parameters.AddWithValue("@NGSINH_NV", SqlDbType.NChar).Value = dateNv.Value;
                        cmd.Parameters.AddWithValue("@GIOITINH_NV", SqlDbType.NVarChar).Value = cmGtNv.Text;
                        cmd.Parameters.AddWithValue("@SDT_NV", SqlDbType.NChar).Value = txtDtNv.Text;
                        cmd.Parameters.AddWithValue("@CHUCVU_NV", SqlDbType.NVarChar).Value = txtCvNv.Text;
                        cmd.Parameters.AddWithValue("@DIACHI_NV", SqlDbType.NVarChar).Value = txtDiaChiNv.Text;
                        cmd.Parameters.AddWithValue("@LUONG_NV", SqlDbType.Float).Value = txtLuongNv.Text;
                        cmd.Parameters.AddWithValue("@BOPHAN_NV", SqlDbType.NVarChar).Value = cmBp.Text;
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

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //if (dt.XoaNV(txtMaNv.Text))
                //{
                //    MessageBox.Show("Xóa Thành Công", "Thông Báo");
                //}
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE_NHAN_VIEN", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MANV", SqlDbType.NChar).Value = txtMaNv.Text;
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn Sửa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.SuaNV(txtMaNv.Text, txtTenNv.Text, txtMailNv.Text, dateNv.Value, cmGtNv.Text, txtDtNv.Text, txtCvNv.Text, txtDiaChiNv.Text, txtLuongNv.Text, cmBp.Text))
                    //{
                    //    MessageBox.Show("Sửa Thành Công", "Thông Báo");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE_NHAN_VIEN", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MANV", SqlDbType.NChar).Value = txtMaNv.Text;
                        cmd.Parameters.Add("@TEN_NV", SqlDbType.NVarChar).Value = txtTenNv.Text;
                        cmd.Parameters.Add("@EMAIL_NV", SqlDbType.NVarChar).Value = txtMailNv.Text;
                        cmd.Parameters.Add("@NGSINH_NV", SqlDbType.Date).Value = dateNv.Value;
                        cmd.Parameters.Add("@GIOITINH_NV", SqlDbType.NVarChar).Value = cmGtNv.Text;
                        cmd.Parameters.Add("@SDT_NV", SqlDbType.NChar).Value = txtDtNv.Text;
                        cmd.Parameters.Add("@CHUCVU_NV", SqlDbType.NVarChar).Value = txtCvNv.Text;
                        cmd.Parameters.Add("@DIACHI_NV", SqlDbType.NVarChar).Value = txtDiaChiNv.Text;
                        cmd.Parameters.Add("@LUONG_NV", SqlDbType.Float).Value = txtLuongNv.Text;
                        cmd.Parameters.Add("@BOPHAN_NV", SqlDbType.NVarChar).Value = cmBp.Text;
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

        private void txtMaNv_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }
    }
}

      

