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
    public partial class frmRPDSSach : Form
    {
        public frmRPDSSach()
        {
            InitializeComponent();
        }

        private void frmRPDSSach_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = DatabaseService.getDataTable("select * from sach");

            RPDSSach rp = new RPDSSach();
            rp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rp;
        }
    }
}
