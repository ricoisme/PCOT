using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public class ExtendedNumericUpDown : NumericUpDown
    {
        private int wheeldelta = 0;

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x20A)
            {
                if (SystemInformation.MouseWheelScrollLines > 0)
                {
                    int delta;
                    wheeldelta += ((int)m.WParam >> 16);
                    delta = (wheeldelta / 120) * 120 / SystemInformation.MouseWheelScrollLines;
                    wheeldelta = wheeldelta % 120;
                    m.WParam = (IntPtr)((delta << 16) | ((int)m.WParam & 0xffff));
                }
            }
            base.WndProc(ref m);
        }

        public class UpDownEventArgs : EventArgs
        {
            internal UpDownEventArgs(bool up)
            {
                this.IsUp = up;
            }
            public bool Handled { get; set; } = false;
            public bool IsUp { get; }
        }
        public delegate void UpDownEventHandler(
        object sender, UpDownEventArgs e);
        public event UpDownEventHandler UpDown;
        public override void DownButton()
        {
            UpDownEventArgs e = new UpDownEventArgs(false);
            UpDown?.Invoke(this, e);
            if (!e.Handled)
                base.DownButton();
        }
        public override void UpButton()
        {
            UpDownEventArgs e = new UpDownEventArgs(true);
            UpDown?.Invoke(this, e);
            if (!e.Handled)
                base.UpButton();
        }
    }
}
