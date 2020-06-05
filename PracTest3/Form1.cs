using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace PracTest3
{
    public partial class Form1 : Form
    {
        //Name: Cameron Nepe
        //ID  : 1262199

        public Form1()
        {
            InitializeComponent();
        }

        //The width of a bar in the bar graph
        const int BAR_WIDTH = 25;

        //the gap between bars in the bar graph
        const int GAP = 5;

        //the factor to scale the the graph by to make it fit nicely in the the picturebox
        const int SCALE_FACTOR = 15;

        /// <summary>
        /// Draws a vertical bar that is part of a bar graph.
        /// i.e. It fills a rectangle at position (x,y) with the specified colour.
        /// Then draws a black outline for the rectangle.
        /// Uses the BAR_WIDTH constant for the size of the rectangle.
        /// </summary>
        /// <param name="paper">The Graphics object to draw on.</param>
        /// <param name="x">The x position of the top left corner of the rectangle.</param>
        /// <param name="y">The y position of the top left corner of the rectangle.</param>
        /// <param name="colour">The colour to fill the background of the rectangle with.</param>
        private void DrawABar(Graphics paper, int x, int y, int length, Color colour)
        {
            //create a brush of specified colour and fill background with this colour 
            SolidBrush brush = new SolidBrush(colour);
            paper.FillRectangle(brush, x, y,BAR_WIDTH, length);

            //draw outline in black
            paper.DrawRectangle(Pens.Black, x, y, BAR_WIDTH, length);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxOutput.Refresh();
            pictureBoxTop.Refresh();
        }

        private void oepnFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int num1, num2, num3, num4, num5, num6, num7, num8, num9;
            List<string> date = new List<string>();
            List<int> calories = new List<int>();
            List<int> steps = new List<int>();
            List<decimal> dist = new List<decimal>();
            List<int> minInactive = new List<int>();
            List<int> minLightlyActive = new List<int>();
            List<int> minFairlyActive = new List<int>();
            List<int> minVeryActive = new List<int>();
            List<int> activityCalories = new List<int>();

            String objectType;
            const string FILTER = "CSV Files|*.csv|ALL Files|*.*";
            StreamReader reader;
            string line;
            string[] csvArray;
            //SET the filter for the dialog control
            openFileDialog1.Filter = FILTER;
            //CHECK to see if the user has selected a file to open
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //OPEN the selected file
                reader = File.OpenText(openFileDialog1.FileName);
                //REPEAT while it is not the end of the file
                int count = 0;
                while (!reader.EndOfStream)
                {
                    try
                    {
                        //READ a whole csv line from the file
                        line = reader.ReadLine();
                        //SPLIT the values from the line using an array
                        csvArray = line.Split(',');

                        //EXTRACT values into separate values
                        objectType = csvArray[0];
                        //Console.WriteLine(objectType);
                        date.Add(csvArray[1]);
                        calories.Add(int.Parse(csvArray[2]));
                        //steps.Add(int.Parse(csvArray[3]));
                        //dist.Add(decimal.Parse(csvArray[4]));
                        //minInactive.Add(int.Parse(csvArray[5]));
                        //minLightlyActive.Add(int.Parse(csvArray[6]));
                        //minFairlyActive.Add(int.Parse(csvArray[7]));
                        //minVeryActive.Add(int.Parse(csvArray[8]));
                        //activityCalories.Add(int.Parse(csvArray[9]));

                        //DISPLAY the values into the listbox neatly padded out
                        //listBoxOutput.Items.Add(objectType.PadRight(10) + date[count].ToString().PadRight(5) + calories[count].ToString().PadRight(5) 
                        //    + steps[count].ToString().PadRight(5) + dist[count].ToString().PadRight(5) + minInactive[count].ToString().PadRight(5)
                        //    + minLightlyActive[count].ToString().PadRight(5) + minVeryActive[count].ToString().PadRight(5) + activityCalories[count].ToString().PadRight(5));

                        listBoxOutput.Items.Add(objectType.PadRight(10));

                        //DRAW the shape depending on the type of object
                        //if (objectType == "Rectangle")
                        //{
                        //    paper.DrawLine(myPen,
                        //    num1, num2, num3, num4)
                        //}
                        //else if (objectType == "Line")
                        //{
                        //    paper.DrawLine(myPen,
                        //    num1, num2, num3, num4)
                        //}
                        count++;
                    }
                    catch //(Exception ex)
                    {
                        Console.WriteLine("Error");
                    }
                    

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Error: ");
            }
        }
    }
}
