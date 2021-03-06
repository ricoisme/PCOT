using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PCOT
{
    public static class ProcessExtensions
    {
        [Flags]
        private enum ThreadAccess : int
        {
            TERMINATE = 0x0001,
            SUSPEND_RESUME = 0x0002,
            GET_CONTEXT = 0x0008,
            SET_CONTEXT = 0x0010,
            SET_INFORMATION = 0x0020,
            QUERY_INFORMATION = 0x0040,
            SET_THREAD_TOKEN = 0x0080,
            IMPERSONATE = 0x0100,
            DIRECT_IMPERSONATION = 0x0200
        }

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        private static extern uint SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        private static extern uint ResumeThread(IntPtr hThread);

        public static void Suspend(this ProcessThreadCollection threads)
        {
            foreach (ProcessThread x in threads)
            {
                var threadHandle = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)x.Id);

                if (threadHandle != IntPtr.Zero)
                {
                    try
                    {
                        SuspendThread(threadHandle);
                    }
                    finally
                    {
                        CloseHandle(threadHandle);
                    }
                }
            }
        }

        public static void Resume(this ProcessThreadCollection threads)
        {
            foreach (ProcessThread x in threads)
            {
                var threadHandle = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)x.Id);

                if (threadHandle != IntPtr.Zero)
                {
                    try
                    {
                        ResumeThread(threadHandle);
                    }
                    finally
                    {
                        CloseHandle(threadHandle);
                    }
                }
            }
        }
    }
}
