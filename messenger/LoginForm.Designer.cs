
namespace messenger
{
    partial class LoginForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new MetroFramework.Controls.MetroPanel();
            this.errorLabel = new MetroFramework.Controls.MetroLabel();
            this.signUpLink = new MetroFramework.Controls.MetroLink();
            this.loginButton = new MetroFramework.Controls.MetroButton();
            this.pwBox = new MetroFramework.Controls.MetroTextBox();
            this.idBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.loginLabel = new MetroFramework.Controls.MetroLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.errorLabel);
            this.panel.Controls.Add(this.signUpLink);
            this.panel.Controls.Add(this.loginButton);
            this.panel.Controls.Add(this.pwBox);
            this.panel.Controls.Add(this.idBox);
            this.panel.Controls.Add(this.metroLabel2);
            this.panel.Controls.Add(this.metroLabel1);
            this.panel.Controls.Add(this.loginLabel);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.HorizontalScrollbarBarColor = true;
            this.panel.HorizontalScrollbarHighlightOnWheel = false;
            this.panel.HorizontalScrollbarSize = 10;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(486, 614);
            this.panel.TabIndex = 1;
            this.panel.VerticalScrollbarBarColor = true;
            this.panel.VerticalScrollbarHighlightOnWheel = false;
            this.panel.VerticalScrollbarSize = 10;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(168, 231);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(139, 19);
            this.errorLabel.TabIndex = 10;
            this.errorLabel.Text = "정보를 입력해주세요";
            // 
            // signUpLink
            // 
            this.signUpLink.Location = new System.Drawing.Point(168, 576);
            this.signUpLink.Name = "signUpLink";
            this.signUpLink.Size = new System.Drawing.Size(140, 23);
            this.signUpLink.TabIndex = 9;
            this.signUpLink.Text = "sign up";
            this.signUpLink.UseSelectable = true;
            this.signUpLink.Click += new System.EventHandler(this.signUpLink_Click);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(315, 176);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(58, 52);
            this.loginButton.TabIndex = 7;
            this.loginButton.Text = "Login";
            this.loginButton.UseSelectable = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // pwBox
            // 
            // 
            // 
            // 
            this.pwBox.CustomButton.Image = null;
            this.pwBox.CustomButton.Location = new System.Drawing.Point(118, 1);
            this.pwBox.CustomButton.Name = "";
            this.pwBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pwBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pwBox.CustomButton.TabIndex = 1;
            this.pwBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pwBox.CustomButton.UseSelectable = true;
            this.pwBox.CustomButton.Visible = false;
            this.pwBox.Lines = new string[0];
            this.pwBox.Location = new System.Drawing.Point(168, 205);
            this.pwBox.MaxLength = 32767;
            this.pwBox.Name = "pwBox";
            this.pwBox.PasswordChar = '*';
            this.pwBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pwBox.SelectedText = "";
            this.pwBox.SelectionLength = 0;
            this.pwBox.SelectionStart = 0;
            this.pwBox.ShortcutsEnabled = true;
            this.pwBox.Size = new System.Drawing.Size(140, 23);
            this.pwBox.TabIndex = 6;
            this.pwBox.UseSelectable = true;
            this.pwBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pwBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.pwBox.TextChanged += new System.EventHandler(this.idBox_TextChanged);
            this.pwBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pwBox_KeyPress);
            // 
            // idBox
            // 
            // 
            // 
            // 
            this.idBox.CustomButton.Image = null;
            this.idBox.CustomButton.Location = new System.Drawing.Point(118, 1);
            this.idBox.CustomButton.Name = "";
            this.idBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.idBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.idBox.CustomButton.TabIndex = 1;
            this.idBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.idBox.CustomButton.UseSelectable = true;
            this.idBox.CustomButton.Visible = false;
            this.idBox.Lines = new string[0];
            this.idBox.Location = new System.Drawing.Point(168, 176);
            this.idBox.MaxLength = 32767;
            this.idBox.Name = "idBox";
            this.idBox.PasswordChar = '\0';
            this.idBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.idBox.SelectedText = "";
            this.idBox.SelectionLength = 0;
            this.idBox.SelectionStart = 0;
            this.idBox.ShortcutsEnabled = true;
            this.idBox.Size = new System.Drawing.Size(140, 23);
            this.idBox.TabIndex = 5;
            this.idBox.UseSelectable = true;
            this.idBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.idBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.idBox.TextChanged += new System.EventHandler(this.idBox_TextChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.Location = new System.Drawing.Point(131, 205);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(31, 23);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "PW";
            // 
            // metroLabel1
            // 
            this.metroLabel1.Location = new System.Drawing.Point(141, 176);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(21, 23);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "ID";
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.loginLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.loginLabel.Location = new System.Drawing.Point(213, 78);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(59, 25);
            this.loginLabel.TabIndex = 2;
            this.loginLabel.Text = "Login";
            this.loginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 614);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Messenger";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel panel;
        private MetroFramework.Controls.MetroButton loginButton;
        private MetroFramework.Controls.MetroTextBox pwBox;
        private MetroFramework.Controls.MetroTextBox idBox;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel loginLabel;
        private MetroFramework.Controls.MetroLink signUpLink;
        private MetroFramework.Controls.MetroLabel errorLabel;
    }
}

