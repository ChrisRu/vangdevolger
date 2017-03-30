
namespace VangDeVolger.Elements
{
    using System.Drawing;
    using System.Windows.Forms;

    public abstract class Element
    {
        public Spot Parent { get; set; }

        public PictureBox Pb { get; set; }

        /// <summary>
        /// Initialize new Element Class
        /// </summary>
        protected Element()
        {
            this.Pb = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }

        /// <summary>
        /// Move towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>Element can move</returns>
        public abstract bool CanMove(Direction direction);

        /// <summary>
        /// Execute on KeyDown
        /// </summary>
        /// <param name="e">KeyEvent Arguments</param>
        public virtual void KeyDown(KeyEventArgs e)
        {
        }

        /// <summary>
        /// Move from current spot to next spot
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        public void Move(Direction direction)
        {
            if (Parent.Neighbors[direction].Element != null) return;

            int x = 0;
            int y = 0;
            switch (direction)
            {
                case Direction.Up:
                    y = -this.Parent.Scale;
                    break;
                case Direction.Down:
                    y = this.Parent.Scale;
                    break;
                case Direction.Left:
                    x = -this.Parent.Scale;
                    break;
                case Direction.Right:
                    x = this.Parent.Scale;
                    break;
            }

            // Move Picturebox
            this.Pb.Location = Point.Add(this.Pb.Location, new Size(x, y));

            // Move Element to Neighboor
            this.Parent.Neighbors[direction].Element = this;

            // Set Element of Spot to null
            this.Parent.Element = null;

            // Set Element Parent to Neighboor
            this.Parent = this.Parent.Neighbors[direction];
        }
    }
}
