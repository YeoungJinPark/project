using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvenManager
{
    public class MySQL : IDBManger
    {
        private MySqlConnection _con;
        public MySQL()
        {
            var sb = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                Port = 3306,
                UserID = "msgServer",
                Password = "1234",
                ConnectionTimeout = 300
            };
            _con = new MySqlConnection(sb.ConnectionString);
            _con.Open();

            Execute("CREATE DATABASE IF NOT EXISTS invendb;");
            sb.Database = "invendb";
            string sql = "USE invendb;\n" +
                "CREATE TABLE IF NOT EXISTS productTbl (" +
                "prodCode VARCHAR(6) PRIMARY KEY, " +
                "prodName VARCHAR(20), " +
                "prodCost INT, " +
                "prodPrice INT, " +
                "prodExpDays INT, " +
                "prodCategory VARCHAR(10), " +
                "prodSalesRate INT);\n" +
                "CREATE TABLE IF NOT EXISTS stockTbl (" +
                "stockIdx INT AUTO_INCREMENT PRIMARY KEY, " +
                "stockDateTime DATE, " +
                "stockDate DATE, " +
                "stockProdCode VARCHAR(6), " +
                "stockType VARCHAR(2), " +
                "stockCount INT);";
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
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("MySQL 오류 발생: " + ex.Message);
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
                //using (var cmd = new MySqlCommand())
                //{
                //    for (int i = 0; i < 5; i++)
                //    {
                //        sql += $"(@Value{i}),";
                //        cmd.Parameters.AddWithValue($"@Value{i}", values[i]);
                //    }
                //    sql = sql.TrimEnd(',');
                //    sql += ";";
                //    cmd.CommandText = sql;
                //    cmd.Connection = _con;
                //    cmd.ExecuteNonQuery();
                //}
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("MySQL 오류 발생: " + ex.Message);
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
                using (var cmd = new MySqlCommand(sql, _con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // _con.Close();
            }
        }

        public DataTable Select(string sql)
        {
            var dt = new DataTable();
            try
            {
                using (var cmd = new MySqlCommand(sql, _con))
                {
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
