using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace SquaresGame
{
    public partial class GameWindow : Form
    {
        //=========== Fields ===========//
        private SquareGameModel model;
        const float dotRadius = 20.0f;
        const float lineWidth = 5.0f;
        private Dot[,] dots;

        //Mouse related
        private bool isMouseDown  = false;
        private Dot startClickDot = null;
        private Dot endClickDot   = null;

        //=========== Ctors ===========//
        public GameWindow()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
        }

        //=========== Methods ===========//
        void DrawGameField(PaintEventArgs e, int N, float dotRadius)
        {
            //Draw Points
            for (int x = 1; x <= N; ++x)
            {
                for (int y = 1; y <= N; ++y)
                {
                    float xCenter = x * ((float)canvas.Width / (N + 1));
                    float yCenter = y * ((float)canvas.Height / (N + 1));

                    float xCoord = xCenter - dotRadius;
                    float yCoord = yCenter - dotRadius;

                    dots[y - 1, x - 1] = new Dot { row = y - 1, col = x - 1, xCoord = xCenter, yCoord = yCenter };
                    e.Graphics.FillEllipse(Brushes.Red, xCoord, yCoord, 2 * dotRadius, 2 * dotRadius);
                    e.Graphics.FillRectangle(Brushes.Blue, xCenter, yCenter, 2, 2);
                }
            }

            //Draw Rectangles from model
            foreach (var rect in model.Rectangles)
            {
                Brush brush = new SolidBrush(rect.Item3.PlayerColor);
                float x = dots[rect.Item1.X, rect.Item1.Y].xCoord;
                float y = dots[rect.Item1.X, rect.Item1.Y].yCoord;
                float width  = Math.Abs(dots[rect.Item2.X, rect.Item2.Y].xCoord - x);
                float height = Math.Abs(dots[rect.Item2.X, rect.Item2.Y].yCoord - y);

                e.Graphics.FillRectangle(brush, x, y, width, height);
            }

            //Draw Lines from model
            foreach(var line in model.Lines)
            {
                Pen p = new Pen(line.Item3.PlayerColor,lineWidth);
                e.Graphics.DrawLine(p, (PointF)dots[line.Item1.X, line.Item1.Y], (PointF)dots[line.Item2.X, line.Item2.Y]);
            }
        }

        Dot GetIntersectedDot(MouseEventArgs e)
        {
            const float delta = dotRadius;
            foreach (Dot dot in dots)
            {
                // Click intersects dot
                if(     (dot.xCoord > e.X - delta && dot.xCoord < e.X + delta)
                    &&  (dot.yCoord > e.Y - delta && dot.yCoord < e.Y + delta))
                {
                    return dot;
                }
            }
            return null;
        }

        //=========== Events ===========//
        private void newGameBtn_Click(object sender, EventArgs e)
        {
            //Setup Model and events
            model = new SquareGameModel(3, //FieldSize
                                        new Player("Sanyi",Color.Black), // PlayerOne
                                        new Player("Balázs",Color.Blue)); // PlayerTwo

            //Subscriptions
            model.UpdateUI += (sender2,e2) => canvas.Invalidate();
            model.PlayerWon += PlayerWon;

            //Setup UI and refresh
            p1NameLabel.Text = model.PlayerOne.PlayerName;
            p2NameLabel.Text = model.PlayerTwo.PlayerName;
            saveGameBtn.Enabled = true;

            dots = new Dot[model.FieldSize, model.FieldSize];
            canvas.Invalidate();
        }

        //===== Model events =====//
        private void PlayerWon(object sender, Player p)
        {
            String message;
            if (p == null) message = "Draw";
            else message = String.Format("{0} has won with {1} points", p.PlayerName, p.Points);

            MessageBox.Show(message);
            model.Restart();
            canvas.Invalidate();
        }

        //===== UI Events =====//
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            if(model != null)
            {
                DrawGameField(e, model.FieldSize, dotRadius);
            }
        }

        //===== Mouse events =====//
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (dots != null)
            {
                isMouseDown = true;
                Dot clicked = GetIntersectedDot(e);
                if (clicked != null)
                {
                    startClickDot = clicked;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e){}

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            if(endClickDot == null)
                startClickDot = null;
            else if(startClickDot != null && endClickDot != null)
            {
                model.AddNewLine((Point)startClickDot, (Point)endClickDot);
                startClickDot = null;
                endClickDot   = null;
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (dots != null)
            {
                Dot intersected = GetIntersectedDot(e);
                if (intersected != null) Cursor = Cursors.Hand;
                else Cursor = Cursors.Default;

                if (isMouseDown && startClickDot != null)
                {
                    if (intersected != null)
                        endClickDot = intersected;
                }
            }
        }

        

        protected override CreateParams CreateParams // Form level double buffering
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED
                return cp;
            }
        }
    }

    class Dot
    {
        public int row, col;
        public float xCoord, yCoord;

        public static explicit operator PointF(Dot d) => new PointF(d.xCoord,d.yCoord); //returns coordinates
        public static explicit operator Point (Dot d) => new Point(d.row,d.col);        //return  row & col
    }
}
