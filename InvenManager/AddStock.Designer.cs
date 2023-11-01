
namespace InvenManager
{
    partial class AddStock
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.gridView = new System.Windows.Forms.DataGridView();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.countBox = new System.Windows.Forms.TextBox();
            this.minus1Button = new System.Windows.Forms.Button();
            this.minus10Button = new System.Windows.Forms.Button();
            this.plus1Button = new System.Windows.Forms.Button();
            this.plus10Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 71);
            this.panel1.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("돋움", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.titleLabel.Location = new System.Drawing.Point(14, 13);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(144, 29);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "상품 입고";
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AllowUserToOrderColumns = true;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridView.Location = new System.Drawing.Point(0, 71);
            this.gridView.MultiSelect = false;
            this.gridView.Name = "gridView";
            this.gridView.ReadOnly = true;
            this.gridView.RowTemplate.Height = 23;
            this.gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridView.Size = new System.Drawing.Size(584, 393);
            this.gridView.TabIndex = 2;
            this.gridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellContentClick);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(266, 479);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(150, 70);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "입고";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(422, 479);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 70);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "취소";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // countBox
            // 
            this.countBox.Location = new System.Drawing.Point(86, 505);
            this.countBox.Name = "countBox";
            this.countBox.Size = new System.Drawing.Size(72, 21);
            this.countBox.TabIndex = 4;
            this.countBox.Text = "1";
            this.countBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // minus1Button
            // 
            this.minus1Button.AutoSize = true;
            this.minus1Button.Location = new System.Drawing.Point(12, 503);
            this.minus1Button.Name = "minus1Button";
            this.minus1Button.Size = new System.Drawing.Size(29, 23);
            this.minus1Button.TabIndex = 5;
            this.minus1Button.Text = "-1";
            this.minus1Button.UseVisualStyleBackColor = true;
            this.minus1Button.Click += new System.EventHandler(this.PlusMinus);
            // 
            // minus10Button
            // 
            this.minus10Button.AutoSize = true;
            this.minus10Button.Location = new System.Drawing.Point(47, 503);
            this.minus10Button.Name = "minus10Button";
            this.minus10Button.Size = new System.Drawing.Size(33, 23);
            this.minus10Button.TabIndex = 5;
            this.minus10Button.Text = "-10";
            this.minus10Button.UseVisualStyleBackColor = true;
            this.minus10Button.Click += new System.EventHandler(this.PlusMinus);
            // 
            // plus1Button
            // 
            this.plus1Button.AutoSize = true;
            this.plus1Button.Location = new System.Drawing.Point(164, 503);
            this.plus1Button.Name = "plus1Button";
            this.plus1Button.Size = new System.Drawing.Size(29, 23);
            this.plus1Button.TabIndex = 5;
            this.plus1Button.Text = "+1";
            this.plus1Button.UseVisualStyleBackColor = true;
            this.plus1Button.Click += new System.EventHandler(this.PlusMinus);
            // 
            // plus10Button
            // 
            this.plus10Button.AutoSize = true;
            this.plus10Button.Location = new System.Drawing.Point(199, 503);
            this.plus10Button.Name = "plus10Button";
            this.plus10Button.Size = new System.Drawing.Size(33, 23);
            this.plus10Button.TabIndex = 5;
            this.plus10Button.Text = "+10";
            this.plus10Button.UseVisualStyleBackColor = true;
            this.plus10Button.Click += new System.EventHandler(this.PlusMinus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 479);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "수량";
            // 
            // AddStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.plus10Button);
            this.Controls.Add(this.plus1Button);
            this.Controls.Add(this.minus10Button);
            this.Controls.Add(this.minus1Button);
            this.Controls.Add(this.countBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.gridView);
            this.Controls.Add(this.panel1);
            this.Name = "AddStock";
            this.Text = "AddStock";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.DataGridView gridView;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox countBox;
        private System.Windows.Forms.Button minus1Button;
        private System.Windows.Forms.Button minus10Button;
        private System.Windows.Forms.Button plus1Button;
        private System.Windows.Forms.Button plus10Button;
        private System.Windows.Forms.Label label1;
    }
}