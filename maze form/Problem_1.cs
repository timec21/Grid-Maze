using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace maze_form
{
    public partial class Problem_1 : UserControl
    {
        private string textfile = @"C:\Users\isgor\OneDrive\Masaüstü\proje\url1.txt";
        private string url1 = @"C:\Users\isgor\OneDrive\Masaüstü\proje\url1.txt";
        private string url2 = @"C:\Users\isgor\OneDrive\Masaüstü\proje\url2.txt";
        public History mazeHistory = new History();

        public Problem_1()
        {
            System.Windows.Forms.Control.ControlCollection control = this.Controls;

            grid g = new grid();
            g.textfile = this.textfile;
            g.TextFileReader(control);
            InitializeComponent();
            Button exit = new Button();
            Button start = new Button();
            start.Location = new Point(370 + g.columns * 35, 60 + (g.rows * 35) / 2);
            start.Size = new Size(150, 70);
            start.Text = "Çalıştır";
            Controls.Add(start);
            start.Click += (sender, args) =>
            {
                List<Tuple<int, int>> path = new List<Tuple<int, int>>();
                List<Tuple<int, int>> mainPath = new List<Tuple<int, int>>();
                Robot robot = new Robot(g.Cells, path, mainPath, g.start_X, g.start_Y, g.end_X, g.end_Y, this.Controls);
                robot.gridHistory = mazeHistory;
                foreach (var grid in g.Cells)
                {
                    if (grid.GetType() != typeof(Wall))
                    {
                        grid.Button.BackColor = Color.Gray;
                        grid.Button.Text = "";
                    }

                }
            };
            Button button = new Button();
            button.Location = new Point(200 + g.columns * 35, 60 + (g.rows * 35) / 2);
            button.Size = new Size(150, 70);
            button.Text = "URL değiştir";
            button.Click += (sender, args) =>
            {

                Controls.Clear();
                g = new grid();
                if (String.Equals(textfile, url1))
                {
                    g.textfile = string.Copy(url2);
                    textfile = string.Copy(url2);

                }
                else
                {
                    g.textfile = string.Copy(url1);
                    textfile = string.Copy(url1);
                }
                g.TextFileReader(control);
                Controls.Add(start);
                button.Location = new Point(200 + g.columns * 35, 60 + (g.rows * 35) / 2);
                start.Location = new Point(370 + g.columns * 35, 60 + (g.rows * 35) / 2);
                exit.Location = new Point(540 + g.columns * 35, 60 + (g.rows * 35) / 2);

                Controls.Add(exit);
                Controls.Add(button);
            };

            Controls.Add(button);

            exit.Text = "Çıkış";
            exit.Size = new Size(150, 70);
            exit.Location = new Point(540 + g.columns * 35, 60 + (g.rows * 35) / 2);
            Controls.Add(exit);
            exit.Click += (sender, args) =>
            {
                Visible = false;
            };
        }

        private void Problem_1_Load(object sender, EventArgs e)
        {

        }
    }
}
