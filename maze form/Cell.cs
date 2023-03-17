using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_form
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Visited { get; set; }
        public Button Button { get; set; }
        public bool Wall { get; set; }
        public bool main { get; set; }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
            Visited = false;
            Wall = false;
            Button button = new Button();
            button.Size = new Size(25, 25);
            button.BackColor = Color.White;
            Button = button;
        }

        //////////////////////////////////////////
        public Cell()
        {

            Button button = new Button();
            button.Size = new Size(35, 35);
            button.BackColor = Color.White;
            button.Text = "0";
            main = true;
            Wall = false;
            Button = button;
        }

    }

}
