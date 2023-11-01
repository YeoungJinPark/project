using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class TcpSocket
    {
        private Socket _socket;
        private Converter _converter;
        private DBClass _db;
        private Dictionary<string, Socket> _clients = new Dictionary<string, Socket>();
        private Dictionary<string, List<string>> _roominfo = new Dictionary<string, List<string>>();

        public List<string> _joinList = new List<string>();

        public TcpSocket(int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Any, port));
            _converter = new Converter();
            _db = new DBClass();
        }

        public async Task StartServerAsync()
        {
            _socket.Listen(10); // 최대 대기 큐 크기 설정

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                Socket clientSocket;
                try
                {
                    clientSocket = await _socket.AcceptAsync(); // 클라이언트 연결 대기 및 비동기 수락
                    Console.WriteLine("수락 후");
                    Console.WriteLine(((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString());
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"연결 종료{((IPEndPoint)ex.InnerException?.TargetSite?.Invoke(null, null)).Address.ToString()}");
                    continue;
                }
                _ = HandleClientAsync(clientSocket); // 클라이언트와 비동기적으로 통신 처리
            }
        }

        private async Task HandleClientAsync(Socket clientSocket)
        {
            try
            {
                byte[] selecBuffer = new byte[4];
                int select = 0;
                while (true)
                {
                    Console.WriteLine("selec");
                    await RecvDataAsync(clientSocket, selecBuffer);
                    select = _converter.IntFromBytes(selecBuffer);
                    Console.WriteLine("selec 후 "+select);
                    switch (select)
                    {
                        case 1:     // 로그인
                            Console.WriteLine("Login");
                            await LoginData(clientSocket);
                            break;

                        case 2:     // 회원 가입
                            Console.WriteLine("Register");
                            await Register(clientSocket);
                            break;

                        case 3:     // 사원 목록
                            Console.WriteLine("EmpList");
                            await LoadEmpList(clientSocket);
                            break;

                        case 4:     // 방 목록
                            Console.WriteLine("RoomList");
                            await LoadRoomList(clientSocket);
                            break;

                        case 5:     // 채팅메세지 전달
                            Console.WriteLine("Routine");
                            await ChatRoutine(clientSocket);
                            break;

                        case 6:     // 서버에 접속중이며 들어간 채팅방 유저목록에 추가
                            Console.WriteLine("JoinRoom");
                            await JoinRoom(clientSocket);
                            break;

                        case 7:     // 채팅방 생성
                            Console.WriteLine("CreateRoom");
                            await CreateRoom(clientSocket);
                            break;

                        case 8:     // 수신 루프 종료
                            await Leave(clientSocket);
                            break;

                        case 9:     // 방 생성 전체 갱신
                            await ListCheck(clientSocket);
                            break;
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("클라이언트 연결 끊김");
                Console.WriteLine(ex.Message);
                RemoveClient(clientSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("에러 발생 " + ex.Message);
                Console.WriteLine(ex.InnerException);
            }
        }
        private async Task LoginData(Socket clientSocket)
        {
            string id = null;
            (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
            if (dataType == "str")
            {
                id = _converter.StringFromBytes(dataBuffer);
            }
            var pw = _db.SelectPW(id);
            await SendData(clientSocket, pw);
            Console.WriteLine("sended pw");
            await LoginCheck(clientSocket, id);
        }

        private async Task LoginCheck(Socket clientSocket, string id)
        {
            int success = 0;
            (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
            if (dataType == "int")
            {
                success = _converter.IntFromBytes(dataBuffer);
            }
            if(success == 1)
            {
                _clients[id] = clientSocket;
            }
        }

        private async Task LoadEmpList(Socket clientSocket)
        {
            var empList = _db.SelectEmpList();
            await SendData(clientSocket, empList);
            Console.WriteLine("sended empList");
        }

        private async Task LoadRoomList(Socket clientSocket)
        {
            try
            {
                string id = "";
                (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
                if (dataType == "str")
                    id = _converter.StringFromBytes(dataBuffer);
                var roomList = _db.SelectRoomList(id);
                await SendData(clientSocket, roomList);
                int roomCount = _db.SelectRoomCount();
                await SendData(clientSocket, roomCount);
                Console.WriteLine("sended roomList");
            }
            catch
            {
                Console.WriteLine("roomList Sending Error!");
            }
        }

        private async Task ChatRoutine(Socket clientSocket)
        {
            (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
            string data = "";
            if (dataType == "str")
            {
                data = _converter.StringFromBytes(dataBuffer);
            }
            var splitData = data.Split(':');
            var index = splitData[0];
            var id = splitData[1];
            var msg = splitData[2];
            WriteChat(index, id, msg);
            Console.WriteLine($"WriteChat {index}, {id}, {msg}");
            await SendChat(index, id, msg);
            Console.WriteLine("SendChat 후");
        }

        private void WriteChat(string index, string id, string msg)
        {
            string folderPath = "ChatRoom"; // 폴더 경로
            if (!Directory.Exists("ChatRoom"))
            {
                Directory.CreateDirectory(folderPath); // 폴더가 없으면 생성
            }
            using (StreamWriter writer = new StreamWriter($"{folderPath}\\{index}Room.txt", true))
            {
                writer.WriteLine($"{id}:{msg}");
            }
        }

        private async Task SendChat(string index, string id, string msg)
        {
            try
            {
                foreach (string client in _roominfo[index])
                {
                    Console.WriteLine($"client:{client} id:{id} msg:{msg}");
                    await SendData(_clients[client], $"{id}:{msg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendChat" + ex.Message);
            }
        }

        private async Task JoinRoom(Socket clientSocket)
        {
            (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
            string data = "";
            if (dataType == "str")
            {
                data = _converter.StringFromBytes(dataBuffer);
            }
            var splitData = data.Split(':');
            if(_roominfo.ContainsKey(splitData[0]))
            {
                _roominfo[splitData[0]].Add(splitData[1]);
            }
            else
            {
                _roominfo[splitData[0]] = new List<string>();
                _roominfo[splitData[0]].Add(splitData[1]);
            }
            await SendChatLog(clientSocket, splitData[0]);
            _joinList.Add(splitData[1]);
            // [0]: Index, [1]: ID  들어간 채팅방 번호와 접속중인 유저 ID
        }

        private async Task CreateRoom(Socket clientSocket)
        {
            int lastRoom = 0;
            string roomName = "";
            string userId = "";
            (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
            if (dataType == "int")  // 마지막 방 번호
                lastRoom = _converter.IntFromBytes(dataBuffer);
            (dataType, dataBuffer) = await RecvData(clientSocket);
            if (dataType == "str")  // 채팅방 이름
                roomName = _converter.StringFromBytes(dataBuffer);
            (dataType, dataBuffer) = await RecvData(clientSocket);
            if (dataType == "str")  // 유저ID
                userId = _converter.StringFromBytes(dataBuffer);
            int result = _db.CreateRoom(roomName, lastRoom, userId);
            Console.WriteLine(result);
            await SendData(clientSocket, result);
            if (result != 0)
            {
                var rName = _db.SelectRoomName(result);
                await SendData(clientSocket, rName);
            }
        }

        private async Task SendChatLog(Socket clientSocket, string index)
        {
            string data = "";
            string path = $"ChatRoom\\{index}Room.txt";
            if(File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    data = await reader.ReadToEndAsync();
                }
            }
            await SendData(clientSocket, data);
        }

        private async Task Register(Socket clientSocket)
        {
            (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
            string data = "";
            if (dataType == "str")
            {
                data = _converter.StringFromBytes(dataBuffer);
            }
            string[] split = data.Split(':');
            _db.InsertUser(split[0], split[1], split[3], split[4], split[5]);
        }

        private async Task ListCheck(Socket clientSocket)
        {
            (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
            int roomCount = 0;
            if (dataType == "int")
            {
                roomCount = _converter.IntFromBytes(dataBuffer);
            }
            var count = _db.SelectRoomCount();
            if(roomCount == count)
            {
                await SendData(clientSocket, 0);
                return;
            }
            else
            {
                await SendData(clientSocket, 1);
                string id = "";
                (dataType, dataBuffer) = await RecvData(clientSocket);
                if (dataType == "string")
                {
                    id = _converter.StringFromBytes(dataBuffer);
                }
                var roomList = _db.SelectRoomList(id, roomCount, count);
                await SendData(clientSocket, roomList);
                await SendData(clientSocket, count);
            }
        }

        private async Task SendData<T>(Socket clientSocket, T data)
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
            catch (Exception ex)
            {
                Console.WriteLine("SendData" + ex.Message);
            }
        }

        private async Task SendDataAsync(Socket clientSocket, byte[] buffer)
        {
            int bytesSend = 0;
            int size = buffer.Length;
            while (bytesSend< size)
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

        private async Task<(string type ,byte[] data)> RecvData(Socket clientSocket)
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
                RemoveClient(clientSocket);
                return (null, null);
            }
        }

        private async Task RecvDataAsync(Socket socket, byte[] buffer)
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

        private void RemoveClient(Socket clientSocket)
        {
            // _clients 딕셔너리에서 해당 클라이언트를 제거합니다.
            string id = _clients.FirstOrDefault(x => x.Value == clientSocket).Key;
            if (!string.IsNullOrEmpty(id))
            {
                _clients.Remove(id);
                Console.WriteLine($"클라이언트 {id}가 연결을 끊었습니다.");
            }
            foreach (var user in _roominfo)
            {
                if (user.Value.Contains(id))
                {
                    user.Value.Remove(id);
                }
            }
            clientSocket.Close();
        }

        private async Task Leave(Socket clientSocket)
        {
            string id = "";
            string idx = "";
            await SendData(clientSocket, 1);
            (string dataType, byte[] dataBuffer) = await RecvData(clientSocket);
            if (dataType == "str")
            {
                id = _converter.StringFromBytes(dataBuffer);
            }
            Console.WriteLine("id:"+id);
            (string type, byte[] dBuff) = await RecvData(clientSocket);
            if (type == "str")
            {
                idx = _converter.StringFromBytes(dBuff);
            }
            Console.WriteLine("idx:"+idx);
            _joinList.Remove(id);
            _roominfo[idx].Remove(id);
        }

        public void closeSocket()
        {
            _socket.Close();
        }
    }
}
