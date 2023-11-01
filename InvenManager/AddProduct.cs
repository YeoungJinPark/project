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
    public partial class AddProduct : Form
    {
        public string[] _result = new string[7];
        public AddProduct()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if(CheckSpace(this))
            {
                MessageBox.Show("값을 전부 입력해주세요.");
                return;
            }
            if(!double.TryParse(costBox.Text, out _)||
                !double.TryParse(priceBox.Text, out _)||
                !double.TryParse(expBox.Text, out _))
            {
                MessageBox.Show("잘못된 형식입니다.\n숫자를 입력해주세요.");
                return;
            }
            _result[0] = codeBox.Text;
            _result[1] = categoryBox.Text;
            _result[2] = nameBox.Text;
            _result[3] = costBox.Text;
            _result[4] = priceBox.Text;
            _result[5] = expBox.Text;
            _result[6] = salesBox.Text;

            this.DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();
        }

        private bool CheckSpace(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Dispose();
            this.Close();
        }
    }
}
