using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            Bitmap bmp = new Bitmap("E://ocr//ocr.jpg");
            pictureBox1.Image = bmp;
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("E://ocr//ocr.jpg");
           

            int xmax = bmp.Width;
            int ymax = bmp.Height;
            Bitmap bwimage = processImage(bmp);
            int counter = 0;
            pictureBox1.Image = bwimage;
            int[] xaxis = new int[xmax];
            int[] yaxis = new int[ymax];
            int[] tempx = new int[xmax];
            int[] tempy = new int[ymax];
            int y, x, i = 1,j = 1;
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
                    yaxis[i++] = tempy[p]+1;
                   
                }
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
            i = 1;
             for (int p = 1; p < xmax; p++)
            {

                if ((tempx[p - 1] != tempx[p] - 1) || (tempx[p] + 1 != tempx[p + 1]))
                {
                    xaxis[i] = tempx[p] +1;
                    i++;
                }
            }
            for (i = 0; i < ymax; i++)
            {
                Console.WriteLine(yaxis[i]+ " " + xaxis[i]);
            }
            //  Console.WriteLine(xaxis[2]+" "+ yaxis[2] + " " + (xaxis[3] - xaxis[2] )+ " " + (yaxis[3] - yaxis[2]));


            int k;
            Bitmap[] croppedImg = new Bitmap[30];
            Bitmap temp;


            for (i = 1, j = 2, k = 0; k <5 ;k++)
            {
                
                Console.WriteLine(xaxis[i] + " " + yaxis[1] + " " + (xaxis[j] - xaxis[i]) + " " + (yaxis[2] - yaxis[1]));
              
                          
                
                Crop filter = new Crop(new Rectangle(xaxis[i], yaxis[1], (xaxis[j] - xaxis[i]), yaxis[2] - yaxis[1]));
                croppedImg[k]= filter.Apply(bwimage);
                i = i + 2;
                j = j + 2;
            }
            pictureBox2.Image = croppedImg[0];
            pictureBox3.Image = croppedImg[1];
            pictureBox4.Image = croppedImg[2];
            pictureBox5.Image = croppedImg[3];
            pictureBox6.Image = croppedImg[4];



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
