using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QYPlugin
{
    public partial class SetForm : Form
    {
        public SetForm() => InitializeComponent();

        private string OutPut { set => textBox1.Text = value; }

        private void GetAppDir(object sender, EventArgs e)
        {
            OutPut = Robot.AppDirectory;
        }

        private void GetLoginQQ(object sender, EventArgs e)
        {
            OutPut = Robot.LoginQQ;
        }

        private void LoginNick_Click(object sender, EventArgs e)
        {
            OutPut = Robot.LoginNick;
        }

        private void LoginStatus_Click(object sender, EventArgs e)
        {
            OutPut = Robot.Status.ToString();
        }
    }
}
