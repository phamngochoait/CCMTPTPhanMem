using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
     public RPHoaDon() {
        }
        
        public override string ResourceName {
            get {
                return "RPHoaDon.rpt";
            }
            set {
                // Do nothing
            }
        }
namespace QLThuVien_V._2
{
    public partial class frmNXB : Form
    {
        public frmNXB()
        {
            InitializeComponent();
        }
        public void Load_Data()
        {
            string str = @"select * from NXB";
            DataTable dt = DatabaseService.getDataTable(str);
            dataNXB.DataSource = dt;
        }
        private void frmNXB_Load(object sender, EventArgs e)
        {
            this.Load_Data();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string them = @"insert into NXB values(N'" + txtTenNXB.Text + "',N'" + txtGhiChu.Text + "')";
            DatabaseService.executeQuery(them);
            Load_Data();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string xoa = @"delete from NXB where MaNXB='" + txtMaNXB.Text + "'";
            DialogResult dialog = MessageBox.Show("Bạn có muốn xóa NXB: " + txtTenNXB.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            string capnhat = @"update NXB set TenNXB=N'" + txtTenNXB.Text + "', GhiChu=N'" + txtGhiChu.Text + "' where MaNXB='" + txtMaNXB.Text + "'";
            DatabaseService.executeQuery(capnhat);
            Load_Data();
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string TimKiemTS = @"select * from NXB where TenNXB LIKE N'%" + txtTim.Text + "%'";
            DataTable dt = DatabaseService.getDataTable(TimKiemTS);
            dataNXB.DataSource = dt;
        }

        private void txtTim_Click(object sender, EventArgs e)
        {
            txtTim.Text = "";
        }

        private void dataNXB_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DataGridViewRow row = new DataGridViewRow();
                row = dataNXB.Rows[e.RowIndex];
                txtMaNXB.Text = row.Cells[0].Value.ToString();
                txtTenNXB.Text = row.Cells[1].Value.ToString();
                txtGhiChu.Text = row.Cells[2].Value.ToString();
            }
            catch (Exception) { }
//manh huy
        }
    }
}
