
namespace InvenManager
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.dateLabel = new System.Windows.Forms.Label();
            this.NextButton = new System.Windows.Forms.Button();
            this.assetLabel = new System.Windows.Forms.Label();
            this.StockButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.productView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.statusButton = new System.Windows.Forms.Button();
            this.listButton = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lastDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.productView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("굴림체", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dateLabel.Location = new System.Drawing.Point(12, 9);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(132, 27);
            this.dateLabel.TabIndex = 0;
            this.dateLabel.Text = "23-09-22";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NextButton
            // 
            this.NextButton.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextButton.Location = new System.Drawing.Point(706, 478);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(96, 48);
            this.NextButton.TabIndex = 1;
            this.NextButton.Text = "다음날로";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextDayClick);
            // 
            // assetLabel
            // 
            this.assetLabel.AutoSize = true;
            this.assetLabel.Font = new System.Drawing.Font("바탕체", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.assetLabel.Location = new System.Drawing.Point(219, 558);
            this.assetLabel.Name = "assetLabel";
            this.assetLabel.Size = new System.Drawing.Size(34, 35);
            this.assetLabel.TabIndex = 2;
            this.assetLabel.Text = "0";
            // 
            // StockButton
            // 
            this.StockButton.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StockButton.Location = new System.Drawing.Point(706, 424);
            this.StockButton.Name = "StockButton";
            this.StockButton.Size = new System.Drawing.Size(96, 48);
            this.StockButton.TabIndex = 1;
            this.StockButton.Text = "상품 입고";
            this.StockButton.UseVisualStyleBackColor = true;
            this.StockButton.Click += new System.EventHandler(this.StockButton_Click);
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(706, 370);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(96, 48);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "상품 등록";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // productView
            // 
            this.productView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productView.Location = new System.Drawing.Point(13, 39);
            this.productView.MultiSelect = false;
            this.productView.Name = "productView";
            this.productView.RowTemplate.Height = 23;
            this.productView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.productView.Size = new System.Drawing.Size(687, 222);
            this.productView.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("바탕체", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 558);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "총자산(원)";
            // 
            // statusButton
            // 
            this.statusButton.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusButton.Location = new System.Drawing.Point(706, 316);
            this.statusButton.Name = "statusButton";
            this.statusButton.Size = new System.Drawing.Size(96, 48);
            this.statusButton.TabIndex = 1;
            this.statusButton.Text = "매출현황";
            this.statusButton.UseVisualStyleBackColor = true;
            this.statusButton.Click += new System.EventHandler(this.statusButton_Click);
            // 
            // listButton
            // 
            this.listButton.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listButton.Location = new System.Drawing.Point(706, 262);
            this.listButton.Name = "listButton";
            this.listButton.Size = new System.Drawing.Size(96, 48);
            this.listButton.TabIndex = 1;
            this.listButton.Text = "재고목록";
            this.listButton.UseVisualStyleBackColor = true;
            this.listButton.Click += new System.EventHandler(this.listButton_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(13, 295);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "구매";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "판매";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(687, 260);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            title1.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            title1.Name = "title";
            title1.Text = "일자별 매출현황";
            this.chart1.Titles.Add(title1);
            // 
            // startDate
            // 
            this.startDate.CustomFormat = "yyyy-MM-dd";
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDate.Location = new System.Drawing.Point(13, 268);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(103, 21);
            this.startDate.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "~";
            // 
            // lastDate
            // 
            this.lastDate.CustomFormat = "yyyy-MM-dd";
            this.lastDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.lastDate.Location = new System.Drawing.Point(142, 268);
            this.lastDate.Name = "lastDate";
            this.lastDate.Size = new System.Drawing.Size(103, 21);
            this.lastDate.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 602);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lastDate);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.productView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.assetLabel);
            this.Controls.Add(this.listButton);
            this.Controls.Add(this.statusButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.StockButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.dateLabel);
            this.Name = "MainForm";
            this.Text = "CVSManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.productView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label assetLabel;
        private System.Windows.Forms.Button StockButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridView productView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button statusButton;
        private System.Windows.Forms.Button listButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker lastDate;
    }
}

