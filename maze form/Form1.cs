using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace maze_form
{
    public partial class Form1 : Form
    {
        public History mazeHistory = new History();

        public Form1()
        {
            InitializeComponent();
            FileStream fs = File.Create("maze.txt");
            fs.Close();
            fs = File.Create("grid.txt");
            fs.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void start1_Load(object sender, EventArgs e)
        {
            start1.problem_1 = problem_11;
            start1.problem_2 = problem_21;
            problem_11.mazeHistory = mazeHistory;
            start1.mazeHistory = mazeHistory;
        }

        private void problem_21_Load(object sender, EventArgs e)
        {

        }


    }
}