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
    public partial class SelectDB : Form
    {
        public string result { get; private set; }
        public SelectDB()
        {
            InitializeComponent();
        }

        private void SelectMySQL(object sender, EventArgs e)
        {
            result = "MySQL";
            this.DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();
        }

        private void SelectSQLite(object sender, EventArgs e)
        {
            result = "SQLite";
            this.DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();
        }
    }
}
