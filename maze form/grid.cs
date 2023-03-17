using maze_form;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace maze_form
{
    internal class grid
    {
        public Cell[,] Cells;
        public string text = "";
        public string txt = "";
        public string textfile;
        public string url1 = @"C:\Users\isgor\OneDrive\Masaüstü\proje\url1.txt";
        public string url2 = @"C:\Users\isgor\OneDrive\Masaüstü\proje\url2.txt";
        public int rows, columns;
        public int start_X = 0, start_Y = 0, end_X = 0, end_Y = 0;


        public void TextFileReader(System.Windows.Forms.Control.ControlCollection control)
        {
            StreamReader streamReader = File.OpenText(textfile);
            while ((text = streamReader.ReadLine()) != null)
            {
                txt = text + txt;
            }
            rows = File.ReadAllLines(textfile).Length;
            columns = (txt.Length) / rows;
            streamReader.Close();

            int a = 0;
            rows = rows + 2;
            columns = columns + 2;
            Cells = new Cell[rows, columns];

            while (a < txt.Length)
            {
                for (int i = rows - 1; i >= 0; i--)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (i == 0 || i == rows - 1 || j == 0 || j == columns - 1)
                        {

                            Cells[i, j] = new Wall();
                        }
                        else
                        {
                            if (txt[a] == '0')
                                Cells[i, j] = new Cell();

                            if (txt[a] == '1')
                            {
                                Cells[i, j] = new Block1();
                                Cells[i, j].Wall = true;
                            }

                            if (txt[a] == '2')
                                if (Cells[i, j] == null)
                                {
                                    Cells[i, j] = new Block_2();
                                    Cells[i, j].Wall = true;
                                    Cells[i, j + 1] = new Block_2();
                                    Cells[i, j + 1].Button.BackColor = Color.Green;
                                }

                            if (txt[a] == '3')
                                if (Cells[i, j] == null)
                                {
                                    Cells[i, j] = new Block_3();
                                    Cells[i, j].Wall = true;
                                    Cells[i - 1, j] = new Block_3();
                                    Cells[i - 1, j].Button.BackColor = Color.Green;
                                    Cells[i - 2, j] = new Block_3();
                                    Cells[i - 2, j].Button.BackColor = Color.Green;
                                }
                            a++;
                        }
                    }
                }
            }
            GridGenerate(control);

        }

        public async void GridGenerate(System.Windows.Forms.Control.ControlCollection control)
        {

            Random rand = new Random();
            int x = 0;
            while (x < 2)
            {
                int randomRow = rand.Next(rows);
                int randomColumn = rand.Next(columns);

                if (Cells[randomRow, randomColumn].main && x == 1)
                {
                    Cells[randomRow, randomColumn].Button.BackColor = Color.Red;
                    Cells[randomRow, randomColumn].Button.Text = "Finish";
                    end_X = randomRow;
                    end_Y = randomColumn;
                    x++;
                }

                if (Cells[randomRow, randomColumn].main && x == 0)
                {
                    Cells[randomRow, randomColumn].Button.BackColor = Color.Orange;
                    Cells[randomRow, randomColumn].Button.Text = "Start";
                    start_X = randomRow;
                    start_Y = randomColumn;
                    x++;
                }
            }


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Cells[i, j].Button.Location = new Point(100 + j * 35, 60 + i * 35);
                    control.Add(Cells[i, j].Button);
                }
            }



        }
    }
}
