using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai5
{
    public partial class Bai5 : Form
    {
        public Bai5()
        {
            InitializeComponent();
        }

        private void server_Click(object sender, EventArgs e)
        {
            Server serverForm = new Server();
            serverForm.Show();
        }

        private void client_Click(object sender, EventArgs e)
        {
            Client clientForm = new Client();
            clientForm.Show();
        }
    }
}