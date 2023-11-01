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
    public partial class LoginForm : Form
    {
        public string _id;
        public string _pw;
        TcpClass _tcp { get; }
        MdiParents _mdi { get; }
        public LoginForm()
        {
            InitializeComponent();
            Font font = loginLabel.Font;
            loginLabel.Font = new Font(font.FontFamily, 20, font.Style);
        }

        public LoginForm(TcpClass tcp, MdiParents mdi) : this()
        {
            _tcp = tcp;
            _mdi = mdi;
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            var id = idBox.Text;
            var pw = pwBox.Text;
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(pw))
                return;

            var checkPW = await _tcp.Login(id);
            if (string.IsNullOrWhiteSpace(checkPW))
            {
                errorLabel.Text = "존재하지 않는 ID입니다.";
                idBox.Clear();
                pwBox.Clear();
                idBox.Focus();
                return;
            }

            if (pw != checkPW)
            {
                errorLabel.Text = "비밀번호가 일치하지 않습니다.";
                pwBox.Clear();
                pwBox.Focus();
                return;
            }

            foreach (Control control in this.Controls)
            {
                control.Dispose();
            }

            _id = id;
            _pw = pw;

            await _tcp.SendData(_tcp._socket, 1);

            MainForm mainForm = new MainForm(_id, _tcp, _mdi);
            mainForm.MdiParent = _mdi;
            mainForm.Show();
        }

        private void idBox_TextChanged(object sender, EventArgs e)
        {
            var id = idBox.Text;
            var pw = pwBox.Text;
            if (id == "" && pw == "")
                errorLabel.Text = "정보를 입력해주세요.";
            else if (id == "" && pw != "")
                errorLabel.Text = "ID를 입력해주세요.";
            else if (id != "" && pw == "")
                errorLabel.Text = "PW를 입력해주세요.";
            else
                errorLabel.Text = "";
        }

        private void signUpLink_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                control.Dispose();
            }
            SignUp signUpForm = new SignUp(_tcp, _mdi);
            signUpForm.MdiParent = _mdi;
            signUpForm.Dock = DockStyle.Fill;
            signUpForm.Show();
        }

        private void pwBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                loginButton_Click(sender, e);
        }
    }
}
