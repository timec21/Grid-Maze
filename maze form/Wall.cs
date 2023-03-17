using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using maze_form;

namespace maze_form
{
    internal class Wall : Cell
    {
        public Wall()
        {
            Button.Text = "";
            Button.BackColor = Color.Black;
            main = false;
            Wall = true;
        }
    }
}
