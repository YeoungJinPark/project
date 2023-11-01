using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messenger
{
    public partial class chatRoom : Form
    {
        public string _id;
        public int _index;

        TcpClass _tcp { get; }
        MdiParents _mdi { get; }
        Converter _converter = new Converter();
        public chatRoom()
        {
            InitializeComponent();
            backButton.FlatStyle = FlatStyle.Flat;
            // 대화 기록 불러오는 함수 필요
        }

        public chatRoom(int index, string roomName, MainForm mainForm, MdiParents mdi) : this()
        {
            _index = index;         // 선택한 방 번호
            _id = mainForm._id;     // 로그인한 유저 id
            _tcp = mainForm._tcp;
            _mdi = mdi;
            opponentLabel.Text = roomName;
            opponentTip.SetToolTip(opponentLabel, opponentLabel.Text);
        }

        private async void chatRoom_Load(object sender, EventArgs e)
        {
            await JoinRoom();
            await RecvChat();
        }

        private async Task JoinRoom()
        {
            var roomInfo = $"{_index}:{_id}";
            int select = 6;
            byte[] selecBuffer = _converter.ConvertToBytes(select);
            await _tcp.SendDataAsync(_tcp._socket, selecBuffer);
            await _tcp.SendData(_tcp._socket, roomInfo);
            (string dataType, byte[] dataBuffer) = await _tcp.RecvData(_tcp._socket);
            if (dataType != "str")
            {
                Console.WriteLine("SendRoomInfo Error! Not String Data Received");
                return;
            }
            var data = _converter.StringFromBytes(dataBuffer);
            if (!string.IsNullOrWhiteSpace(data))
            {
                var splitData = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string chatlog in splitData)
                {
                    var splitLog = chatlog.Split(':');
                    addChat(splitLog[0], splitLog[1]);
                }
            }
        }

        private async Task RecvChat()
        {
            while(true)
            {
                (string dataType, byte[] dataBuffer) = await _tcp.RecvData(_tcp._socket);
                if (dataType != "str")
                {
                    return;
                }
                var data = _converter.StringFromBytes(dataBuffer);
                string[] chatData = data.Split(':');
                addChat(chatData[0], chatData[1]);
                // 방 나갈때 break 추가 필요
            }
        }

        private async Task SendChat()
        {
            string msg = $"{_index}:{_id}:{chatBox.Text}";
            chatBox.Clear();
            int select = 5;
            byte[] selecBuffer = _converter.ConvertToBytes(select);
            await _tcp.SendDataAsync(_tcp._socket, selecBuffer);
            await _tcp.SendData(_tcp._socket, msg);
        }

        public void addChat(string id, string msg)
        {
            //string _time = DateTime.Now.ToString("HH:mm");
            //ListViewItem item = new ListViewItem(_time);
            //item.SubItems.Add(id);
            //item.SubItems.Add(msg);
            //msgList.Items.Add(item);
            //msgList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //ListView 사용 포기 열의 길이를 바꿔도 항목의 길이가 변하지 않음
            chatTextBox.SelectionFont = new System.Drawing.Font("Yu Gothic UI", 12, System.Drawing.FontStyle.Bold);
            chatTextBox.AppendText(id + ": ");
            chatTextBox.SelectionFont = new System.Drawing.Font("Yu Gothic UI", 12, System.Drawing.FontStyle.Regular);
            chatTextBox.AppendText(msg + Environment.NewLine);
            chatTextBox.ScrollToCaret();
        }

        private void chatBox_TextChanged(object sender, EventArgs e)
        {
            string txt = chatBox.Text;
            if (!string.IsNullOrWhiteSpace(txt))
                chatButton.Enabled = true;
            else
                chatButton.Enabled = false;
        }

        private async void chatButton_Click(object sender, EventArgs e)
        {
            await SendChat();
        }

        private void chatBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                chatButton_Click(sender, e);
        }

        private void opponentLabel_MouseHover(object sender, EventArgs e)
        {
            opponentTip.Show(opponentLabel.Text, opponentLabel);
        }

        private async void backButton_Click(object sender, EventArgs e)
        {
            int select = 8;
            byte[] selecBuffer = _converter.ConvertToBytes(select);
            await _tcp.SendDataAsync(_tcp._socket, selecBuffer);
            await _tcp.SendData(_tcp._socket, _id);
            Console.WriteLine("index:"+_index);
            await _tcp.SendData(_tcp._socket, _index.ToString());
            foreach (Control control in this.Controls)
            {
                control.Dispose();
            }
            MainForm mainForm = new MainForm(_id, _tcp, _mdi);
            mainForm.MdiParent = _mdi;
            mainForm.Dock = DockStyle.Fill;
            mainForm.SetPage(1);
            mainForm.Show();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string chatLog = chatTextBox.Text;
            using (SaveLog dialog = new SaveLog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.fileName+".txt";
                }
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter writer = new StreamWriter($"{Path.Combine(path, fileName)}", true))
            {
                writer.WriteLine(chatLog);
            }
            MessageBox.Show("저장완료");
        }
    }
}
