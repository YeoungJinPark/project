
namespace messenger
{
    partial class chatRoom
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chatRoom));
            this.chatButton = new MetroFramework.Controls.MetroButton();
            this.opponentLabel = new MetroFramework.Controls.MetroLabel();
            this.backButton = new MetroFramework.Controls.MetroButton();
            this.chatBox = new MetroFramework.Controls.MetroTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chatTextBox = new System.Windows.Forms.RichTextBox();
            this.opponentTip = new MetroFramework.Components.MetroToolTip();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatButton
            // 
            this.chatButton.Enabled = false;
            this.chatButton.Location = new System.Drawing.Point(411, 572);
            this.chatButton.Name = "chatButton";
            this.chatButton.Size = new System.Drawing.Size(56, 23);
            this.chatButton.TabIndex = 11;
            this.chatButton.Text = "전송";
            this.chatButton.UseSelectable = true;
            this.chatButton.Click += new System.EventHandler(this.chatButton_Click);
            // 
            // opponentLabel
            // 
            this.opponentLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.opponentLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.opponentLabel.Location = new System.Drawing.Point(117, 14);
            this.opponentLabel.MaximumSize = new System.Drawing.Size(241, 45);
            this.opponentLabel.Name = "opponentLabel";
            this.opponentLabel.Size = new System.Drawing.Size(241, 45);
            this.opponentLabel.TabIndex = 9;
            this.opponentLabel.Text = "채팅방 이름";
            this.opponentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.opponentLabel.MouseHover += new System.EventHandler(this.opponentLabel_MouseHover);
            // 
            // backButton
            // 
            this.backButton.AutoSize = true;
            this.backButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backButton.BackgroundImage")));
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.Location = new System.Drawing.Point(12, 14);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(46, 45);
            this.backButton.TabIndex = 8;
            this.backButton.UseSelectable = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // chatBox
            // 
            // 
            // 
            // 
            this.chatBox.CustomButton.Image = null;
            this.chatBox.CustomButton.Location = new System.Drawing.Point(370, 1);
            this.chatBox.CustomButton.Name = "";
            this.chatBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.chatBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.chatBox.CustomButton.TabIndex = 1;
            this.chatBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.chatBox.CustomButton.UseSelectable = true;
            this.chatBox.CustomButton.Visible = false;
            this.chatBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.chatBox.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.chatBox.Lines = new string[0];
            this.chatBox.Location = new System.Drawing.Point(12, 572);
            this.chatBox.MaxLength = 32767;
            this.chatBox.Name = "chatBox";
            this.chatBox.PasswordChar = '\0';
            this.chatBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.chatBox.SelectedText = "";
            this.chatBox.SelectionLength = 0;
            this.chatBox.SelectionStart = 0;
            this.chatBox.ShortcutsEnabled = true;
            this.chatBox.ShowClearButton = true;
            this.chatBox.Size = new System.Drawing.Size(392, 23);
            this.chatBox.TabIndex = 7;
            this.chatBox.UseSelectable = true;
            this.chatBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.chatBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.chatBox.TextChanged += new System.EventHandler(this.chatBox_TextChanged);
            this.chatBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chatBox_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chatTextBox);
            this.panel1.Location = new System.Drawing.Point(12, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 500);
            this.panel1.TabIndex = 12;
            // 
            // chatTextBox
            // 
            this.chatTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatTextBox.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatTextBox.HideSelection = false;
            this.chatTextBox.Location = new System.Drawing.Point(0, 0);
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.Size = new System.Drawing.Size(453, 498);
            this.chatTextBox.TabIndex = 0;
            this.chatTextBox.Text = "";
            // 
            // opponentTip
            // 
            this.opponentTip.Style = MetroFramework.MetroColorStyle.Blue;
            this.opponentTip.StyleManager = null;
            this.opponentTip.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroButton1
            // 
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButton1.Location = new System.Drawing.Point(364, 14);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(102, 45);
            this.metroButton1.TabIndex = 13;
            this.metroButton1.Text = "대화 내보내기";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // chatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(480, 607);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chatButton);
            this.Controls.Add(this.opponentLabel);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.chatBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(480, 607);
            this.Name = "chatRoom";
            this.Load += new System.EventHandler(this.chatRoom_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton chatButton;
        private MetroFramework.Controls.MetroLabel opponentLabel;
        private MetroFramework.Controls.MetroButton backButton;
        private MetroFramework.Controls.MetroTextBox chatBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox chatTextBox;
        private MetroFramework.Components.MetroToolTip opponentTip;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}
