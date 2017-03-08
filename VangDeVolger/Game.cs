using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Birds;
using VangDeVolger.Blocks;

namespace VangDeVolger
{
    public partial class Game : Form
    {
        public static int WindowWidth;
        public static int WindowHeight;
        public static List<Block> Blocks;

        private readonly Bird _player;
        public const int PlayerSpeed = 3;
        public const int BlockSize = 32;
        public const int BirdSize = 28;

        public enum Directions
        {
            Up, Down, Left, Right
        }

        public static Size EnumToSize(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    return new Size(0, -BlockSize);
                case Directions.Down:
                    return new Size(0, BlockSize);
                case Directions.Left:
                    return new Size(-BlockSize, 0);
                case Directions.Right:
                    return new Size(BlockSize, 0);
                default:
                    return new Size(0, 0);
            }
        }

        /// <summary>
        /// Initialize game
        /// </summary>
        public Game()
        {
            InitializeComponent();

            WindowWidth = this.Width;
            WindowHeight = this.Height;

            _player = new PlayerBird(new Point(0, 0), PlayerSpeed);

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
                        block = new BlockSolid(new Point(x, y));
                    }
                    else if (chance <= 25)
                    {
                        block = new BlockMovable(new Point(x, y));
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
            var egg = new BlockEgg(location);
            Blocks.Add(egg);
            Controls.Add(egg.TimeLabel);
        }
    }
}
