//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace QLThuVien_V._2
{
    public partial class frmMain : Form
    {
        string username, password;
        public frmMain()
        {
            InitializeComponent();
        }
        public frmMain(string user, string pass)
        {
            InitializeComponent();
            username = user;
            password = pass;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Bạn đã đăng nhập bằng tài khoản: " + username);
            DataSetPhanQuyenTableAdapters.QueriesTableAdapter q = new DataSetPhanQuyenTableAdapters.QueriesTableAdapter();
            string phanquyen = q.QueryPhanQuyen__(username).ToString();
            //MessageBox.Show(phanquyen);
            if (phanquyen == "1")
            {
                lbSach.Enabled = true;
                lbTheLoai.Enabled = true;
                lbTacGia.Enabled = true;
                lbNXB.Enabled = true;
                lbDocGia.Enabled = true;
                lbMuonSach.Enabled = true;
                lbTraSach.Enabled = true;
                btnSaoLuu.Enabled = true;
                btnKhoiPhuc.Enabled = true;
                btnPhanQuyen.Enabled = true;
            }
            else
                MessageBox.Show("Bạn đăng nhập bằng quyền User\n Vì vậy một số tính năng sẽ bị hạn chế.", "Thông báo");
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form OUT = new frmDangNhap();
            OUT.ShowDialog();
        }

        private void btnSaoLuu_Click(object sender, EventArgs e)
        {
            bool bBackUpStatus = true;

            Cursor.Current = Cursors.WaitCursor;

            if (Directory.Exists(@"c:\SQLBackup"))
            {
                if (File.Exists(@"c:\SQLBackup\QLTV.bak"))
                {
                    if (MessageBox.Show(@"QLTV.bak đã có \nBạn có muốn thay thế nó?", "Back", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        File.Delete(@"c:\SQLBackup\QLTV.bak");
                    }
                    else
                        bBackUpStatus = false;
                }
            }
            else
                Directory.CreateDirectory(@"c:\SQLBackup");

            if (bBackUpStatus)
            {
                //Connect to DB
                SqlConnection connect;
                //string con = @"Data Source=DESKTOP-KA710V7\SQLEXPRESS;Initial Catalog=QL_TS_THPT_NANGKHIEU;Integrated Security=True";
                string con = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLThuVienVer.2;Integrated Security=True";
                connect = new SqlConnection(con);
                connect.Open();
                //----------------------------------------------------------------------------------------------------

                //Execute SQL---------------
                SqlCommand command;
                command = new SqlCommand(@"backup database QLThuVienVer.2 to disk ='c:\SQLBackup\QLTV.bak' with init,stats=10", connect);
                command.ExecuteNonQuery();
                //-------------------------------------------------------------------------------------------------------------------------------

                connect.Close();

                //MessageBox.Show("Backup dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult dialog = MessageBox.Show("Backup dữ liệu thành công! Bạn có muốn mở thư mục?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                    System.Diagnostics.Process.Start(@"c:\SQLBackup");
                else if (dialog == DialogResult.No)
                {
                    //
                }
            }
        }

        private void lbSach_Click(object sender, EventArgs e)
        {
            Form S = new frmTTSach();
            S.ShowDialog();
        }

        private void lbTheLoai_Click(object sender, EventArgs e)
        {
            Form S = new frmTheLoai();
            S.ShowDialog();
        }

        private void lbTacGia_Click(object sender, EventArgs e)
        {
            Form S = new frmTacGia();
            S.ShowDialog();
        }

        private void lbNXB_Click(object sender, EventArgs e)
        {
            Form S = new frmNXB();
            S.ShowDialog();
        }

        private void lbDocGia_Click(object sender, EventArgs e)
        {
            Form S = new frmDocGia();
            S.ShowDialog();
        }

        private void lbMuonSach_Click(object sender, EventArgs e)
        {
            Form S = new frmMuonSach();
            S.ShowDialog();
        }

        private void lbTraSach_Click(object sender, EventArgs e)
        {
            Form S = new frmTraSach();
            S.ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            Form DMK = new frmDoiMK(username, password);
            DMK.ShowDialog();
        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (File.Exists(@"c:\SQLBackup\QLTV.bak"))
                {
                    if (MessageBox.Show("Bạn có muốn khôi phục?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Connect SQL-----------
                        SqlConnection connect;
                        //string con = @"Data Source=DESKTOP-KA710V7\SQLEXPRESS;Initial Catalog=QL_TS_THPT_NANGKHIEU;Integrated Security=True";
                        string con = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLThuVienVer;Integrated Security=True";
                        connect = new SqlConnection(con);
                        connect.Open();
                        //-----------------------------------------------------------------------------------------

                        //Excute SQL----------------
                        SqlCommand command;
                        command = new SqlCommand("use master", connect);
                        command.ExecuteNonQuery();
                        command = new SqlCommand(@"restore database QLThuVienVer from disk = 'c:\SQLBackup\QLTV.bak'", connect);
                        command.ExecuteNonQuery();
                        //--------------------------------------------------------------------------------------------------------
                        connect.Close();

                        MessageBox.Show("Khôi phục dữ liệu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show(@"Không thực hiện bất kỳ sự chứng thực ở trên (hoặc không có trong đường dẫn chính xác )", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            Form DMK = new frmPhanQuyen();
            DMK.ShowDialog();
        }

        private void labelX7_Click(object sender, EventArgs e)
        {
            Form DMK = new frmRPDSSach();
            DMK.ShowDialog();
        }

        private void labelX2_Click(object sender, EventArgs e)
        {
            Form DMK = new frmThongKe();
            DMK.ShowDialog();
        }
    }
}
