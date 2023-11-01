using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TcpServer
{
    class DBClass
    {
        private MySqlConnectionStringBuilder _sb = new MySqlConnectionStringBuilder();

        public DBClass()
        {
            // _sb.Server = "10.10.20.36";
            // _sb.Server = "172.16.5.163";
            _sb.Server = "127.0.0.1";
            _sb.Port = 3306;
            _sb.Database = "serverdb";
            _sb.UserID = "msgServer";
            _sb.Password = "1234";
        }

        public DataTable SelectPW(string id)
        {
            try
            {
                using (var conn = new MySqlConnection(_sb.ConnectionString))
                {
                    var sql = $"SELECT userPW FROM usersTbl WHERE userID = '{id}'";
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        var rdr = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public DataTable SelectEmpList()
        {
            try
            {
                using (var conn = new MySqlConnection(_sb.ConnectionString))
                {
                    var sql = $"SELECT userID, userName, userPos, userDept FROM usersTbl";
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        var rdr = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int SelectRoomCount()
        {
            try
            {
                using (var conn = new MySqlConnection(_sb.ConnectionString))
                {
                    var sql = $"SELECT COUNT(*) FROM roomstbl";
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        var count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public DataTable SelectRoomList(string id)
        {
            try
            {
                using (var conn = new MySqlConnection(_sb.ConnectionString))
                {
                    var sql = $"SELECT rooms.*FROM roomstbl AS rooms " +
                        $"JOIN jointbl ON rooms.roomIdx = jointbl.roomIdx " +
                        $"JOIN userstbl ON jointbl.userIdx = userstbl.userIdx " +
                        $"WHERE userstbl.userID = '{id}';";
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        var rdr = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public DataTable SelectRoomList(string id, int start, int end)
        {
            try
            {
                using (var conn = new MySqlConnection(_sb.ConnectionString))
                {
                    var sql = $"SELECT rooms.* FROM roomstbl " +
                        $"WHERE roomIdx > {start} AND roomIdx <= {end};";
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        var rdr = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public void InsertUser(string id, string pw, string name, string pos, string dept)
        {
            try
            {
                using (var conn = new MySqlConnection(_sb.ConnectionString))
                {
                    var sql = $"INSERT INTO usersTbl (userID, userPW, userName, userPos, userDept) " +
                        $"VALUES ('{id}', '{pw}', '{name}', '{pos}', '{dept}');";
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int CreateRoom(string roomName, int lastRoom, string userId)
        {
            try
            {
                string[] data = userId.Split(':');
                string sql = "";
                // List<int> roomList = new List<int>();
                List<List<int>> userList = new List<List<int>>();
                Dictionary<int, int> count = new Dictionary<int, int>();
                using (var conn = new MySqlConnection(_sb.ConnectionString))
                {
                    conn.Open();
                    foreach (string user in data)
                    {
                        sql = $"SELECT * FROM jointbl WHERE userIdx = " +
                            $"(SELECT userIdx FROM userstbl WHERE userID = '{user}');";
                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            var rdr = cmd.ExecuteReader();
                            var roomList = new List<int>();
                            while (rdr.Read())
                            {
                                roomList.Add(rdr.GetInt32(0));
                            }
                            userList.Add(roomList);
                            rdr.Close();
                        }
                    }
                }
                for (int i = 0; i < userList.Count-1; i++)
                {
                    for(int j = 0; j < userList[i].Count; j++)
                    {
                        foreach(int room in userList[i+1])
                        {
                            if (userList[i][j] == room)
                            {
                                if (count.ContainsKey(room))
                                {
                                    count[room]++;
                                    if (count[room] >= userList.Count)
                                        return room;
                                }
                                else
                                    count[room] = 1;
                            }
                        }
                    }
                }

                sql = $"INSERT INTO roomsTbl (roomName) VALUES ('{roomName}');\n";

                foreach (string user in data )
                {
                    sql += $"INSERT INTO joinTbl (roomIdx, userIdx) VALUES " +
                        $"('{lastRoom+1}', (SELECT userIdx FROM userstbl WHERE userID = '{user}'));\n";
                }
                
                using (var conn = new MySqlConnection(_sb.ConnectionString))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public string SelectRoomName(int index)
        {
            using (var conn = new MySqlConnection(_sb.ConnectionString))
            {
                var sql = $"SELECT roomName FROM roomstbl WHERE roomIdx = '{index}';";
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                    return cmd.ExecuteScalar().ToString();
            }
        }
    }
}
