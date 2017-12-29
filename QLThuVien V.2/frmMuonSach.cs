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
    public partial class frmMuonSach : Form
    {
        public frmMuonSach()
        {
            InitializeComponent();
        }
        // Load TL Sách
        public void Load_dataTL()
        {
            string str = @"select TenTL from THELOAI";
            DataTable dt = DatabaseService.getDataTable(str);
            dataTheLoai.DataSource = dt;
        }
        // Load Sách
        public void Load_DataSach()
        {
            string str = @"select TenSach,TacGia,NXB from SACH";
            DataTable dt = DatabaseService.getDataTable(str);
            dataSach.DataSource = dt;
        }
        // Load ĐG
        public void Load_cboxDG()
        {
            string dt = "select * from DOCGIA";
            cboxMaDG.DisplayMember = "MaDG";
            cboxMaDG.DataSource = DatabaseService.getDataTable(dt);
        }
        // Load ĐG
        public void Load_cboxTenDG()
        {
            string dt = "select * from DOCGIA where MaDG='" + cboxMaDG.Text + "'";
            cboxTenDG.DisplayMember = "TenDG";
            cboxTenDG.DataSource = DatabaseService.getDataTable(dt);
        }
        // Sinh mã
        public string sinhMa()
        {
            DataSetTaoMaPMTableAdapters.QueriesTableAdapter q = new DataSetTaoMaPMTableAdapters.QueriesTableAdapter();
            string SoMaPM;
            SoMaPM = "000" + q.QueryMaPM().ToString();
            SoMaPM = SoMaPM.Substring(SoMaPM.Length - 4, 4);
            return "PM" + SoMaPM;
        }

        private void frmMuonSach_Load(object sender, EventArgs e)
        {
            this.Load_dataTL();
            this.Load_DataSach();
            this.Load_cboxDG();
            this.dateNgayMuon.Value = DateTime.Now;
            this.dateNgayTra.Value = dateNgayMuon.Value.AddDays(7);
        }

        private void textBoxX2_Click(object sender, EventArgs e)
        {
            textBoxX2.Text = "";
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            string TimKiemTS = @"select TenTL from THELOAI where TenTL LIKE N'%" + textBoxX2.Text + "%'";
            DataTable dt = DatabaseService.getDataTable(TimKiemTS);
            dataTheLoai.DataSource = dt;
        }

        private void dataTheLoai_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string s = "";
            try
            {

                DataGridViewRow row = new DataGridViewRow();
                row = dataTheLoai.Rows[e.RowIndex];
                s = row.Cells[0].Value.ToString();
                //MessageBox.Show(s);
                string str = @"select TenSach,TacGia,NXB from SACH where TheLoai=N'" + s + "'";
                DataTable dt = DatabaseService.getDataTable(str);
                dataSach.DataSource = dt;

            }
            catch (Exception) { }
        }

        private void textBoxX3_Click(object sender, EventArgs e)
        {
            textBoxX3.Text = "";
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            string TimKiemTS = @"select TenSach,TacGia,NXB from SACH where TenSach LIKE N'%" + textBoxX3.Text + "%'";
            DataTable dt = DatabaseService.getDataTable(TimKiemTS);
            dataSach.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataSach.SelectedRows)
            {
                listBox1.Items.Add(row.Cells[0].Value.ToString());
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
                this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
        }

        private void cboxMaDG_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_cboxTenDG();
        }

        private void dateNgayMuon_ValueChanged(object sender, EventArgs e)
        {
            this.dateNgayTra.Value = dateNgayMuon.Value.AddDays(7);
        }

        private void btnMuon_Click(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (var listBoxItem in listBox1.Items)
            {

                sum++;
            }
            //txtSL.Text = Convert.ToInt32(sum).ToString();
            if (sum == 0)
            {
                MessageBox.Show("Không có sách nào được chọn để mượn", "Thông báo");
            }
            else
            {
                if (txtMaPM.Text == "")
                    MessageBox.Show("Mã Phiếu mượn không được trống!!");
                else
                {
                    string muon = @"insert into HOADON(MaPM,MaDG,TinhTrang) values('" + txtMaPM.Text + "','" + cboxMaDG.Text + "',0)"; // 0 chưa trả
                    //MessageBox.Show(muon);
                    DatabaseService.executeQuery(muon);

                    //
                    // Thêm vào CT Mượn
                    foreach (var listBoxItem in listBox1.Items)
                    {
                        //MessageBox.Show(listBoxItem.ToString());
                        string ctMuon = @"insert into CHITIETHOADON(MaPM,MaSach,NgayThue,NgayTra) values('" + txtMaPM.Text + "',(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "'),'" + dateNgayMuon.Text + "','" + dateNgayTra.Text + "')";
                        string upTontai = @"update SACH set SLTon=(SLTon-1) where MaSach=(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "')";
                        string upSLMuon = @"update SACH set SLMuon=(SLMuon+1) where MaSach=(select MaSach from SACH where SACH.TenSach=N'" + listBoxItem.ToString() + "')";
                        DatabaseService.executeQuery(ctMuon);
                        DatabaseService.executeQuery(upTontai);
                        DatabaseService.executeQuery(upSLMuon);
                    }
                }
                MessageBox.Show("Mượn thành công!!");
                listBox1.Items.Clear();
            }

            //



        }

        private void btnTheLoai_Click(object sender, EventArgs e)
        {
            string MaPM;
            string sql = " select MaPM from HOADON";
            DataTable tb = DatabaseService.getDataTable(sql);
            int s = tb.Rows.Count;
            if (tb.Rows.Count < 1)
                MaPM = "PM0001";
            else
                MaPM = sinhMa();
            txtMaPM.Text = MaPM;
        }

    }
}
