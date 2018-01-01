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
    public partial class frmTTSach : Form
    {
        public frmTTSach()
        {
            InitializeComponent();
        }
        public void Load_Data()
        {
            string str = @"select * from SACH";
            DataTable dt = DatabaseService.getDataTable(str);
            dataSach.DataSource = dt;
        }
     public override string FullResourceName {
            get {
                return "QLThuVien_V._2.RPHoaDon.rpt";
            }
            set {
                // Do nothing
            }
        public void Load_cboxTheLoai()
        {
            string dt = "select * from THELOAI";
            cboxTheLoai.DisplayMember = "TenTL";
            cboxTheLoai.DataSource = DatabaseService.getDataTable(dt);
        }
        // Load Tác Giả
        public void Load_cboxTacGia()
        {
            string dt = "select * from TACGIA";
            cboxTacGia.DisplayMember = "TenTG";
            cboxTacGia.DataSource = DatabaseService.getDataTable(dt);
        }
      
        public void Load_cboxNXB()
        {
            string dt = "select * from NXB";
            cboxNXB.DisplayMember = "TenNXB";
            cboxNXB.DataSource = DatabaseService.getDataTable(dt);
        }
        private void frmTTSach_Load(object sender, EventArgs e)
        {
            this.Load_Data();
            this.Load_cboxTheLoai();
            this.Load_cboxTacGia();
            this.Load_cboxNXB();
        }

        private void dataSach_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DataGridViewRow row = new DataGridViewRow();
                row = dataSach.Rows[e.RowIndex];
                txtMaSach.Text = row.Cells[0].Value.ToString();
                cboxTheLoai.Text = row.Cells[1].Value.ToString();
                txtTenSach.Text = row.Cells[2].Value.ToString();
                cboxTacGia.Text = row.Cells[3].Value.ToString();
                cboxNXB.Text = row.Cells[4].Value.ToString();
                dateNgayNhap.Text = row.Cells[5].Value.ToString();
                txtSLTon.Text = row.Cells[6].Value.ToString();
                txtSLMuon.Text = row.Cells[7].Value.ToString();
            }
            catch (Exception){}
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string them = @"insert into SACH values (N'" + cboxTheLoai.Text + "',N'" + txtTenSach.Text + "',N'" + cboxTacGia.Text + "',N'" + cboxNXB.Text + "','" + dateNgayNhap.Text + "','" + txtSLTon.Text + "','" + txtSLMuon.Text + "')";
            DatabaseService.executeQuery(them);
            Load_Data();
        }

        private void btnTheLoai_Click(object sender, EventArgs e)
        {
            Form S = new frmTheLoai();
            S.ShowDialog();
        }

        private void btnTacGia_Click(object sender, EventArgs e)
        {
            Form S = new frmTacGia();
            S.ShowDialog();
        }

        private void btnNXB_Click(object sender, EventArgs e)
        {
            Form S = new frmNXB();
            S.ShowDialog();
        }

        private void txtTim_Click(object sender, EventArgs e)
        {
            txtTim.Text = "";
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string TimKiemTS = @"select * from SACH where TenSach LIKE N'%" + txtTim.Text + "%' or TacGia LIKE N'%" + txtTim.Text + "%' or TheLoai LIKE N'%" + txtTim.Text + "%'";
            DataTable dt = DatabaseService.getDataTable(TimKiemTS);
            dataSach.DataSource = dt;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string XoaTS = "DELETE FROM SACH WHERE MaSach='" + txtMaSach.Text + "'";
            DialogResult dialog = MessageBox.Show("Bạn có muốn xóa sách: " +txtTenSach.Text + "\nCủa tác giả: " + cboxTacGia.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            string CapNhatTS = @"update SACH set TheLoai=N'" + cboxTheLoai.Text + "',TenSach=N'" + txtTenSach.Text + "',TacGia=N'" + cboxTacGia.Text + "',NXB=N'" + cboxNXB.Text + "',NgayNhap=N'" + dateNgayNhap.Text + "',SLTon='" + txtSLTon.Text + "',SLMuon='" + txtSLMuon.Text + "' where MaSach='" + txtMaSach.Text + "'";
            DatabaseService.executeQuery(CapNhatTS);
            Load_Data();
        }

        private void cboxTheLoai_Click(object sender, EventArgs e)
        {
            this.Load_cboxTheLoai();
        }

        private void cboxTacGia_Click(object sender, EventArgs e)
        {
            this.Load_cboxTacGia();
        }

        private void cboxNXB_Click(object sender, EventArgs e)
        {
            this.Load_cboxNXB();
        }
    }
}
