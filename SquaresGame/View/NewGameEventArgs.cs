using System;

namespace SquaresGame
{
    public class NewGameEventArgs : EventArgs
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public NewGameEventArgs(Player p1, Player p2) : base()
        {
            PlayerOne = p1;
            PlayerTwo = p2;
        }
    }
}