
namespace messenger
{
    partial class RoomBox
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
            this.roomPanel = new MetroFramework.Controls.MetroPanel();
            this.listLabel = new MetroFramework.Controls.MetroLabel();
            this.lastMsgLabel = new MetroFramework.Controls.MetroLabel();
            this.roomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // roomPanel
            // 
            this.roomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.roomPanel.Controls.Add(this.listLabel);
            this.roomPanel.Controls.Add(this.lastMsgLabel);
            this.roomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomPanel.HorizontalScrollbarBarColor = true;
            this.roomPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.roomPanel.HorizontalScrollbarSize = 10;
            this.roomPanel.Location = new System.Drawing.Point(0, 0);
            this.roomPanel.Name = "roomPanel";
            this.roomPanel.Size = new System.Drawing.Size(480, 120);
            this.roomPanel.TabIndex = 0;
            this.roomPanel.UseCustomBackColor = true;
            this.roomPanel.VerticalScrollbarBarColor = true;
            this.roomPanel.VerticalScrollbarHighlightOnWheel = false;
            this.roomPanel.VerticalScrollbarSize = 10;
            // 
            // listLabel
            // 
            this.listLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.listLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.listLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.listLabel.Location = new System.Drawing.Point(3, 0);
            this.listLabel.Name = "listLabel";
            this.listLabel.Size = new System.Drawing.Size(474, 54);
            this.listLabel.TabIndex = 2;
            this.listLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.listLabel.UseCustomBackColor = true;
            this.listLabel.Click += new System.EventHandler(this.Room_Click);
            // 
            // lastMsgLabel
            // 
            this.lastMsgLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lastMsgLabel.Location = new System.Drawing.Point(3, 54);
            this.lastMsgLabel.Name = "lastMsgLabel";
            this.lastMsgLabel.Size = new System.Drawing.Size(474, 66);
            this.lastMsgLabel.TabIndex = 3;
            this.lastMsgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lastMsgLabel.UseCustomBackColor = true;
            this.lastMsgLabel.Click += new System.EventHandler(this.Room_Click);
            // 
            // RoomBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.roomPanel);
            this.Name = "RoomBox";
            this.Size = new System.Drawing.Size(480, 120);
            this.roomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel roomPanel;
        private MetroFramework.Controls.MetroLabel listLabel;
        private MetroFramework.Controls.MetroLabel lastMsgLabel;
    }
}
