using System;
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

            GameLevel = new Level(Controls, Width, Height);
        }


        /// <summary>
        /// Execute code when form has loaded
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">Arguments given by form</param>
        private void Game_Load(object sender, EventArgs e)
        {
            menuStrip1.Hide();
        }

        /// <summary>
        /// Show About Messagebox
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">Arguments given by form</param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vang De Vogel is made by:\n\nLeon Hubert\nChristian Ruigrok", "About");
        }

        /// <summary>
        /// Show How To Play Messagebox
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">Arguments given by form</param>
        private void HowToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Movement: \n\tPress the arrow keys.\n\nHow to get score:\n\tCapture red birds by moving blocks.\n\nPick up items for temporary boosts and destroy eggs to prevent new birds from spawning.\n\nGood luck!", "How to play");
        }

        /// <summary>
        /// Execute on KeyDown
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">KeyData</param>
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            GameLevel.KeyDown(e);

            // Toggle Menu
            if (e.KeyCode == Keys.Escape)
            {
                Level.Paused = !Level.Paused;

                if (menuStrip1.Visible)
                {
                    menuStrip1.Hide();
                }
                else
                {
                    menuStrip1.Show();
                }
            }
        }
    }
}
