using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Data;
using System.Windows.Forms;

namespace messenger
{
    public class TcpClass
    {
        public Socket _socket;
        private int _port;
        private string _ip;
        private Converter _converter = new Converter();

        public TcpClass(string ip, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _port = port;
            _ip = ip;
        }

        public async Task Connect()
        {
            //await _socket.ConnectAsync(IPAddress.Parse(_ip), _port);
            try
            {
                await _socket.ConnectAsync(IPAddress.Parse(_ip), _port);
                Console.WriteLine("커넥트 후");
                // 연결 성공
            }
            catch (Exception ex)
            {
                Console.WriteLine($"연결 에러: {ex.Message}");
            }
        }

        public async Task<string> Login(string id)
        {
            int select = 1;
            string pw = "";
            // 서버에 필요한 데이터를 select, 1 로그인 2 회원가입 ...
            byte[] selecBuffer = _converter.ConvertToBytes(select);
            await SendDataAsync(_socket, selecBuffer);
            await SendData(_socket, id);
            (string dataType, byte[] dataBuffer) = await RecvData(_socket);
            if (dataType == "tbl")
            {
                var data = _converter.StringFromBytes(dataBuffer);
                var tbl = _converter.JsonToDataTable(data);
                if(tbl.Rows.Count > 0)
                    pw = tbl.Rows[0]["userPW"].ToString();
                return pw;
            }
            return "";
            // Tcp클래스는 소켓을 통한 서버와의 통신을 목적으로 함.
            // 다음부터는 데이터를 가공하는것은 다른쪽에서 하는 방향으로.
        }

        public async Task<(string, byte[])> LoadEmpList()
        {
            int select = 3;
            byte[] selecBuffer = _converter.ConvertToBytes(select);
            await SendDataAsync(_socket, selecBuffer);
            (string dataType, byte[] dataBuffer) = await RecvData(_socket);
            return (dataType, dataBuffer);
        }

        public async Task<(string, byte[])> LoadRoomListT(string id)
        {
            try
            {
                int select = 4;
                byte[] selecBuffer = _converter.ConvertToBytes(select);
                await SendDataAsync(_socket, selecBuffer);
                await SendData(_socket, id);
                (string dataType, byte[] dataBuffer) = await RecvData(_socket);
                return (dataType, dataBuffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadRoomListT Exception: {ex.Message}");
                throw;
            }
        }
        
        public async Task Register(string[] fields)
        {
            try
            {
                var data = string.Join(":", fields);
                int select = 2;
                byte[] selecBuffer = _converter.ConvertToBytes(select);
                await SendDataAsync(_socket, selecBuffer);
                await SendData(_socket, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register Exception: {ex.Message}");
                throw;
            }
        }

        public async Task SendData<T>(Socket clientSocket, T data)
        {
            try
            {
                // 데이터 byte 변환, 헤더 생성 (데이터 타입, 크기)
                string dataType = null;
                byte[] dataBuffer = null;

                if (data is string strData)
                {
                    dataBuffer = _converter.ConvertToBytes(strData);
                    dataType = "str";
                }

                else if (data is int intData)
                {
                    dataBuffer = _converter.ConvertToBytes(intData);
                    dataType = "int";
                }

                else if (data is DataTable tableData)
                {
                    var json = _converter.DataTableToJson(tableData);
                    dataBuffer = _converter.ConvertToBytes(json);
                    dataType = "tbl";
                }
                var dataSize = _converter.ConvertSize(dataBuffer.Length);
                var header = $"{dataType}:{dataSize}";
                var bytesBuffer = _converter.ConvertToBytes(header);
                // 헤더, 데이터 순서로 전송
                await SendDataAsync(clientSocket, bytesBuffer);
                await SendDataAsync(clientSocket, dataBuffer);
            }
            catch
            {

            }
        }

        public async Task SendDataAsync(Socket clientSocket, byte[] buffer)
        {
            int bytesSend = 0;
            int size = buffer.Length;
            while (bytesSend < size)
            {
                int remainingBytes = size - bytesSend;
                int sended = await clientSocket.SendAsync(new ArraySegment<byte>(buffer, bytesSend, remainingBytes), SocketFlags.None);
                if (sended == 0)
                {
                    // 연결이 닫힌 경우 또는 에러 처리
                    return;
                }
                bytesSend += sended;
            }
        }

        public async Task<(string type, byte[] data)> RecvData(Socket clientSocket)
        {
            byte[] bytesBuffer = new byte[8];   // 헤더 크기
            try
            {
                // 헤더 수신
                await RecvDataAsync(clientSocket, bytesBuffer);
                // 헤더 언패킹
                var header = _converter.StringFromBytes(bytesBuffer).Split(':');
                var dataType = header[0];
                var dataSize = int.Parse(header[1]);
                // 데이터 수신
                var dataBuffer = new byte[dataSize];
                await RecvDataAsync(clientSocket, dataBuffer);

                return (dataType, dataBuffer);
            }
            catch (SocketException)
            {
                clientSocket.Close();
                return (null, null);
            }
        }

        public async Task RecvDataAsync(Socket socket, byte[] buffer)
        {
            int bytesRead = 0;
            int size = buffer.Length;
            while (bytesRead < size)
            {
                int remainingBytes = size - bytesRead;
                int received = await socket.ReceiveAsync(new ArraySegment<byte>(buffer, bytesRead, remainingBytes), SocketFlags.None);
                if (received == 0)
                {
                    // 연결이 닫힌 경우 또는 에러 처리
                    return;
                }
                bytesRead += received;
            }
        }
    }
}
