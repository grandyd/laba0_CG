using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace laba0_CG
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }


    public struct HSVcolor
    {
        public ushort h;
        public byte s;
        public byte v;

        public HSVcolor(ushort _h, byte _s, byte _v)
        {
            this.h = _h;
            this.s = _s;
            this.v = _v;
        }
    }
    public class ColorConvert
    {
        public Bitmap rgbImage;
        public HSVcolor[,] hsvImage;

        byte clamp(byte value, byte min, byte max)
        {
            return Math.Min(Math.Max(value,min),max);
        }

        ushort clamp(ushort value, ushort min, ushort max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public HSVcolor RGBtoHSV(Color Source)
        {
            float eps = 0.00001f;
            ushort hue = 0;
            byte saturation = 0;
            byte value = 0;

            float _R = Source.R / 255f;
            float _G = Source.G / 255f;
            float _B = Source.B / 255f;
            float cMax = Math.Max(_R, Math.Max(_G, _B));
            float cMin = Math.Min(_R, Math.Min(_G, _B));
            float delta = cMax - cMin;

            value = (byte)(100 * cMax);

            if (cMax<eps)
            {
                saturation = 0;
            }
            else
            {
                saturation = (byte)(100 * delta / cMax);
            }

            if (delta<eps)
            {
                hue = 0;
            }
            else if (cMax==_R)
            {
                hue = (ushort)(60f * (((_G - _B) / delta) % 6f));
            }
            else if (cMax == _G)
            {
                hue = (ushort)(60f * (((_B - _R) / delta) + 2f));
            }
            else if (cMax == _B)
            {
                hue = (ushort)(60f * (((_R - _G) / delta) + 4f));
            }

            return new HSVcolor(clamp(hue, 0, 359), saturation, value);
        }

        public Color HSVtoRGB(HSVcolor source)
        {
            float _R = 0f, _G = 0f, _B = 0f;
            float C = source.s * source.v / 100f / 100f;
            float X = C * (1 - Math.Abs((source.h / 60f) % 2f - 1));
            float m = source.v / 100f - C;

            if (source.h<60)
            {
                _R = C;_G = X;_B = 0f;
            }
            else if (source.h<120)
            {
                _R = X; _G = C; _B = 0f;
            }
            else if (source.h < 180)
            {
                _R = 0f; _G = C; _B = X;
            }
            else if (source.h < 240)
            {
                _R = 0f; _G = X; _B = C;
            }
            else if (source.h < 300)
            {
                _R = X; _G = 0f; _B = C;
            }
            else if (source.h < 360)
            {
                _R = C; _G = 0f; _B = X;
            }

            byte R = (byte)(255f * (_R + m));
            byte G = (byte)(255f * (_G + m));
            byte B = (byte)(255f * (_B + m));

            return Color.FromArgb(clamp(R, 0, 255), clamp(G, 0, 255), clamp(B, 0, 255));

        }

        public void ConvertRGBimagetoHSV()
        {
            int width = rgbImage.Width;
            int height = rgbImage.Height;
            hsvImage = new HSVcolor[width, height];
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    hsvImage[i, j] = RGBtoHSV(rgbImage.GetPixel(i, j));
                }
            }
        }

        public void convertHSVimagetoRGB()
        {
            int width = hsvImage.GetLength(0);
            int height = hsvImage.GetLength(1);
            rgbImage = new Bitmap(width, height);
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    rgbImage.SetPixel(i, j, HSVtoRGB(hsvImage[i, j]));
                }
            }
        }
    }

}
