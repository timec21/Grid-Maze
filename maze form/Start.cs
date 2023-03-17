using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace maze_form
{
    public partial class Start : UserControl
    {
        public UserControl problem_1 = new UserControl();
        public UserControl problem_2 = new UserControl();
        public History mazeHistory = new History();


        public int Row;
        public int Col;
        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            problem_1.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Row = int.Parse(textBox1.Text) % 2 == 0 ? int.Parse(textBox1.Text) + 1 : int.Parse(textBox1.Text);
            Col = int.Parse(textBox2.Text) % 2 == 0 ? int.Parse(textBox2.Text) + 1 : int.Parse(textBox2.Text);
            problem_2.Visible = true;
            problem2_Screen(Row, Col, problem_2);
        }

        public void problem2_Screen(int row, int col, UserControl problem2)
        {
            Maze maze = new Maze(row, col, problem2.Controls);
            maze.mazeHistory = this.mazeHistory;
            maze.Generate();
            System.Windows.Forms.Button exit = new System.Windows.Forms.Button();
            exit.Text = "Çıkış";
            exit.Size = new Size(150, 70);
            exit.Location = new Point(130 + row * 25, 100 + (col * 25) / 2);
            problem2.Controls.Add(exit);
            exit.Click += (sender, args) =>
            {
                problem2.Controls.Clear();
                problem2.Visible = false;

            };

            System.Windows.Forms.TextBox newTextBox1 = new System.Windows.Forms.TextBox();
            newTextBox1.Location = new Point(510 + row * 25, -60 + (col * 25) / 2);
            problem2.Controls.Add(newTextBox1);
            newTextBox1.Text = textBox1.Text;
            System.Windows.Forms.TextBox newTextBox2 = new System.Windows.Forms.TextBox();
            newTextBox2.Location = new Point(510 + row * 25, -30 + (col * 25) / 2);
            problem2.Controls.Add(newTextBox2);
            newTextBox2.Text = textBox2.Text;


            System.Windows.Forms.Label newLabel1 = new System.Windows.Forms.Label();
            newLabel1.Text = label1.Text;
            newLabel1.Location = new Point(460 + row * 25, -60 + (col * 25) / 2);
            problem2.Controls.Add(newLabel1);

            System.Windows.Forms.Label newLabel2 = new System.Windows.Forms.Label();
            newLabel2.Location = new Point(460 + row * 25, -30 + (col * 25) / 2);
            newLabel2.Text = label2.Text;
            problem2.Controls.Add(newLabel2);


            System.Windows.Forms.Button restart = new System.Windows.Forms.Button();
            restart.Text = "Değiştir";
            restart.Size = new Size(150, 70);
            restart.Location = new Point(460 + row * 25, 10 + (col * 25) / 2);
            problem2.Controls.Add(restart);
            restart.Click += (sender, args) =>
            {
                problem2.Controls.Clear();
                row = int.Parse(newTextBox1.Text) % 2 == 0 ? int.Parse(newTextBox1.Text) + 1 : int.Parse(newTextBox1.Text);
                col = int.Parse(newTextBox2.Text) % 2 == 0 ? int.Parse(newTextBox2.Text) + 1 : int.Parse(newTextBox2.Text);
                Maze newMaze = new Maze(row, col, problem2.Controls);
                newMaze.mazeHistory = this.mazeHistory;
                newMaze.Generate();

                exit.Location = new Point(130 + row * 25, 100 + (col * 25) / 2);
                restart.Location = new Point(460 + row * 25, 10 + (col * 25) / 2);
                newLabel2.Location = new Point(460 + row * 25, -30 + (col * 25) / 2);
                newLabel1.Location = new Point(460 + row * 25, -60 + (col * 25) / 2);
                newTextBox2.Location = new Point(510 + row * 25, -30 + (col * 25) / 2);
                newTextBox1.Location = new Point(510 + row * 25, -60 + (col * 25) / 2);

                problem2.Controls.Add(restart);
                problem2.Controls.Add(exit);
                problem2.Controls.Add(newTextBox1);
                problem2.Controls.Add(newTextBox2);
                problem2.Controls.Add(newLabel1);
                problem2.Controls.Add(newLabel2);
            };


        }


    }
}
