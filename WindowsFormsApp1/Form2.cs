using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Bitmap ProcessImage(Bitmap bmp)
        {
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayimage = filter.Apply(bmp);

            Threshold th = new Threshold();
            th.ApplyInPlace(grayimage);
            return grayimage;
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("C:\\Users\\Onam\\Desktop\\ocr\\work\\h.jpg");
            //pictureBox1.Image = image;
            Bitmap grayimage = ProcessImage(bmp);
            grayimage.SetResolution(90, 60);
            pictureBox1.Image = grayimage;
            int[,] zone = new int[90, 60];
            for (int j = 0; j < 90; j += 10)
                for (int i = 0; i < 60; i += 10)
                {
                    int x = 0;
                    Bitmap sector = grayimage.Clone(new System.Drawing.Rectangle(i, j, 10, 10), grayimage.PixelFormat);
                    for (int k = 0; k < 10; k++)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            if (sector.GetPixel(k, l).Name == "ff000000") x++;
                        }
                    }
                    zone[j, i] = x;

                    textBox1.Text += zone[j, i] + " ";


                }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


    }
}