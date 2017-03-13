using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Birds;
using VangDeVolger.Blocks;

namespace VangDeVolger
{
    public partial class Game : Form
    {
        public static int WindowWidth;
        public static int WindowHeight;

        public Level GameLevel;

        public const int BlockSize = 32;

        public enum Directions
        {
            Up, Down, Left, Right
        }

        public static Size EnumToSize(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    return new Size(0, -BlockSize);
                case Directions.Down:
                    return new Size(0, BlockSize);
                case Directions.Left:
                    return new Size(-BlockSize, 0);
                case Directions.Right:
                    return new Size(BlockSize, 0);
                default:
                    return new Size(0, 0);
            }
        }

        /// <summary>
        /// Initialize game
        /// </summary>
        public Game()
        {
            InitializeComponent();

            WindowWidth = Width;
            WindowHeight = Height;

            GameLevel = new Level(Controls);
        }


        /// <summary>
        /// Use Player Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            Level.Player.Move(e);

            if (e.KeyCode == Keys.Escape)
            {
                if (!menuStrip1.Visible)
                {
                    menuStrip1.Show();
                }
                else
                {
                    menuStrip1.Hide();
                }
            }
        }


        /// <summary>
        /// Execute code when form has loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            menuStrip1.Hide();
        }

        private void Timer1_Tick(object sender, EventArgs e) => GameLevel.MoveEnemy(sender, e);

        private void Timer2_Tick(object sender, EventArgs e) => GameLevel.CreateRandomBlock(sender, e);
        
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vang De Vogel is made by:\n\nLeon Hubert\nChristian Ruigrok", "About");
        }

        private void HowToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Movement: \n\tPress the arrow keys.\n\nHow to get score:\n\tCapture red birds by moving blocks.\n\nPick up items for temporary boosts and destroy eggs to prevent new birds from spawning.\n\nGood luck!", "How to play");
        }
    }
}
