using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class HoughTransform
    {
        public List<Line> Accumulator;
        public HoughTransform()
        {
            this.Accumulator = new List<Line>();
        }

        public bool Transform(Node first, Node second)
        {
            if (first.Player == second.Player)
            {
                double theta;
                try
                {
                    //theta = Math.Atan2((first.Y - second.Y), (first.X - second.X));
                    theta = Math.Atan((first.X - second.X) / (first.Y - second.Y));
                }
                catch (DivideByZeroException)
                {
                    theta = Math.PI / 2;
                }

                double r = first.X * Math.Cos(theta) + first.Y * Math.Sin(theta);
                double r2 = second.X * Math.Cos(theta) + second.Y * Math.Sin(theta);

                Line line = new Line(theta, r, r2, first.Player);

                return AddLine(line);
            }
            return false;
        }

        private bool AddLine(Line line)
        {
            if (Accumulator.Count == 0)
            {
                Accumulator.Add(line);
            }
            else
            {
                bool foundMatch = false;
                foreach (Line l in Accumulator)
                {
                    if (line.Theta == l.Theta && (line.R == l.R || line.R == l.R2))
                    {
                        foundMatch = true;
                        l.Score += 1;

                        if (CheckScore(l)) return true;
                        
                        break;
                    }
                }
                if (!foundMatch)
                {
                    Accumulator.Add(line);
                }
            }
            
            return false;
        }

        private bool CheckScore(Line l)
        {
            if (l.Score > 2)
            {
                Console.WriteLine($"Player '{l.Player}' wins! Press enter to play again.");
                return true;
            }
            else return false;
        }

        // obsolete
        private double NormalizeAngle(double value)
        {
            double twoPi = Math.PI + Math.PI;
            while (value <= -Math.PI) value += twoPi;
            while (value > Math.PI) value -= twoPi;
            return value;
        }
    }
}
