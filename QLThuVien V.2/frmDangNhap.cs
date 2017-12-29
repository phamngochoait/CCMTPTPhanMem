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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập hoặc mật khẩu!", "Thông báo");
                return;
            }
            DataTable dt = DatabaseService.getDataTable("Select * from NGUOIDUNG where ID = '" + txtTaiKhoan.Text + "' and Password = '" + txtMatKhau.Text + "'");
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Xin chào: " + txtTaiKhoan.Text + "!\nBạn đã đăng nhập thành công!", "Thông báo");
                this.Hide();
                //Form main = new frmMain(txtTaiKhoan.Text, txtMatKhau.Text);
                Form main = new frmMain("admin","admin");
                //Form main = new frmMain();
                main.Show();
            }

            else
            {
                MessageBox.Show("Đăng nhập không thành công!", "Thông báo");
                txtTaiKhoan.Clear();
                txtMatKhau.Clear();
                txtTaiKhoan.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
                Application.Exit();
            else if (dialog == DialogResult.No)
            {
                //
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
