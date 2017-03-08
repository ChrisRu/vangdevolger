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
        public static int WindowWidth;
        public static int WindowHeight;

        private readonly Bird _player;
        public const int PlayerSpeed = 3;
        public const int BlockSize = 32;
        public const int BirdSize = 28;

        public List<Block> Blocks;

        /// <summary>
        /// Initialize game
        /// </summary>
        public Game()
        {
            InitializeComponent();

            WindowWidth = this.Width;
            WindowHeight = this.Height;

            _player = new PlayerBird(new Point(0, 0), 3);
            
            Blocks = RandomGrid(Height, Width, BlockSize);
            createEgg();

            Render();
        }

        /// <summary>
        /// Returns a random list of blocks
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="increase"></param>
        /// <returns></returns>
        public List<Block> RandomGrid(int height, int width, int increase)
        {
            var blocks = new List<Block>();
            var random = new Random();

            for (var y = 0; y < height; y += increase)
            {
                for (var x = 0; x < width; x += increase)
                {
                    var chance = random.Next(100);
                    Block block = null;

                    if (y == 0 && x == 0)
                    {
                        break;
                    }

                    if (chance <= 5)
                    {
                        block = new BlockSolid(new Point(x, y), ref Blocks);
                    }
                    else if (chance <= 25)
                    {
                        block = new BlockMovable(new Point(x, y), ref Blocks);
                    }

                    if (block != null)
                    {
                        blocks.Add(block);
                    }
                }
            }

            for (var i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != null)
                {
                    blocks[i] = SetSiblingBlocks(blocks, blocks[i]);
                }
            }

            return blocks;
        }

        /// <summary>
        /// Checks around the block and adds sibling properties to the block
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="mainBlock"></param>
        /// <returns></returns>
        public Block SetSiblingBlocks(List<Block> blocks, Block mainBlock)
        {
            var x = mainBlock.Pb.Location.X;
            var y = mainBlock.Pb.Location.Y;

            var topSibling =
                blocks.FirstOrDefault(
                    block => (block.Pb.Location.X == x && block.Pb.Location.Y == y - BlockSize));

            var bottomSibling =
                blocks.FirstOrDefault(
                    block => (block.Pb.Location.X == x && block.Pb.Location.Y == y + BlockSize));

            var leftSibling =
                blocks.FirstOrDefault(
                    block => (block.Pb.Location.X == x - BlockSize && block.Pb.Location.Y == y));

            var rightSibling =
                blocks.FirstOrDefault(
                    block => (block.Pb.Location.X == x + BlockSize && block.Pb.Location.Y == y));

            mainBlock.SiblingTop = topSibling;
            mainBlock.SiblingBottom = bottomSibling;
            mainBlock.SiblingLeft = leftSibling;
            mainBlock.SiblingRight = rightSibling;

            return mainBlock;
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
        private void Timer1_Tick(object sender, EventArgs e)
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

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void createEgg()
        {
            var location = new Point(220, 220);
            foreach (var block in Blocks)
            {
                if (block.Pb.Location.X == location.X && block.Pb.Location.Y == location.Y)
                {
                    return;
                }
            }
            var egg = new BlockEgg(location, ref Blocks);
            Blocks.Add(egg);
            Controls.Add(egg.TimeLabel);
            //egg.Pb.Controls.Add(egg.TimeLabel);
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
