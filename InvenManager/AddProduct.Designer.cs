
namespace InvenManager
{
    partial class AddProduct
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
            this.codeLabel = new System.Windows.Forms.Label();
            this.codeBox = new System.Windows.Forms.TextBox();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.categoryBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.costLabel = new System.Windows.Forms.Label();
            this.costBox = new System.Windows.Forms.TextBox();
            this.priceLabel = new System.Windows.Forms.Label();
            this.priceBox = new System.Windows.Forms.TextBox();
            this.expLabel = new System.Windows.Forms.Label();
            this.expBox = new System.Windows.Forms.TextBox();
            this.salesLabel = new System.Windows.Forms.Label();
            this.salesBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(442, 71);
            this.panel1.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("돋움", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.titleLabel.Location = new System.Drawing.Point(14, 13);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(144, 29);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "상품 등록";
            // 
            // codeLabel
            // 
            this.codeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.codeLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.codeLabel.Location = new System.Drawing.Point(12, 91);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(77, 25);
            this.codeLabel.TabIndex = 1;
            this.codeLabel.Text = "상품코드";
            this.codeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // codeBox
            // 
            this.codeBox.Location = new System.Drawing.Point(95, 91);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(123, 25);
            this.codeBox.TabIndex = 2;
            // 
            // categoryLabel
            // 
            this.categoryLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.categoryLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.categoryLabel.Location = new System.Drawing.Point(224, 91);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(77, 25);
            this.categoryLabel.TabIndex = 1;
            this.categoryLabel.Text = "카테고리";
            this.categoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // categoryBox
            // 
            this.categoryBox.Location = new System.Drawing.Point(307, 91);
            this.categoryBox.Name = "categoryBox";
            this.categoryBox.Size = new System.Drawing.Size(123, 25);
            this.categoryBox.TabIndex = 2;
            // 
            // nameLabel
            // 
            this.nameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nameLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.nameLabel.Location = new System.Drawing.Point(12, 135);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(77, 25);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "상품명";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(95, 135);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(335, 25);
            this.nameBox.TabIndex = 2;
            // 
            // costLabel
            // 
            this.costLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.costLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.costLabel.Location = new System.Drawing.Point(12, 178);
            this.costLabel.Name = "costLabel";
            this.costLabel.Size = new System.Drawing.Size(77, 25);
            this.costLabel.TabIndex = 1;
            this.costLabel.Text = "입고가";
            this.costLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // costBox
            // 
            this.costBox.Location = new System.Drawing.Point(95, 178);
            this.costBox.Name = "costBox";
            this.costBox.Size = new System.Drawing.Size(123, 25);
            this.costBox.TabIndex = 2;
            // 
            // priceLabel
            // 
            this.priceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.priceLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.priceLabel.Location = new System.Drawing.Point(224, 178);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(77, 25);
            this.priceLabel.TabIndex = 1;
            this.priceLabel.Text = "판매가";
            this.priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(307, 178);
            this.priceBox.Name = "priceBox";
            this.priceBox.Size = new System.Drawing.Size(123, 25);
            this.priceBox.TabIndex = 2;
            // 
            // expLabel
            // 
            this.expLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.expLabel.Location = new System.Drawing.Point(12, 224);
            this.expLabel.Name = "expLabel";
            this.expLabel.Size = new System.Drawing.Size(77, 25);
            this.expLabel.TabIndex = 1;
            this.expLabel.Text = "유통기한";
            this.expLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // expBox
            // 
            this.expBox.Location = new System.Drawing.Point(95, 224);
            this.expBox.Name = "expBox";
            this.expBox.Size = new System.Drawing.Size(123, 25);
            this.expBox.TabIndex = 2;
            // 
            // salesLabel
            // 
            this.salesLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.salesLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.salesLabel.Location = new System.Drawing.Point(224, 224);
            this.salesLabel.Name = "salesLabel";
            this.salesLabel.Size = new System.Drawing.Size(77, 25);
            this.salesLabel.TabIndex = 1;
            this.salesLabel.Text = "일일판매율";
            this.salesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // salesBox
            // 
            this.salesBox.Location = new System.Drawing.Point(307, 224);
            this.salesBox.Name = "salesBox";
            this.salesBox.Size = new System.Drawing.Size(123, 25);
            this.salesBox.TabIndex = 2;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(82, 293);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(136, 56);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "등록";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(227, 293);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(136, 56);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "취소";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 361);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.categoryBox);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.salesBox);
            this.Controls.Add(this.expBox);
            this.Controls.Add(this.costBox);
            this.Controls.Add(this.codeBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.salesLabel);
            this.Controls.Add(this.expLabel);
            this.Controls.Add(this.costLabel);
            this.Controls.Add(this.codeLabel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AddProduct";
            this.Text = "AddProduct";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.TextBox codeBox;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.TextBox categoryBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label costLabel;
        private System.Windows.Forms.TextBox costBox;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.TextBox priceBox;
        private System.Windows.Forms.Label expLabel;
        private System.Windows.Forms.TextBox expBox;
        private System.Windows.Forms.Label salesLabel;
        private System.Windows.Forms.TextBox salesBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
    }
}