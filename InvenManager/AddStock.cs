using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvenManager
{
    public partial class AddStock : Form
    {
        private IDBManger _db;
        private DataGridViewRow _lastSelectedCell;
        public string _code;
        public int _cost;
        public string _count;
        public int _asset;
        public AddStock()
        {
            InitializeComponent();
        }

        public AddStock(IDBManger db, string asset) : this()
        {
            _db = db;
            _asset = int.Parse(asset);
            LoadData();
        }

        private void LoadData()
        {
            string sql = "SELECT * FROM productTbl;";
            var dt = _db.Select(sql);
            if(dt.Rows.Count > 0)
                gridView.DataSource = dt;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Dispose();
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var currCell = gridView.CurrentRow;
            if (string.IsNullOrWhiteSpace(countBox.Text))
            {
                MessageBox.Show("수량을 입력해주세요.");
                return;
            }

            if (currCell != null)
            {
                _cost = int.Parse(currCell.Cells["prodCost"].Value.ToString());
                _code = currCell.Cells["prodCode"].Value.ToString();
                _count = countBox.Text;
                if (_asset - _cost * int.Parse(_count) <= 0)
                    this.DialogResult = DialogResult.No;
                else
                    this.DialogResult = DialogResult.OK;
                this.Dispose();
                this.Close();
            }
            else
                MessageBox.Show("추가할 상품이 존재하지 않습니다.");
        }
        /// <summary>
        /// 선택한 행이 이전과 다르면 수량을 1로 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var currCell = gridView.CurrentRow;
            if (currCell != _lastSelectedCell)
                countBox.Text = "1";
            _lastSelectedCell = currCell;
        }

        private void PlusMinus(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int.TryParse(btn.Text, out var num);
            int.TryParse(countBox.Text, out var count);
            count += num;
            if (count > 0)
                countBox.Text = count.ToString();
            else
                countBox.Text = "1";
        }
    }
}
