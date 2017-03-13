using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VangDeVolger.Birds;
using VangDeVolger.Blocks;

namespace VangDeVolger
{
    public class Level
    {
        public Control.ControlCollection Controls;
        public static List<Block> Blocks;
        public static List<Bird> Enemies;

        public static Bird Player;

        public Level(Control.ControlCollection controls)
        {
            Controls = controls;

            Player = new PlayerBird(new Point(0, 0));

            Blocks = RandomGrid(Game.WindowHeight, Game.WindowWidth, Game.BlockSize);
            Enemies = new List<Bird>();

            CreateEgg();
            CreateFood();
            CreateStopwatch();

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

                    if (y == 0 && x == 0) continue;

                    if (chance <= 5)
                    {
                        Block block = new BlockSolid(new Point(x, y));
                        blocks.Add(block);
                    }
                    else if (chance <= 25)
                    {
                        Block block = new BlockMovable(new Point(x, y));
                        blocks.Add(block);
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
        */

        /// <summary>
        /// Add all elements to the Controls
        /// </summary>
        public void Render()
        {
            foreach (var block in Blocks)
            {
                Controls.Add(block.Pb);
            }
            Controls.Add(Player.Pb);
        }

        /// <summary>
        /// Execute code every tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MoveEnemy(object sender, EventArgs e)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Move(new KeyEventArgs(new Keys()));
            }
        }


        public Point RandomOpenPosition()
        {
            while (true)
            {
                var random = new Random();
                var randomX = random.Next(0, Game.WindowWidth / Game.BlockSize) * Game.BlockSize;
                var randomY = random.Next(0, Game.WindowHeight / Game.BlockSize) * Game.BlockSize;
                var location = new Point(randomX, randomY);

                var tempPb = new Rectangle
                {
                    Location = location,
                    Size = new Size(Game.BlockSize, Game.BlockSize)
                };

                var noIntersects = !tempPb.IntersectsWith(Player.Pb.Bounds);

                foreach (var enemy in Enemies)
                {
                    if (tempPb.IntersectsWith(enemy.Pb.Bounds))
                    {
                        noIntersects = false;
                    }
                }

                foreach (var block in Blocks)
                {
                    if (block.Pb.Bounds.IntersectsWith(tempPb))
                    {
                        noIntersects = false;
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
            var food = new BlockFood(RandomOpenPosition());
            Blocks.Add(food);
            Controls.Add(food.Pb);
        }

        public void CreateStopwatch()
        {
            if (Enemies.Count > 0)
            {
                var stopwatch = new BlockStopwatch(RandomOpenPosition());
                Blocks.Add(stopwatch);
                Controls.Add(stopwatch.Pb);
            }
        }

        public void CreateEgg()
        {
            var egg = new BlockEgg(RandomOpenPosition());
            egg.SpawnBird += CreateEnemy;
            Blocks.Add(egg);
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
        public void CreateRandomBlock(object sender, EventArgs e)
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
    }
}
