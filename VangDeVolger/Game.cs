
namespace VangDeVolger
{
    using System;
    using System.Drawing;
    using System.Media;
    using System.Windows.Forms;

    using Elements.Birds;

    public partial class Game : Form
    {
        public Level GameLevel { get; set; }

        public SoundPlayer SoundPlayer { get; set; }
        public SoundPlayer SoundEffectPlayer { get; set; }

        private bool _soundPlaying;

        private int _size = 16;

        private int _scale = 32;

        private Color _backColor;

        /// <summary>
        /// Game Class initializes VangDeVolger Game
        /// </summary>
        public Game()
        {
            this.InitializeComponent();

            this._backColor = Color.FromArgb(162, 255, 162);
            this.GameLevel = new Level(this.Controls, this._size, this._scale, this.menuStrip1.Height);
            this.ClientSize = new Size(this._size * this._scale, (this._size * this._scale) + this.menuStrip1.Height);

            this.SoundPlayer = new SoundPlayer(Properties.Resources.LoopyMusic);
        }

        /// <summary>
        /// Execute code when form has loaded
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">Arguments given by form</param>
        private void Game_Load(object sender, EventArgs e)
        {
            this.BackColor = this._backColor;
            this.SoundPlayer.PlayLooping();
            this._soundPlaying = true;
            this.musicToolStripMenuItem.Checked = true;
            this.mediumToolStripMenuItem1.Checked = true;

            this.GameLevel.Enemy.GameEnd += this.StartNewGame;
        }

