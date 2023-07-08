using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
namespace QUANLYKHOHANG
{
    public partial class FmKhoHang : Form
    {
        SqlConnection _cnn = new SqlConnection("Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True");
        
        SQL dt = new SQL();
        public FmKhoHang()
        {
            InitializeComponent();
        }
        public bool KTThongTin()
        {
            if (txtMaKho.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKho.Focus();
                return false;
            }
            if (txtTenKho.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKho.Focus();
                return false;
            }
            if (txtDiaChiKho.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChiKho.Focus();
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
            cmd.CommandText = "danhsachkhohang";
            cmd.Connection = sqlconn;
            sda.SelectCommand = cmd;
            DataTable da = new DataTable();
            sda.Fill(da);
            DataKhoHang.DataSource = da;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnTimKiem.Enabled = true;
            btnXoa.Enabled = true;
            btnXuat.Enabled = true;
            if (KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn thêm không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.ThemKHO(txtMaKho.Text, txtTenKho.Text, txtDiaChiKho.Text, txtGhiChu.Text))
                    //{
                    //    MessageBox.Show("Thêm Thành Công");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("INSERT_KHO_HANG", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MAKHO", SqlDbType.NChar).Value = txtMaKho.Text;
                        cmd.Parameters.AddWithValue("@TENKHO", SqlDbType.NVarChar).Value = txtTenKho.Text;
                        cmd.Parameters.AddWithValue("@DIACHI", SqlDbType.NVarChar).Value = txtDiaChiKho.Text;
                        cmd.Parameters.AddWithValue("@GHICHU", SqlDbType.NVarChar).Value = txtGhiChu.Text;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn Xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //if (dt.XoaKHO(txtMaKho.Text))
                //{
                //    MessageBox.Show("Xóa Thành Công");
                //}
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE_KHO_HANG", _cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MAKHO", SqlDbType.NChar).Value = txtMaKho.Text;
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(KTThongTin())
            {
                if (MessageBox.Show("Bạn có chắc chắn Sửa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //if (dt.SuaKHO(txtMaKho.Text, txtTenKho.Text, txtDiaChiKho.Text, txtGhiChu.Text))
                    //{
                    //    MessageBox.Show("Sửa Thành Công");
                    //}
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE_KHO_HANG", _cnn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MAKHO", SqlDbType.NChar).Value = txtMaKho.Text;
                        cmd.Parameters.AddWithValue("@TENKHO", SqlDbType.NVarChar).Value = txtTenKho.Text;
                        cmd.Parameters.AddWithValue("@DIACHI", SqlDbType.NVarChar).Value = txtDiaChiKho.Text;
                        cmd.Parameters.AddWithValue("@GHICHU", SqlDbType.NVarChar).Value = txtGhiChu.Text;
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

                for (int i = 1; i < DataKhoHang.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = DataKhoHang.Columns[i - 1].HeaderText;

                }

                for (int i = 0; i < DataKhoHang.Rows.Count; i++)
                {
                    for (int j = 0; j < DataKhoHang.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = DataKhoHang.Rows[i].Cells[j].Value;
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
            }
        }

        private void FmKhocs_Load(object sender, EventArgs e)
        {
            //string stringconn = @"Data Source=LAPTOP-OJDSJCBC;Initial Catalog=QUANLYKHOHANG;Integrated Security=True";
            //SqlConnection sqlconn = new SqlConnection(stringconn);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter sda = new SqlDataAdapter();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "danhsachkhohang";
            //cmd.Connection = sqlconn;
            //sda.SelectCommand = cmd;
            //DataTable da = new DataTable();
            //sda.Fill(da);
            //DataKhoHang.DataSource = da;
            _cnn.Open();
            LoadDulieu();
            //DataKhoHang.DataSource = dt.LoadKHO();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "MAKHO", "*" + txtTimKiem.Text + "*");
            (DataKhoHang.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void DataKhoHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKho.Text = DataKhoHang.SelectedRows[0].Cells[0].Value.ToString();
            txtTenKho.Text = DataKhoHang.SelectedRows[0].Cells[1].Value.ToString();
            txtDiaChiKho.Text = DataKhoHang.SelectedRows[0].Cells[2].Value.ToString();
            txtGhiChu.Text = DataKhoHang.SelectedRows[0].Cells[3].Value.ToString();
        }
    }
}
