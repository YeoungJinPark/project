using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messenger
{
    public partial class MdiParents : Form
    {
        TcpClass _tcp;
        public MdiParents()
        {
            InitializeComponent();
        }

        public MdiParents(TcpClass tcp) : this()
        {
            _tcp = tcp;
            LoginForm loginForm = new LoginForm(_tcp, this);
            loginForm.MdiParent = this;
            //loginForm.Dock = DockStyle.Fill;
            loginForm.Show();
        }
    }
}
