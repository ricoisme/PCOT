using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PCOT
{
    public class UseWinApi
    {
        #region 構造体
        /// <summary>
        /// RECT構造体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int X;
            public int Y;
            public int W;
            public int H;
        }
        #endregion

        #region 定数
        /// <summary>無視するクラス名(フォルダ)</summary>
        private const string IGNORE_CLASS_NAME = "CabinetWClass";
        /// <summary>ShowWindowAsync関数のパラメータに渡す定義値</summary>
        private const int SW_RESTORE = 9;
        /// <summary>スクロールポジション</summary>
        private const int EM_SETSCROLLPOS = 0x04DE;
        /// <summary>DPI取得用代表X座標</summary>
        private const int LOGPIXELSX = 88;
        /// <summary>基本DPI</summary>
        private const float BASE_DPI = 96f;

        /// <summary>画面サイズ取得時のモード</summary>
        private enum DWMWINDOWATTRIBUTE : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
        };

        private enum DeviceCaps
        {
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,

            /// <summary>
            /// Horizontal width in pixels
            /// </summary>
            HORZRES = 8,

            /// <summary>
            /// Horizontal width of entire desktop in pixels
            /// </summary>
            DESKTOPHORZRES = 118
        }
        #endregion

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hwnd, ref RECT rect);

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32")]
        private extern static int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("gdi32.dll")]
        private extern static int GetDeviceCaps(IntPtr hdc, int index);

        /// <summary>
        /// 画面ハンドルを取得
        /// </summary>
        /// <param name="prcs">対象プロセス</param>
        /// <returns>取得した画面ハンドル</returns>
        /// <remarks>※プロセス名ではなく、タイトルを指定する</remarks>
        public static IntPtr GetWindowHandle(Process prcs)
        {
            var hndl = FindWindow(null, prcs.MainWindowTitle);
            if (hndl == IntPtr.Zero)
            {
                // 取得に失敗した場合はメイン画面ハンドルを取得
                hndl = prcs.MainWindowHandle;
                return hndl;
            }

            StringBuilder csb = new StringBuilder(256);
            GetClassName(hndl, csb, csb.Capacity);

            if (csb.ToString() == IGNORE_CLASS_NAME)
            {
                // フォルダの画面ハンドルが取得された場合はメイン画面ハンドルを取得
                hndl = prcs.MainWindowHandle;
                return hndl;
            }

            // 取得された画面サイズで比べて大きい方を優先
            Rectangle findRect = new Rectangle();
            GetWindowRectContainsFrame(hndl, ref findRect);

            Rectangle mainRect = new Rectangle();
            GetWindowRectContainsFrame(prcs.MainWindowHandle, ref mainRect);

            if(findRect.Width <= mainRect.Width && findRect.Height <= mainRect.Height)
            {
                hndl = prcs.MainWindowHandle;
            }

            return hndl;
        }

        /// <summary>
        /// 外部プロセスのウィンドウを起動する
        /// </summary>
        /// <param name="hWnd">該当プロセスのウィンドウハンドル</param>
        public static void WakeupWindow(IntPtr hWnd)
        {
            // メイン・ウィンドウが最小化されていれば元に戻す
            if (IsIconic(hWnd))
            {
                ShowWindowAsync(hWnd, SW_RESTORE);
            }

            // メイン・ウィンドウを最前面に表示する
            SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// ウィンドウサイズを取得(フレームなし)
        /// </summary>
        /// <param name="hWnd">対象プロセスのウィンドウハンドル</param>
        /// <param name="rect">取得される座標とサイズ</param>
        public static void GetWindowRectWithoutFrame(IntPtr hWnd, ref Rectangle rect)
        {
            RECT src = new RECT();
            DwmGetWindowAttribute(
                hWnd, (int)DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out src, Marshal.SizeOf(src));
            rect = new Rectangle(new Point(src.X, src.Y), new Size(src.W, src.H));
        }

        /// <summary>
        /// ウィンドウサイズを取得(フレームあり)
        /// </summary>
        /// <param name="hWnd">対象プロセスのウィンドウハンドル</param>
        /// <param name="rect">取得される座標とサイズ</param>
        public static void GetWindowRectContainsFrame(IntPtr hWnd, ref Rectangle rect)
        {
            RECT src = new RECT();
            GetWindowRect(hWnd, ref src);
            rect = new Rectangle(new Point(src.X, src.Y), new Size(src.W, src.H));
        }

        /// <summary>
        /// 検索テキストへのスクロール
        /// </summary>
        public static void ScrollToSearchText(IntPtr hWnd, int[] pos)
        {
            IntPtr iPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)) * pos.Length);
            Marshal.Copy(pos, 0, iPtr, pos.Length);
            SendMessage(hWnd, EM_SETSCROLLPOS, IntPtr.Zero, iPtr);
        }

        /// <summary>
        /// 現在アクティブなプロセス名を取得
        /// </summary>
        /// <returns>アクティブなプロセス名</returns>
        public static string GetActiveProcessName()
        {
            var hndl = GetForegroundWindow();
            var processId = 0;
            GetWindowThreadProcessId(hndl, out processId);
            return Process.GetProcessById(processId).ProcessName;
        }

        /// <summary>
        /// システムのDPI情報を取得
        /// </summary>
        /// <param name="currentDpi">DPI</param>
        /// <param name="scaleFactor">DPI倍率</param>
        public static void GetDpiInfo(ref int currentDpi, ref float scaleFactor)
        {
            using (Graphics screen = Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr hdc = screen.GetHdc();

                int virtualWidth = GetDeviceCaps(hdc, (int)DeviceCaps.HORZRES);
                int physicalWidth = GetDeviceCaps(hdc, (int)DeviceCaps.DESKTOPHORZRES);
                screen.ReleaseHdc(hdc);
                currentDpi = (int)(96f * physicalWidth / virtualWidth);
                scaleFactor = currentDpi / 96f;
            }
        }
    }
}
