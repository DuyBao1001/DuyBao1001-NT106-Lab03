using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03
{
    static class Program
    {
        /// <summary>
        /// Điểm vào chính của ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Bật các kiểu trực quan cho các điều khiển
            Application.EnableVisualStyles();

            // Đặt chế độ tương thích cho ứng dụng
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD:Bai6/Program.cs

            // Khởi chạy form Dashboard làm form chính của ứng dụng
            Application.Run(new lab3_bai6_db());
=======
            Application.Run(new frmDashboard());
>>>>>>> bb3f19a485bbc4d2ba207c4d58b87cf299097e59:Bai1/Program.cs
        }
    }
}