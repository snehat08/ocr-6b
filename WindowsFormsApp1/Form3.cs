using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("E://ocr//ocr.png");
            pictureBox1.Image = bmp;
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("E://ocr//ocr.png");
           

            int xmax = bmp.Width;
            int ymax = bmp.Height;
            Bitmap bwimage = processImage(bmp);
          
            pictureBox1.Image = bwimage;

            int[] xaxis = new int[xmax];
            int[] yaxis = new int[ymax];
            int[] tempx = new int[xmax];
            int[] tempy = new int[ymax];
            int y, x, i = 2 ;
            int iy = 1;
            int ix = 1;
            yaxis[0] = 0;
            for (y = 1; y < ymax; y++)
            {
                for (x = 1; x < xmax; x++)
                {
                    if(bwimage.GetPixel(x,y).Name == "ff000000")
                    {
                        tempy[iy++] = y;
                        break;
                    }
                }
            }
            for(int p = 1; p < ymax; p++)
            {
               
                if ((tempy[p-1] != tempy[p] - 1) || (tempy[p] + 1 != tempy[p + 1]))
                {
                    yaxis[i] = tempy[p];
                    i++;
                }
            }
            for(i = 2; i<ymax; i++)
            {
                //Console.WriteLine(yaxis[i]);
            }

            for (x = 1; x <xmax; x++)
            {
                for (y = 0;y < ymax; y++)
                {
                    if (bwimage.GetPixel(x, y).Name == "ff000000")
                    {
                        tempx[ix++] = x;
                        break;
                    }
                }
            }
            xaxis[1] = tempx[1] + 1;
            i = 2;
            for (int p = 1; p < xmax; p++)
            {

                if ((tempx[p - 1] != tempx[p] - 1) || (tempx[p] + 1 != tempx[p + 1]))
                {
                    xaxis[i] = tempx[p];
                    i++;
                }
            }
            for (i = 0; i < ymax; i++)
            {
                Console.WriteLine(yaxis[i]);
            }
            Console.WriteLine(xaxis[2]+" "+ yaxis[2] + " " + (xaxis[3] - xaxis[2] )+ " " + (yaxis[3] - yaxis[2]));
            Crop filter = new Crop(new Rectangle(xaxis[2],yaxis[2],xaxis[3]-xaxis[2],yaxis[3]-yaxis[2]));
            Bitmap croppedImg = filter.Apply(bwimage);
            pictureBox1.Image = croppedImg;



        }


        public Bitmap processImage(Bitmap img)
        {
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
             Bitmap grayimage = filter.Apply(img);

            Threshold th = new Threshold();
            th.ApplyInPlace(grayimage);

            return grayimage;
        } 
    }

}
