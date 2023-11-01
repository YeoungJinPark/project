using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messenger
{
    public partial class MainForm : Form
    {
        public TcpClass _tcp;
        MdiParents _mdi;
        Converter _converter = new Converter();
        public string _id;
        public int _lastRoom = 0;
        public MainForm()
        {
            InitializeComponent();
            tabControl.SelectedIndex = 0;
        }

        public MainForm(string id, TcpClass tcp, MdiParents mdi) : this()
        {
            _id = id;
            myNameLabel.Text = id;
            _tcp = tcp;
            _mdi = mdi;
            int x = myNameLabel.Location.X + myNameLabel.Width + 5;
            meLabel.Location = new Point(x, meLabel.Location.Y);
            userList.Columns.Add("ID");
            userList.Columns.Add("이름");
            userList.Columns.Add("직위");
            userList.Columns.Add("부서");
            userList.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            userList.Columns[0].Width = 70;
            userList.Columns[1].Width = 80;
            userList.Columns[2].Width = 80;
            userList.Columns[3].Width = 90;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadList();
        }

        private async Task LoadList()
        {
            await LoadEmpList();
            await LoadRoomList();
        }

        public void AddRoom(int idx, string roomName, string msg)
        {
            var room = new RoomBox(idx, roomName, msg, _mdi, this);
            room.Dock = DockStyle.Top;
            roomList.Controls.Add(room);
        }

        public void AddEmployees(string id, string name, string position, string department)
        {
            ListViewItem item = new ListViewItem(new string[] { id, name, position, department });
            userList.Items.Add(item);
        }

        private async Task LoadEmpList()
        {
            (string dataType, byte[] dataBuffer) = await _tcp.LoadEmpList();
            if (dataType != "tbl")
            {
                Console.WriteLine("LoadEmpList Error! Not DataTable Received");
                return;
            }
            var data = _converter.StringFromBytes(dataBuffer);
            var tbl = _converter.JsonToDataTable(data);
            if (tbl.Rows.Count > 0)
            {
                //id, name, pos , dept
                foreach (DataRow row in tbl.Rows)
                    AddEmployees(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
            }
        }

        private async Task LoadRoomList()
        {
            try
            {
                (string dataType, byte[] dataBuffer) = await _tcp.LoadRoomListT(_id);
                if (dataType != "tbl")
                {
                    Console.WriteLine("LoadRoomList Error! Not DataTable Received");
                    return;
                }
                var data = _converter.StringFromBytes(dataBuffer);
                var tbl = _converter.JsonToDataTable(data);
                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        // 0: index, 1: roomName, 2: lastMsg
                        AddRoom(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString());
                    }
                }
                (dataType, dataBuffer) = await _tcp.RecvData(_tcp._socket);
                if (dataType != "int")
                {
                    Console.WriteLine("LoadRoomList Error! Not DataTable Received");
                    return;
                }
                _lastRoom = _converter.IntFromBytes(dataBuffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadRoomListT Exception: {ex.Message}");
                throw;
            }
        }

        public void SetPage(int page)
        {
            // 0 : user, 1 : room
            tabControl.SelectedIndex = page;
        }

        private async void createButton_Click(object sender, EventArgs e)
        {
            if (userList.SelectedItems.Count < 1)
            {
                MessageBox.Show("1명이상 선택해주세요.");
                return;
            }
            string roomName = "";
            string userId = _id;
            using (NameDialog dialog = new NameDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    roomName = dialog._roomName;
                }
            }
            foreach (ListViewItem item in userList.SelectedItems)
            {
                userId += ':'+item.Text;
            }
            int select = 7;
            byte[] selecBuffer = _converter.ConvertToBytes(select);
            await _tcp.SendDataAsync(_tcp._socket, selecBuffer);

            await _tcp.SendData(_tcp._socket, _lastRoom);
            await _tcp.SendData(_tcp._socket, roomName);
            await _tcp.SendData(_tcp._socket, userId);

            (string dataType, byte[] dataBuffer) = await _tcp.RecvData(_tcp._socket);
            if (dataType != "int")
            {
                Console.WriteLine("CreateRoom Error! Not Int Received");
                return;
            }
            var result = _converter.IntFromBytes(dataBuffer);
            if (result == 0)
            {
                _lastRoom += 1;
                AddRoom(_lastRoom, roomName, "");
                chatRoom chatForm = new chatRoom(_lastRoom, roomName, this, _mdi);
                foreach (Control control in this.Controls)
                {
                    control.Dispose();
                }
                chatForm.MdiParent = _mdi;
                chatForm.Dock = DockStyle.Fill;
                chatForm.Show();
            }
            else if (result != -1)
            {
                (dataType, dataBuffer) = await _tcp.RecvData(_tcp._socket);
                if (dataType != "str")
                {
                    Console.WriteLine("CreateRoom Error! Not str Received");
                    return;
                }
                var rName = _converter.StringFromBytes(dataBuffer);
                chatRoom chatForm = new chatRoom(result, rName, this, _mdi);
                foreach (Control control in this.Controls)
                {
                    control.Dispose();
                }
                chatForm.MdiParent = _mdi;
                chatForm.Dock = DockStyle.Fill;
                chatForm.Show();
            }
            else
                MessageBox.Show("Error");
        }

        private async void P2PMenuItem_Click(object sender, EventArgs e)
        {
            if (userList.SelectedItems.Count == 1)
            {
                ListViewItem item = userList.SelectedItems[0];
                string userId = item.SubItems[0].Text;
                string userName = item.SubItems[1].Text;
                int select = 7;
                byte[] selecBuffer = _converter.ConvertToBytes(select);
                await _tcp.SendDataAsync(_tcp._socket, selecBuffer);

                await _tcp.SendData(_tcp._socket, _lastRoom);
                await _tcp.SendData(_tcp._socket, $"{userName}");
                await _tcp.SendData(_tcp._socket, $"{_id}:{userId}");
                (string dataType, byte[] dataBuffer) = await _tcp.RecvData(_tcp._socket);
                if (dataType != "int")
                {
                    Console.WriteLine("P2PChat Error! Not Int Received");
                    return;
                }
                var result = _converter.IntFromBytes(dataBuffer);
                if (result == 0)
                {
                    _lastRoom += 1;
                    AddRoom(_lastRoom, userName, "");
                    chatRoom chatForm = new chatRoom(_lastRoom, userName, this, _mdi);
                    foreach (Control control in this.Controls)
                    {
                        control.Dispose();
                    }
                    chatForm.MdiParent = _mdi;
                    chatForm.Dock = DockStyle.Fill;
                    chatForm.Show();
                }
                else if (result != -1)
                {
                    (dataType, dataBuffer) = await _tcp.RecvData(_tcp._socket);
                    if (dataType != "str")
                    {
                        Console.WriteLine("CreateRoom Error! Not str Received");
                        return;
                    }
                    var rName = _converter.StringFromBytes(dataBuffer);
                    chatRoom chatForm = new chatRoom(result, rName, this, _mdi);
                    foreach (Control control in this.Controls)
                    {
                        control.Dispose();
                    }
                    chatForm.MdiParent = _mdi;
                    chatForm.Dock = DockStyle.Fill;
                    chatForm.Show();
                }
                else
                    MessageBox.Show("Error");
            }
            else
                MessageBox.Show("1명만 선택해주세요.");
        }

        private async Task ListUpdate()
        {
            int select = 9;
            byte[] selecBuffer = _converter.ConvertToBytes(select);
            await _tcp.SendDataAsync(_tcp._socket, selecBuffer);

            await _tcp.SendData(_tcp._socket, _lastRoom);
            (string dataType, byte[] dataBuffer) = await _tcp.RecvData(_tcp._socket);
            if (dataType != "int")
                Console.WriteLine("ListUpdate Error! Not Int Received");
            int result = _converter.IntFromBytes(dataBuffer);
            if (result == 0)
                return;
            await _tcp.SendData(_tcp._socket, _id);
            (dataType, dataBuffer) = await _tcp.RecvData(_tcp._socket);
            if (dataType != "tbl")
                Console.WriteLine("ListUpdate Error! Not tbl Received");
            var data = _converter.StringFromBytes(dataBuffer);
            var tbl = _converter.JsonToDataTable(data);
            (dataType, dataBuffer) = await _tcp.RecvData(_tcp._socket);
            if (dataType != "int")
                Console.WriteLine("ListUpdate Error! Not int Received");
            int count = _converter.IntFromBytes(dataBuffer);

            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    // 0: index, 1: roomName, 2: lastMsg
                    AddRoom(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString());
                }
            }

            _lastRoom = count;
        }

        private async void metroButton1_Click(object sender, EventArgs e)
        {
            await ListUpdate();
        }
    }
}
