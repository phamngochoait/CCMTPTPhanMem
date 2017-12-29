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
    public partial class frmTacGia : Form
    {
        public frmTacGia()
        {
            InitializeComponent();
        }
        public void Load_Data()
        {
            string str = @"select * from TACGIA";
            DataTable dt = DatabaseService.getDataTable(str);
            dataTacGia.DataSource = dt;
        }
        private void frmTacGia_Load(object sender, EventArgs e)
        {
            this.Load_Data();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string them = @"insert into TACGIA values(N'" + txtTenTG.Text + "',N'" + txtGhiChu.Text + "')";
            DatabaseService.executeQuery(them);
            Load_Data();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string xoa = @"delete from TACGIA where MaTG='" + txtMaTG.Text + "'";
            DialogResult dialog = MessageBox.Show("Bạn có muốn xóa tác giả: " + txtTenTG.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    DatabaseService.executeQuery(xoa);
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
            string capnhat = @"update TACGIA set TenTG=N'" + txtTenTG.Text + "', GhiChu=N'" + txtGhiChu.Text + "' where MaTG='" + txtMaTG.Text + "'";
            DatabaseService.executeQuery(capnhat);
            Load_Data();
        }

        private void dataTacGia_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DataGridViewRow row = new DataGridViewRow();
                row = dataTacGia.Rows[e.RowIndex];
                txtMaTG.Text = row.Cells[0].Value.ToString();
                txtTenTG.Text = row.Cells[1].Value.ToString();
                txtGhiChu.Text = row.Cells[2].Value.ToString();
            }
            catch (Exception) { }
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string TimKiemTS = @"select * from TACGIA where TenTG LIKE N'%" + txtTim.Text + "%'";
            DataTable dt = DatabaseService.getDataTable(TimKiemTS);
            dataTacGia.DataSource = dt;
        }

        private void txtTim_Click(object sender, EventArgs e)
        {
            txtTim.Text = "";
        }
    }
}
