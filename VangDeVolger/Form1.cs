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

        int enemy_x;
        int enemy_y;

        int player_x;
        int player_y;

        int prev_player_x;
        int prev_player_y;

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

            prev_player_x = pictureBox1.Location.X;
            prev_player_y = pictureBox1.Location.Y;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            player_x = pictureBox1.Location.X;
            player_y = pictureBox1.Location.Y;

            enemy_x = pictureBox2.Location.X;
            enemy_y = pictureBox2.Location.Y;

            if (!pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds)) {
                if (enemy_x < player_x)
                {
                    enemy_x += 2;
                }
                if (enemy_x > player_x)
                {
                    enemy_x -= 2;
                }
                if (enemy_y < player_y)
                {
                    enemy_y += 2;
                }
                if (enemy_y > player_y)
                {
                    enemy_y -= 2;
                }
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Game over!");
            }
            Point p = new Point(enemy_x, enemy_y);
            pictureBox2.Location = p;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            player_x = pictureBox1.Location.X;
            player_y = pictureBox1.Location.Y;

            if (!pictureBox1.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                if (e.KeyCode.Equals(Keys.Down))
                {
                    prev_player_y = player_y;
                    player_y = player_y + 3;
                }
                if (e.KeyCode.Equals(Keys.Up))
                {
                    prev_player_y = player_y;
                    player_y = player_y - 3;
                }
                if (e.KeyCode.Equals(Keys.Left))
                {
                    prev_player_x = player_x;
                    player_x = player_x - 3;
                }
                if (e.KeyCode.Equals(Keys.Right))
                {
                    prev_player_x = player_x;
                    player_x = player_x + 3;
                }
            }
            else
            {
                player_x = prev_player_x;
                player_y = prev_player_y;
            }

            pictureBox1.Location = new Point(player_x, player_y);
        }
    }

       
}
