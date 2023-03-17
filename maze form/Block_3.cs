using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using maze_form;

namespace maze_form
{
    internal class Block_3 : Cell
    {
        public Block_3()
        {
            Button.BackColor = Color.Blue;
            Button.Text = "3";
            main = false;
        }
    }
}
