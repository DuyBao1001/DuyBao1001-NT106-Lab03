using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03
{
    public partial class lab03_Bai3_Dashboard : Form
    {
        public lab03_Bai3_Dashboard()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            lab03_bai3_server sv = new lab03_bai3_server();
            sv.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            lab03_bai3_client client = new lab03_bai3_client();
            client.Show();
        }
    }
}