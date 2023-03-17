using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_form
{
    public class History
    {

        public List<Cell[,]> maze = new List<Cell[,]>();
        public List<Cell[,]> Grid = new List<Cell[,]>();
        public List<int> mazeElapsedTime = new List<int>();
        public List<int> gridElapsedTime = new List<int>();
        public History()
        {

        }

        public void showMazeElapsedTime(Cell[,] cell, List<Tuple<int, int>> path, Stopwatch stopwatch, System.Windows.Forms.Control.ControlCollection control)
        {

            maze.Add(cell);
            mazeElapsedTime.Add(path.Count);
            Button time = new Button();
            TimeSpan ts = stopwatch.Elapsed;
            time.Text = "Geçen Süre " + ts.TotalSeconds + " sn";
            time.Size = new Size(150, 70);
            time.Location = new Point(295 + maze[maze.Count - 1].GetLength(0) * 25, 100 + (maze[maze.Count - 1].GetLength(1) * 25) / 2);
            Button button = new Button();
            button.Text = "Geçilen Kare :  " + path.Count;
            button.Size = new Size(150, 70);
            button.Location = new Point(460 + maze[maze.Count - 1].GetLength(0) * 25, 100 + (maze[maze.Count - 1].GetLength(1) * 25) / 2);

            saveHistory(path, "maze", stopwatch);
            control.Add(time);
            control.Add(button);
        }

        public void showGridElapsedTime(Cell[,] cell, List<Tuple<int, int>> path, Stopwatch stopwatch, System.Windows.Forms.Control.ControlCollection control)
        {

            Grid.Add(cell);
            gridElapsedTime.Add(path.Count);
            Button time = new Button();
            TimeSpan ts = stopwatch.Elapsed;
            time.Text = "Geçen Süre " + ts.TotalSeconds + " sn";
            time.Size = new Size(150, 70);
            time.Location = new Point(370 + Grid[Grid.Count - 1].GetLength(1) * 35, -20 + (Grid[Grid.Count - 1].GetLength(0) * 35) / 2);

            Button button = new Button();
            button.Text = "Geçilen Kare :  " + path.Count;
            button.Size = new Size(150, 70);
            button.Location = new Point(540 + Grid[Grid.Count - 1].GetLength(1) * 35, -20 + (Grid[Grid.Count - 1].GetLength(0) * 35) / 2);

            saveHistory(path, "grid", stopwatch);
            control.Add(button);
            control.Add(time);
        }

        public void saveHistory(List<Tuple<int, int>> path, string type, Stopwatch stopwatch)
        {
            string fileName = type + ".txt";

            using (StreamWriter sw = File.AppendText(fileName))
            {
                int i = 1;
                if (string.Equals(type, "maze"))
                    sw.WriteLine("Labirent Boyutu : " + maze[maze.Count - 1].GetLength(0).ToString() + ", " + maze[maze.Count - 1].GetLength(1).ToString());
                else sw.WriteLine("Izgaranın Boyutu : " + Grid[Grid.Count - 1].GetLength(0).ToString() + ", " + Grid[Grid.Count - 1].GetLength(1).ToString());

                foreach (Tuple<int, int> item in path)
                {
                    sw.WriteLine("Adım " + i + ": (" + item.Item1.ToString() + ", " + item.Item2.ToString() + ")");
                    i++;
                }
                TimeSpan ts = stopwatch.Elapsed;
                sw.WriteLine("Geçen Süre: " + ts.TotalSeconds + " Saniye");
                sw.WriteLine("Geçilen Kare :  " + path.Count);
                sw.WriteLine();
                sw.Close();
            }


        }
    }
}
