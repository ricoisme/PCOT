using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public class DataGridViewEx : DataGridView
    {
        public override Size GetPreferredSize(Size proposedSize)
        {
            return base.GetPreferredSize(new Size(this.Width, proposedSize.Height));
        }

        // ホイールマウス制御  
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // マウス ホイールを回転したときにスクロールする行数を取得  
            int scroll = SystemInformation.MouseWheelScrollLines;

            // MouseEventArgsクラスの移動量を１行に変更  
            MouseEventArgs ex = new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta / scroll);

            base.OnMouseWheel(ex);
        }
    }
}
