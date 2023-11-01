using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class Program
    {
        static async Task Main()
        {
            TcpSocket server = new TcpSocket(9113);
            try
            {
                await server.StartServerAsync();
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10054) // 클라이언트 연결이 끊어진 경우의 예외 코드
                {
                    Console.WriteLine("Client connection closed.");
                }
                else
                {
                    Console.WriteLine($"SocketException: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"서버 에러 Exception: {ex.Message}");
                server.closeSocket();
            }
        }
    }
}
