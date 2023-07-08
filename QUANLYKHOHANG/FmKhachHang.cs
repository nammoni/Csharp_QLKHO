using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System;
using System.Data.SqlClient;


namespace QUANLYKHOHANG
{
    public partial class FmKhachHang : Form
    {
        SqlConnection _cnn = new SqlConnection("Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True");
        SQL dt = new SQL();
        public FmKhachHang()
        {
            InitializeComponent();
        }
        public void LoadKH()
        {
          DataKhachHang.DataSource = dt.LoadKH();
        }

      

        private void KhachHang_Load(object sender, System.EventArgs e)
        {
            //string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            //SqlConnection sqlconn = new SqlConnection(stringconn);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter sda = new SqlDataAdapter();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "danhsachkhachhang";
            //cmd.Connection = sqlconn;
            //sda.SelectCommand = cmd;
            //DataTable da = new DataTable();
            //sda.Fill(da);
            //DataKhachHang.DataSource = da;
            //LoadKH();
            _cnn.Open();
            LoadDulieu();
        }

        private void btnXoa_Click(object sender, System.EventArgs e)
        {
            if(dt.XoaKH(txtMaKh.Text))
            {
                MessageBox.Show("Xóa Thành Công");
            }
        }

        private void btnSua_Click(object sender, System.EventArgs e)
        {
           
               if(dt.SuaKH(txtMaKh.Text, txtTenKh.Text, txtDiaChi.Text, cmGt.Text, txtDTKh.Text, txtMailKh.Text, txtFax.Text))
            {
                MessageBox.Show("Xóa Thành Công");
            }
        }

        private void DataKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKh.Text = DataKhachHang.SelectedRows[0].Cells[0].Value.ToString();
            txtTenKh.Text = DataKhachHang.SelectedRows[0].Cells[1].Value.ToString();
            txtDiaChi.Text = DataKhachHang.SelectedRows[0].Cells[2].Value.ToString();
            cmGt.SelectedItem = DataKhachHang.SelectedRows[0].Cells[3].Value.ToString();
            txtDTKh.Text = DataKhachHang.SelectedRows[0].Cells[4].Value.ToString();
            txtMailKh.Text = DataKhachHang.SelectedRows[0].Cells[5].Value.ToString();
            txtFax.Text = DataKhachHang.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void txtTenKh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            this.errorProvider1.Clear();
        }

        private void txtDTKh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMailKh_TextChanged(object sender, EventArgs e)
        {
            if (!(new Regex(@"^[\w\.]+@([\w]+\.)+[\w]{2,4}$").IsMatch(txtMailKh.Text)))
            {
                errorProvider1.SetError(txtMailKh, "Định dạng email sai!");
                return;
            }
            this.errorProvider1.Clear();
        }
        public bool KTThongTin()
        {
            if (txtMaKh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKh.Focus();
                return false;
            }
            if (txtTenKh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKh.Focus();
                return false;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
            if (cmGt.Text == "")
            {
                MessageBox.Show("Vui lòng chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmGt.Focus();
                return false;
            }
            if (txtDTKh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDTKh.Focus();
                return false;
            }
            if (txtMailKh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập gmail", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMailKh.Focus();
                return false;
            }
            if (txtFax.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số fax", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFax.Focus();
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
            cmd.CommandText = "danhsachkhachhang";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            DataKhachHang.DataSource = da;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoaKh.Enabled = true;
            btnTimKiem.Enabled = true;
            btnXuat.Enabled = true;
            btnSuaKh.Enabled = true;
            if (KTThongTin())
            {
                //if (MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //{
                //    if (dt.ThemKH(txtMaKh.Text, txtTenKh.Text, txtDiaChi.Text, cmGt.Text, txtDTKh.Text, txtMailKh.Text, txtFax.Text))
                //    {
                //        MessageBox.Show("Thêm Thành Công");
                //        cmGt.Items.Add(cmGt.Text);
                //    }
                //}
                if (MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    try
                    {

                        SqlCommand cmd = new SqlCommand("INSERT_KHACH_HANG", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MAKH", SqlDbType.NChar).Value = txtMaKh.Text;
                        cmd.Parameters.AddWithValue("@TENKH", SqlDbType.NVarChar).Value = txtTenKh.Text;
                        cmd.Parameters.AddWithValue("@DIACHI", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                        cmd.Parameters.AddWithValue("@GIOITINH", SqlDbType.NVarChar).Value = cmGt.Text;
                        cmd.Parameters.AddWithValue("@SDT", SqlDbType.NChar).Value = txtDTKh.Text;
                        cmd.Parameters.AddWithValue("@EMAIL", SqlDbType.NVarChar).Value = txtMailKh.Text;
                        cmd.Parameters.AddWithValue("@FAX", SqlDbType.NChar).Value = txtFax.Text;
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

        private void btnXoaKh_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn Xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //if (dt.XoaKH(txtMaKh.Text))
                //{
                //    MessageBox.Show("Xóa Thành Công");
                //}
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE_KHACH_HANG", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MAKH", SqlDbType.NChar).Value = txtMaKh.Text;
                    cmd.ExecuteNonQuery();
                    LoadDulieu();
                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa không Thành Công!!!", "Thông Báo");
                }
            }
        }

        private void btnSuaKh_Click(object sender, EventArgs e)
        {
            if (KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn Sửa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.SuaKH(txtMaKh.Text, txtTenKh.Text, txtDiaChi.Text, cmGt.Text, txtDTKh.Text, txtMailKh.Text, txtFax.Text))
                    //{
                    //    MessageBox.Show("Xóa Thành Công");
                    //}

                    try
                    {

                        SqlCommand cmd = new SqlCommand("UPDATE_KHACH_HANG", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MAKH", SqlDbType.NChar).Value = txtMaKh.Text;
                        cmd.Parameters.AddWithValue("@TENKH", SqlDbType.NVarChar).Value = txtTenKh.Text;
                        cmd.Parameters.AddWithValue("@DIACHI", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                        cmd.Parameters.AddWithValue("@GIOITINH", SqlDbType.NVarChar).Value = cmGt.Text;
                        cmd.Parameters.AddWithValue("@SDT", SqlDbType.NChar).Value = txtDTKh.Text;
                        cmd.Parameters.AddWithValue("@EMAIL", SqlDbType.NVarChar).Value = txtMailKh.Text;
                        cmd.Parameters.AddWithValue("@FAX", SqlDbType.NChar).Value = txtFax.Text;
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

        private void btnXuat_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "C:";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel Files(2003)|*.xls|Excel Files(2007)|*.xlsx";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 20;

                for (int i = 1; i < DataKhachHang.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = DataKhachHang.Columns[i - 1].HeaderText;

                }

                for (int i = 0; i < DataKhachHang.Rows.Count; i++)
                {
                    for (int j = 0; j < DataKhachHang.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = DataKhachHang.Rows[i].Cells[j].Value;
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "MAKH", "*" + txtTimKiem.Text + "*");
            (DataKhachHang.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }
    }
}
