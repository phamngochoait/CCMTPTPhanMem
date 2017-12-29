using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;

namespace QLThuVien_V._2
{
    public partial class frmDoiMK : Form
    {

        string username, password;
        public frmDoiMK()
        {
            InitializeComponent();
        }
        public frmDoiMK(string user, string pass)
        {
            InitializeComponent();
            username = user;
            password = pass;
        }
        public void XacMinh()
        {
            String[] strLabel = {
"AQH1524",
"MDK2514",
"MASD8722",
"KMZA4267"
};
            Random r = new Random();
            int iSelect = r.Next(0, 3);
            txtMaXacNhan.Text = strLabel[iSelect];
        }
        private void frmDoiMK_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = username;
            XacMinh();

        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTheLoai_Click(object sender, EventArgs e)
        {
            XacMinh();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string update = "update NGUOIDUNG set Password='" + txtMatKhauMoi.Text + "' where(ID=N'" + txtTaiKhoan.Text + "' and Password='" + txtMatKhauCu.Text + "')";
            string ten = txtTaiKhoan.Text;
            if (ten == "")
            {
                MessageBox.Show("Bạn chưa nhập tên truy cập");
            }
            if (txtMaXacNhan.Text != txtMaXacMinh_.Text)
            {
                MessageBox.Show("Mã xác nhận không đúng");
                XacMinh();
                return;
            }
            else
            {
                if (txtMatKhauCu.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mật khẩu");
                }
                else
                {
                    if (txtMatKhauMoi.Text == "")
                    {
                        MessageBox.Show("Bạn chưa nhập mật khẩu mới");
                    }
                    else
                    {
                        if (txtXacNhanMatKhau.Text == "")
                        {
                            MessageBox.Show("Bạn chưa nhập lại mật khẩu");
                        }
                        else
                        {
                            if ((txtMatKhauMoi.Text == txtXacNhanMatKhau.Text) && txtMatKhauCu.Text == password)
                            {
                               
                                DatabaseService.executeQuery(update);
                             
                                MessageBox.Show("Bạn đã thay đổi mật khẩu thành công");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Bạn nhập lại mật khẩu không đúng");
                            }
                        }
                    }
                }
            }
        }


        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
                this.Hide();
            else if (dialog == DialogResult.No)
            {
                //
            }
        }
    }
}
