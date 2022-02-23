using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba0_CG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        ColorConvert cc = new ColorConvert();
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string imagepath;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "image files|*.png *.jpg";
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                imagepath = dialog.FileName;
                cc.rgbImage = new Bitmap(imagepath);
                this.pictureBox1.Image = cc.rgbImage;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            cc.rgbImage = new Bitmap("C:\\Users\\user\\Desktop\\pepechill.png");
            this.pictureBox1.Image = cc.rgbImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            cc.ConvertRGBimagetoHSV();
            int width = cc.rgbImage.Width;
            int height = cc.rgbImage.Height;

            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    int tmpH = cc.hsvImage[i, j].h + trackBar1.Value;
                    if (tmpH<0)
                    {
                        cc.hsvImage[i, j].h = (ushort)(tmpH + 360);
                    }
                    else if (tmpH>=360)
                    {
                        cc.hsvImage[i, j].h = (ushort)(tmpH - 360);
                    }
                    else
                    {
                        cc.hsvImage[i, j].h = (ushort)(tmpH);
                    }
                }
            }

            trackBar1.Value = 0;
            cc.convertHSVimagetoRGB();
            pictureBox1.Image = cc.rgbImage;
            this.Cursor = Cursors.Default;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cc.ConvertRGBimagetoHSV();

            //int width = cc.rgbImage.Width;
            //int height = cc.rgbImage.Height;

            //for (int i = 0; i < height; i++)
            //{
            //    for (int j = 0; j < width; j++)
            //    {
            //        //cc.hsvImage[i, j].v += (byte)100;
            //    }
            //}

            cc.convertHSVimagetoRGB();
            pictureBox1.Image = cc.rgbImage;
        }
    }
}
