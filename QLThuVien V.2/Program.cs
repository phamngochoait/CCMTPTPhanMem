using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThuVien_V._2
{
    
	
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

}
