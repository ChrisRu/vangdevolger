using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VangDeVolger.Birds;
using VangDeVolger.Blocks;

namespace VangDeVolger
{
    public partial class Game : Form
    {
        private readonly Bird _player;
        public const int PlayerSpeed = 3;
        public const int BlockSize = 32;
        public const int BirdSize = 28;

        public List<Block> Blocks = new List<Block>();

        /// <summary>
        /// Initialize game
        /// </summary>
        public Game()
        {
            InitializeComponent();

            _player = new PlayerBird(new Point(0, 0), 3);

            _createGrid();
            Render();
        }

        /// <summary>
        /// Create the blocks on the grid
        /// </summary>
        private void _createGrid()
        {
            var tempGrid = new List<Block>();
            var random = new Random();
            for (var y = 0; y < this.Height; y += BlockSize)
            {
                for (var x = 0; x < this.Width; x += BlockSize)
                {
                    var chance = random.Next(100);
                    Block block = null;

                    if (y == 0 && x == 0)
                    {
                        break;
                    }

                    if (chance <= 5)
                    {
                        block = new BlockSolid(new Point(x, y));
                    }
                    else if (chance <= 25)
                    {
                        block = new BlockMoveable(new Point(x, y));
                    }

                    if (block != null)
                    {
                        tempGrid.Add(block);
                    }
                }
            }

            foreach (var block in tempGrid)
            {
                Blocks.Add(_setSiblingBlocks(block));
            }
        }

        /// <summary>
        /// Checks around the block and adds sibling properties to the block
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private Block _setSiblingBlocks(Block block)
        {
            var x = block.Pb.Location.X;
            var y = block.Pb.Location.Y;

            var topSibling =
                Blocks.FirstOrDefault(
                    blocker => (blocker.Pb.Location.X == x && blocker.Pb.Location.Y == y - BlockSize));

            var bottomSibling =
                Blocks.FirstOrDefault(
                    blocker => (blocker.Pb.Location.X == x && blocker.Pb.Location.Y == y + BlockSize));

            var leftSibling =
                Blocks.FirstOrDefault(
                    blocker => (blocker.Pb.Location.X == x - BlockSize && blocker.Pb.Location.Y == y));

            var rightSibling =
                Blocks.FirstOrDefault(
                    blocker => (blocker.Pb.Location.X == x + BlockSize && blocker.Pb.Location.Y == y));

            block.SiblingTop = topSibling;
            block.SiblingBottom = bottomSibling;
            block.SiblingLeft = leftSibling;
            block.SiblingRight = rightSibling;

            return block;
        }

        /// <summary>
        /// Add all elements to the Controls
        /// </summary>
        public void Render()
        {
            foreach (var block in Blocks)
            {
                Controls.Add(block.Pb);
            }

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
            _player.Move(ref Blocks, e);
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
