
namespace messenger
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.userList = new System.Windows.Forms.ListView();
            this.menuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.P2PMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.createButton = new MetroFramework.Controls.MetroButton();
            this.profilePictureBox = new System.Windows.Forms.PictureBox();
            this.myNameLabel = new MetroFramework.Controls.MetroLabel();
            this.meLabel = new MetroFramework.Controls.MetroLabel();
            this.TabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.roomList = new MetroFramework.Controls.MetroPanel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.TabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(72, 40);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 1;
            this.tabControl.Size = new System.Drawing.Size(486, 614);
            this.tabControl.TabIndex = 1;
            this.tabControl.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.userList);
            this.tabPage1.Controls.Add(this.metroPanel1);
            this.tabPage1.HorizontalScrollbar = true;
            this.tabPage1.HorizontalScrollbarBarColor = true;
            this.tabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage1.HorizontalScrollbarSize = 10;
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(478, 566);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "사원 목록";
            this.tabPage1.VerticalScrollbar = true;
            this.tabPage1.VerticalScrollbarBarColor = true;
            this.tabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage1.VerticalScrollbarSize = 10;
            // 
            // userList
            // 
            this.userList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userList.ContextMenuStrip = this.menuStrip;
            this.userList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userList.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userList.FullRowSelect = true;
            this.userList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.userList.HideSelection = false;
            this.userList.Location = new System.Drawing.Point(0, 52);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(478, 514);
            this.userList.TabIndex = 4;
            this.userList.UseCompatibleStateImageBehavior = false;
            this.userList.View = System.Windows.Forms.View.Details;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.P2PMenuItem});
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(144, 26);
            // 
            // P2PMenuItem
            // 
            this.P2PMenuItem.Name = "P2PMenuItem";
            this.P2PMenuItem.Size = new System.Drawing.Size(143, 22);
            this.P2PMenuItem.Text = "1:1 채팅하기";
            this.P2PMenuItem.Click += new System.EventHandler(this.P2PMenuItem_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.createButton);
            this.metroPanel1.Controls.Add(this.profilePictureBox);
            this.metroPanel1.Controls.Add(this.myNameLabel);
            this.metroPanel1.Controls.Add(this.meLabel);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(13, 3, 3, 3);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(478, 46);
            this.metroPanel1.TabIndex = 3;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // createButton
            // 
            this.createButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.createButton.Location = new System.Drawing.Point(379, 0);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(99, 46);
            this.createButton.TabIndex = 6;
            this.createButton.Text = "채팅방 생성";
            this.createButton.UseSelectable = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // profilePictureBox
            // 
            this.profilePictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.profilePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("profilePictureBox.Image")));
            this.profilePictureBox.Location = new System.Drawing.Point(0, 0);
            this.profilePictureBox.MaximumSize = new System.Drawing.Size(63, 46);
            this.profilePictureBox.Name = "profilePictureBox";
            this.profilePictureBox.Size = new System.Drawing.Size(63, 46);
            this.profilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePictureBox.TabIndex = 4;
            this.profilePictureBox.TabStop = false;
            // 
            // myNameLabel
            // 
            this.myNameLabel.AutoSize = true;
            this.myNameLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.myNameLabel.Location = new System.Drawing.Point(70, 10);
            this.myNameLabel.Name = "myNameLabel";
            this.myNameLabel.Size = new System.Drawing.Size(66, 25);
            this.myNameLabel.TabIndex = 5;
            this.myNameLabel.Text = "내이름";
            // 
            // meLabel
            // 
            this.meLabel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.meLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.meLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.meLabel.Location = new System.Drawing.Point(135, 10);
            this.meLabel.Name = "meLabel";
            this.meLabel.Size = new System.Drawing.Size(40, 26);
            this.meLabel.TabIndex = 3;
            this.meLabel.Text = "Me";
            this.meLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.meLabel.UseCustomBackColor = true;
            this.meLabel.UseCustomForeColor = true;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.metroButton1);
            this.TabPage2.Controls.Add(this.roomList);
            this.TabPage2.HorizontalScrollbarBarColor = true;
            this.TabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.TabPage2.HorizontalScrollbarSize = 10;
            this.TabPage2.Location = new System.Drawing.Point(4, 44);
            this.TabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(478, 566);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "채팅방";
            this.TabPage2.VerticalScrollbarBarColor = true;
            this.TabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.TabPage2.VerticalScrollbarSize = 10;
            // 
            // roomList
            // 
            this.roomList.AutoScroll = true;
            this.roomList.HorizontalScrollbar = true;
            this.roomList.HorizontalScrollbarBarColor = true;
            this.roomList.HorizontalScrollbarHighlightOnWheel = false;
            this.roomList.HorizontalScrollbarSize = 10;
            this.roomList.Location = new System.Drawing.Point(0, 43);
            this.roomList.Name = "roomList";
            this.roomList.Size = new System.Drawing.Size(472, 522);
            this.roomList.TabIndex = 2;
            this.roomList.VerticalScrollbar = true;
            this.roomList.VerticalScrollbarBarColor = true;
            this.roomList.VerticalScrollbarHighlightOnWheel = false;
            this.roomList.VerticalScrollbarSize = 10;
            // 
            // metroButton1
            // 
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButton1.Location = new System.Drawing.Point(415, 4);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(55, 33);
            this.metroButton1.TabIndex = 3;
            this.metroButton1.Text = "갱신";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(486, 614);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTabPage tabPage1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel meLabel;
        private MetroFramework.Controls.MetroLabel myNameLabel;
        private System.Windows.Forms.PictureBox profilePictureBox;
        private MetroFramework.Controls.MetroTabPage TabPage2;
        private System.Windows.Forms.ListView userList;
        private MetroFramework.Controls.MetroPanel roomList;
        private MetroFramework.Controls.MetroButton createButton;
        private System.Windows.Forms.ContextMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem P2PMenuItem;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}