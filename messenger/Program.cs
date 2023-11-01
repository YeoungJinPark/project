using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messenger
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // TcpClass _tcp = new TcpClass("10.10.20.36", 9113);
            // TcpClass _tcp = new TcpClass("172.16.5.163", 9113);
            TcpClass _tcp = new TcpClass("127.0.0.1", 9113);
            await _tcp.Connect();
            Application.Run(new MdiParents(_tcp));
        }
    }
}
