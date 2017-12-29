using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThuVien_V._2
{
    public partial class frmDocGia : Form
    {
        public frmDocGia()
        {
            InitializeComponent();
        }
        public void Load_Data()
        {
            string str = @"select * from DOCGIA";
            DataTable dt = DatabaseService.getDataTable(str);
            dataDocGia.DataSource = dt;
        }

        private void frmDocGia_Load(object sender, EventArgs e)
        {
            this.Load_Data();
        }

        private void dataDocGia_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DataGridViewRow row = new DataGridViewRow();
                row = dataDocGia.Rows[e.RowIndex];
                txtMaDG.Text = row.Cells[0].Value.ToString();
                txtTenDG.Text = row.Cells[1].Value.ToString();
                dateNgaySinh.Text = row.Cells[2].Value.ToString();
                if (row.Cells[3].Value.ToString() == "False")
                    swGioiTinh.Value = true;
                else
                    swGioiTinh.Value = false;
                
                txtDiaChi.Text = row.Cells[4].Value.ToString();
                txtSDT.Text = row.Cells[5].Value.ToString();
                txtEmail.Text = row.Cells[6].Value.ToString();
                txtGhiChu.Text = row.Cells[7].Value.ToString();
                
            }
            catch (Exception) { }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string GioiTinh = "";
            if (swGioiTinh.Value == true)
                GioiTinh = "0";
            else GioiTinh = "1";
            string them = @"insert into DOCGIA values (N'" + txtTenDG.Text + "','" + dateNgaySinh.Text + "',N'" + GioiTinh + "',N'" + txtDiaChi.Text + "','" + txtSDT.Text + "','" + txtEmail.Text + "',N'" + txtGhiChu.Text + "')";
            DatabaseService.executeQuery(them);
            Load_Data();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string XoaTS = "DELETE FROM DOCGIA WHERE MaDG='" + txtMaDG.Text + "'";
            DialogResult dialog = MessageBox.Show("Bạn có muốn xóa độc giả: " + txtTenDG.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    DatabaseService.executeQuery(XoaTS);
                    Load_Data();
                }
                catch (Exception)
                {
                }
            }
            else if (dialog == DialogResult.No)
            {
                //
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string GioiTinh = "";
            if (swGioiTinh.Value == true)
                GioiTinh = "0";
            else GioiTinh = "1";

            string CapNhatTS = @"update DOCGIA set TenDG=N'" + txtTenDG.Text + "',GioiTinh='" + GioiTinh + "',NgaySinh='" + dateNgaySinh.Text + "',DiaChi=N'" + txtDiaChi.Text + "',SDT='" + txtSDT.Text + "',Email='" + txtEmail.Text + "',GhiChu=N'" + txtGhiChu.Text + "' where MaDG='" + txtMaDG.Text + "'";
            DatabaseService.executeQuery(CapNhatTS);
            Load_Data();
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string TimKiemTS = @"select * from DOCGIA where TenDG LIKE N'%" + txtTim.Text + "%'";
            DataTable dt = DatabaseService.getDataTable(TimKiemTS);
            dataDocGia.DataSource = dt;
        }

        private void txtTim_Click(object sender, EventArgs e)
        {
            txtTim.Text = "";
        }
    }
}
