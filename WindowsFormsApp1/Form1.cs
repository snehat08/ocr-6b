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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("E:\\ocr\\h.jpg");
            bmp.SetResolution(90, 60);
            pictureBox1.Image = bmp;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("E:\\ocr\\h.jpg");
            bmp.SetResolution(90, 60);
            int i = 0,x=0,p=0;
            int[] xaxis = new int[90];
            for(x=1;x<60;x+=2)
            {
                int f = 0;
                for(int y =1;y<90;y++)
                {
                    if (bmp.GetPixel(x, y).Equals(Color.Black)) f++;

                }
                if (f == 0)
                {
                    xaxis[p++] = x;
                }
            }
            
        }
    }
}
