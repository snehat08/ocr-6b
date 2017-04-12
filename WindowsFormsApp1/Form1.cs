using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("E:\\ocr\\ocr.jpg");
            bmp.SetResolution(90, 60);
            pictureBox1.Image = bmp;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("E:\\ocr\\ocr.jpg");
            bmp.SetResolution(90, 60);
            int xval = bmp.Width;
            int yval = bmp.Height;

            int i = 0, x = 0, p = 0, y= 0,m = 0;
            int[] xaxis = new int[xval];
            int[] yaxis = new int[yval];
//            yaxis[1] = 1;
            int f=0, f2=0;

            Bitmap grayimage = ProcessImage(bmp);          
            pictureBox1.Image = grayimage;

           

            for (y = 1 ; y < yval ; y++)
            {
                f = 0;
                for (x = 1; x < xval; x++)
                {
                    if (grayimage.GetPixel(x, y).Name == "ff000000") f2 = 1;
                }
                if (f2 == 0)
                {
                    yaxis[m++] = y;
                }
            }
            m = 0;
            for(x=1; x<xval; x++)
            {
                for (m = 1; m < yval; m++)
                {
                    if ((yaxis[m] + 1)!= yaxis[m + 1]) { break; }
                   
                }
                for(y=yaxis[m];y<yaxis[m+1];y++)
                {
                    if (grayimage.GetPixel(x, y).Name == "ff000000") f = 1;
                }
                m++;
                if (f == 0)
                {
                    xaxis[p++] = x;
                }

            }
            for(p=0;p<xval;p++)
            {
                textBox1.Text += xaxis[p] + " ";
            }

           

          
        }
        public Bitmap ProcessImage(Bitmap bmp)
        {
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayimage = filter.Apply(bmp);

            Threshold th = new Threshold();
            th.ApplyInPlace(grayimage);
            return grayimage;
        }

    }
   
}
