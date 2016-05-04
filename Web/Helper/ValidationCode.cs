using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Web.Helper;

namespace Crosscutting.NetFramework
{
    /// <summary>
    /// 验证码生成类
    /// </summary>
    public class ValidationCode
    {
        public static string VerificationCode
        {
            get
            {
                var validationCode = HttpContext.Current.Session[ViewConstant.ValidationCode];
                return validationCode == null ? "" : validationCode.ToString();
            }
            private set 
            {
                HttpContext.Current.Session[ViewConstant.ValidationCode] = value;
            }
        }

        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <returns></returns>
        private static string GenerateCheckCode()
        {
            int number;
            string checkCode = String.Empty;
            char code;
            var rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                number = rand.Next(10000);
                if (number % 2 == 0)
                {
                    code = (char)('0' + Convert.ToChar(number % 10));
                }
                else
                {
                    code = (char)('A' + Convert.ToChar(number % 26));
                }
                checkCode += code.ToString();
            }

            VerificationCode = checkCode.ToUpper();
            return checkCode;
        }

        private static byte[] GenerateCheckCodeImage(string code)
        {
            if (code == "" || code.Trim() == String.Empty)
            {
                return null;
            }
            //创建一个图形对象
            var image = new Bitmap(Convert.ToInt32(Math.Ceiling(code.Length * 12.5)), 20);
            //根据图形对象创建一个画布
            Graphics g = Graphics.FromImage(image);

            try
            {
                //画布填充为白色
                g.Clear(Color.White);

                var rand = new Random();

                for (int i = 0; i < 5; i++)
                {
                    //第一个坐标点
                    int x1 = rand.Next(image.Width);
                    int y1 = rand.Next(image.Height);
                    //第二个坐标点
                    int x2 = rand.Next(image.Width);
                    int y2 = rand.Next(image.Height);
                    //根据两点坐标在画布上画线，形成噪点线背景
                    g.DrawLine(new Pen(Color.SeaGreen, 1), x1, y1, x2, y2);
                }
                //创建字体对象
                var font = new Font("Arial", 12, (FontStyle.Italic | FontStyle.Strikeout | FontStyle.Bold));
                //创建一个线性渐变画刷
                var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                //将验证码写入画布
                g.DrawString(code, font, brush, 2, 2);

                for (int i = 0; i < 120; i++)
                {
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);
                    //根据坐标点产生随机颜色的像素点
                    image.SetPixel(x, y, Color.FromArgb(rand.Next(100)));
                }
                //给画布绘制图形边框
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                // 其实区别就在如下，传统web form 解除掉下面的注释就好，不注释就会报一个HttpException的错误。
                // 提示说“自定义使用TextWriter时，OutputStream不可用”。
                //HttpContext.Current.Response.ClearContent();
                //HttpContext.Current.Response.ContentType = "image/Jpeg";
                //HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }

        }

        public static byte[] CreateCheckIamge()
        {
            return GenerateCheckCodeImage(GenerateCheckCode());
        }

        public static void RemoveValidationCode()
        {
            HttpContext.Current.Session.Remove(ViewConstant.ValidationCode);
        }
    }
}
