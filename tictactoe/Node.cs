using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Symbol Player { get; set; }
        public enum Symbol { X, O, Empty};
        public Node(int x, int y, Symbol player)
        {
            this.X = x;
            this.Y = y;
            this.Player = player;
        }
    }
}
