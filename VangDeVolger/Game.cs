using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.PathFinding;

namespace VangDeVolger
{
    public partial class Game : Form
    {
        public Level GameLevel { get; set; }
        public SoundPlayer SoundPlayer { get; set; }
        public int EnemyMoveInterval = 500;
        private bool _soundPlaying;
        private int _size = 16;
        private int _scale = 32;

        /// <summary>
        /// Initialize game
        /// </summary>
        public Game()
        {
            InitializeComponent();

            GameLevel = new Level(Controls, _size, _scale, menuStrip1.Height);
            ClientSize = new Size(_size * _scale, _size * _scale + menuStrip1.Height);

            SoundPlayer = new SoundPlayer(Properties.Resources.LoopyMusic);
        }


        /// <summary>
        /// Execute code when form has loaded
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">Arguments given by form</param>
        private void Game_Load(object sender, EventArgs e)
        {
            SoundPlayer.PlayLooping();
            _soundPlaying = true;
            musicToolStripMenuItem.Checked = true;
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

        private void UpdateScale(int scale)
        {
            _scale = scale;
            for (int y = 0; y < GameLevel.Grid.GetLength(0); y++)
            {
                for (int x = 0; x < GameLevel.Grid.GetLength(1); x++)
                {
                    GameLevel.Grid[x, y].Scale = _scale;
                    if (GameLevel.Grid[x, y].Element != null)
                    {
                        GameLevel.Grid[x, y].Element.Pb.Size = new Size(GameLevel.Grid[x, y].Scale, GameLevel.Grid[x, y].Scale);
                        GameLevel.Grid[x, y].Element.Pb.Location = new Point(GameLevel.Grid[x, y].Scale * x, GameLevel.Grid[x, y].Scale * y + menuStrip1.Height);
                    }
                }
            }
            ClientSize = new Size(_size * _scale, _size * _scale + menuStrip1.Height);
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            // Ctrl + = = Zoom * 1.1
            if (e.KeyValue == 187 && e.Modifiers == Keys.Control)
            {
                UpdateScale((int)(_scale * 1.1));
            }

            // Ctrl + - = Zoom / 1.1
            if (e.KeyCode == Keys.OemMinus && e.Modifiers == Keys.Control)
            {
                UpdateScale((int)(_scale / 1.1));
            }

            // Ctrl + 0 = Reset Zoom
            if (e.KeyCode == Keys.D0 && e.Modifiers == Keys.Control)
            {
                UpdateScale(32);
            }

            // Escape / Pause = Pause
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Pause)
            {
                _togglePaused(!GameLevel.Paused);
            }

            // Further KeyCodes
            if (!GameLevel.Paused)
            {
                GameLevel.Player.KeyDown(e);
            }
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
                _togglePaused(false);
                _toggleMusic(false);
            }
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _togglePaused(!GameLevel.Paused);
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _togglePaused(true);

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

            _togglePaused(false);
        }

        private void MusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _toggleMusic(!_soundPlaying);
        }

        private void _toggleMusic(bool play)
        {
            if (!play)
            {
                _soundPlaying = false;
                SoundPlayer.Stop();
                musicToolStripMenuItem.Checked = false;
            }
            else
            {
                _soundPlaying = true;
                SoundPlayer.PlayLooping();
                musicToolStripMenuItem.Checked = true;
            }
        }

        private void _togglePaused(bool play)
        {
            if (!play)
            {
                GameLevel.Paused = false;
                pauseToolStripMenuItem.Checked = false;
                _toggleMusic(true);
                BackColor = Color.White;
            }
            else
            {
                GameLevel.Paused = true;
                pauseToolStripMenuItem.Checked = true;
                _toggleMusic(false);
                BackColor = Color.DimGray;
            }
        }
    }
}
