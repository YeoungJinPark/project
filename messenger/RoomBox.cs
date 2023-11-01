using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messenger
{
    public partial class RoomBox : UserControl
    {
        public string _roomName
        {
            get
            {
                return listLabel.Text;
            }
            set
            {
                listLabel.Text = value;
            }
        }

        public string _msg
        {
            get
            {
                return lastMsgLabel.Text;
            }
            set
            {
                lastMsgLabel.Text = value;
            }
        }

        public int _index { get; set; }
        public MdiParents _mdi { get; set; }
        public MainForm _mainForm { get; set; }

        public RoomBox()
        {
            InitializeComponent();
        }

        public RoomBox(int idx, string roomName, string msg, MdiParents mdi, MainForm main) : this()
        {
            _index = idx;
            _roomName = roomName;
            _msg = msg;
            _mdi = mdi;
            _mainForm = main;
        }

        private void Room_Click(object sender, EventArgs e)
        {
            chatRoom chatForm = new chatRoom(_index, _roomName, _mainForm, _mdi);
            foreach (Control control in _mainForm.Controls)
            {
                control.Dispose();
            }
            chatForm.MdiParent = _mdi;
            chatForm.Dock = DockStyle.Fill;
            chatForm.Show();
        }
    }
}
