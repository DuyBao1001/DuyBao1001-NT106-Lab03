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

            // Khởi chạy form Dashboard làm form chính của ứng dụng
            Application.Run(new lab3_bai6_db());
        }
    }
}