using GameUtil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public class ImageViewer : PictureBox
    {
        #region 定数
        /// <summary>
        /// サイズ超過区分
        /// </summary>
        private enum SizeOverType
        {
            Both,
            Width,
            Height,
            None
        }

        /// <summary>ズーム単位</summary>
        private const float ZOOM_UNIT = 1.1f;
        /// <summary>ズーム最大</summary>
        private const float ZOOM_MAX = 15f;
        #endregion

        #region 変数
        public string ImageFilePath = string.Empty;
        /// <summary>アフィン変換行列</summary>
        public Matrix MatAffine = new Matrix();
        /// <summary>拡大/縮小補間モード</summary>
        public InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic;
        /// <summary>描画用Graphicsオブジェクト</summary>
        private Graphics graphics = null;
        /// <summary>描画用ビットマップ</summary>
        private Bitmap srcBmp = null;
        /// <summary>補間モード</summary>
        /// <summary>マウスダウンフラグ</summary>
        private bool isMouseDown = false;
        /// <summary>マウスをクリックした位置の保持用</summary>
        private PointF oldPoint;
        /// <summary>最小倍率</summary>
        private float minScale = 1f;
        /// <summary>倍率変更フラグ</summary>
        private bool isScaleChanged = false;
        #endregion

        #region オーバーライド
        /// <summary>
        /// マウスホイール
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 0)
            {
                // 拡大
                if (MatAffine.Elements[0] < ZOOM_MAX)  // X方向の倍率を代表してチェック
                {
                    // ポインタの位置周りに拡大
                    ScaleAt(ref MatAffine, ZOOM_UNIT, e.Location);
                    if (MatAffine.Elements[0] > 1)
                    {
                        interpolationMode = InterpolationMode.NearestNeighbor;
                        graphics.InterpolationMode = interpolationMode;
                    }
                    isScaleChanged = true;
                }
            }
            else
            {
                // 縮小
                if (MatAffine.Elements[0] > minScale)  // X方向の倍率を代表してチェック
                {
                    // ポインタの位置周りに縮小
                    ScaleAt(ref MatAffine, 1.0f / ZOOM_UNIT, e.Location);
                    if (MatAffine.Elements[0] < 1)
                    {
                        interpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.InterpolationMode = interpolationMode;
                    }
                }
                else
                {
                    isScaleChanged = false;
                }
            }

            // 画像の描画
            DrawImage();
        }

        /// <summary>
        /// マウスダブルクリック
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            ZoomFit(ref MatAffine);
            DrawImage();
        }

        /// <summary>
        /// マウスダウン
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                // マウスをクリックした位置の記録
                oldPoint.X = e.X;
                oldPoint.Y = e.Y;
                // マウスダウンフラグ
                isMouseDown = true;
            }
        }

        /// <summary>
        /// マウス移動
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // マウスをクリックしながら移動中のとき
            if (isMouseDown == true)
            {
                using (var srcBmp = new Bitmap(ImageFilePath))
                {
                    float x = e.X - oldPoint.X;
                    float y = e.Y - oldPoint.Y;
                    float chkW = Width - srcBmp.Width * MatAffine.Elements[0];
                    float chkH = Height - srcBmp.Height * MatAffine.Elements[0];

                    // 移動制限
                    if (x + MatAffine.OffsetX >= 0 || x + MatAffine.OffsetX <= chkW)
                    {
                        x = 0;
                    }

                    if (y + MatAffine.OffsetY >= 0 || y + MatAffine.OffsetY <= chkH)
                    {
                        y = 0;
                    }

                    // 画像の移動
                    MatAffine.Translate(x, y, MatrixOrder.Append);

                    // 画像の描画
                    DrawImage();

                    // ポインタ位置の保持
                    oldPoint.X = e.X;
                    oldPoint.Y = e.Y;
                }
            }
        }

        /// <summary>
        /// マウスアップ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // マウスダウンフラグ
            isMouseDown = false;
        }

        /// <summary>
        /// サイズ変更
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if ((Width == 0) || (Height == 0)) return;

            // PictureBoxと同じ大きさのBitmapクラスを作成する。
            Bitmap bmpPicBox = new Bitmap(Width, Height);
            // 空のBitmapをPictureBoxのImageに指定する。
            Image = bmpPicBox;
            // Graphicsオブジェクトの作成(FromImageを使う)
            graphics = Graphics.FromImage(Image);
            graphics.InterpolationMode = interpolationMode;

            if (string.IsNullOrEmpty(ImageFilePath))
            {
                return;
            }

            if (!isScaleChanged)
            {
                var sizeOver = CheckSizeOver(1);
                if (sizeOver == SizeOverType.None)
                {
                    MatAffine.Reset();
                    MoveCenter(ref MatAffine, 1);
                    minScale = 1;
                }
                else
                {
                    ZoomFit(ref MatAffine);
                    minScale = MatAffine.Elements[0];
                }
            }
            else
            {
                if (CheckSizeOver(MatAffine.Elements[0]) == SizeOverType.None)
                {
                    if (CheckSizeOver(1) == SizeOverType.None)
                    {
                        MoveCenter(ref MatAffine, MatAffine.Elements[0]);
                        minScale = 1f;
                    }
                    else
                    {
                        ZoomFit(ref MatAffine);
                    }
                }
                else
                {
                    MoveCorrectLocation(ref MatAffine, MatAffine.Elements[0]);
                    if (CheckSizeOver(1) == SizeOverType.None)
                    {
                        minScale = 1f;
                    }
                }
            }

            // 画像の描画
            DrawImage();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 画像ファイルのパスを設定
        /// </summary>
        /// <param name="imgFilePath"></param>
        public void SetImageFilePath(string imgFilePath)
        {
            ImageFilePath = imgFilePath;
        }

        /// <summary>
        /// 初回表示
        /// </summary>
        public void InitDrawImage(bool isUpdate)
        {

            if (!isUpdate)
            {
                MatAffine = new Matrix();

                if (CheckSizeOver(1) == SizeOverType.None)
                {
                    MoveCenter(ref MatAffine, 1);
                }
                else
                {
                    ZoomFit(ref MatAffine);
                }

                minScale = MatAffine.Elements[0];
            }

            DrawImage();
        }

        /// <summary>
        /// 画像の描画
        /// </summary>
        public void DrawImage()
        {
            graphics.Clear(BackColor);

            srcBmp = new Bitmap(Util.GetImage(ImageFilePath));

            RectangleF rect = new RectangleF(-0.5f, -0.5f, srcBmp.Width, srcBmp.Height);

            PointF[] points = new PointF[]
            {
                new PointF(rect.Left,rect.Top),
                new PointF(rect.Right, rect.Top),
                new PointF(rect.Left, rect.Bottom)
            };

            // 描画先の座標をアフィン変換で求める（左上、右上、左下の順）
            PointF[] destPoints = (PointF[])points.Clone();
            // 描画先の座標をアフィン変換で求める（変換後の座標は上書きされる）
            MatAffine.TransformPoints(destPoints);

            // 描画
            graphics.DrawImage(
                srcBmp,
                destPoints,
                rect,
                GraphicsUnit.Pixel
                );

            // 再描画
            Refresh();
        }

        /// <summary>
        /// 中央表示
        /// </summary>
        /// <param name="mat">アフィン変換行列</param>
        private void MoveCenter(ref Matrix mat, float scale)
        {
            using (Bitmap srcBmp = new Bitmap(ImageFilePath))
            {
                int srcWidth = (int)(srcBmp.Width * scale);
                int srcHeight = (int)(srcBmp.Height * scale);
                int picWidth = Width;
                int picHeight = Height;
                float x = 0;
                float y = 0;

                float scaleBak = mat.Elements[0];
                mat = new Matrix();
                mat.Scale(scaleBak, scaleBak);

                if (picWidth > srcWidth && picHeight > srcHeight)
                {
                    x = CommonUtil.GetCenterX(0, picWidth, srcWidth);
                    y = CommonUtil.GetCenterY(0, picHeight, srcHeight);
                }
                else if (picWidth > srcWidth)
                {
                    x = CommonUtil.GetCenterX(0, picWidth, srcWidth);
                    y = -CommonUtil.GetCenterY(0, srcHeight, picHeight);
                }
                else if (picHeight > srcHeight)
                {
                    x = -CommonUtil.GetCenterX(0, srcWidth, picWidth);
                    y = CommonUtil.GetCenterY(0, picHeight, srcHeight);
                }
                else
                {
                    x = -CommonUtil.GetCenterX(0, srcWidth, picWidth);
                    y = -CommonUtil.GetCenterY(0, srcHeight, picHeight);
                }

                mat.Translate(x, y, MatrixOrder.Append);
            }
        }

        /// <summary>
        /// 画像をピクチャボックスのサイズに合わせて全体に表示するアフィン変換行列を求める
        /// </summary>
        /// <param name="mat">アフィン変換行列</param>
        private void ZoomFit(ref Matrix mat)
        {
            // アフィン変換行列の初期化（単位行列へ）
            mat.Reset();

            using (Bitmap srcBmp = new Bitmap(ImageFilePath))
            {
                int srcW = srcBmp.Width;
                int srcH = srcBmp.Height;
                int dstW = Width;
                int dstH = Height;

                float scale;

                // 縦に合わせるか？横に合わせるか？
                if (srcH * dstW > dstH * srcW)
                {
                    // ピクチャボックスの縦方法に画像表示を合わせる場合
                    scale = dstH / (float)srcH;
                    mat.Scale(scale, scale, MatrixOrder.Append);
                    // 中央へ平行移動
                    mat.Translate((dstW - srcW * scale) / 2f, 0f, MatrixOrder.Append);
                }
                else
                {
                    // ピクチャボックスの横方法に画像表示を合わせる場合
                    scale = dstW / (float)srcW;
                    mat.Scale(scale, scale, MatrixOrder.Append);
                    // 中央へ平行移動
                    mat.Translate(0f, (dstH - srcH * scale) / 2f, MatrixOrder.Append);
                }

                minScale = mat.Elements[0];
                isScaleChanged = false;
            }
        }

        /// <summary>
        /// 画像の表示位置を補正
        /// </summary>
        /// <param name="mat">アフィン変換行列</param>
        /// <param name="scale">倍率</param>
        private void MoveCorrectLocation(ref Matrix mat, float scale)
        {
            using (var srcBmp = new Bitmap(ImageFilePath))
            {
                float offsetX = mat.OffsetX;
                float offsetY = mat.OffsetY;
                float chkW = Width - srcBmp.Width * scale;
                float chkH = Height - srcBmp.Height * scale;

                if (CheckSizeOver(scale) == SizeOverType.Both)
                {
                    if (offsetX >= 0)
                    {
                        mat.Translate(-offsetX, 0f, MatrixOrder.Append);
                    }

                    if (offsetY >= 0)
                    {
                        mat.Translate(0f, -offsetY, MatrixOrder.Append);
                    }

                    if (offsetX <= chkW)
                    {
                        mat.Translate(chkW - offsetX, 0f, MatrixOrder.Append);
                    }

                    if (offsetY <= chkH)
                    {
                        mat.Translate(0f, chkH - offsetY, MatrixOrder.Append);
                    }
                }
                else
                {
                    MoveCenter(ref mat, scale);
                }
            }
        }

        /// <summary>
        /// 指定した点（point）周りの拡大縮小
        /// </summary>
        /// <param name="scale">倍率</param>
        /// <param name="point">基準点の座標</param>
        private void ScaleAt(ref Matrix mat, float scale, PointF point)
        {
            SizeOverType sizeOver;
            if (scale > 1)
            {
                sizeOver = CheckSizeOver(MatAffine.Elements[0]);
            }
            else
            {
                sizeOver = CheckSizeOver(MatAffine.Elements[0] * scale);
            }

            switch (sizeOver)
            {
                case SizeOverType.Both:
                    // 原点へ移動
                    mat.Translate(-point.X, -point.Y, MatrixOrder.Append);

                    // 拡大縮小
                    mat.Scale(scale, scale, MatrixOrder.Append);

                    // 元へ戻す
                    mat.Translate(point.X, point.Y, MatrixOrder.Append);
                    break;
                default:
                    if (sizeOver != SizeOverType.None)
                    {
                        // 初期値へ移動
                        mat.Translate(-mat.OffsetX, -mat.OffsetY, MatrixOrder.Append);

                        // 拡大縮小
                        mat.Scale(scale, scale, MatrixOrder.Append);

                        // 中心へ移動
                        MoveCenter(ref MatAffine, MatAffine.Elements[0]);
                    }
                    else
                    {
                        if (scale < 1 && CheckSizeOver(1) != SizeOverType.None)
                        {
                            ZoomFit(ref MatAffine);
                        }
                        else
                        {
                            // 初期値へ移動
                            mat.Translate(-mat.OffsetX, -mat.OffsetY, MatrixOrder.Append);

                            // 拡大縮小
                            mat.Scale(scale, scale, MatrixOrder.Append);

                            // 中心へ移動
                            MoveCenter(ref MatAffine, MatAffine.Elements[0]);
                        }
                    }
                    break;
            }

            MoveCorrectLocation(ref mat, mat.Elements[0]);
        }

        /// <summary>
        /// 描画領域よりもサイズが大きいかどうかをチェック
        /// </summary>
        /// <param name="scale">倍率</param>
        /// <returns>サイズ超過区分</returns>
        private SizeOverType CheckSizeOver(float scale)
        {
            using (Bitmap srcBmp = new Bitmap(ImageFilePath))
            {
                float currentW = srcBmp.Width * scale;
                float currentH = srcBmp.Height * scale;

                if (currentW > Width && currentH > Height)
                {
                    // 両方超過
                    return SizeOverType.Both;
                }
                else if (currentW > Width)
                {
                    // 幅だけ超過
                    return SizeOverType.Width;
                }
                else if (currentH > Height)
                {
                    // 高さだけ超過
                    return SizeOverType.Height;
                }
                else
                {
                    // 超過なし
                    return SizeOverType.None;
                }
            }
        }
        #endregion
    }
}
