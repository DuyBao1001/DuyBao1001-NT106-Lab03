using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Bai1
{
    public partial class frmServer : Form
    {
        private UdpClient udpServer;
        private Thread listenThread;

        public frmServer()
        {
            InitializeComponent();
            lvMessage.Columns.Add("Tin nhắn nhận được", -2);
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            int port;
            if (!int.TryParse(textBoxPort.Text, out port) || port < 1 || port > 65535)
            {
                MessageBox.Show("Vui lòng nhập một số hiệu cổng hợp lệ (1-65535).");
                return;
            }

            try
            {
                // 1. Khởi tạo UdpClient trên luồng UI với port đã nhập
                udpServer = new UdpClient(port);

                // 2. Khởi tạo và chạy luồng lắng nghe (Thread)
                listenThread = new Thread(new ThreadStart(StartListening));
                listenThread.IsBackground = true; // Để luồng tự tắt khi đóng form
                listenThread.Start();

                //UI
                btnListen.Enabled = false;
                textBoxPort.Enabled = false;
                UpdateMessages("Server đang lắng nghe trên port " + port);
            }
            catch (SocketException)
            {
                MessageBox.Show("Lỗi: Port " + port + " đã được sử dụng.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void StartListening()
        {
            //lắng nghe từ bất kỳ nguồn IP nào
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            while (true) 
            {
                try
                {
                    byte[] receiveBytes = udpServer.Receive(ref remoteEP);

                    string receivedData = Encoding.UTF8.GetString(receiveBytes);

                    string message = remoteEP.Address.ToString() + ":" + remoteEP.Port + " - " + receivedData;

                    UpdateMessages(message);
                }
                catch // lỗi xảy ra thoát vòng lặp
                {
                    break;
                }
            }
        }

        private void UpdateMessages(string message)
        {
            if (lvMessage.InvokeRequired)
            {
                // Nếu đang ở thread khác, gọi lại hàm này trên thread UI
                lvMessage.Invoke(new MethodInvoker(delegate { UpdateMessages(message); }));
            }
            else
            {
                lvMessage.Items.Add(new ListViewItem(message));
            }
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            listenThread?.Abort();
            udpServer?.Close();
        }
    }
}