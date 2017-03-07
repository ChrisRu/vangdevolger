using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VangDeVolger
{
    public partial class Game : Form
    {
        private Bird _player;
        private const int PlayerSpeed = 3;

        /// <summary>
        /// Initialize game
        /// </summary>
        public Game()
        {
            InitializeComponent();

            _player = new PlayerBird(new Point(50, 50), 3);
            Controls.Add(_player.Pb);
        }

        /// <summary>
        /// Execute code when form has loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        /// <summary>
        /// Execute code every tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Use Player Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            _player.Move(e);
            label2.Text = "Player: X: " + _player.Pb.Location.X + " Y: " + _player.Pb.Location.Y;
        }

        /*

        private void timer1_Tick(object sender, EventArgs e)
        {
            const int speed = 2;
            _playerX = pictureBox1.Location.X;
            _playerY = pictureBox1.Location.Y;

            _enemyX = pictureBox2.Location.X;
            _enemyY = pictureBox2.Location.Y;

            // Check of de player en enemy elkaar raken
            if (!pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                // Check of de enemy ee blok raakt
                if (!pictureBox2.Bounds.IntersectsWith(pictureBox3.Bounds))
                {
                    if (_enemyX < _playerX)
                    {
                        _prevEnemyX = _enemyX;
                        _enemyX += speed;
                    }
                    if (_enemyX > _playerX)
                    {
                        _prevEnemyX = _enemyX;
                        _enemyX -= speed;
                    }
                    if (_enemyY < _playerY)
                    {
                        _prevEnemyY = _enemyY;
                        _enemyY += speed;
                    }
                    if (_enemyY > _playerY)
                    {
                        _prevEnemyY = _enemyY;
                        _enemyY -= speed;
                    }
                }
                // Vind de korste weg langs het blok naar de speler
                else
                {
                    // Omhoog erlangs
                    if (_playerY < _enemyY)
                    {
                        _enemyY -= speed;
                    }
                    // Omlaag erlangs
                    if (_playerY > _enemyY)
                    {
                        _enemyY += speed;
                    }
                    // dit is kutter dan kut.
                }
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Game over!");
            }
            label1.Text = "Enemy: X: " + _enemyX + " Y: " + _enemyY;
            var p = new Point(_enemyX, _enemyY);
            pictureBox2.Location = p;
        }
        */
    }
}
