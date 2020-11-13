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
        private int rectCount;
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
            rectCount = 0;
            linesToEnd = CalcLinesToEnd();
        }

        //=========== Methods ===========//
        //=== Public ===//
        public void AddNewLine(Tuple<Point, Point> line)
        {
            if (!lines.Any(p => IsSameLine(p,line)) && IsLine(line) && IsPermittedLine(line))
            {
                //Check if adding makes rectangle
                UpdateRectangles(line);

                //Add new Line
                var newLine = SanitizeLine(line);
                var newLineWithPlayer = new Tuple<Point,Point,Player>(newLine.Item1,newLine.Item2,ActivePlayer);
                lines.Add(newLineWithPlayer);

                //Check if game ended
                GameEnded = CheckWinCondition();

                //Check if scored
                if (!IsScored() && !GameEnded)
                    ChangeActivePlayer();
                else
                    ++ActivePlayer.Points;
                OnUpdateUI(this,EventArgs.Empty);
            }

            // Broadcast event if game ended
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
            rectCount = 0;
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

        private Tuple<Point,Point> SanitizeLine(Tuple<Point, Point> line) 
        {
            // Sets line direction left-to-right and top-to-bottom
            int dRow = line.Item2.X - line.Item1.X;
            int dCol = line.Item2.Y - line.Item1.Y;

            if (dRow >= 0 && dCol >= 0)
                return new Tuple<Point, Point>(line.Item1, line.Item2);
            else
                return new Tuple<Point, Point>(line.Item2, line.Item1);
        }

        private void UpdateRectangles(Tuple<Point, Point> line)
        {
            //Get differences
            int dRow = Math.Abs(line.Item2.X - line.Item1.X);
            int dCol = Math.Abs(line.Item2.Y - line.Item1.Y);

            Point diffV = new Point(dCol, dRow);

            //Check existence of parallel lines and side lines in two directions
            for (int i = 0; i < 2; ++i)
            {
                // Flip Difference vector
                diffV.X *= (-1);
                diffV.Y *= (-1);

                //Setup Parallel line
                Point parallelP1 = new Point(line.Item1.X + diffV.X, line.Item1.Y + diffV.Y);
                Point parallelP2 = new Point(line.Item2.X + diffV.X, line.Item2.Y + diffV.Y);
                var parallel = SanitizeLine(new Tuple<Point, Point>(parallelP1, parallelP2));

                //Check if parallel exist
                bool parallelExists = lines.Any(l =>
                {
                    return (l.Item1.Equals(parallel.Item1) && l.Item2.Equals(parallel.Item2));
                });


                //Setup Side line
                var sideOne = SanitizeLine(new Tuple<Point, Point>(line.Item1, parallelP1));
                var sideTwo = SanitizeLine(new Tuple<Point, Point>(line.Item2, parallelP2));

                //Check if side line exist
                bool sidesExist = lines.Any(l =>
                {
                    return l.Item1.Equals(sideOne.Item1) && l.Item2.Equals(sideOne.Item2);
                }) 
                               && lines.Any(l =>
                {
                    return l.Item1.Equals(sideTwo.Item1) && l.Item2.Equals(sideTwo.Item2);
                });

                //All conditions met -> create rectangle
                if (parallelExists && sidesExist)
                {
                    Point[] rectPoints = { line.Item1,line.Item2,parallelP1,parallelP2 };
                    Tuple<Point, Point> rect = PointsToRectangle(rectPoints);
                    rectangles.Add(new Tuple<Point, Point, Player>(rect.Item1, rect.Item2, ActivePlayer));
                }
            }
        }

        private Tuple<Point,Point> PointsToRectangle(Point[] points)
        {
            Point topLeft;
            Point bottomRight;

            topLeft = points[0];
            bottomRight = points[0];
            for(int i = 0; i < points.Length; ++i)
            {
                if (points[i].X + points[i].Y < topLeft.X + topLeft.Y)
                    topLeft = points[i];

                if (points[i].X + points[i].Y > bottomRight.X + bottomRight.Y)
                    bottomRight = points[i];
            }

            return new Tuple<Point, Point>(topLeft,bottomRight);
        }

        private bool IsScored()
        {
            bool scored = rectangles.Count > rectCount;
            if(scored) 
                ++rectCount;

            return scored;
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
    }
}
