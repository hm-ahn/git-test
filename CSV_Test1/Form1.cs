using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSV_Test1
{
    public partial class Form1 : Form
    {
        public static double GetStandardDeviation(List<int> valueList, double average)
        {
            double variance = 0d;

            foreach (double value in valueList)
            {
                variance += Math.Pow(value - average, 2);
            }
            return Math.Sqrt(variance / valueList.Count);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "CSV (*.CSV) | *.CSV";


            string[] items = new string[10];
            bool isRun = false;
            double variance = 0d;

            List<int>[] lists = new List<int>[10];

            for (int i = 0; i < lists.Length; i++)
            {
                lists[i] = new List<int>();
            }

            if (open.ShowDialog() == DialogResult.OK)
            {
                string filename = open.FileName;

                if (File.Exists(filename))
                {
                    //do work
                    foreach (String line in File.ReadAllLines(filename))
                    {
                        if (isRun == false)
                        {
                            isRun = true;
                        }
                        else if (line == ", , , , , , , , , ")
                        {

                        }
                        else
                        {
                            int i = 0;
                            items = line.Split(',');
                            foreach (string part in items)
                            {
                                lists[i].Add(Convert.ToInt32(part));
                                i++;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }

            for (int i = 0; i < lists.Length; i++)
            {
                lists[i] = lists[i].OrderBy(x => x).ToList();
            }

            StringBuilder builder = new StringBuilder();

            /// <summary>
            /// 최소값
            /// </summary>
            builder.Append("Minimum Values : ");
            foreach (List<int> temp in lists)
            {
                builder.Append(temp.Min() + " ");
            }
            Console.WriteLine(builder.ToString());
            builder.Clear();

            /// <summary>
            /// 최대값
            /// </summary>
            builder.Append("Maximum Values : ");
            foreach (List<int> temp in lists)
            {
                builder.Append(temp.Max() + " ");
            }
            Console.WriteLine(builder.ToString());
            builder.Clear();

            /// <summary>
            /// 평균값
            /// </summary>
            builder.Append("Average Values : ");
            foreach (List<int> temp in lists)
            {
                builder.Append(temp.Average() + " ");
            }
            Console.WriteLine(builder.ToString());
            builder.Clear();

            /// <summary>
            /// 표준편차
            /// </summary>
            builder.Append("Standard Deviation : ");
            for (int i = 0; i < lists.Length; i++)
            {
                builder.Append(GetStandardDeviation(lists[i], variance) + " ");
            }
            Console.WriteLine(builder.ToString());
            builder.Clear();

            /// <summary>
            /// 중간값
            /// </summary>
            int count = 0;
            builder.Append("Median Values : ");
            foreach (List<int> temp in lists)
            {
                builder.Append(temp[lists[0].Count / 2] + " ");
                count++;
            }
            Console.WriteLine(builder.ToString());
            builder.Clear();

            sw.Stop();
            Console.WriteLine("Elapsed Time : " + sw.Elapsed.ToString());
        }
    }
}