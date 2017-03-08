using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    internal class PlayerBird : Bird
    {
        private readonly Image _image = VangDeVolger.Properties.Resources.vogel_groen;
        private readonly Image _imageActive = VangDeVolger.Properties.Resources.vogel_groen_active;

        private string _facingDirection = "left";
        private bool _goingRight = false;

        private Point _previousPosition;

        /// <summary>
        /// Initialize PlayerBird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public PlayerBird(Point position, int speed) : base(position, speed)
        {
            this.Pb.Image = _image;
        }

        /// <summary>
        /// Move Bird
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="e"></param>
        internal override void Move(ref List<Block> blocks, KeyEventArgs e)
        {
            var direction = new Size(0, 0);

            // Collision checking
            foreach (var block in blocks)
            {
                if (!Pb.Bounds.IntersectsWith(block.Pb.Bounds)) continue;

                if (block.GetType().Name == "BlockMovable")
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        block.Move(ref blocks, new Size(0, Game.BlockSize));
                    }
                    if (e.KeyCode == Keys.Up)
                    {
                        block.Move(ref blocks, new Size(0, -Game.BlockSize));
                    }
                    if (e.KeyCode == Keys.Right)
                    {
                        block.Move(ref blocks, new Size(Game.BlockSize, 0));
                    }
                    if (e.KeyCode == Keys.Left)
                    {
                        block.Move(ref blocks, new Size(-Game.BlockSize, 0));
                    }
                }
                else
                {
                    this.Pb.Location = _previousPosition;
                    return;
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                direction = new Size(0, Speed);
            }
            if (e.KeyCode == Keys.Up)
            {
                direction = new Size(0, -Speed);
            }
            if (e.KeyCode == Keys.Left)
            {
                _goingRight = false;
                direction = new Size(-Speed, 0);
            }
            if (e.KeyCode == Keys.Right)
            {
                _goingRight = true;
                direction = new Size(Speed, 0);
            }

            if(!_goingRight && !_facingDirection.Equals("left"))
            {
                this.Pb.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                _facingDirection = "left";
                this.Pb.Invalidate();
            }
            if (_goingRight && !_facingDirection.Equals("right"))
            {
                this.Pb.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                _facingDirection = "right";
                this.Pb.Invalidate();
            }

            _previousPosition = this.Pb.Location;
            var newLocation = Point.Add(this.Pb.Location, direction);

            if (newLocation.X < 0 || newLocation.X > Game.WindowWidth) return;
            if (newLocation.Y < 0 || newLocation.Y > Game.WindowHeight) return;
            
            this.Pb.Location = newLocation;
        }
    }
}
