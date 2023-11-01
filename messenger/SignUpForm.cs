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
    public partial class SignUp : Form
    {
        TcpClass _tcp;
        MdiParents _mdi;
        public SignUp()
        {
            InitializeComponent();
        }

        public SignUp(TcpClass tcp, MdiParents mdi) : this()
        {
            _tcp = tcp;
            _mdi = mdi;
        }

        private async void duplicatedButton_Click(object sender, EventArgs e)
        {
            var id = idBox.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                idLabel.Text = "ID를 입력해주세요";
                return;
            }
            var check = await _tcp.Login(id);
            if (check != "")
                idLabel.Text = "사용 불가능한 ID입니다.";
            else
                idLabel.Text = "사용 가능한 ID입니다.";
            await _tcp.SendData(_tcp._socket, 0);
        }

        private void idBox_TextChanged(object sender, EventArgs e)
        {
            var id = idBox.Text;
            errLabel.Text = "";
            if (id == "")
                idLabel.Text = "ID를 입력해주세요.";
            else
                idLabel.Text = "";
        }

        private void pwBox_TextChanged(object sender, EventArgs e)
        {
            var pw = pwBox.Text;
            errLabel.Text = "";
            if (pw == "")
                pwLabel.Text = "PW를 입력해주세요.";
            else
                pwLabel.Text = "";
        }

        private void confirmBox_TextChanged(object sender, EventArgs e)
        {
            var pw = pwBox.Text;
            var confirm = confirmBox.Text;
            errLabel.Text = "";
            if (pw != confirm)
                confirmLabel.Text = "비밀번호가 일치하지 않습니다.";
            else
                confirmLabel.Text = "";
        }
        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            errLabel.Text = "";
        }

        private async void registerButton_Click(object sender, EventArgs e)
        {
            string[] fields = { idBox.Text, pwBox.Text, confirmBox.Text,
                                nameBox.Text, posBox.Text, deptBox.Text };
            if(fields.Any(string.IsNullOrWhiteSpace))
            {
                errLabel.Text = "정보를 모두 입력해주세요.";
                return;
            }
            var check = await _tcp.Login(fields[0]);
            if(check != "")
            {
                errLabel.Text = "사용 불가능한 ID입니다.";
                idBox.Clear();
                idBox.Focus();
                return;
            }
            if(fields[1] != fields[2])
            {
                errLabel.Text = "비밀번호가 일치하지 않습니다.";
                pwBox.Clear();
                confirmBox.Clear();
                pwBox.Focus();
                return;
            }
            await _tcp.SendData(_tcp._socket, 0);
            await _tcp.Register(fields);
            MessageBox.Show("가입 완료.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
            backButton_Click(sender, e);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                control.Dispose();
            }
            LoginForm loginForm = new LoginForm(_tcp, _mdi); // _tcp 값을 전달하여 새로운 loginForm 객체 생성
            loginForm.MdiParent = _mdi;
            loginForm.Dock = DockStyle.Fill;
            loginForm.Show();
        }
    }
}
