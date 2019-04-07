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

        private void SendFriend_Click(object sender, EventArgs e)
        {
            OutPut = Robot.Send.Friend(targqq.Text, tmsg.Text) ? "成功" : "失败";
        }

        private void SendGroup_Click(object sender, EventArgs e)
        {
            OutPut = Robot.Send.Group(targGroup.Text, tmsg.Text) ? "成功" : "失败";
        }

        private void GroupTmp_Click(object sender, EventArgs e)
        {
            OutPut = Robot.Send.GTmp(targGroup.Text, targqq.Text, tmsg.Text) ? "成功" : "失败";
        }

        private void Like_Click(object sender, EventArgs e)
        {
            OutPut = Robot.Like(targqq.Text).ToString();
        }
    }
}
