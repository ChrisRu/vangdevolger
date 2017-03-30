using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements
{
    public abstract class Element
    {
        public Spot Parent { get; set; }
        public PictureBox Pb { get; set; }

        /// <summary>
        /// Initialize Element Class
        /// </summary>
        protected Element()
        {
            Pb = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }

        /// <summary>
        /// Move towards direction
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        /// <returns>bool Can move</returns>
        public abstract bool CanMove(Direction direction);

        /// <summary>
        /// Execute on KeyDown
        /// </summary>
        /// <param name="e"></param>
        public virtual void KeyDown(KeyEventArgs e) { }

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
                    y = -Parent.Scale;
                    break;
                case Direction.Down:
                    y = Parent.Scale;
                    break;
                case Direction.Left:
                    x = -Parent.Scale;
                    break;
                case Direction.Right:
                    x = Parent.Scale;
                    break;
            }

            // Move Picturebox
            Pb.Location = Point.Add(Pb.Location, new Size(x, y));
            // Move Element to Neighboor
            Parent.Neighbors[direction].Element = this;
            // Set Element of Spot to null
            Parent.Element = null;
            // Set Element Parent to Neighboor
            Parent = Parent.Neighbors[direction];
        }
    }
}
