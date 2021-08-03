using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortVisualize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
        }

        Graphics g;
        Pen p = new Pen(Color.Black, 5);
        Font font = new Font("Microsoft Sans Serif", 20f);
        int[] array;
        int i;
        public static int j = 1;
        bool sorted = false;

        public void DrawArray()
        {
            int length = array.Length;
            double widthBlock = this.Width;
            double heightBlock = this.Height;
            double sizeOfRectangles = (widthBlock - 0.1 * widthBlock) / length;

            for (int i = 0; i < array.Length; i++)
            {
                RectangleF rectangleF = new RectangleF((float)(i * sizeOfRectangles + 20), 100, (float)sizeOfRectangles - 20, 30);

                if (CheckSort(array) && i >= array.Length - j)
                {
                    g.FillRectangle(Brushes.LightGreen, rectangleF);
                    if (i != array.Length - 1 && array[i] < array[i + 1])
                        j += 0;
                    else
                        j++;

                }
                if (sorted)
                    g.FillRectangle(Brushes.LightGreen, rectangleF);

                g.DrawRectangle(p, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);

                g.DrawString(array[i].ToString(), font, Brushes.Black, rectangleF);
            }
        }

        public static bool CheckSort(int[] arr)
        {
            bool b = true;
            for (int i = arr.Length - j - 1; i >= 0; i--)
            {
                if (arr[arr.Length - j] < arr[i])
                    b = false;
            }
            return b;
        }

        public bool IsSorted(int[] arr)
        {
            bool s = true; ;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                    s = false;
            }
            return s;
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (array == null)
                {
                    var strings = txtBoxNums.Text.Split(new char[] { ',' });
                    foreach (var a in strings)
                    {
                        array = strings.Select(int.Parse).ToArray();
                    }
                    i = 0;
                    txtBoxNums.Visible = false;
                    DrawArray();
                    return;

                }
                while (i < array.Length - 1)
                {                  
                    if (array[i] > array[i + 1])
                    {
                        lblInformation.Text = "Swap " + array[i] + " and " + array[i + 1];
                        this.Invalidate();
                        Application.DoEvents();

                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;

                        DrawArray();
                        return;
                    }
                    else
                        i++;

                    if (IsSorted(array))
                    {
                        sorted = true;
                        DrawArray();
                        MessageBox.Show("Sorted!");
                        this.Invalidate();
                        Application.DoEvents();
                        j = 1;
                        sorted = false;
                        lblInformation.Text = "";
                        array = null;
                        txtBoxNums.Visible = true;
                        return;
                    }
                }

                if (i == array.Length - 1)
                {
                    MessageBox.Show("Pass is done!");
                    i = 0;
                    //return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter correct values!");
            }
        }

        private void txtBoxNums_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 44 && ch != 45)
            {
                e.Handled = true;
            }
        }
    }
}
