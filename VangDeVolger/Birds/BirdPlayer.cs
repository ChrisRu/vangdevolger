using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
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
        public PlayerBird(Point position) : base(position)
        {
            Speed = 3;
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

                if (block is BlockMovable)
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
                else if (block is BlockEgg)
                {
                    block.Touch(Game.Directions.Up); //maakt niet zoveel uit welke richting je het ei aanraakt want hij gaat toch kapot
                    break;
                }
                else if (block is BlockFood)
                {       
                    block.Touch(Game.Directions.Up); //maakt niet zoveel uit welke richting je het food aanraakt want hij gaat toch kapot
                    break;
                }
                else if (block is BlockStopwatch)
                {
                    block.Touch(Game.Directions.Up); //maakt niet zoveel uit welke richting je de stopwatch aanraakt want hij gaat toch kapot
                    break;
                }
                else
                {
                    tempPb.Location = Pb.Location;
                }
            }

            if (tempPb.Location.X < 0 || tempPb.Location.X > (Game.WindowWidth - (Pb.Width * 1.4))) return;
            if (tempPb.Location.Y < 0 || tempPb.Location.Y > Game.WindowHeight - (Pb.Height * 2.3)) return;

            if (canMove)
            {
                Pb.Location = tempPb.Location;
            }

            ChangeDirection(_imageLeft, _imageRight);
        }
    }
}
