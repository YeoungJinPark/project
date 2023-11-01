using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvenManager
{
    class SQLite : IDBManger
    {
        private SQLiteConnection _con;
        public SQLite()
        {
            string dbPath = "sqlitedb.db";
            _con = new SQLiteConnection($"Data Source={dbPath};");
            _con.Open();

            string sql = "CREATE TABLE IF NOT EXISTS productTbl (" +
                "prodCode TEXT PRIMARY KEY, " +
                "prodName TEXT, " + 
                "prodCost INTEGER, " +
                "prodPrice INTEGER, " +
                "prodExpDays INTEGER, " +
                "prodCategory TEXT, " +
                "prodSalesRate INTEGER);\n" +
                "CREATE TABLE IF NOT EXISTS stockTbl (" +
                "stockIdx INTEGER PRIMARY KEY, " +
                "stockDatetime DATE, " +
                "stockDate DATE, " +
                "stockProdCode TEXT, " +
                "stockType TEXT, " +
                "stockCount INTEGER);";
            Execute(sql);
        }
        public void InsertProduct(string[] values)
        {
            try
            {
                string sql = "INSERT INTO productTbl (prodCode, prodCategory, prodName, " +
                    "prodCost, prodPrice, prodExpDays, prodSalesRate) VALUES " +
                    $"('{values[0]}', '{values[1]}', '{values[2]}', {values[3]}, " +
                    $"{values[4]}, {values[5]}, {values[6]});";
                Execute(sql);
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("SQLite 오류 발생: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // MessageBox.Show(ex.Message);
            }
        }
        public void InsertStock(string[] values)
        {
            try
            {
                string sql = $"INSERT INTO stockTbl (stockDateTime, stockDate, " +
                    $"stockProdCode, stockType, stockCount) VALUES ('{values[0]}', " +
                    $"{(values[1] is null ? "NULL" : $"'{values[1]}'")}, " +
                    $"'{values[2]}', '{values[3]}', {values[4]});";
                Execute(sql);
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("SQLite 오류 발생: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("여기");
            }
        }

        public void Execute(string sql)
        {
            try
            {
                using (var cmd = new SQLiteCommand(sql, _con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {

            }
        }
        public DataTable Select(string sql)
        {
            var dt = new DataTable();
            try
            {
                using (var cmd = new SQLiteCommand(sql, _con))
                {
                    // _con.Open();
                    using (var rdr = cmd.ExecuteReader())
                        dt.Load(rdr);
                }
            }
            catch
            {
                
            }
            return dt;
        }

        public void CloseCon()
        {
            _con.Close();
        }
    }
}
