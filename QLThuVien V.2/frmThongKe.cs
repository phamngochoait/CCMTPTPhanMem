using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLThuVien_V._2
{
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string currTime = DateTime.Now.Date.ToShortDateString();
            if (Convert.ToString(listBox1.SelectedItem) == "Báo cáo tên 10 đầu sách được mượn nhiều nhất.")
            {
                string str = @"SELECT TOP(10) MaSach,TenSach,TacGia,NXB,SLMuon FROM SACH ORDER BY SLMuon DESC;";
                DataTable dt = DatabaseService.getDataTable(str);
                dataHienThi.DataSource = dt;
            }
            //
            if (Convert.ToString(listBox1.SelectedItem) == "Danh mục sách không được mượn lần nào.")
            {
                string str = @"SELECT MaSach,TenSach,TacGia,NXB,SLMuon FROM SACH where SLMuon=0";
                DataTable dt = DatabaseService.getDataTable(str);
                dataHienThi.DataSource = dt;
            }
            //
            if (Convert.ToString(listBox1.SelectedItem) == "Danh mục sách đang được mượn.")
            {
                try
                {
                    string str = @"select MaSach,TenSach,TacGia,NXB from SACH where MaSach=(select MaSach from CHITIETHOADON where CHITIETHOADON.MaSach=SACH.MaSach)";
                    DataTable dt = DatabaseService.getDataTable(str);
                    dataHienThi.DataSource = dt;
                }
                catch (Exception) { }
                //MessageBox.Show(DateTime.Now.Date.ToShortDateString());
            }
            //
            if (Convert.ToString(listBox1.SelectedItem) == "Danh mục sách đã quá hạn trả.")
            {
                string str = @"SELECT MaSach,TenSach,TacGia,NXB from SACH where MaSach=(select MaSach from CHITIETHOADON where NgayTra <'" + currTime + "')";
                DataTable dt = DatabaseService.getDataTable(str);
                dataHienThi.DataSource = dt;
            }
            if (Convert.ToString(listBox1.SelectedItem) == "Danh sách sinh viên bị phạt vì trả quá hạn.")
            {
                string str = @"select * from DOCGIA where MaDG=(select MaDG from HOADON,CHITIETHOADON where HOADON.MaPM=CHITIETHOADON.MaPM and CHITIETHOADON.NgayTra<'" + currTime + "')";
                DataTable dt = DatabaseService.getDataTable(str);
                dataHienThi.DataSource = dt;
            }
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {

        }
    }
}
