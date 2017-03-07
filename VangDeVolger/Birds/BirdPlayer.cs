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
        /// <param name="keys"></param>
        internal override void Move(ref List<Block> blocks, byte[] keys)
        {
            var direction = new Size(0, 0);

            // Collision checking
            if (blocks.Any(block => this.Pb.Bounds.IntersectsWith(block.Pb.Bounds)))
            {
                this.Pb.Location = _previousPosition;
                return;
            }

            if (keys[(int)Keys.Down] == 128)
            {
                direction = new Size(0, PlayerSpeed);
            }
            if (keys[(int)Keys.Up] == 128)
            {
                direction = new Size(0, -PlayerSpeed);
            }
            if (keys[(int)Keys.Left] == 128)
            {
                _goingRight = false;
                direction = new Size(-PlayerSpeed, 0);
            }
            if (keys[(int)Keys.Right] == 128)
            {
                _goingRight = true;
                direction = new Size(PlayerSpeed, 0);
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
            this.Pb.Location = Point.Add(this.Pb.Location, direction);
        }
    }
}
