using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4
{
    public partial class frmCinemaServer : Form
    {

        // Server socket
        private Socket serverSocket;
        // Danh sách các client đã kết nối
        private List<Socket> clients = new List<Socket>();
        // Trạng thái chỗ ngồi (key: số chỗ, value: tên khách hàng đã đặt)
        private Dictionary<int, string> seatStatus = new Dictionary<int, string>();

        // Luồng gửi cập nhật chỗ ngồi
        private Thread updateThread;


        public frmCinemaServer()
        {
            InitializeComponent();

            btnListen.Click += new EventHandler(btnListen_Click);



            for (int i = 1; i <= 25; i++)
            {
                seatStatus[i] = "";
            }
            UpdateSeatStatus();
        }



        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo server socket
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Bind socket đến địa chỉ và cổng
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, int.Parse(txtPort.Text)));
                // Lắng nghe kết nối
                serverSocket.Listen(10);
                // Tạo và chạy luồng lắng nghe kết nối từ client
                Thread listenThread = new Thread(AcceptClients);
                listenThread.Start();

                btnListen.Enabled = false;

                // Thông báo thành công
                MessageBox.Show("Server is listening for connections!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Bắt đầu luồng gửi cập nhật chỗ ngồi
                updateThread = new Thread(SendSeatUpdates);
                updateThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Chấp nhận kết nối từ client
        private void AcceptClients()
        {
            while (true)
            {
                try
                {
                    // Chấp nhận kết nối từ client
                    Socket client = serverSocket.Accept();
                    // Thêm vào danh sách các client đã kết nối
                    clients.Add(client);
                    // Tạo 1 luồng xử lí riêng cho client
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                    // Cập nhật số lượng kết nối (safely on UI thread)
                    UpdateConnectionCount();
                }
                catch (Exception ex)
                {
                    // Lỗi xử lí kết nối
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Xử lí kết nối từ client
        private void HandleClient(Socket client)
        {
            while (true)
            {
                try
                {
                    // Nhận dữ liệu từ client
                    byte[] buffer = new byte[1024];
                    int bytesReceived = client.Receive(buffer);
                    string request = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

                    // Xử lí yêu cầu từ client
                    ProcessClientRequest(client, request);
                }
                catch (Exception ex)
                {
                    // Xử lí lỗi (ví dụ: client ngắt kết nối)
                    MessageBox.Show("Error: " + ex.Message);
                    // Xoa client khỏi danh sách
                    clients.Remove(client);
                    // Dong kết nối với client
                    client.Close();
                    // Cập nhật số lượng kết nối (safely on UI thread)
                    UpdateConnectionCount();
                    break;
                }
            }
        }

        // Xử lí yêu cầu từ client
        private void ProcessClientRequest(Socket client, string request)
        {
            try
            {
                // Chia request thành các phần (định dạng: "clientName,seatNumber")
                string[] parts = request.Split(',');
                string clientName = parts[0];
                int seatNumber = int.Parse(parts[1]);

                // Đặt vé
                seatStatus[seatNumber] = clientName;

                // Cập nhật UI Server trên thread chính
                if (InvokeRequired)
                {
                    Invoke(new Action(() => UpdateSeatStatus()));
                }
                else
                {
                    UpdateSeatStatus();
                }

                // Gửi phản hồi cho client
                byte[] data = Encoding.ASCII.GetBytes("booked");
                client.Send(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing client request: " + ex.Message);
            }
        }

        // Cập nhật số lượng kết nối trên UI server (safely on UI thread)
        private delegate void UpdateConnectionCountDelegate(int connectionCount);
        private void UpdateConnectionCount(int connectionCount)
        {
            Number_of_connections.Text = connectionCount.ToString();
        }
        // Cập nhật số lượng kết nối trên UI server (safely on UI thread)
        private void UpdateConnectionCount()
        {
            if (Number_of_connections.InvokeRequired)
            {
                Number_of_connections.Invoke(new UpdateConnectionCountDelegate(UpdateConnectionCount), clients.Count);
            }
            else
            {
                Number_of_connections.Text = clients.Count.ToString();
            }
        }

        // Cập nhật trạng thái chỗ ngồi trên UI server
        private void UpdateSeatStatus()
        {
            // Cập nhật trạng thái từng chỗ ngồi
            for (int i = 1; i <= 25; i++)
            {
                Button btn = this.Controls.Find($"btnSeat{i}", true).FirstOrDefault() as Button;
                if (btn != null)
                {
                    if (seatStatus[i] != "")
                    {
                        btn.BackColor = Color.Gray;
                        btn.Enabled = false;
                        btn.Text = $"{i} ({seatStatus[i]})"; // hiển thị tên khách hàng đã đặt
                    }
                    else
                    {
                        btn.BackColor = Color.White;
                        btn.Enabled = true;
                        btn.Text = i.ToString();
                    }
                }
            }

            // Cập nhật số lượng chỗ ngồi đã đặt và còn trống
            Number_of_seats_selected.Text = seatStatus.Where(x => x.Value != "").Count().ToString();
            Number_of_empty_seats.Text = seatStatus.Where(x => x.Value == "").Count().ToString();
        }

        // gửi cập nhật trạng thái chỗ ngồi đến tất cả các client đã kết nối
        private void SendSeatUpdates()
        {
            while (true)
            {
                // gửi cập nhật trạng thái chỗ ngồi đến tất cả các client
                foreach (Socket client in clients)
                {
                    try
                    {
                        // tạo chuỗi cập nhật trạng thái chỗ ngồi
                        string seatUpdate = "";
                        for (int i = 1; i <= 25; i++)
                        {
                            if (seatStatus[i] != "")
                            {
                                seatUpdate += $"{i},{seatStatus[i]};";
                            }
                        }

                        // gửi cập nhật
                        byte[] data = Encoding.ASCII.GetBytes(seatUpdate);
                        client.Send(data);
                    }
                    catch (Exception ex)
                    {
                        // xử lí lỗi gửi cập nhật
                        MessageBox.Show("Error sending seat updates: " + ex.Message);
                        // xóa client khỏi danh sách
                        clients.Remove(client);
                    }
                }

                // Chờ 1 giây trước khi gửi cập nhật tiếp theo
                Thread.Sleep(1000); // Cập nhậ mỗi 1s
            }
        }

        // Xử lí sự kiện click cho tất cả các nút chỗ ngồi
        private void btnSeat1_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat2_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat3_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat4_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat5_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat6_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat7_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat8_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat9_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat10_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat11_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat12_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat13_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat14_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat15_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat16_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat17_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat18_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat19_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat20_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat21_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat22_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat23_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat24_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat25_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }

        // Cập nhật sự kiện click cho nút chỗ ngồi
        private void SeatButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int seatNumber = int.Parse(clickedButton.Text);
            if (seatStatus[seatNumber] != "")
            {
                MessageBox.Show("This seat is already booked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // mô phỏng đặt vé (thay màu, ...)
                // ...
            }
        }
    }
}
