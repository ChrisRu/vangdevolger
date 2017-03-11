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
        public static Block[,] Blocks;
        public static List<Bird> Enemies;

        public static Bird Player;
        public const int BlockSize = 32;

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

            WindowWidth = Width;
            WindowHeight = Height;

            Player = new PlayerBird(new Point(0, 0));

            Blocks = RandomGrid(Height / 32, Width / 32);
            Enemies = new List<Bird>();

            CreateEgg();
            CreateFood();
            CreateStopwatch();

            Render();

            Blocks[14, 14] = new BlockSolid(14, 14);
            Blocks[5, 5] = new BlockSolid(5, 5);
        }

        /// <summary>
        /// Returns a random list of blocks
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public Block[,] RandomGrid(int height, int width)
        {
            var blocks = new Block[height, width];

            var random = new Random();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var chance = random.Next(100);

                    if (y == 0 && x == 0) continue;

                    if (chance <= 5)
                    {
                        Block block = new BlockSolid(x, y);
                        blocks[y, x] = block;
                    }
                    else if (chance <= 25)
                    {
                        Block block = new BlockMovable(x, y);
                        blocks[y, x] = block;
                    }
                }
            }

            /*
            // TODO: Onnodige for loop bullshit
            for (var i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != null)
                {
                    blocks[i] = SetSiblingBlocks(blocks, blocks[i]);
                }
            }
            */

            return blocks;
        }

        /*
        /// <summary>
        /// Checks around the block and adds sibling properties to the block
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="mainBlock"></param>
        /// <returns></returns>
        // TODO: Onnodige bullshit
        public Block SetSiblingBlocks(Block[][] blocks, Block mainBlock)
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
        */

        /// <summary>
        /// Add all elements to the Controls
        /// </summary>
        public void Render()
        {
            foreach (var block in Blocks)
            {
                if (block != null)
                {
                    Controls.Add(block.Pb);
                }
            }
            Controls.Add(Player.Pb);
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
            menuStrip1.Hide();
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
            }
        }

        /// <summary>
        /// Use Player Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            Player.Move(e);

            if (e.KeyCode == Keys.Escape)
            {
                if (!menuStrip1.Visible)
                {
                    menuStrip1.Show();
                }
                else
                {
                    menuStrip1.Hide();
                }
            }
        }

        public Point RandomOpenPosition()
        {
            while (true)
            {
                var random = new Random();
                var randomX = random.Next(0, Blocks.GetLength(0));
                var randomY = random.Next(0, Blocks.GetLength(1));
                var location = new Point(randomX, randomY);

                var tempPb = new Rectangle
                {
                    Location = new Point(location.X * 32, location.Y * 32),
                    Size = new Size(BlockSize, BlockSize)
                };

                var noIntersects = true;
                if (tempPb.IntersectsWith(Player.Pb.Bounds))
                {
                    noIntersects = false;
                }

                foreach(var enemy in Enemies)
                {
                    if (tempPb.IntersectsWith(enemy.Pb.Bounds))
                    {
                        noIntersects = false;
                    }
                }
                
                foreach (var block in Blocks)
                {
                    if (block != null)
                    {
                        if (block.Pb.Bounds.IntersectsWith(tempPb))
                        {
                            noIntersects = false;
                        }
                    }
                }

                if (noIntersects)
                {
                    return location;
                }
            }
        }

        public void CreateFood()
        {
            var location = RandomOpenPosition();
            var food = new BlockFood(location.X, location.Y);
            Blocks[location.Y, location.X] = food;
            Controls.Add(food.Pb);
        }

        public void CreateStopwatch()
        {
            if (Enemies.Count > 0)
            {
                var location = RandomOpenPosition();
                var stopwatch = new BlockStopwatch(location.X, location.Y);
                Blocks[location.Y, location.X] = stopwatch;
                Controls.Add(stopwatch.Pb);
            }
        }

        public void CreateEgg()
        {
            var location = RandomOpenPosition();
            var egg = new BlockEgg(location.X, location.Y);
            egg.SpawnBird += CreateEnemy;
            Blocks[location.Y, location.X] = egg;
            Controls.Add(egg.Pb);
        }

        public void CreateEnemy(object sender, EventArgs e)
        {
            var block = (Block)sender;

            if (block != null)
            {
                var enemy = new EnemyBird(block.Pb.Location);
                Enemies.Add(enemy);
                Controls.Add(enemy.Pb);
            }
        }

        /// <summary>
        /// Random Item Spawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer2_Tick(object sender, EventArgs e)
        {
            var randomNumber = new Random().Next(0, 15);
            switch (randomNumber)
            {
                case 0:
                    CreateEgg();
                    break; // egg
                case 1:
                    CreateFood();
                    break; // food
                case 2:
                    CreateStopwatch();
                    break; // stopwatch
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vang De Vogel is made by:\n\nLeon Hubert\nChristian Ruigrok", "About");
        }

        private void HowToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Movement: \n\tPress the arrow keys.\n\nHow to get score:\n\tCapture red birds by moving blocks.\n\nPick up items for temporary boosts and destroy eggs to prevent new birds from spawning.\n\nGood luck!", "How to play");
        }
    }
}
