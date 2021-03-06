﻿using SquaresGame.Model;
using SquaresGame.Persistence;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SquaresGame.View
{
    public partial class GameWindow : Form
    {
        #region Fields
        //=========== Fields ===========//
        private SquaresGameModel model;
        private ISquaresGameDataAccess dataAccess;

        //UI related
        private const float dotRadius = 20.0f;
        private const float lineWidth = 5.0f;
        private Dot[,] dots;

        //Mouse related
        private bool isMouseDown  = false;
        private Dot startClickDot = null;
        private Dot endClickDot   = null;
        #endregion

        #region Constructors
        //=========== Ctors ===========//
        public GameWindow()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, 
                          true);
        }
        #endregion

        #region Methods
        //=========== Methods ===========//
        private void DrawGameField(PaintEventArgs e)
        {
            //Draw Dots
            foreach (var dot in dots)
            {
                float xCoord = dot.xCoord - dotRadius;
                float yCoord = dot.yCoord - dotRadius;
                e.Graphics.FillEllipse(Brushes.Red, xCoord, yCoord, 2 * dotRadius, 2 * dotRadius);
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
        
        private void InitDots(int N)
        {
            dots = new Dot[N,N];
            for (int x = 1; x <= N; ++x)
            {
                for (int y = 1; y <= N; ++y)
                {
                    float xCenter = x * ((float)canvas.Width / (N + 1));
                    float yCenter = y * ((float)canvas.Height / (N + 1));
                    dots[y - 1, x - 1] = new Dot { row = y - 1, col = x - 1, xCoord = xCenter, yCoord = yCenter };
                }
            }
        }

        private Dot GetIntersectedDot(MouseEventArgs e)
        {
            const float delta = dotRadius;
            foreach (Dot dot in dots)
            {
                // Cursor intersects dot
                if(     (dot.xCoord > e.X - delta && dot.xCoord < e.X + delta)
                    &&  (dot.yCoord > e.Y - delta && dot.yCoord < e.Y + delta))
                {
                    return dot;
                }
            }
            return null;
        }

        private async Task LoadGame(String path)
        {
            try
            {
                if (model == null)
                {
                    GameStateWrapper state = await dataAccess.LoadGameAsync(path);
                    model = SquaresGameModel.FromSave(state, dataAccess);
                    NewGame(model);
                }
                else
                {
                    await model.LoadGameAsync(path);
                    p1NameLabel.Text = model.PlayerOne.PlayerName;
                    p2NameLabel.Text = model.PlayerTwo.PlayerName;
                }
                InitDots(model.FieldSize);
                UpdateUI(this, EventArgs.Empty);
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
        #endregion

        #region EventHandlers
        //=========== Events ===========//

        //===== Model event handlers =====//
        private void UpdateUI(object sender, EventArgs e)
        {
            canvas.Invalidate();
            p1PointLabel.Text = model.PlayerOne.Points.ToString();
            p2PointLabel.Text = model.PlayerTwo.Points.ToString();
        }

        private void PlayerWon(object sender, Player p)
        {
            String message;
            if (p == null) message = "Draw";
            else message = String.Format("{0} has won with {1} points", p.PlayerName, p.Points);

            MessageBox.Show(message);
            model.Restart();
            UpdateUI(this, EventArgs.Empty);
        }

        //===== UI Event handlers =====//
        private void GameWindow_Load(object sender, EventArgs e)
        {
            dataAccess = new SquaresGameDataAccess();
        }

        private void newGameBtn_Click(object sender, EventArgs e)
        {
            GameStarter starter = new GameStarter();
            Player[] players = null;
            int fieldSize = 0;

            starter.PlayerCreated += (sender2, args) => { players = args.Item1; fieldSize = args.Item2; };  
            starter.ShowDialog();

            if(starter.DialogResult == DialogResult.OK)
            {
                model = new SquaresGameModel(fieldSize, players[0], players[1], dataAccess);
                NewGame(model);
            }
        }

        private async void saveGameBtn_Click(object sender, EventArgs e)
        {
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await model.SaveGameAsync(saveDialog.FileName);
                    MessageBox.Show("Game saved!");
                }
                catch (Exception excp)
                {
                    MessageBox.Show(excp.Message);
                }
            }
        }

        private async void loadGameBtn_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                await LoadGame(openDialog.FileName);
            }
        }

        private void NewGame(SquaresGameModel model)
        {
            //Subscriptions
            model.UpdateUI += UpdateUI;
            model.EndGame += PlayerWon;

            //Setup UI and refresh
            p1NameLabel.Visible = true;
            p1PointLabel.Visible = true;
            p2NameLabel.Visible = true;
            p2PointLabel.Visible = true;
            p1NameLabel.Text = model.PlayerOne.PlayerName;
            p2NameLabel.Text = model.PlayerTwo.PlayerName;
            saveGameBtn.Enabled = true;

            InitDots(model.FieldSize);
            UpdateUI(this, EventArgs.Empty);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            if(model != null)
            {
                DrawGameField(e);
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

        private async void canvas_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string file = files[0];

            await LoadGame(file);
        }

        private void canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        #endregion

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
