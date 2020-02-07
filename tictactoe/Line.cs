using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class Line
    {
        public double Theta { get; private set; }
        public double R { get; private set; }
        public double R2 { get; private set; }
        public Node.Symbol Player { get; private set; }
        //public Node First { get; set; }
        //public Node Second { get; set; }
        public int Score { get; set; }
        public Line(double theta, double r, double r2, Node.Symbol player)
        {
            this.Theta = theta;
            this.R = r;
            this.R2 = r2;
            this.Player = player;
            this.Score = 1;
        }
    }
}
