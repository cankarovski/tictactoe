using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tictactoe
{
    class Game

    {
        Node[,] GameBoard = new Node[3,3];
        Node LastMove;

        private delegate void LoopHandler(int x, int y);

        public Game()
        {
            // constructor
        }

        public void Play()
        {
            //InitializeBoard();
            //DisplayBoard();

            LoopHandler initializeBoard;
            initializeBoard = InitNode;
            initializeBoard += DisplayNode;
            TraverseNodes(initializeBoard);

            var player = Node.Symbol.X;
            int moves = 0;
            while (true)
            {
                player = PlayerSwitch(player);

                if (EvaluateBoard())
                    if (PromptNewGame()) break;

                if (CatsGame(moves)) break;
                moves++;
            }
        }
        private bool PromptNewGame()
        {
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
                return true;
            else
            {
                Environment.Exit(0);
                return false;
            }
        }

        private bool CatsGame(int moves)
        {
            if (moves > 7)
            {
                Console.WriteLine($"It's a tie! Press enter to play again.");
                if (PromptNewGame()) return true;
            }
            return false;
        }

        private Node.Symbol PlayerSwitch(Node.Symbol player)
        {
            if (player == Node.Symbol.X)
            {
                PlayerMove(player);
                return Node.Symbol.O;
            }
            else
            {
                PlayerMove(player);
                return Node.Symbol.X;
            }
        }

        private void PlayerMove(Node.Symbol player)
        {
            Node playerMove;

            while (true)
            {
                var matches = CheckInput(player);

                playerMove = GameBoard[matches[0] - 1, matches[1] - 1];

                if (playerMove.Player == Node.Symbol.Empty)
                {
                    playerMove.Player = player;
                    break;
                }
            }
            
            DisplayBoard();

            LastMove = playerMove;
        }

        private int[] CheckInput(Node.Symbol player)
        {
            MatchCollection matches = null;
            int[] pair = new int[2];
            while (true)
            {
                Console.Write($"Player '{player}' is on! Enter two numbers [1-3], first for row and second for column: ");
                var PlayerInput = Console.ReadLine();

                matches = Regex.Matches(PlayerInput, @"([1-3])");


                if (matches.Count > 1)
                {
                    pair[0] = Int32.Parse(matches[0].Value);
                    pair[1] = Int32.Parse(matches[1].Value);
                    break;
                }
                else if (PlayerInput == "random")
                {
                    pair = RandomChoice(pair);
                    break;
                }
            }
            return pair;
        }

        private int[] RandomChoice(int[] pair)
        {
            while (true)
            {
                Random random = new Random();
                pair[0] = random.Next(1, 4);
                pair[1] = random.Next(1, 4);

                if (GameBoard[pair[0] - 1, pair[1] - 1].Player == Node.Symbol.Empty) break;
            }
            return pair;
        }

        private void TraverseNodes(LoopHandler callback)
        {
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    callback(i, j);
                }
            }
        }

        private void InitializeBoard()
        {
            LoopHandler handler = (i,j) => GameBoard[i, j] = new Node(i, j, Node.Symbol.Empty);
            TraverseNodes(handler);
        }

        private void InitNode(int i, int j)
        {
            GameBoard[i, j] = new Node(i, j, Node.Symbol.Empty);
        }

        private void DisplayBoard()
        {
            Console.WriteLine();

            LoopHandler handler = DisplayNode;
            TraverseNodes(handler);
        }
        private void DisplayNode(int i, int j)
        {
            if (GameBoard[i, j].Player == Node.Symbol.Empty)
                Console.Write($"[   ] ");
            else
                Console.Write($"[ {GameBoard[i, j].Player} ] ");
            if (j == GameBoard.GetLength(1) - 1) Console.WriteLine(Environment.NewLine);
        }

        private bool EvaluateBoard()
        {
            HoughTransform hough = new HoughTransform();

            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    if (GameBoard[i, j].Player != Node.Symbol.Empty)
                    {
                        bool gameOver = TraverseNeighbours(i, j, hough);
                        if (gameOver) return true;
                    }
                }
            }
            return false;
        }

        private bool TraverseNeighbours(int i, int j, HoughTransform hough)
        {
            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    try
                    {
                        var neighbour = GameBoard[i + k, j + l];
                        if (neighbour.Player != Node.Symbol.Empty && !(k == 0 && l == 0))
                        {
                            bool foundLine = hough.Transform(GameBoard[i, j], neighbour);
                            if (foundLine) return true;
                        }

                    }
                    catch (IndexOutOfRangeException) { }
                }
            }
            return false;
        }
    }
}
