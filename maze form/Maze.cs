using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace maze_form
{
    internal class Maze
    {
        private int Rows { get; set; }
        private int Cols { get; set; }
        private int delay { get; set; }
        private Cell[,] Cells { get; set; }
        public Tuple<int, int> Start = new Tuple<int, int>(0, 0);
        public Tuple<int, int> End = new Tuple<int, int>(0, 0);
        private Random Rand { get; set; }
        private System.Windows.Forms.Control.ControlCollection Control { get; set; }
        private List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> mainPath = new List<Tuple<int, int>>();
        public History mazeHistory = new History();
        Stopwatch stopwatch = new Stopwatch();


        public Maze(int rows, int cols, System.Windows.Forms.Control.ControlCollection control)
        {
            Control = control;
            Rows = rows;
            Cols = cols;
            Cells = new Cell[rows, cols];
            Rand = new Random();
            delay = 250;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Cells[i, j] = new Block1(i, j);
                    Cells[i, j].Button.Location = new Point(100 + i * 25, 85 + j * 25);
                }
            }
        }

        public void Generate()
        {
            Stack<Cell> stack = new Stack<Cell>();
            Cell current = Cells[1, 1];
            current.Visited = true;
            Point location;
            while (true)
            {
                List<Cell> unvisitedNeighbors = new List<Cell>();
                int row = current.Row;
                int col = current.Col;
                if (row > 1 && !Cells[row - 2, col].Visited)
                {
                    unvisitedNeighbors.Add(Cells[row - 2, col]);
                }
                if (row < Rows - 2 && !Cells[row + 2, col].Visited)
                {
                    unvisitedNeighbors.Add(Cells[row + 2, col]);
                }
                if (col > 1 && !Cells[row, col - 2].Visited)
                {
                    unvisitedNeighbors.Add(Cells[row, col - 2]);
                }
                if (col < Cols - 2 && !Cells[row, col + 2].Visited)
                {
                    unvisitedNeighbors.Add(Cells[row, col + 2]);
                }
                if (unvisitedNeighbors.Count > 0)
                {
                    Cell next = unvisitedNeighbors[Rand.Next(unvisitedNeighbors.Count)];

                    if (next.Row < current.Row)
                    {
                        location = Cells[next.Row + 1, next.Col].Button.Location;
                        Cells[next.Row + 1, next.Col] = new Cell(next.Row + 1, next.Col);
                        Cells[next.Row + 1, next.Col].Button.Location = location;
                        Cells[next.Row + 1, next.Col].Wall = false;
                    }
                    else if (next.Row > current.Row)
                    {
                        location = Cells[next.Row - 1, next.Col].Button.Location;
                        Cells[next.Row - 1, next.Col] = new Cell(next.Row - 1, next.Col);
                        Cells[next.Row - 1, next.Col].Wall = false;
                        Cells[next.Row - 1, next.Col].Button.Location = location;
                    }
                    else if (next.Col < current.Col)
                    {
                        location = Cells[next.Row, next.Col + 1].Button.Location;
                        Cells[next.Row, next.Col + 1] = new Cell(next.Row, next.Col + 1);
                        Cells[next.Row, next.Col + 1].Wall = false;
                        Cells[next.Row, next.Col + 1].Button.Location = location;
                    }
                    else if (next.Col > current.Col)
                    {
                        location = Cells[next.Row, next.Col - 1].Button.Location;
                        Cells[next.Row, next.Col - 1] = new Cell(next.Row, next.Col - 1);
                        Cells[next.Row, next.Col - 1].Wall = false;
                        Cells[next.Row, next.Col - 1].Button.Location = location;
                    }
                    stack.Push(current);
                    current = next;
                    current.Visited = true;
                }
                else if (stack.Count > 0)
                {
                    current = stack.Pop();
                }
                else
                {
                    break;
                }

            }

            for (int i = 0; i < Rows; i++)
            {

                for (int j = 0; j < Cols; j++)
                {
                    if (Cells[i, j].Visited)
                    {
                        location = Cells[i, j].Button.Location;
                        Cells[i, j] = new Cell(i, j);
                        Cells[i, j].Button.Location = location;
                    }
                }
            }

            selectStart();
            Print();

            Button finishButton = new Button();
            finishButton.Enabled = false;
            finishButton.Text = "Sonuç";
            finishButton.Size = new Size(150, 70);
            finishButton.Location = new Point(295 + Rows * 25, 10 + (Cols * 25) / 2);
            Control.Add(finishButton);

            finishButton.Click += (sender, args) =>
            {
                delay = 0;
            };

            Button startButton = new Button();
            startButton.Text = "Başla";
            startButton.Size = new Size(150, 70);
            startButton.Location = new Point(130 + Rows * 25, 10 + (Cols * 25) / 2);
            Control.Add(startButton);

            startButton.Click += (sender, args) =>
            {
                foreach (Cell item in Cells)
                {
                    item.Button.BackColor = Color.Gray;
                }
                ShowPath();
                startButton.Enabled = false;
                finishButton.Enabled = true;
            };

        }

        private void selectStart()
        {
            switch (Rand.NextInt64(4))
            {
                case 0:
                    Start = Tuple.Create(0, 1);
                    End = Tuple.Create(Rows - 1, Cols - 2);
                    break;
                case 1:
                    Start = Tuple.Create(Rows - 1, Cols - 2);
                    End = Tuple.Create(0, 1);
                    break;
                case 2:
                    Start = Tuple.Create(Rows - 1, 1);
                    End = Tuple.Create(0, Cols - 2);
                    break;
                case 3:
                    Start = Tuple.Create(0, Cols - 2);
                    End = Tuple.Create(Rows - 1, 1);
                    break;

            }

        }
        public void Print()
        {

            List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            foreach (Cell item in Cells)
            {
                Control.Add(item.Button);
            }

            Cells[Start.Item1, Start.Item2].Wall = false;
            Cells[Start.Item1, Start.Item2].Button.BackColor = Color.Green;
            Cells[End.Item1, End.Item2].Wall = false;
            Cells[End.Item1, End.Item2].Button.BackColor = Color.Red;
        }

        public async void ShowPath()
        {
            mainPath.Add(new Tuple<int, int>(End.Item1, End.Item2));
            Robot robot = new Robot(Cells, path, mainPath, Start, End);
            stopwatch = robot.stopwatch1;
            foreach (var item in path)
            {
                showAround(item.Item1, item.Item2);
                await Task.Delay(delay);
            }
            mainPath.Reverse();
            foreach (var item in mainPath)
            {
                await Task.Delay(delay / 2);
                Cells[item.Item1, item.Item2].Button.BackColor = Color.Yellow;
            }
            mazeHistory.showMazeElapsedTime(Cells, path, stopwatch, Control);
        }

        public async void showAround(int x, int y)
        {

            await Task.Delay(delay / 2);

            for (var i = -1; i < 2; i++)
            {
                if (IsValid(x, y, i) && Cells[x + i, y].Wall)
                    Cells[x + i, y].Button.BackColor = Color.Black;
                else if (IsValid(x, y, i))
                    Cells[x + i, y].Button.BackColor = Color.White;

                if (IsValid(x, y, i) && Cells[x, y + i].Wall)
                    Cells[x, y + i].Button.BackColor = Color.Black;
                else if (IsValid(x, y, i))
                    Cells[x, y + i].Button.BackColor = Color.White;
            }
            for (int i = path.FindIndex(t => t.Item1 == x && t.Item2 == y); 0 < i; i--)
            {
                Tuple<int, int> tuple = path[i];
                Cells[tuple.Item1, tuple.Item2].Button.BackColor = Color.Blue;
            }

        }

        private bool IsValid(int x, int y, int i)
        {
            if (i == -1)
                return x > 0 && x < Rows && y > 0 && y < Cols;
            if (i == 0)
                return x >= 0 && x < Rows && y >= 0 && y < Cols;

            if (i == 1)
                return x >= 0 && x < Rows - 1 && y >= 0 && y < Cols - 1;

            return x >= 0 && x < Rows && y >= 0 && y < Cols;

        }

    }
}
