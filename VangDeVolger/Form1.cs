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
        public Form1()
        {
            InitializeComponent();
        }

        private int _enemyX;
        private int _enemyY;

        private int _playerX;
        private int _playerY;

        private int _prevPlayerX;
        private int _prevPlayerY;

        private int _prevEnemyX;
        private int _prevEnemyY;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test 1");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test 2");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

            _prevPlayerX = pictureBox1.Location.X;
            _prevPlayerY = pictureBox1.Location.Y;

            _prevEnemyX = pictureBox2.Location.X;
            _prevEnemyY = pictureBox2.Location.Y;
        }

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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            const int speed = 2;

            _playerX = pictureBox1.Location.X;
            _playerY = pictureBox1.Location.Y;

            // Check of de speler het blok raakt
            if (!pictureBox1.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                if (e.KeyCode.Equals(Keys.Down))
                {
                    _prevPlayerY = _playerY;
                    _playerY += speed;
                }
                if (e.KeyCode.Equals(Keys.Up))
                {
                    _prevPlayerY = _playerY;
                    _playerY -= speed;
                }
                if (e.KeyCode.Equals(Keys.Left))
                {
                    _prevPlayerX = _playerX;
                    _playerX -= speed;
                }
                if (e.KeyCode.Equals(Keys.Right))
                {
                    _prevPlayerX = _playerX;
                    _playerX += speed;
                }
            }
            else
            {
                _playerX = _prevPlayerX;
                _playerY = _prevPlayerY;
            }

            label2.Text = "Player: X: " + _playerX + " Y: " + _playerY;

            pictureBox1.Location = new Point(_playerX, _playerY);
        }
    }
}
