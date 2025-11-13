using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai6;

namespace Lab03
{
    public partial class lab3_bai6_db : Form
    {
        public lab3_bai6_db()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            lab3_bai6_server sv = new lab3_bai6_server();
            sv.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            lab3_bai6_client client = new lab3_bai6_client();
            client.Show();
        }
    }
}