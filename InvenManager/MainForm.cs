using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InvenManager
{
    public partial class MainForm : Form
    {
        private IDBManger _db;
        private ConfigManager _con = new ConfigManager();
        private string _today;
        public MainForm()
        {
            InitializeComponent();
            InitConfig();
        }

        private void InitConfig()
        {
            var result = _con.GetValue("selectedDB");
            if (result == null)
            {
                result = SelectDialog();
                _con.SetValue("selectedDB", result);
            }
            _db = SelectDB(result);

            result = _con.GetValue("savedDate");
            if (result != null)
                dateLabel.Text = result;
            _today = dateLabel.Text;
            result = _con.GetValue("asset");
            if (result != null)
                assetLabel.Text = result;
            else
                assetLabel.Text = "30000";
            var date = DateTime.ParseExact("20" + dateLabel.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            startDate.Value = date;
            lastDate.Value = startDate.Value;
        }

        /// <summary>
        /// DB선택 Dialog창
        /// </summary>
        /// <returns>선택한DB정보(string)</returns>
        private string SelectDialog()
        {
            using (var selectDialog = new SelectDB())
            {
                var result = selectDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return selectDialog.result;
                }
            }
            return null;
        }
        
        private IDBManger SelectDB(string select)
        {
            IDBManger dbManger = null;

            if (select == "MySQL")
            {
                dbManger = new MySQL();
            }
            else if (select == "SQLite")
            {
                dbManger = new SQLite();
            }

            if (dbManger != null)
            {
                return dbManger;
            }

            return null;
        }

        private void NextDayClick(object sender, EventArgs e)
        {
            // 판매, 차트정산 등
            Settle();
            var nextday = DateTime.ParseExact(_today, "yy-MM-dd", CultureInfo.InvariantCulture);
            _today = nextday.AddDays(1).ToString("yy-MM-dd");
            dateLabel.Text = _today;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (var addDialog = new AddProduct())
            {
                var result = addDialog.ShowDialog();
                if(result == DialogResult.OK)
                {
                    _db.InsertProduct(addDialog._result);
                }
            }
        }

        private void StockButton_Click(object sender, EventArgs e)
        {
            using (var addDialog = new AddStock(_db, assetLabel.Text))
            {
                var result = addDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string[] values = new string[5];
                    values[0] = "20" + _today;
                    values[1] = "20" + _today;
                    values[2] = addDialog._code;
                    values[3] = "구매";
                    values[4] = addDialog._count;
                    int.TryParse(assetLabel.Text, out var asset);
                    int.TryParse(addDialog._count, out var cnt);
                    assetLabel.Text = (asset - addDialog._cost * cnt).ToString();
                    _db.InsertStock(values);
                }
            }
        }

        private void Settle()
        {
            var day = DateTime.ParseExact(_today, "yy-MM-dd", CultureInfo.InvariantCulture);
            day = day.AddDays(-1);
            Random rd = new Random();
            string sql = "SELECT st.stockProdCode, st.stockCount, st.stockDate, " +
                "pd.prodSalesRate, pd.prodPrice, pd.prodExpDays " +
                "FROM stockTbl AS st " +
                "INNER JOIN productTbl AS pd ON st.stockProdCode = pd.prodCode " +
                $"WHERE st.stockType = '재고' AND st.stockDateTime = '{day:yyyy-MM-dd}';";
            var stockDT = _db.Select(sql);
            int total = 0;
            day = day.AddDays(+1);
            if(stockDT.Rows.Count > 0)
            {
                foreach (DataRow stcokRow in stockDT.Rows)
                {
                    // 판매처리
                    DateTime stockDate = DateTime.Parse(stcokRow["stockDate"].ToString());
                    string stockCode = stcokRow["stockProdCode"].ToString();
                    int exp = int.Parse(stcokRow["prodExpDays"].ToString());
                    int stockCount = int.Parse(stcokRow["stockCount"].ToString());
                    int sales = int.Parse(stcokRow["prodSalesRate"].ToString());
                    int price = int.Parse(stcokRow["prodPrice"].ToString());
                    int salesCount = rd.Next(sales + 1);
                    string[] values;

                    if (salesCount > 0)
                    {
                        stockCount = stockCount - salesCount;
                        if (stockCount < 0)
                            stockCount = 0;
                        values = new string[5]
                            { "20"+_today, null, stockCode, "판매", salesCount.ToString() };
                        _db.InsertStock(values);
                        total += price * sales;
                    }
                    // 폐기처리
                    if (stockCount > 0)
                    {
                        TimeSpan dayDiff = day - stockDate;
                        if (dayDiff.Days > exp)
                        {
                            values = new string[5]
                            { "20"+_today, $"{stockDate.Date.ToString("yyyy-MM-dd")}", stockCode, "폐기", $"{stockCount}" };
                            _db.InsertStock(values);
                            stockCount = 0;
                        }
                        values = new string[5]
                            { "20"+_today, $"{stockDate.Date.ToString("yyyy-MM-dd")}", stockCode, "재고", $"{stockCount}" };
                        _db.InsertStock(values);
                    }
                }
            }

            sql = "SELECT stockDateTime, stockDate, stockProdCode, " +
                "stockType, stockCount FROM stockTbl " +
                $"WHERE stockType = '구매' AND stockDateTime = '20{_today}';";
            var buyDT = _db.Select(sql);
            foreach (DataRow row in buyDT.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item + "\t");
                }
                Console.WriteLine();
            }
            if (buyDT.Rows.Count > 0)
            {
                foreach (DataRow buyRow in buyDT.Rows)
                {
                    buyRow["stockType"] = "재고";
                    var values = new string[5];
                    for (int i = 0; i < 5; i++)
                    {
                        values[i] = buyRow[i] is DateTime dateTime ?
                            $"{dateTime:yyyy-MM-dd}" : $"{buyRow[i]}";
                    }
                    _db.InsertStock(values);
                }
            }
            assetLabel.Text = $"{int.Parse(assetLabel.Text) + total}";
        }

        private void Chart()
        {
            string start = startDate.Text;
            string end = lastDate.Text;
            //string salesSql = "SELECT DATE_FORMAT(st.stockDateTime, '%Y-%m-%d') AS date, " +
            //    "SUM(pt.prodPrice * st.stockCount) AS stp " +
            //    "FROM producttbl AS pt " +
            //    "INNER JOIN stocktbl AS st ON st.stockProdCode = pt.prodCode " +
            //    "WHERE st.stockType = '판매' " +
            //    $"AND st.stockDateTime >= '{startDate.Text}' " +
            //    $"AND st.stockDateTime <= '{lastDate.Text}'" +
            //    "GROUP BY date ORDER BY date;";

            //string purchaseSql = "SELECT DATE_FORMAT(st.stockDateTime, '%Y-%m-%d') AS date, " +
            //    "SUM(pt.prodCost * st.stockCount) AS ptp " +
            //    "FROM producttbl AS pt " +
            //    "INNER JOIN stocktbl AS st ON st.stockProdCode = pt.prodCode " +
            //    "WHERE st.stockType = '구매' " +
            //    $"AND st.stockDateTime >= '{startDate.Text}' " +
            //    $"AND st.stockDateTime <= '{lastDate.Text}' " +
            //    "GROUP BY date ORDER BY date;";
            //var salesTotal = _db.Select(salesSql);
            //var purchaseTotal = _db.Select(purchaseSql);
            //chart1.Series["판매"].Points.DataBind(salesTotal.AsEnumerable(), "sdate", "stp", "");
            //chart1.Series["구매"].Points.DataBind(purchaseTotal.AsEnumerable(), "pdate", "ptp", "");
            //salesTotal.Merge(purchaseTotal, false);

            string sql = "SELECT st.stockDateTime AS date, " +
            "SUM(CASE WHEN st.stockType = '판매' THEN pt.prodPrice * st.stockCount ELSE 0 END) AS stp, " +
            "SUM(CASE WHEN st.stockType = '구매' THEN pt.prodCost * st.stockCount ELSE 0 END) AS ptp " +
            "FROM producttbl AS pt " +
            "INNER JOIN stocktbl AS st ON st.stockProdCode = pt.prodCode " +
            $"WHERE st.stockDateTime >= '{startDate.Text}' " +
            $"AND st.stockDateTime <= '{lastDate.Text}'" +
            "GROUP BY date ORDER BY date;";

            var totalData = _db.Select(sql);

            chart1.Series["판매"].Points.DataBind(totalData.AsEnumerable(), "date", "stp", "");
            chart1.Series["구매"].Points.DataBind(totalData.AsEnumerable(), "date", "ptp", "");

            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";

            chart1.DataBind();

            //chart1.Series["판매"].XValueMember = "date";
            //chart1.Series["판매"].YValueMembers = "stp";
            //chart1.Series["판매"].Points.DataBind(salesTotal.AsEnumerable(), "date", "stp", "");

            //chart1.Series["구매"].XValueMember = "date";
            //chart1.Series["구매"].YValueMembers = "ptp";
            //chart1.Series["구매"].Points.DataBind(salesTotal.AsEnumerable(), "date", "ptp", "");
            //chart1.DataBind();
        }

        private void statusButton_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM stockTbl WHERE stockType = '판매';";
            var salesDT = _db.Select(sql);
            if(salesDT.Rows.Count > 0)
                productView.DataSource = salesDT;

            Chart();
        }

        private void listButton_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM stockTbl WHERE stockType = '재고';";
            var stockDT = _db.Select(sql);
            if(stockDT.Rows.Count > 0)
                productView.DataSource = stockDT;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _con.SetValue("savedDate", dateLabel.Text);
            _con.SetValue("asset", assetLabel.Text);
            _db.CloseCon();
        }
    }
}
