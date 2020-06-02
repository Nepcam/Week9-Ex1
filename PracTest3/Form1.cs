using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
                while (!reader.EndOfStream)
                {
                    //READ a whole csv line from the file
                    line = reader.ReadLine();
                    //SPLIT the values from the line using an array
                    csvArray = line.Split(',');

                    //EXTRACT values into separate values
                    objectType = csvArray[0];
                }
            }
        }
    }
}
