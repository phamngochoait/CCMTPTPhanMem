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
    public partial class frmTraSach : Form
    {
        public frmTraSach()
        {
            InitializeComponent();
        }
        // Load Mã ĐG
        public void Load_cboxDG()
        {
            string dt = "select * from DOCGIA";
            cboxMaDG.DisplayMember = "MaDG";
            cboxMaDG.DataSource = DatabaseService.getDataTable(dt);
        }
        // Load  Tên ĐG
        public void Load_cboxTenDG()
        {
            string dt = "select * from DOCGIA where MaDG='" + cboxMaDG.Text + "'";
            cboxTenDG.DisplayMember = "TenDG";
            cboxTenDG.DataSource = DatabaseService.getDataTable(dt);
        }
        // Load Mã Phiếu Mượn
        public void Load_cboxPM()
        {
            string dt = "select * from HOADON where MaDG='" + cboxMaDG.Text + "'";
            cboxMaPM.DisplayMember = "MaPM";
            cboxMaPM.DataSource = DatabaseService.getDataTable(dt);
        }
        // Load Ngày Mượn
        public void Load_Ngay()
        {
            string sqlcboxSup_ = "select * from CHITIETHOADON where MaPM='" + cboxMaPM.Text + "'"; // tới đây!!
            cboxNgayMuon.DisplayMember = "NgayThue";
            cboxNgayMuon.DataSource = DatabaseService.getDataTable(sqlcboxSup_);
        }

        public void Load_Muon()
        {
            //string Muon = @"select MaSach,TenSach from tblSach where MaSach=(select MaSach from tblChiTietMuon where tblChiTietMuon.MaPM='"+cboxMaPM.Text+"' and tblChiTietMuon.NgayThue='"+cboxNgayMuon.Text+"')";
            string Muon = @"select MaSach,TenSach from SACH where MaSach=(  select MaSach from CHITIETHOADON where (CHITIETHOADON.MaPM='" + cboxMaPM.Text + "' and CHITIETHOADON.NgayThue='" + cboxNgayMuon.Text + "' and SACH.MaSach=CHITIETHOADON.MaSach))";
            //MessageBox.Show(Muon);
            DataTable dt = DatabaseService.getDataTable(Muon);
            dataSach.DataSource = dt;
        }
        private void frmTraSach_Load(object sender, EventArgs e)
        {
            this.Load_cboxDG();
            this.Load_cboxTenDG();
            this.Load_Ngay();
        }

        private void cboxMaDG_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Load_cboxTenDG();
        }

        private void cboxMaDG_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cboxMaPM.Text = "";
            cboxNgayMuon.Text = "";
            this.Load_cboxTenDG();
            this.Load_cboxPM();
            this.Load_Ngay();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (cboxMaPM.Text != "")
                this.Load_Muon();
            else
                MessageBox.Show("Độc Giả: " + cboxTenDG.Text + " chưa mượn sách!\n Hoặc đã trả xong.", "Thông báo");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataSach.SelectedRows)
                {
                    listBox1.Items.Add(row.Cells[1].Value.ToString());
                }
            }
            catch(Exception){}
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
                this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
        }
        public int Kiemtra()
        {
            int check = 1; // 0--->chưa trả đủ
            int TongSoMuon, TongSoTra;
            TongSoMuon = dataSach.Rows.Count - 1;
            TongSoTra = Int16.Parse(listBox1.Items.Count.ToString());
            if (TongSoMuon > TongSoTra)
            {
                //MessageBox.Show("->> in phiếu mới!!");
                check = 0;
            }
            else
                if (TongSoMuon == TongSoTra)
                {
                    //MessageBox.Show("thành công");
                    check = 1;
                }
            return check;
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            if (Kiemtra() == 1)
            {
                string xoaHoaDonMuon = @"update HOADON set TinhTrang=1 where MaPM='" + cboxMaPM.Text + "' and MaDG='" + cboxMaDG.Text + "'";
                //MessageBox.Show(xoaHoaDonMuon);
                DatabaseService.executeQuery(xoaHoaDonMuon);
                //
                foreach (var listBoxItem in listBox1.Items)
                {
                    string tra = @"delete from CHITIETHOADON where MaSach=(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "' and CHITIETHOADON.MaPM='" + cboxMaPM.Text + "' and CHITIETHOADON.NgayThue='" + cboxNgayMuon.Text + "')";
                    string upTontai = @"update SACH set SLTon=(SLTon+1) where MaSach=(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "')";
                    string upSLMuon = @"update SACH set SLMuon=(SLMuon-1) where MaSach=(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "')";
                    //MessageBox.Show(upTontai);
                    DatabaseService.executeQuery(upTontai);
                    //MessageBox.Show(upSLMuon);
                    DatabaseService.executeQuery(upSLMuon);
                    //MessageBox.Show(tra);
                    DatabaseService.executeQuery(tra);
                }
                MessageBox.Show("Bạn đã trả hết sách thành công!! Bạn có thể mượn ở lần tới.");
                listBox1.Items.Clear();
            }
            else
            {
                string xoaHoaDonMuon = @"update HOADON set TinhTrang=0 where MaPM='" + cboxMaPM.Text + "' and MaDG='" + cboxMaDG.Text + "'";
                //MessageBox.Show(xoaHoaDonMuon);
                DatabaseService.executeQuery(xoaHoaDonMuon);
                foreach (var listBoxItem in listBox1.Items)
                {
                    string tra = @"delete from CHITIETHOADON where MaSach=(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "' and CHITIETHOADON.MaPM='" + cboxMaPM.Text + "' and CHITIETHOADON.NgayThue='" + cboxNgayMuon.Text + "')";
                    string upTontai = @"update SACH set SLTon=(SLTon+1) where MaSach=(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "')";
                    string upSLMuon = @"update SACH set SLMuon=(SLMuon-1) where MaSach=(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "')";
                    //MessageBox.Show(upTontai);
                    DatabaseService.executeQuery(upTontai);
                    //MessageBox.Show(upSLMuon);
                    DatabaseService.executeQuery(upSLMuon);
                    //MessageBox.Show(tra);
                    DatabaseService.executeQuery(tra);
                }
                MessageBox.Show("Bạn đã trả sách thành công nhưng chưa đủ!!");
                this.Load_Muon();
                listBox1.Items.Clear();
            }


            //MessageBox.Show("Trả thành công");
        }
    }
}
