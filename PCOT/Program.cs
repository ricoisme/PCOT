using GameUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process process = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(process.ProcessName);

            if (processes.Length > 1)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                    process.ProcessName + "はすでに起動しています。");

                return;
            }

            SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            typeof(Form).GetField("defaultIcon",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static).SetValue(
                    null, new System.Drawing.Icon("Pcot.ico"));

            Application.Run(new frmStart());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public extern static IntPtr SetProcessDPIAware();
    }
}
