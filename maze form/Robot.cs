using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_form
{

    internal class Robot
    {

        private Cell[,] maze { get; set; }
        private int rows;
        private int cols;
        private Tuple<int, int> Start;
        private Tuple<int, int> End;
        private bool[,] Visited;
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        List<Tuple<int, int>> mainPath = new List<Tuple<int, int>>();
        public Stopwatch stopwatch1 = new Stopwatch();

        public Robot(Cell[,] maze, List<Tuple<int, int>> path, List<Tuple<int, int>> mainPath, Tuple<int, int> start, Tuple<int, int> end)
        {
            this.maze = maze;
            rows = maze.GetLength(0);
            cols = maze.GetLength(1);
            Visited = new bool[rows, cols];
            this.path = path;
            this.mainPath = mainPath;
            this.Start = start;
            this.End = end;
            stopwatch1.Start();
            Solve(start.Item1, start.Item2);
            stopwatch1.Stop();
        }

        public bool Solve(int x, int y)
        {

            if (x == End.Item1 && y == End.Item2)
            {
                path.Add(new Tuple<int, int>(x, y));
                return true;
            }

            if (maze[x, y].Wall || Visited[x, y] == true)
            {
                return false;
            }

            Visited[x, y] = true;
            path.Add(new Tuple<int, int>(x, y));

            if (IsValid(x + 1, y) && Solve(x + 1, y)) { mainPath.Add(new Tuple<int, int>(x, y)); return true; }
            if (IsValid(x - 1, y) && Solve(x - 1, y)) { mainPath.Add(new Tuple<int, int>(x, y)); return true; }
            if (IsValid(x, y + 1) && Solve(x, y + 1)) { mainPath.Add(new Tuple<int, int>(x, y)); return true; }
            if (IsValid(x, y - 1) && Solve(x, y - 1)) { mainPath.Add(new Tuple<int, int>(x, y)); return true; }

            return false;

        }
        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < rows && y >= 0 && y < cols;
        }



        ////////////////////////////////// 

        private Cell[,] grid;
        private int start_X;
        private int start_Y;
        private int end_X;
        private int end_Y;
        int pathTime = 0;
        private int delay = 300;
        public History gridHistory = new History();
        List<Tuple<int, int>> bfsPath = new List<Tuple<int, int>>();
        HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();
        Dictionary<Tuple<int, int>, Tuple<int, int>> parent = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
        public System.Windows.Forms.Control.ControlCollection Controls;
        Stopwatch stopwatch = new Stopwatch();
        public Robot(Cell[,] grid, List<Tuple<int, int>> path, List<Tuple<int, int>> mainPath, int start_X, int start_Y, int end_X, int end_Y, System.Windows.Forms.Control.ControlCollection control)
        {
            this.Controls = control;
            this.grid = grid;
            this.path = path;
            this.mainPath = mainPath;
            this.start_X = start_X;
            this.start_Y = start_Y;
            this.end_X = end_X;
            this.end_Y = end_Y;
            rows = grid.GetLength(0);
            cols = grid.GetLength(1);
            pathTime = 0;
            stopwatch.Start();
            gridSolve();
            stopwatch.Stop();
        }

        public void gridSolve()
        {

            Tuple<int, int> start = Tuple.Create(start_X, start_Y);
            Tuple<int, int> end = Tuple.Create(end_X, end_Y);
            parent[start] = null;

            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();


            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                Tuple<int, int> currentPoint = queue.Dequeue();

                if (currentPoint.Equals(end))
                {
                    break;
                }

                int i = currentPoint.Item1;
                int j = currentPoint.Item2;
                List<Tuple<int, int>> neighbors = new List<Tuple<int, int>> {
                Tuple.Create(i-1, j),
                Tuple.Create(i+1, j),
                Tuple.Create(i, j-1),
                Tuple.Create(i, j+1)
            };

                foreach (Tuple<int, int> neighbor in neighbors)
                {
                    if (IsValid(neighbor) && !visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parent[neighbor] = currentPoint;
                    }
                }


            }

            List<Tuple<int, int>> path = GetShortestPath(start, end);
            Console.Write("Shortest path: ");
            foreach (Tuple<int, int> point in path)
            {
                Console.Write("({0}, {1}) ", point.Item1, point.Item2);

            }
            foreach (Tuple<int, int> addneighbors in path)
            {
                int i = addneighbors.Item1;
                int j = addneighbors.Item2;
                if (addneighbors == end || addneighbors == start)
                {
                    bfsPath.Add(addneighbors);
                }
                else
                {
                    bfsPath = new List<Tuple<int, int>>(){
                    Tuple.Create(i, j),
                    Tuple.Create(i - 1, j),
                    Tuple.Create(i + 1, j),
                    Tuple.Create(i, j - 1),
                    Tuple.Create(i, j + 1)
                };
                }

                foreach (Tuple<int, int> tuple in bfsPath)
                {
                    if (visited.Contains(tuple))
                    {
                        mainPath.Add(tuple);
                        pathTime = pathTime + 1;
                        visited.Remove(tuple);
                    }
                }
            }


            Button result = new Button();
            result.Text = "Sonuç Göster";
            result.Size = new Size(150, 70);
            result.Location = new Point(200 + cols * 35, -20 + (rows * 35) / 2);
            Controls.Add(result);

            result.Click += (sender, args) =>
            {
                delay = 0;
            };
            showPath();
        }

        public List<Tuple<int, int>> GetShortestPath(Tuple<int, int> start, Tuple<int, int> end)
        {
            Tuple<int, int> current = end;
            while (!current.Equals(start))
            {
                path.Add(current);
                current = parent[current];
            }
            path.Add(start);
            path.Reverse();
            return path;
        }
        private bool IsValid(Tuple<int, int> point)
        {
            int i = point.Item1;
            int j = point.Item2;
            return i >= 0 && i < rows && j >= 0 && j < cols && (string.Equals(grid[i, j].Button.Text, "0") || !grid[i, j].Wall || string.Equals(grid[i, j].Button.Text, "Finish") || string.Equals(grid[i, j].Button.Text, "Start"));
        }
        private async void showPath()
        {

            foreach (Tuple<int, int> pair in mainPath)
            {
                showAround(pair.Item1, pair.Item2);

                grid[start_X, start_Y].Button.BackColor = Color.Orange;
                grid[start_X, start_Y].Button.Text = "Start";
                grid[pair.Item1, pair.Item2].Button.BackColor = Color.Yellow;
                grid[end_X, end_Y].Button.BackColor = Color.Red;
                grid[end_X, end_Y].Button.Text = "Finish";
                await Task.Delay(delay * 2);
            }

            foreach (Tuple<int, int> pair in path)
            {
                grid[start_X, start_Y].Button.BackColor = Color.Orange;
                grid[start_X, start_Y].Button.Text = "Start";
                grid[pair.Item1, pair.Item2].Button.BackColor = Color.Purple;
                grid[end_X, end_Y].Button.BackColor = Color.Red;
                grid[end_X, end_Y].Button.Text = "Finish";
                await Task.Delay(delay / 2);
            }
            gridHistory.showGridElapsedTime(grid, path, stopwatch, Controls);

        }
        public void showAround(int x, int y)
        {
            for (var i = -1; i < 2; i++)
            {
                if (IsValid(x, y, i) && grid[x + i, y] is Cell)
                {
                    grid[x + i, y].Button.BackColor = Color.White;
                    grid[x + i, y].Button.Text = "0";
                }

                if (IsValid(x, y, i) && grid[x, y + i] is Cell)
                {
                    grid[x, y + i].Button.BackColor = Color.White;
                    grid[x, y + i].Button.Text = "0";
                }

                if (IsValid(x, y, i) && grid[x + i, y] is Wall)
                {
                    grid[x + i, y].Button.BackColor = Color.Black;
                }

                if (IsValid(x, y, i) && grid[x, y + i] is Wall)
                {
                    grid[x, y + i].Button.BackColor = Color.Black;
                }

                if (IsValid(x, y, i) && grid[x + i, y] is Block1)
                {
                    grid[x + i, y].Button.BackColor = Color.Blue;
                    grid[x + i, y].Button.Text = "1";
                }

                if (IsValid(x, y, i) && grid[x, y + i] is Block1)
                {
                    grid[x, y + i].Button.BackColor = Color.Blue;
                    grid[x, y + i].Button.Text = "1";
                }

                if (IsValid(x, y, i) && grid[x + i, y] is Block_2 && grid[x + i, y].Wall == false)
                {
                    grid[x + i, y].Button.BackColor = Color.Green;
                    grid[x + i, y].Button.Text = "2";
                }

                if (IsValid(x, y, i) && grid[x, y + i] is Block_2 && grid[x, y + i].Wall == false)
                {
                    grid[x, y + i].Button.BackColor = Color.Green;
                    grid[x, y + i].Button.Text = "2";
                }

                if (IsValid(x, y, i) && grid[x + i, y] is Block_2 && grid[x + i, y].Wall == true)
                {
                    grid[x + i, y].Button.BackColor = Color.Blue;
                    grid[x + i, y].Button.Text = "2";
                }

                if (IsValid(x, y, i) && grid[x, y + i] is Block_2 && grid[x, y + i].Wall == true)
                {
                    grid[x, y + i].Button.BackColor = Color.Blue;
                    grid[x, y + i].Button.Text = "2";
                }

                if (IsValid(x, y, i) && grid[x + i, y] is Block_3 && grid[x + i, y].Wall == false)
                {
                    grid[x + i, y].Button.BackColor = Color.Green;
                    grid[x + i, y].Button.Text = "3";
                }

                if (IsValid(x, y, i) && grid[x, y + i] is Block_3 && grid[x, y + i].Wall == false)
                {
                    grid[x, y + i].Button.BackColor = Color.Green;
                    grid[x, y + i].Button.Text = "3";
                }

                if (IsValid(x, y, i) && grid[x + i, y] is Block_3 && grid[x + i, y].Wall == true)
                {
                    grid[x + i, y].Button.BackColor = Color.Blue;
                    grid[x + i, y].Button.Text = "3";
                }

                if (IsValid(x, y, i) && grid[x, y + i] is Block_3 && grid[x, y + i].Wall == true)
                {
                    grid[x, y + i].Button.BackColor = Color.Blue;
                    grid[x, y + i].Button.Text = "3";
                }

            }

            for (int i = mainPath.FindIndex(t => t.Item1 == x && t.Item2 == y); 0 < i; i--)
            {
                Tuple<int, int> tuple = mainPath[i];
                grid[tuple.Item1, tuple.Item2].Button.BackColor = Color.Yellow;
            }
        }

        private bool IsValid(int x, int y, int i)
        {
            if (i == -1)
                return x > 0 && x < rows && y > 0 && y < cols;
            if (i == 0)
                return x >= 0 && x < rows && y >= 0 && y < cols;

            if (i == 1)
                return x >= 0 && x < rows - 1 && y >= 0 && y < cols - 1;

            return x >= 0 && x < rows && y >= 0 && y < cols;
        }

    }
}




