using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;

namespace HHOnline.Framework.Web.HttpHandlers
{
    public class ValidCode : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string c = GetValidCode();
            context.Session["Vcode"] = c;
            Bitmap bmp = DrawValidCode(c, 30, 2);

            context.Response.ClearContent();
            context.Response.ContentType = "image/gif";
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            context.Response.BinaryWrite(ms.ToArray());
            bmp.Dispose();
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region -Twist Image-
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (Math.PI * 2 * (double)j) / dBaseAxisLen : (Math.PI * 2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);
                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
        #endregion

        #region -Draw Code-
        Bitmap DrawValidCode(string code, int fontSize, int padding)
        {
            Color[] colors = new Color[] { Color.Green, Color.Pink, Color.Blue, Color.Red, Color.Black, Color.DarkOrange };
            string[] fonts = { "Arial", "Courier New", "Times New Roman", "Tahoma", "Segoe UI", "Fixedsys" };

            Bitmap image = new Bitmap(code.Length * (fontSize + padding) + 4 * padding, fontSize * 2 + 2 * padding);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);

            Random rand = new Random(DateTime.Now.Millisecond);

            #region -Draw Pixel-
            Pen pen = new Pen(Color.LightGray, 0);
            int c = 4 * 10;
            for (int i = 0; i < c; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                g.DrawRectangle(pen, x, y, 1, 1);
            }
            #endregion

            #region -Draw Code-
            int left = 0, top = 0, top1 = 1, top2 = 1;
            int n1 = (image.Height - fontSize - 2 * padding);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;
            Font f;
            Brush b;
            int cindex, findex;
            for (int i = 0; i < code.Length; i++)
            {
                cindex = rand.Next(colors.Length - 1);
                findex = rand.Next(fonts.Length - 1);
                f = new System.Drawing.Font(fonts[findex], fontSize, System.Drawing.FontStyle.Bold);
                b = new System.Drawing.SolidBrush(colors[cindex]);
                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }
                left = i * (fontSize + padding);
                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }

            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();

            image = TwistImage(image, true, 8, 4);
            #endregion

            return image;
        }
        #endregion

        #region -GetCode-
        /// <summary>
        /// Produce English
        /// </summary>
        /// <returns></returns>
        string GetValidCode()
        {
            string codes = GlobalSettings.characters;
            string result = string.Empty;
            Random rand = new Random((int)(DateTime.Now.Ticks));
            for (int i = 0; i < 4; i++)
            {
                result += codes[rand.Next(codes.Length)];
            }
            return result;
        }
        #endregion
    }
}
