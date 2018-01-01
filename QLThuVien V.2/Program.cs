using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThuVien_V._2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDangNhap());
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
}
