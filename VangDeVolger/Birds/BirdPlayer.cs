using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    internal class PlayerBird : Bird
    {
        private readonly Image _imageLeft = Properties.Resources.bird_green_left;
        private readonly Image _imageRight = Properties.Resources.bird_green_right;

        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public PlayerBird(Point position, int speed) : base(position, speed)
        {
            Pb.Image = _imageRight;
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="e"></param>
        public override void Move(KeyEventArgs e)
        {
            Size direction;

            switch (e.KeyCode)
            {
                case Keys.Down:
                    direction = new Size(0, Speed);
                    break;
                case Keys.Up:
                    direction = new Size(0, -Speed);
                    break;
                case Keys.Left:
                    direction = new Size(-Speed, 0);
                    GoingRight = false;
                    break;
                case Keys.Right:
                    direction = new Size(Speed, 0);
                    GoingRight = true;
                    break;
                default:
                    direction = new Size(0, 0);
                    break;

            }
            
            var tempPb = new Rectangle
            {
                Location = Point.Add(Pb.Location, direction),
                Size = Pb.Size
            };

            var canMove = true;

            // Collision checking
            foreach (var block in Game.Blocks.ToList())
            {
                if (!tempPb.IntersectsWith(block.Pb.Bounds)) continue;

                if (block.GetType().Name == "BlockMovable")
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            canMove = block.Touch(Game.Directions.Down);
                            break;
                        case Keys.Up:
                            canMove = block.Touch(Game.Directions.Up);
                            break;
                        case Keys.Right:
                            canMove = block.Touch(Game.Directions.Right);
                            break;
                        case Keys.Left:
                            canMove = block.Touch(Game.Directions.Left);
                            break;
                    }
                }
                else
                {
                    tempPb.Location = Pb.Location;
                }

                if(block.GetType().Name == "BlockEgg")
                {
                    block.Touch(Game.Directions.Up); //maakt niet zoveel uit welke richting je het ei aanraakt want hij gaat toch kapot
                    break;
                }
            }

            if (tempPb.Location.X < 0 || tempPb.Location.X > Game.WindowWidth) return;
            if (tempPb.Location.Y < 0 || tempPb.Location.Y > Game.WindowHeight) return;

            if (canMove)
            {
                Pb.Location = tempPb.Location;
            }

            ChangeDirection(_imageLeft, _imageRight);
        }
    }
}
