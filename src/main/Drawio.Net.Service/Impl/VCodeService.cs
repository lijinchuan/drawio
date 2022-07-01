using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Service.Impl
{
    public class VCodeService : IVCodeService
    {
        /// <summary>
        ///  生成的验证码
        /// </summary>
        private static ConcurrentDictionary<string, string> UserVCodes = new ConcurrentDictionary<string, string>();

        private static int ImgWidth = 60;
        private static int ImgHeight = 30;
        private const string allChar = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,T,U,W,X,Y,Z";
        private static readonly string[] allCharArray = allChar.Split(',');
        //Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        private static readonly Color[] c = { Color.DarkBlue };
        //定义字体
        //string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
        private static readonly string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };

        private string CreateRandomCode(int codeCount)
        {
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(30);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        public byte[] GenVode(string userName)
        {
            using (Bitmap image = new Bitmap(ImgWidth, ImgHeight))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.Clear(Color.White);
                    //定义颜色

                    string code = CreateRandomCode(4);

                    Random random = new Random();
                    //画图片的背景噪音线

                    //随机输出噪点
                    for (int i = 0; i < 30; i++)
                    {
                        int x = random.Next(image.Width);
                        int y = random.Next(image.Height);
                        g.DrawRectangle(new Pen(Color.Blue, 0), x, y, 1, 1);
                    }

                    int lastii = 12;
                    //输出不同字体和颜色的验证码字符
                    for (int i = 0; i < code.Length; i++)
                    {
                        int cindex = random.Next(c.Length);
                        int findex = random.Next(font.Length);

                        Font f = new System.Drawing.Font(font[findex], 15, System.Drawing.FontStyle.Bold);
                        Brush b = new System.Drawing.SolidBrush(c[cindex]);
                        int ii = random.Next(0, ImgHeight / 4);
                        int magin = 12;
                        //magin -= Math.Abs(lastii - ii);
                        g.DrawString(code.Substring(i, 1), f, b, 3 + (i * magin), ii);
                        lastii = ii;

                    }

                    //画图片的边框线

                    g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        UserVCodes[userName] = code;
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
