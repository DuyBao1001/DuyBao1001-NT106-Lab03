using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class frmDashboard: Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            frmServer f = new frmServer();
            f.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            frmClient f = new frmClient();
            f.Show();
        }
    }
}
