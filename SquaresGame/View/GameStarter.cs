using SquaresGame.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SquaresGame.View
{
    public partial class GameStarter : Form
    {
        #region Fields
        //======== Fields ========//
        private int fieldSize;
        private Player[] players;
        private String p1Name;
        private Color p1Color;

        private String p2Name;
        private Color p2Color;
        #endregion

        #region Events
        //======== Events ========//
        public event EventHandler<Tuple<Player[],int>> PlayerCreated;
        #endregion

        #region Constructor
        //======== CTOR ========//
        public GameStarter()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void GameStarter_Load(object sender, EventArgs e)
        {
            fieldSize = 3;
            radio3.Checked = true;
            players = new Player[2];
            p1Color = Color.Green;
            p2Color = Color.Blue;
            p1ColorBtn.BackColor = p1Color;
            p2ColorBtn.BackColor = p2Color;
            p1NameEdit.Text = "PlayerOne";
            p2NameEdit.Text = "PlayerTwo";
        }

        private void p1ColorBtn_Click(object sender, EventArgs e)
        {
            setPlayerColor(p1ColorBtn, ref p1Color);
        }

        private void p2ColorBtn_Click(object sender, EventArgs e)
        {
            setPlayerColor(p2ColorBtn, ref p2Color);
        }

        private void setPlayerColor(Button colorBtn,ref Color c)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            c = colorDialog.Color;
            colorBtn.BackColor = c;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            //Setup players
            p1Name = p1NameEdit.Text;
            p2Name = p2NameEdit.Text;
            players[0] = new Player(p1Name, p1Color);
            players[1] = new Player(p2Name, p2Color);

            if(!CheckPlayersValidity(players[0],players[1]))
            {
                ToolTip toolTip = new ToolTip();
                toolTip.SetToolTip(okBtn, "Wrong player data");
                return;
            }


            //Broadcast event
            OnPlayersCreated(this, players);

            //Close
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool CheckPlayersValidity(Player p1, Player p2)
        {
            return !String.IsNullOrEmpty(p1.PlayerName) && !String.IsNullOrEmpty(p2.PlayerName)
              &&   !p1.PlayerName.Equals(p2.PlayerName) && !p1.PlayerColor.Equals(p2.PlayerColor);
        }
        private void radio3_CheckedChanged(object sender, EventArgs e)
        {
            fieldSize = 3;
        }

        private void radio5_CheckedChanged(object sender, EventArgs e)
        {
            fieldSize = 5;
        }

        private void radio9_CheckedChanged(object sender, EventArgs e)
        {
            fieldSize = 9;
        }
        #endregion

        #region Event Senders
        //======= Event senders =======//
        protected virtual void OnPlayersCreated(object sender, Player[] players) 
            => PlayerCreated?.Invoke(this, new Tuple<Player[], int>(players,fieldSize));
        #endregion
    }
}
