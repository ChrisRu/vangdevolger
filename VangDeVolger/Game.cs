using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using VangDeVolger.Elements;

namespace VangDeVolger
{
    public partial class Game : Form
    {
        private readonly int _size = 20;
        private readonly int _scale = 32;
        public Level GameLevel { get; set; }
        public SoundPlayer Snd { get; set; }
        public int EnemyMoveInterval = 500;

        /// <summary>
        /// Initialize game
        /// </summary>
        public Game()
        {
            InitializeComponent();

            GameLevel = new Level(Controls, _size, _scale, menuStrip1.Height);
            ClientSize = new Size(_size * _scale, _size * _scale + menuStrip1.Height);

            Snd = new SoundPlayer(Properties.Resources.LoopyMusic);
        }


        /// <summary>
        /// Execute code when form has loaded
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">Arguments given by form</param>
        private void Game_Load(object sender, EventArgs e)
        {
            Snd.PlayLooping();
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

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            GameLevel.KeyDown(e);

            // Toggle Menu
            if (e.KeyCode == Keys.Escape)
            {
                if (menuStrip1.Visible)
                {
                    GameLevel.Paused = false;
                    pauseToolStripMenuItem.Checked = false;
                }
                else
                {
                    GameLevel.Paused = true;
                    pauseToolStripMenuItem.Checked = true;
                }
            }
        }

        private void OffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Snd.Stop();
        }

        private void OnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Snd.PlayLooping();
        }

        private void EasyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EnemyMoveInterval = 700;
        }

        private void MediumToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EnemyMoveInterval = 500;
        }

        private void HardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EnemyMoveInterval = 300;
        }
        private void Game_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                GameLevel.Paused = true;
                pauseToolStripMenuItem.Checked = true;
            }
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GameLevel.Paused)
            {
                GameLevel.Paused = false;
                pauseToolStripMenuItem.Checked = false;
            }
            else
            {
                GameLevel.Paused = true;
                pauseToolStripMenuItem.Checked = true;
            }
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLevel.Paused = true;
            pauseToolStripMenuItem.Checked = true;

            var gridSizeInput = Microsoft.VisualBasic.Interaction.InputBox("Set a grid size:", "Grid size", "16");
            int gridSize;
            try
            {
                gridSize = Convert.ToInt32(gridSizeInput);
            }
            catch (Exception error)
            {
                Console.Write(error);
                gridSize = _size;
            }

            foreach (Spot spot in GameLevel.Grid)
            {
                if (spot.Element?.Pb != null)
                {
                    Controls.Remove(spot.Element.Pb);
                }
            }

            GameLevel = new Level(Controls, gridSize, _scale, menuStrip1.Height);
            ClientSize = new Size(gridSize * _scale, gridSize * _scale + menuStrip1.Height);

            GameLevel.Paused = false;
            pauseToolStripMenuItem.Checked = false;
        }
    }
}
