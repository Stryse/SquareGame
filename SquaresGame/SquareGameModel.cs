using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquaresGame
{ 
    public class Player
    {
        public String PlayerName  { get; private set; }
        public Color  PlayerColor { get; private set; }
        public int    Points      { get; set; }

        public Player(String pName, Color pColor)
        {
            PlayerName  = pName;
            PlayerColor = pColor;
            Points      = 0;
        }
    }

    public class SquareGameModel
    {
        //=========== Fields ===========//
        private readonly List<Tuple<Point, Point, Player>> lines;
        private readonly List<Tuple<Point, Point, Player>> rectangles;
        private readonly int linesToEnd;

        //=========== Properties ===========//
        public int FieldSize { get; private set; }
        public Player PlayerOne { get; private set; }
        public Player PlayerTwo { get; private set; }
        public Player ActivePlayer { get; private set; }
        public bool GameEnded { get; private set; }
        public IReadOnlyList<Tuple<Point,Point,Player>> Lines { get { return lines.AsReadOnly(); } }
        public IReadOnlyList<Tuple<Point,Point,Player>> Rectangles { get { return rectangles.AsReadOnly(); } }

        //=========== Events ===========//
        public event EventHandler UpdateUI; // Singlecast --> Action
        public event EventHandler<Player> PlayerWon;
        public event EventHandler<Tuple<Point, Point, Player>> Scored;

        //=========== CTORS ===========//
        public SquareGameModel(int fieldSize, Player p1, Player p2)
        {
            //Field Inits
            if (fieldSize < 2)
                throw new ArgumentOutOfRangeException("fieldSize", "argument has to be > 1");

            FieldSize = fieldSize;
            PlayerOne = p1;
            PlayerTwo = p2;
            ActivePlayer = PlayerOne;
            GameEnded  = false;
            lines      = new List<Tuple<Point, Point, Player>>();
            rectangles = new List<Tuple<Point, Point, Player>>();
            linesToEnd = CalcLinesToEnd();
        }

        //=========== Methods ===========//
        //=== Public ===//
        public void AddNewLine(Tuple<Point, Point> line)
        {
            if (!lines.Any(p => IsSameLine(p,line)) && IsLine(line) && IsPermittedLine(line))
            {
                //Add new Line
                var newLine = new Tuple<Point, Point, Player>(line.Item1, line.Item2, ActivePlayer);
                lines.Add(newLine);

                //Check if game ended
                GameEnded = CheckWinCondition();

                //Check if scored
                if (!IsScored(line) && !GameEnded)
                    ChangeActivePlayer();
                else
                {
                    ++ActivePlayer.Points;
                    OnScored(this,newLine);
                }
                OnUpdateUI(this,EventArgs.Empty);
            }

            // Broadcast if game ended
            if (GameEnded)
            {
                if      (PlayerOne.Points > PlayerTwo.Points) OnPlayerWon(this, PlayerOne);
                else if (PlayerTwo.Points > PlayerOne.Points) OnPlayerWon(this, PlayerTwo);
                else                                          OnPlayerWon(this, null);                  
            }
        }

        public void AddNewLine(Point a, Point b)
        {
            AddNewLine(new Tuple<Point, Point>(a, b));
        }

        public void Restart()
        {
            lines.Clear();
            rectangles.Clear();
            PlayerOne.Points = 0;
            PlayerTwo.Points = 0;
            ActivePlayer = PlayerOne;
            GameEnded = false;
        }

        //=== Private ===//
        private void ChangeActivePlayer()
        {
            ActivePlayer = (ActivePlayer == PlayerOne) ? PlayerTwo : PlayerOne;
        }

        private bool IsScored(Tuple<Point,Point> newLine)
        {
            return false;
        }

        private int CalcLinesToEnd()
        {
            // We are calculating the number of all possible permitted lines with math formula
            int maxCornerLCount = 4 * 2; // 4 corners -> 2 possible line per corner
            int maxInnerLCount = ((FieldSize * FieldSize) - (4 * FieldSize) + 4) * 4; // num of inner lines 
                                                                                      //-> 4 possible line per inner point
            int maxSideLCount = ((4 * FieldSize) - 8) * 3; // num of side lines -> 3 possible line per side

            return (maxCornerLCount + maxInnerLCount + maxSideLCount) / 2;
        }

        private bool CheckWinCondition()
        {
            return lines.Count == linesToEnd;
        }

        private bool IsLine(Tuple<Point,Point> line)
        {
            return !(line.Item2.X - line.Item1.X == 0 && line.Item2.Y - line.Item1.Y == 0); 
        }

        private bool IsSameLine(Tuple<Point,Point,Player> p1,Tuple<Point,Point> p2)
        {
            return (p1.Item1 == p2.Item1 && p1.Item2 == p2.Item2) 
              ||   (p1.Item2 == p2.Item1 && p1.Item1 == p2.Item2);
        }

        private bool IsPermittedLine(Tuple<Point,Point> line)
        {
            int dx = Math.Abs(line.Item2.X - line.Item1.X);
            int dy = Math.Abs(line.Item2.Y - line.Item1.Y);

            return dx <= 1 && dy <= 1 && dx * dy != 1;
        }

        //=========== Event Senders ===========//
        protected virtual void OnUpdateUI(object sender, EventArgs e) => UpdateUI?.Invoke(sender,e);
        protected virtual void OnPlayerWon(object sender,Player p) => PlayerWon?.Invoke(sender,p);
        protected virtual void OnScored(object sender, Tuple<Point, Point, Player> line) => Scored?.Invoke(sender, line);
    }
}
