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
            paper.FillRectangle(brush, x, y, BAR_WIDTH, length);

            //draw outline in black
            paper.DrawRectangle(Pens.Black, x, y, BAR_WIDTH, length);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxOutput.Items.Clear();
            pictureBoxTop.Refresh();
        }

        private void oepnFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string FILTER = "CSV Files|*.csv|ALL Files|*.*";
            StreamReader reader;
            Graphics paper = pictureBoxTop.CreateGraphics();
            Pen pen1 = new Pen(Color.Black, 2);
            string line = "", objectType = "";
            string[] csvArray;
            int totalSteps = 0;
            double stepsPerMetre = 0;
            int x = 0;
            int y = 0;

            List<int> calories = new List<int>();
            List<int> steps = new List<int>();
            List<double> dist = new List<double>();
            List<int> minInactive = new List<int>();
            List<int> minLightlyActive = new List<int>();
            List<int> minFairlyActive = new List<int>();
            List<int> minVeryActive = new List<int>();
            List<int> activityCalories = new List<int>();

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

                        //CHECK for bad data in the file
                        if (csvArray.Length == 9)
                        {
                            //EXTRACT values into separate variables
                            objectType = csvArray[0];
                            calories.Add(int.Parse(csvArray[1]));
                            steps.Add(int.Parse(csvArray[2]));
                            dist.Add(double.Parse(csvArray[3]));
                            minInactive.Add(int.Parse(csvArray[4]));
                            minLightlyActive.Add(int.Parse(csvArray[5]));
                            minFairlyActive.Add(int.Parse(csvArray[6]));
                            minVeryActive.Add(int.Parse(csvArray[7]));
                            activityCalories.Add(int.Parse(csvArray[8]));

                            stepsPerMetre = CalculateStepsPerMetre(steps[count], dist[count]);

                            //DISPLAY the values into the listbox neatly padded out
                            listBoxOutput.Items.Add(objectType.PadRight(15) + calories[count].ToString().PadRight(8) + steps[count].ToString().PadRight(8) + dist[count].ToString().PadRight(8)
                                + minInactive[count].ToString().PadRight(8) + minLightlyActive[count].ToString().PadRight(8) + minFairlyActive[count].ToString().PadRight(8)
                                + minVeryActive[count].ToString().PadRight(8) + activityCalories[count].ToString().PadRight(8) + stepsPerMetre.ToString("N3"));

                            //DRAW bar graph
                            y = pictureBoxTop.Height - (int)dist[count] * SCALE_FACTOR;
                            DrawABar(paper, x, y, (int)dist[count] * SCALE_FACTOR, Color.Black);
                            x += BAR_WIDTH;

                            count++;

                            //ADD up all the steps
                            totalSteps = steps.Sum();
                            
                        }
                        else
                        {
                            Console.WriteLine("This data is incorrect " + line);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error: " + line);
                    }
                }
                MessageBox.Show(totalSteps.ToString());
                reader.Close();
            }
            else
            {
                MessageBox.Show("Error: " + line);
            }
        }

        /// <summary>
        /// Calculates the number of steps for each Km 
        /// </summary>
        /// <param name="numSteps"></param>
        /// <param name="distWalked"></param>
        /// <returns></returns>
        private double CalculateStepsPerMetre(int numSteps, double distWalked)
        {
            double stepsPerMetre = numSteps / (distWalked * 1000);
            return stepsPerMetre;
        }
    }
}
