using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Diagnostics;

namespace GameUtil
{
    /// <summary>
    /// FPS管理クラス
    /// </summary>
    public class Fps
    {
        #region 定数
        /// <summary>平均サンプル数</summary>
        private const int N = 60;
        /// <summary>設定FPS</summary>
        private const int FPS = 60;
        /// <summary>単位：1秒</summary>
        public const int SEC = 60;
        /// <summary>単位：1分</summary>
        public const int MIN = 3600;
        /// <summary>単位：1時間</summary>
        public const int HOUR = 216000;
        #endregion

        #region 変数
        /// <summary>FPS</summary>
        public float fps;
        public static List<int> FpsCountList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        /// <summary>測定開始時刻</summary>
        private int startTime;
        /// <summary>カウンタ</summary>
        private int count;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Fps()
        {
            startTime = 0;
            count = 0;
            fps = 0;
        }

        #region メソッド
        /// <summary>
        /// FPS更新
        /// </summary>
        /// <returns></returns>
        public bool UpdateFps()
        {
            if (count == 0)
            { //1フレーム目なら時刻を記憶
                startTime = Environment.TickCount;
            }
            if (count == N)
            {
                //60フレーム目なら平均を計算する
                int t = Environment.TickCount;
                fps = 1000f / ((t - startTime) / (float)N);
                count = 0;
                startTime = t;
            }

            count++;

            for(int i = 0; i < FpsCountList.Count; i++)
            {
                FpsCountList[i] += 1;
            }

            return true;
        }

        /// <summary>
        /// FPS待機
        /// </summary>
        public void WaitFps()
        {
            int tookTime = Environment.TickCount - startTime;  //かかった時間
            int waitTime = count * 1000 / FPS - tookTime;  //待つべき時間
            if (waitTime > 0)
            {
                Thread.Sleep(waitTime);    //待機
            }
        }
        #endregion
    }
}
