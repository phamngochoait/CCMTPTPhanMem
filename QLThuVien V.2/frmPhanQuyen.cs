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
    public partial class frmPhanQuyen : Form
    {
        public frmPhanQuyen()
        {
            InitializeComponent();
        }
        public void Load_Data()
        {
            string str = @"select * from NGUOIDUNG";
            DataTable dt = DatabaseService.getDataTable(str);
            dataNguoiDung.DataSource = dt;
        }
        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            this.Load_Data();
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            string update = "update NGUOIDUNG set Password='" + txtID.Text + "' where(ID=N'" + txtID.Text + "')";
            DatabaseService.executeQuery(update);
            Load_Data();
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            string _PhanQuyen = "";
            if (cboxPhanQuyen.Text == "Quản Trị")
                _PhanQuyen = "1";
            if (cboxPhanQuyen.Text == "Nhân Viên")
                _PhanQuyen = "2";
            string update = "update NGUOIDUNG set PhanQuyen=N'" + _PhanQuyen + "' where(ID=N'" + txtID.Text + "')";
            DatabaseService.executeQuery(update);
            Load_Data();
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string TimKiemTS = @"select * from NGUOIDUNG where ID LIKE N'%" + txtTim.Text + "%'";
            DataTable dt = DatabaseService.getDataTable(TimKiemTS);
            dataNguoiDung.DataSource = dt;
        }

        private void txtTim_Click(object sender, EventArgs e)
        {
            txtTim.Text = "";
        }

        private void dataNguoiDung_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DataGridViewRow row = new DataGridViewRow();
                row = dataNguoiDung.Rows[e.RowIndex];
                txtID.Text = row.Cells[0].Value.ToString();
                txtPassword.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value.ToString() == "1")
                    cboxPhanQuyen.Text = "Quản Trị";
                if (row.Cells[2].Value.ToString() == "2")
                    cboxPhanQuyen.Text = "Nhân Viên";
            }
            catch (Exception) { }
        }

    }
}
