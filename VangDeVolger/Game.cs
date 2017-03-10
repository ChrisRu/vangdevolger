using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
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
        public static List<Bird> Enemies;

        public static Bird _player;
        public const int BlockSize = 32;

        //public Bitmap bmp;

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

            _player = new PlayerBird(new Point(0, 0));

            Blocks = RandomGrid(Height, Width, BlockSize);
            Enemies = new List<Bird>();
            CreateEgg();
            CreateFood();
            CreateStopwatch();

            //bmp = new Bitmap(WindowWidth, WindowHeight);

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

            // TODO: Onnodige for loop bullshit
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

        // TODO: Onnodige bullshit
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
            //using (var graphics = Graphics.FromImage(bmp))
            //{
                foreach (var block in Blocks)
                {
                    Controls.Add(block.Pb);
                    //graphics.DrawImage(block.Pb.Image, block.Pb.Location.X, block.Pb.Location.Y, block.Pb.Width,
                    //    block.Pb.Height);

                }
                //graphics.DrawImage(_player.Pb.Image, _player.Pb.Location.X, _player.Pb.Location.Y, _player.Pb.Width,
                //        _player.Pb.Height);
                Controls.Add(_player.Pb);
            //}
        }

        /// <summary>
        /// Execute code when form has loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
        }

        /// <summary>
        /// Execute code every tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Move(new KeyEventArgs(new Keys()));
                label1.Text = "Enemy: X: " + enemy.Pb.Location.X + "Y :" + enemy.Pb.Location.X;
            }
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

        private void CreateEgg()
        {
            while (true)
            {
                var random = new Random();
                var randomX = random.Next(0, WindowWidth / BlockSize) * BlockSize;
                var randomY = random.Next(0, WindowHeight / BlockSize) * BlockSize;
                var location = new Point(randomX, randomY);

                var tempPb = new Rectangle
                {
                    Location = location,
                    Size = new Size(BlockSize, BlockSize)
                };

                if (Blocks.Any(block => tempPb.IntersectsWith(block.Pb.Bounds)) || randomX == 0 && randomY == 0)
                {
                    continue;
                }

                var egg = new BlockEgg(location);
                egg.SpawnBird += CreateEnemy;

                Blocks.Add(egg);
                Controls.Add(egg.Pb);
                break;
            }
        }

        private void CreateFood()
        {
            while (true)
            {
                var random = new Random();
                var randomX = random.Next(0, WindowWidth / BlockSize) * BlockSize;
                var randomY = random.Next(0, WindowHeight / BlockSize) * BlockSize;
                var location = new Point(randomX, randomY);

                var tempPb = new Rectangle
                {
                    Location = location,
                    Size = new Size(BlockSize, BlockSize)
                };

                if (Blocks.Any(block => tempPb.IntersectsWith(block.Pb.Bounds)) || randomX == 0 && randomY == 0)
                {
                    continue;
                }

                var food = new BlockFood(location);
                Blocks.Add(food);
                Controls.Add(food.Pb);
                break;
            }
        }

        private void CreateStopwatch()
        {
            while (true)
            {
                var random = new Random();
                var randomX = random.Next(0, WindowWidth / BlockSize) * BlockSize;
                var randomY = random.Next(0, WindowHeight / BlockSize) * BlockSize;
                var location = new Point(randomX, randomY);

                var tempPb = new Rectangle
                {
                    Location = location,
                    Size = new Size(BlockSize, BlockSize)
                };

                if (Blocks.Any(block => tempPb.IntersectsWith(block.Pb.Bounds)) || randomX == 0 && randomY == 0)
                {
                    continue;
                }

                var stopwatch = new BlockStopwatch(location);
                Blocks.Add(stopwatch);
                Controls.Add(stopwatch.Pb);
                break;
            }
        }

        public void CreateEnemy(object sender, EventArgs e)
        {
            var block = (Block) sender;

            if (block == null) return;

            var enemy = new EnemyBird(block.Pb.Location);
            Enemies.Add(enemy);
            Controls.Add(enemy.Pb);
        }

        /// <summary>
        /// Random Item Spawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer2_Tick(object sender, EventArgs e)
        {
            var rnd = new Random();
            var chance = rnd.Next(1, 5);

            if (chance != 3) return;

            var choice = rnd.Next(1, 4); // kies een van de drie mogelijke item spawns random
            switch (choice)
            {
                case 1: CreateEgg(); break; // egg
                case 2: CreateFood(); break; // food
                case 3: CreateStopwatch(); break; // stopwatch
            }
        }
    }
}
