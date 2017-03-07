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
    public partial class Form1 : Form
    {
        private Bird _player;
        private const int _playerSpeed = 3;

        public Form1()
        {
            InitializeComponent();

            _player = new PlayerBird(new Point(50, 50));
            Controls.Add(_player.Pb);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Use Player Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Size direction;

            if (e.KeyCode.Equals(Keys.Down))
            {
                direction = new Size(0, _playerSpeed);
            }
            else if (e.KeyCode.Equals(Keys.Up))
            {
                direction = new Size(0, -_playerSpeed);
            }
            else if (e.KeyCode.Equals(Keys.Left))
            {
                direction = new Size(-_playerSpeed, 0);
            }
            else if (e.KeyCode.Equals(Keys.Right))
            {
                direction = new Size(_playerSpeed, 0);
            }
            else
            {
                direction = new Size(0, 0);
            }

            _player.Pb.Location = Point.Add(_player.Pb.Location, direction);

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