        /// <summary>
        /// Show About Messagebox
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">Arguments given by form</param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vang De Volger is made by:\n\nLeon Hubert\nChristian Ruigrok", "About");
        }

        /// <summary>
        /// Show How To Play Messagebox
        /// </summary>
        /// <param name="sender">The form</param>
        /// <param name="e">Arguments given by form</param>
        private void HowToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Movement: \n\tPress the arrow keys.\n\nHow to win:\n\tCapture the red bird by moving blocks.\n\nGood luck!", "How to play");
        }

        /// <summary>
        /// Change Scale for Game
        /// </summary>
        /// <param name="scale">Game scale (default 32)</param>
        private void UpdateScale(int scale)
        {
            this._scale = scale;
            for (int y = 0; y < this.GameLevel.Grid.GetLength(0); y++)
            {
                for (int x = 0; x < this.GameLevel.Grid.GetLength(1); x++)
                {
                    this.GameLevel.Grid[x, y].Scale = this._scale;
                    if (this.GameLevel.Grid[x, y].Element != null)
                    {
                        this.GameLevel.Grid[x, y].Element.Pb.Size = new Size(this.GameLevel.Grid[x, y].Scale, this.GameLevel.Grid[x, y].Scale);
                        this.GameLevel.Grid[x, y].Element.Pb.Location = new Point(this.GameLevel.Grid[x, y].Scale * x, (this.GameLevel.Grid[x, y].Scale * y) + this.menuStrip1.Height);
                    }
                }
            }
            this.ClientSize = new Size(this._size * this._scale, (this._size * this._scale) + this.menuStrip1.Height);
        }

        /// <summary>
        /// Execute on Form KeyUp
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">KeyEvent Arguments</param>
        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            // Ctrl + = = Zoom * 1.1
            if (e.KeyValue == 187 && e.Modifiers == Keys.Control)
            {
                this.UpdateScale((int)(this._scale * 1.1));
            }

            // Ctrl + - = Zoom / 1.1
            if (e.KeyCode == Keys.OemMinus && e.Modifiers == Keys.Control)
            {
                this.UpdateScale((int)(this._scale / 1.1));
            }

            // Ctrl + 0 = Reset Zoom
            if (e.KeyCode == Keys.D0 && e.Modifiers == Keys.Control)
            {
                this.UpdateScale(32);
            }

            // Escape / Pause = Pause
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Pause)
            {
                this._togglePaused(!this.GameLevel.Paused);
            }

            // Player Movement
            if (!this.GameLevel.Paused)
            {
                this.GameLevel.Player.KeyDown(e);
            }
        }

        private void EasyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Enemy enemy = this.GameLevel.Enemy;
            enemy.MoveTimer.Interval = 700;
            this.easyToolStripMenuItem1.Checked = true;
            this.mediumToolStripMenuItem1.Checked = false;
            this.hardToolStripMenuItem1.Checked = false;
        }

        private void MediumToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Enemy enemy = this.GameLevel.Enemy;
            enemy.MoveTimer.Interval = 500;
            this.easyToolStripMenuItem1.Checked = false;
            this.mediumToolStripMenuItem1.Checked = true;
            this.hardToolStripMenuItem1.Checked = false;
        }

        private void HardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Enemy enemy = this.GameLevel.Enemy;
            enemy.MoveTimer.Interval = 300;
            this.easyToolStripMenuItem1.Checked = false;
            this.mediumToolStripMenuItem1.Checked = false;
            this.hardToolStripMenuItem1.Checked = true;
        }

        private void Game_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this._togglePaused(false);
                this._toggleMusic(false);
            }
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e) => this._togglePaused(!this.GameLevel.Paused);

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e) => this._restartGame();

        private void MusicToolStripMenuItem_Click(object sender, EventArgs e) => this._toggleMusic(!this._soundPlaying);

        /// <summary>
        /// Restart game and ask for new gridSize
        /// </summary>
        private void _restartGame()
        {
            this._togglePaused(true);

            string gridSizeInput = Microsoft.VisualBasic.Interaction.InputBox("Set a grid size:", "Grid size", "16");
            int gridSize;

            try
            {
                gridSize = Convert.ToInt32(gridSizeInput);
            }
            catch (Exception error)
            {
                Console.Write(error);
                gridSize = this._size;
            }

            foreach (Spot spot in this.GameLevel.Grid)
            {
                if (spot.Element?.Pb != null)
                {
                    this.Controls.Remove(spot.Element.Pb);
                }
            }

            this._size = gridSize;

            this.GameLevel = new Level(this.Controls, gridSize, this._scale, this.menuStrip1.Height);
            this.ClientSize = new Size(gridSize * this._scale, (gridSize * this._scale) + this.menuStrip1.Height);

            this._togglePaused(false);

            this.GameLevel.Enemy.GameEnd += this.StartNewGame;
        }

        /// <summary>
        /// Toggle Music Playing
        /// </summary>
        /// <param name="play">bool to play</param>
        private void _toggleMusic(bool play)
        {
            this._soundPlaying = play;
            this.musicToolStripMenuItem.Checked = play;
            (play ? (Action)this.SoundPlayer.PlayLooping : this.SoundPlayer.Stop)();
        }

        /// <summary>
        /// Toggle Pause for the game
        /// </summary>
        /// <param name="play">bool to pause</param>
        private void _togglePaused(bool play)
        {
            this.GameLevel.Paused = play;
            this.pauseToolStripMenuItem.Checked = play;
            this.BackColor = !play ? this._backColor : Color.DimGray;
        }

        private void ThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog1.Color;
                _backColor = colorDialog1.Color;
            }
        }

        /// <summary>
        /// Ask to start new game or not
        /// </summary>
        /// <param name="sender">Enemy instance</param>
        /// <param name="e">Event Arguments with possible Victory</param>
        public void StartNewGame(bool victory)
        {
            string message;
            string title;
            if (victory)
            {
                this.SoundEffectPlayer = new SoundPlayer(Properties.Resources.yay);
                this.SoundEffectPlayer.Play();
                message = "You won!\nPlay again?";
                title = "Victory!";
            }
            else
            {
                this.SoundEffectPlayer = new SoundPlayer(Properties.Resources.fail);
                this.SoundEffectPlayer.Play();
                message = "You lost!\nTry again?";
                title = "Game Over";
            }

            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _toggleMusic(true);
                this._restartGame();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
