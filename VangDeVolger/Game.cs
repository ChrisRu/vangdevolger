using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger
{
    public partial class Game : Form
    {
        public Level GameLevel;

        /// <summary>
        /// Initialize game
        /// </summary>
        public Game()
        {
            InitializeComponent();

            GameLevel = new Level(Controls, Width, Height, 32);
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

        /// <summary>
        /// Execute code every tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Use Player Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            GameLevel.KeyDown(e);

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
