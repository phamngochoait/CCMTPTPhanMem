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
    public partial class frmTheLoai : Form
    {
        public frmTheLoai()
        {
            InitializeComponent();
        }
        public void Load_data()
        {
            string str = @"select * from THELOAI";
            DataTable dt = DatabaseService.getDataTable(str);
            dataTheLoai.DataSource = dt;
        }
        private void txtTim_Click(object sender, EventArgs e)
        {
            txtTim.Text = "";
        }

        private void frmTheLoai_Load(object sender, EventArgs e)
        {
            this.Load_data();
        }

        private void dataTheLoai_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DataGridViewRow row = new DataGridViewRow();
                row = dataTheLoai.Rows[e.RowIndex];
                txtMaTL.Text = row.Cells[0].Value.ToString();
                txtTenTL.Text = row.Cells[1].Value.ToString();
                txtGhiChu.Text = row.Cells[2].Value.ToString();
            }
            catch (Exception) { }
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string TimKiemTS = @"select * from THELOAI where TenTL LIKE N'%" + txtTim.Text + "%'";
            DataTable dt = DatabaseService.getDataTable(TimKiemTS);
            dataTheLoai.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string them = @"insert into THELOAI values(N'" + txtTenTL.Text + "',N'" + txtGhiChu.Text + "')";
            DatabaseService.executeQuery(them);
            Load_data();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string xoa = @"delete from THELOAI where MaTL='" + txtMaTL.Text + "'";
            DialogResult dialog = MessageBox.Show("Bạn có muốn xóa thể loại: " + txtTenTL.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    DatabaseService.executeQuery(xoa);
                    Load_data();
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
 private void btnXoa_Click(object sender, EventArgs e)
        {
            string xoa = @"delete from THELOAI where MaTL='" + txtMaTL.Text + "'";
            DialogResult dialog = MessageBox.Show("Bạn có muốn xóa thể loại: " + txtTenTL.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    DatabaseService.executeQuery(xoa);
                    Load_data();
                }
                catch (Exception)
                {
                }
            }
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string capnhat = @"update THELOAI set TenTL=N'" + txtTenTL.Text + "', GhiChu=N'" + txtGhiChu.Text + "' where MaTL='" + txtMaTL.Text + "'";
            DatabaseService.executeQuery(capnhat);
            Load_data();
        }
    }
}
