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

            _playerX = pictureBox1.Location.X;
            _playerY = pictureBox1.Location.Y;

            _enemyX = pictureBox2.Location.X;
            _enemyY = pictureBox2.Location.Y;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            const int speed = 2;
            _playerX = pictureBox1.Location.X;
            _playerY = pictureBox1.Location.Y;

            _enemyX = pictureBox2.Location.X;
            _enemyY = pictureBox2.Location.Y;

            if (!pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds)) {
                if (_enemyX < _playerX)
                {
                    _enemyX += speed;
                }
                if (_enemyX > _playerX)
                {
                    _enemyX -= speed;
                }
                if (_enemyY < _playerY)
                {
                    _enemyY += speed;
                }
                if (_enemyY > _playerY)
                {
                    _enemyY -= speed;
                }
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Game over!");
            }
            var p = new Point(_enemyX, _enemyY);
            pictureBox2.Location = p;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            const int speed = 3;
            _prevPlayerX = _playerX;
            _prevPlayerY = _playerY;

            _playerX = pictureBox1.Location.X;
            _playerY = pictureBox1.Location.Y;

            if (!pictureBox1.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                if (e.KeyCode.Equals(Keys.Down))
                {
                    _playerY += speed;
                }
                if (e.KeyCode.Equals(Keys.Up))
                {
                    _playerY -= speed;
                }
                if (e.KeyCode.Equals(Keys.Left))
                {
                    _playerX -= speed;
                }
                if (e.KeyCode.Equals(Keys.Right))
                {
                    _playerX += speed;
                }
            }
            else
            {
                _playerX = _prevPlayerX;
                _playerY = _prevPlayerY;
            }

            pictureBox1.Location = new Point(_playerX, _playerY);
        }
    }

       
}
